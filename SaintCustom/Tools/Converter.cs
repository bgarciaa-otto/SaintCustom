using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace SaintCustom.Tools
{
    internal static class Converter
    {
        public static List<T> ConvertToList<T>(this DataTable dataTable) where T : class
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                string json = JsonConvert.SerializeObject(dataTable);
                List<T> result = JsonConvert.DeserializeObject<List<T>>(json);
                return result;
            }

            return new List<T>();
        }
    }
}
