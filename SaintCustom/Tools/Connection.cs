using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SaintCustom.Tools
{
    internal class Connection
    {
        private string ConnectionString { get; set; }
        public int TimeOut { get; set; } = 300;

        private SqlConnection ConnectionSQL;
        private SqlCommand Command;
        private SqlTransaction Transaction;
        private List<Parameter> Parameters;
        private string CurrentTransactionName;

        public Connection() => ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"];

        private void Connect()
        {
            if (ConnectionSQL == null)
                ConnectionSQL = new SqlConnection(ConnectionString);
            else
            {
                Disconnect();
                ConnectionSQL = new SqlConnection(ConnectionString);
            }

            if (ConnectionSQL.State.Equals(ConnectionState.Closed))
                ConnectionSQL.Open();
        }

        private void Disconnect()
        {
            if (ConnectionSQL != null && ConnectionSQL.State.Equals(ConnectionState.Open))
                ConnectionSQL.Close();
            Transaction = null;
            CurrentTransactionName = null;
        }


        private void CreateCommand(string queryString)
        {
            Command = new SqlCommand(queryString, ConnectionSQL)
            {
                CommandType = CommandType.Text,
                CommandTimeout = TimeOut
            };
            if (Transaction != null)
                Command.Transaction = Transaction;
        }

        private void CreateCommandSP(string spName)
        {
            Command = new SqlCommand(spName, ConnectionSQL)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = TimeOut
            };
            if (Transaction != null)
                Command.Transaction = Transaction;
            UpdateParameters();
        }

        private void UpdateParameters()
        {
            if (Parameters?.Count > 0)
                foreach (Parameter item in Parameters)
                    Command.Parameters.Add(item.GetParameter());
        }

        private DataSet ExecuteQuery()
        {
            using (var sda = new SqlDataAdapter(Command))
            {
                var ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
        }

        public object ExecuteScalarSQL(string queryString)
        {
            try
            {
                Connect();
                CreateCommand(queryString);
                return Command.ExecuteScalar();
            }
            finally
            {
                Disconnect();
            }
        }

        public DataSet ExecuteSQL(string queryString)
        {
            try
            {
                Connect();
                CreateCommand(queryString);
                return ExecuteQuery();
            }
            finally
            {
                Disconnect();
            }
        }

        public object ExecuteScalarSP(string spName, List<Parameter> lstParameters = null)
        {
            try
            {
                Parameters = lstParameters;
                Connect();
                CreateCommandSP(spName);
                var result = Command.ExecuteScalar();
                ValidateParameters(ref lstParameters);
                return result;
            }
            finally
            {
                Disconnect();
            }
        }

        private void ValidateParameters(ref List<Parameter> lstParameters)
        {
            foreach (var param in lstParameters)
            {
                var direction = param.Direction;
                if (direction == System.Data.ParameterDirection.Output || direction == System.Data.ParameterDirection.InputOutput || direction == System.Data.ParameterDirection.ReturnValue)
                    param.Value = Command.Parameters[lstParameters.IndexOf(param)].Value;
            }
        }

        public DataSet ExecuteSP(string spName, List<Parameter> lstParameters = null)
        {
            try
            {
                Parameters = lstParameters;
                Connect();
                CreateCommandSP(spName);
                return ExecuteQuery();
            }
            finally
            {
                Disconnect();
            }
        }


        public void BeginTransaction(string transactionName = null)
        {
            if (!string.IsNullOrWhiteSpace(transactionName))
                CurrentTransactionName = transactionName;
            Connect();
            Transaction = string.IsNullOrWhiteSpace(transactionName) ? ConnectionSQL.BeginTransaction() : ConnectionSQL.BeginTransaction(transactionName);
        }

        public void Commit()
        {
            if (Transaction != null)
                Transaction.Commit();
            Disconnect();
        }

        public void Rollback()
        {
            if (Transaction != null)
                if (string.IsNullOrWhiteSpace(CurrentTransactionName))
                    Transaction.Rollback();
                else
                    Transaction.Rollback(CurrentTransactionName);

            Disconnect();
        }

        public void Save(string savePointName) => Transaction.Save(savePointName);

        public object ExecuteScalarSPWithTransaction(string spName, List<Parameter> lstParameters = null)
        {
            try
            {
                Parameters = lstParameters;
                CreateCommandSP(spName);
                return Command.ExecuteScalar();
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public DataSet ExecuteSPWithTransaction(string spName, List<Parameter> lstParameters = null)
        {
            try
            {
                Parameters = lstParameters;
                CreateCommandSP(spName);
                return ExecuteQuery();
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public object ExecuteScalarSQLWithTransaction(string queryString)
        {
            try
            {
                CreateCommand(queryString);
                return Command.ExecuteScalar();
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public DataSet ExecuteSQLWithTransaction(string queryString)
        {
            try
            {
                CreateCommand(queryString);
                return ExecuteQuery();
            }
            catch
            {
                Rollback();
                throw;
            }
        }


        public void ExecuteNonQuery(string queryString)
        {
            try
            {
                Connect();
                CreateCommand(queryString);
                Command.ExecuteNonQuery();
            }
            finally
            {
                Disconnect();
            }
        }

        public void ExecuteNonQuerySP(string spName)
        {
            try
            {
                Connect();
                CreateCommandSP(spName);
                Command.ExecuteNonQuery();
            }
            finally
            {
                Disconnect();
            }
        }

        public void ExecuteNonQuerySP(string spName, List<Parameter> lstParameters)
        {
            try
            {
                Parameters = lstParameters;
                Connect();
                CreateCommandSP(spName);
                Command.ExecuteNonQuery();
            }
            finally
            {
                Disconnect();
            }
        }
    }


	internal class Parameter
	{
		public string ParameterName;
		public object Value;
		public SqlDbType SqlDbType;
		public ParameterDirection Direction;
		public object Size;

		public Parameter(string Name, SqlDbType Type, object Value)
		{
			ParameterName = Name;
			SqlDbType = Type;
			this.Value = Value;
		}

		public Parameter(string Name, SqlDbType Type, object Value, ParameterDirection Direction)
		{
			ParameterName = Name;
			SqlDbType = Type;
			this.Value = Value;
			this.Direction = Direction;
		}

		public Parameter(string Name, SqlDbType Type, object Value, ParameterDirection Direction, object Size)
		{
			ParameterName = Name;
			SqlDbType = Type;
			this.Value = Value;
			this.Direction = Direction;
			this.Size = Size;
		}

		public SqlParameter GetParameter() => new SqlParameter
		{
			ParameterName = ParameterName,
			SqlDbType = SqlDbType,
			Value = Value,
			Direction = Direction,
			Size = (Size == null) ? 0 : int.Parse($"{Size}")
		};
	}
}
