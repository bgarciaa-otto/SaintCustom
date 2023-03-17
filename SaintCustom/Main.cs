using SaintCustom.Models;
using SaintCustom.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SaintCustom
{
    public partial class Main : Form
    {
        private List<DropDown> providers;
        private List<DropDown> warehouses;
        private List<DropDown> classifications;
        private BindingList<Product> products;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            var methods = new Methods();
            providers = methods.GetProviders();
            warehouses = methods.GetWarehouses();
            classifications = methods.GetClassifications();

            cmbProvider.DataSource = providers;
            cmbWarehouse.DataSource = warehouses;
            colClassificationCode.DataSource = classifications;
            colClassificationCode.DisplayMember = "Description";
            colClassificationCode.ValueMember = "Code";


            cmbProvider.SelectedIndex = -1;
            cmbWarehouse.SelectedIndex = -1;

            products = new BindingList<Product>();
            dgvItems.DataSource = products;

            EnabledControls(false);
        }


        private void ComboBox_TextUpdate(object sender, EventArgs e)
        {
            var control = (ComboBox)sender;

            string filter_param = control.Text;

            List<DropDown> filteredItems = new List<DropDown>();

            switch (control.Name)
            {
                case "cmbProvider":
                    filteredItems = providers.Where(x => x.FullDescription.ToUpper().Contains(filter_param.ToUpper())).ToList();
                    break;
                case "cmbWarehouse":
                    filteredItems = warehouses.Where(x => x.FullDescription.ToUpper().Contains(filter_param.ToUpper())).ToList();
                    break;
            }

            control.DataSource = filteredItems;

            if (String.IsNullOrWhiteSpace(filter_param))
                switch (control.Name)
                {
                    case "cmbProvider":
                        filteredItems = providers;
                        break;
                    case "cmbWarehouse":
                        filteredItems = warehouses;
                        break;
                }

            control.DroppedDown = true;

            // this will ensure that the drop down is as long as the list
            control.IntegralHeight = true;

            // remove automatically selected first item
            control.SelectedIndex = -1;

            control.Text = filter_param;

            // set the position of the cursor
            control.SelectionStart = filter_param.Length;
            control.SelectionLength = 0;
        }

        private void PrintZPL(List<Product> list)
        {
            string lines = string.Empty;

            var column = 1;

            foreach (var product in list)
            {
                for (int i = 0; i < product.Quantity; i++)
                {
                    if (column > 3)
                        column = 1;
                    lines += GetZPL(product, column);
                    column++;
                }
            }
            if (column <= 3)
                lines += GetZPL(new Product(), -1);

            Print.SendStringToPrinter(ConfigurationSettings.AppSettings["PrinterName"], lines);
        }

        private string GetZPL(Product product, int column)
        {
            switch (column)
            {
                case 1:
                    return $@"
^XA

^CF0,12
^FO0,0^FD{product.Description}^FS
^CF0,40
^FO60,14^FD{product.Price:C0}^FS
^FO28,52^BY2^BC,40^FD{product.Code}^FS

";
                case 2:
                    return $@"
^CF0,12
^FO271,0^FD{product.Description}^FS
^CF0,40
^FO331,14^FD{product.Price:C0}^FS
^FO309,52^BY2^BC,40^FD{product.Code}^FS

";
                case 3:
                    return $@"
^CF0,12
^FO542,0^FD{product.Description}^FS
^CF0,40
^FO602,14^FD{product.Price:C0}^FS
^FO580,52^BY2^BC,40^FD{product.Code}^FS

^XZ";
                default:
                    return $@"^XZ";
            }
        }

        private void dgvItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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

        private void dgvItems_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            var datasource = ((DataGridView)sender).DataSource as BindingList<Product>;

            if (datasource.Count <= dgvItems.CurrentRow.Index) return;
            var product = datasource[e.RowIndex];

            if (String.IsNullOrWhiteSpace(product.Code))
            {
                if (String.IsNullOrWhiteSpace(product.Description))
                {
                    dgvItems.CurrentCell = dgvItems.Rows[e.RowIndex].Cells[1];
                    dgvItems.EndEdit();
                    dgvItems.Refresh();
                    return;
                }

                if (String.IsNullOrWhiteSpace(product.ClassificationCode))
                {
                    dgvItems.CurrentCell = dgvItems.Rows[e.RowIndex].Cells[6];
                    dgvItems.EndEdit();
                    dgvItems.Refresh();
                    return;
                }

                SaveProduct(product);
            }
            else
            {
                new Methods().UpdateProduct(product);
            }

            if (rbtnPrintSingle.Checked)
            {
                List<Product> list = new List<Product>();
                list.Add(product);
                PrintZPL(list);
            }
        }

        private void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        private void SaveProduct(Product product)
        {
            var code = new Methods().SaveProduct(product);

            products[dgvItems.CurrentRow.Index].Code = code;

            dgvItems.EndEdit();
            dgvItems.Refresh();
        }

        private void dgvItems_KeyDown(object sender, KeyEventArgs e)
        {
            var currentCell = ((DataGridView)sender).CurrentCell;
            var columnIndex = currentCell.ColumnIndex;
            var value = $"{currentCell.Value}";

            SearchProduct modal;
            Product selectedProduct = null;

            if (e.KeyCode == Keys.F1)
            {
                switch (columnIndex)
                {
                    case 0:
                        modal = new SearchProduct(code: value);
                        modal.ShowDialog();
                        selectedProduct = modal.SelectedProduct;
                        break;
                    case 1:
                        modal = new SearchProduct(description: value);
                        modal.ShowDialog();
                        selectedProduct = modal.SelectedProduct;
                        break;
                }

                if (selectedProduct != null)
                {
                    products[dgvItems.CurrentRow.Index].Code = selectedProduct.Code;
                    products[dgvItems.CurrentRow.Index].Description = selectedProduct.Description;
                    products[dgvItems.CurrentRow.Index].Stock = selectedProduct.Stock;
                    products[dgvItems.CurrentRow.Index].Cost = selectedProduct.Cost;
                    products[dgvItems.CurrentRow.Index].Price = selectedProduct.Price;
                    products[dgvItems.CurrentRow.Index].ClassificationCode = selectedProduct.ClassificationCode;
                    products[dgvItems.CurrentRow.Index].PrintZPL = true;

                    dgvItems.CurrentCell = dgvItems.Rows[dgvItems.CurrentRow.Index].Cells[3];
                    dgvItems.EndEdit();
                    dgvItems.Refresh();
                }
            }
        }

        private void rbtnPrintMultiple_CheckedChanged(object sender, EventArgs e)
        {
            dgvItems.Columns["colPrintZPL"].Visible = rbtnPrintMultiple.Checked;
            btnPrint.Visible = rbtnPrintMultiple.Checked;
        }

        private void dgvItems_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["colPrintZPL"].Value = true;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var list = products.Where(x => x.PrintZPL).ToList();
            PrintZPL(list);
            products.Select(x => { x.PrintZPL = false; return x; }).ToList();

            dgvItems.EndEdit();
            dgvItems.Refresh();
        }

        private void dgvItems_EnabledChanged(object sender, EventArgs e)
        {
            DataGridView control = sender as DataGridView;
            if (control.Enabled)
            {
                control.DefaultCellStyle.BackColor = SystemColors.Window;
                control.DefaultCellStyle.ForeColor = SystemColors.ControlText;
                control.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Window;
                control.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                control.ReadOnly = false;
                control.EnableHeadersVisualStyles = true;
            }
            else
            {
                control.DefaultCellStyle.BackColor = SystemColors.Control;
                control.DefaultCellStyle.ForeColor = SystemColors.GrayText;
                control.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
                control.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.GrayText;
                control.CurrentCell = null;
                control.ReadOnly = true;
                control.EnableHeadersVisualStyles = false;
            }
        }

        private void btnNewDocument_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(cmbProvider.Text) || String.IsNullOrWhiteSpace(cmbWarehouse.Text))
            {
                MessageBox.Show("Debe seleccionar el Proveedor y el Deposito", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EnabledControls();
            cmbProvider.Enabled = false;
            cmbWarehouse.Enabled = false;
            txtDocumentNumber.ReadOnly = true;
            btnNewDocument.Enabled = false;
            btnLoadDocument.Enabled = false;

            txtDocumentNumber.Text = new Methods().SavePurchaseOrderHeader($"{cmbProvider.SelectedValue}", $"{cmbWarehouse.SelectedValue}");

        }

        private void EnabledControls(bool enabled = true)
        {
            dgvItems.Enabled = enabled;
            dtpDocumentDate.Enabled = enabled;
            txtDueDay.Enabled = enabled;
            rbtnPrintSingle.Enabled = enabled;
            rbtnPrintMultiple.Enabled = enabled;
            btnPrint.Enabled = enabled;

            //btnSearch.Enabled = enabled;
            btnSave.Enabled = enabled;
            btnFinish.Enabled = enabled;
        }

        private void btnLoadDocument_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txtDocumentNumber.Text) || String.IsNullOrWhiteSpace(cmbProvider.Text))
                {
                    MessageBox.Show("Debe ingresar el numero del documento y seleccionar el Proveedor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var document = new Methods().GetPurchaseOrder(txtDocumentNumber.Text, $"{cmbProvider.SelectedValue}");

                if (document == null)
                {
                    MessageBox.Show("El documento ingresado no existe.", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                EnabledControls();
                cmbProvider.Enabled = false;
                cmbWarehouse.Enabled = false;
                txtDocumentNumber.ReadOnly = true;
                btnNewDocument.Enabled = false;
                btnLoadDocument.Enabled = false;

                cmbWarehouse.SelectedValue = document.WarehouseCode;
                dtpDocumentDate.Value = document.DocumentDate ?? DateTime.Now;
                txtDueDay.Text = $"{document.DueDay}";

                products = new BindingList<Product>(document.Products);
                dgvItems.DataSource = products;

                dgvItems.EndEdit();
                dgvItems.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(dtpDocumentDate.Text) || String.IsNullOrWhiteSpace(txtDueDay.Text))
            {
                MessageBox.Show("Debe ingresar la fecha del documento y el numero de dias de vencimiento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnSave_Click(null, null);

            FinishOrder();

            InitializeControls();
        }

        private void InitializeControls()
        {
            EnabledControls(false);

            cmbProvider.Enabled = true;
            cmbWarehouse.Enabled = true;
            txtDocumentNumber.ReadOnly = false;
            btnNewDocument.Enabled = true;
            btnLoadDocument.Enabled = true;

            cmbProvider.SelectedIndex = -1;
            cmbWarehouse.SelectedIndex = -1;
            txtDocumentNumber.Text = "";
            dtpDocumentDate.Value = DateTime.Now;
            txtDueDay.Text = "";
            rbtnPrintMultiple.Checked = true;

            products = new BindingList<Product>();
            dgvItems.DataSource = products;

            dgvItems.EndEdit();
            dgvItems.Refresh();

            cmbProvider.Focus();
        }

        private void FinishOrder()
        {
            new Methods().FinishOrder(txtDocumentNumber.Text, $"{cmbProvider.SelectedValue}", dtpDocumentDate.Value, txtDueDay.Text);
        }

        private void btnLoadOtherDocument_Click(object sender, EventArgs e)
        {
            InitializeControls();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var pendingProducts = products.Where(x => x.LineNumber == 0).ToList();

            if(pendingProducts.Count > 0)
            {
                new Methods().SaveOrderDetail(txtDocumentNumber.Text, $"{cmbProvider.SelectedValue}", $"{cmbWarehouse.SelectedValue}", pendingProducts);

                btnLoadDocument_Click(null, null);
            }
        }

        private void dgvItems_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
                e.Cancel = true;
        }
    }
}
