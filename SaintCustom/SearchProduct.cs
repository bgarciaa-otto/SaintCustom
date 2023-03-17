using SaintCustom.Models;
using SaintCustom.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace SaintCustom
{
    public partial class SearchProduct : Form
    {
        private string _code { get; set; }
        private string _description { get; set; }

        private BindingList<Product> products;

        private List<DropDown> classifications;

        internal Product SelectedProduct { get; set; }


        public SearchProduct(string code = null, string description = null)
        {
            InitializeComponent();
            _code = code;
            _description = description;
        }

        private void SearchProduct_Load(object sender, System.EventArgs e)
        {
            txtCode.Text = _code;
            txtDescription.Text = _description;

            classifications = new Methods().GetClassifications();
            colClassificationCode.DataSource = classifications;
            colClassificationCode.DisplayMember = "Description";
            colClassificationCode.ValueMember = "Code";

            RunSearch();
        }

        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            _code = txtCode.Text;
            _description = txtDescription.Text;
            RunSearch();
        }

        private void RunSearch()
        {
            var result = new Methods().GetProducts(_code, _description);
            products = new BindingList<Product>(result);
            dgvProducts.DataSource = products;
        }

        private void dgvProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (String.IsNullOrWhiteSpace($"{e.Value}")) return;

            switch (e.ColumnIndex)
            {
                case 2:
                case 3:
                    e.Value = $"{e.Value:N0}";
                    break;
                case 4:
                case 5:
                    e.Value = $"{e.Value:C0}";
                    break;
            }
        }

        private void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            SelectedProduct = (dgvProducts.DataSource as BindingList<Product>)[e.RowIndex];
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                this.Close();
                return;
            }
            SelectedProduct = (dgvProducts.DataSource as BindingList<Product>)[dgvProducts.SelectedRows[0].Index];
            this.Close();
        }
    }
}
