namespace SaintCustom
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLoadDocument = new System.Windows.Forms.Button();
            this.lblWarehouse = new System.Windows.Forms.Label();
            this.cmbWarehouse = new System.Windows.Forms.ComboBox();
            this.btnNewDocument = new System.Windows.Forms.Button();
            this.lblProvider = new System.Windows.Forms.Label();
            this.txtDocumentNumber = new System.Windows.Forms.TextBox();
            this.lblDocument = new System.Windows.Forms.Label();
            this.rbtnPrintMultiple = new System.Windows.Forms.RadioButton();
            this.rbtnPrintSingle = new System.Windows.Forms.RadioButton();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtDueDay = new System.Windows.Forms.TextBox();
            this.dtpDocumentDate = new System.Windows.Forms.DateTimePicker();
            this.lblDueDay = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.btnLoadOtherDocument = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClassificationCode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colPrintZPL = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colLineNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbProvider
            // 
            this.cmbProvider.DisplayMember = "FullDescription";
            this.cmbProvider.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(65, 3);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(250, 21);
            this.cmbProvider.TabIndex = 0;
            this.cmbProvider.ValueMember = "Code";
            this.cmbProvider.TextUpdate += new System.EventHandler(this.ComboBox_TextUpdate);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnLoadDocument);
            this.panel1.Controls.Add(this.lblWarehouse);
            this.panel1.Controls.Add(this.cmbWarehouse);
            this.panel1.Controls.Add(this.btnNewDocument);
            this.panel1.Controls.Add(this.lblProvider);
            this.panel1.Controls.Add(this.txtDocumentNumber);
            this.panel1.Controls.Add(this.cmbProvider);
            this.panel1.Controls.Add(this.lblDocument);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(323, 115);
            this.panel1.TabIndex = 2;
            // 
            // btnLoadDocument
            // 
            this.btnLoadDocument.Location = new System.Drawing.Point(255, 55);
            this.btnLoadDocument.Name = "btnLoadDocument";
            this.btnLoadDocument.Size = new System.Drawing.Size(60, 23);
            this.btnLoadDocument.TabIndex = 3;
            this.btnLoadDocument.Text = "Cargar";
            this.btnLoadDocument.UseVisualStyleBackColor = true;
            this.btnLoadDocument.Click += new System.EventHandler(this.btnLoadDocument_Click);
            // 
            // lblWarehouse
            // 
            this.lblWarehouse.AutoSize = true;
            this.lblWarehouse.Location = new System.Drawing.Point(3, 33);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new System.Drawing.Size(49, 13);
            this.lblWarehouse.TabIndex = 1;
            this.lblWarehouse.Text = "Deposito";
            // 
            // cmbWarehouse
            // 
            this.cmbWarehouse.DisplayMember = "FullDescription";
            this.cmbWarehouse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbWarehouse.FormattingEnabled = true;
            this.cmbWarehouse.Location = new System.Drawing.Point(65, 30);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new System.Drawing.Size(250, 21);
            this.cmbWarehouse.TabIndex = 0;
            this.cmbWarehouse.ValueMember = "Code";
            this.cmbWarehouse.TextUpdate += new System.EventHandler(this.ComboBox_TextUpdate);
            // 
            // btnNewDocument
            // 
            this.btnNewDocument.Location = new System.Drawing.Point(189, 55);
            this.btnNewDocument.Name = "btnNewDocument";
            this.btnNewDocument.Size = new System.Drawing.Size(60, 23);
            this.btnNewDocument.TabIndex = 3;
            this.btnNewDocument.Text = "Nuevo";
            this.btnNewDocument.UseVisualStyleBackColor = true;
            this.btnNewDocument.Click += new System.EventHandler(this.btnNewDocument_Click);
            // 
            // lblProvider
            // 
            this.lblProvider.AutoSize = true;
            this.lblProvider.Location = new System.Drawing.Point(3, 6);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(56, 13);
            this.lblProvider.TabIndex = 1;
            this.lblProvider.Text = "Proveedor";
            // 
            // txtDocumentNumber
            // 
            this.txtDocumentNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocumentNumber.Location = new System.Drawing.Point(65, 57);
            this.txtDocumentNumber.Name = "txtDocumentNumber";
            this.txtDocumentNumber.Size = new System.Drawing.Size(120, 20);
            this.txtDocumentNumber.TabIndex = 2;
            // 
            // lblDocument
            // 
            this.lblDocument.AutoSize = true;
            this.lblDocument.Location = new System.Drawing.Point(3, 60);
            this.lblDocument.Name = "lblDocument";
            this.lblDocument.Size = new System.Drawing.Size(62, 13);
            this.lblDocument.TabIndex = 1;
            this.lblDocument.Text = "Documento";
            // 
            // rbtnPrintMultiple
            // 
            this.rbtnPrintMultiple.AutoSize = true;
            this.rbtnPrintMultiple.Checked = true;
            this.rbtnPrintMultiple.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbtnPrintMultiple.Location = new System.Drawing.Point(102, 94);
            this.rbtnPrintMultiple.Name = "rbtnPrintMultiple";
            this.rbtnPrintMultiple.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbtnPrintMultiple.Size = new System.Drawing.Size(137, 18);
            this.rbtnPrintMultiple.TabIndex = 3;
            this.rbtnPrintMultiple.TabStop = true;
            this.rbtnPrintMultiple.Text = "Imprimir seleccionados";
            this.rbtnPrintMultiple.UseVisualStyleBackColor = true;
            this.rbtnPrintMultiple.CheckedChanged += new System.EventHandler(this.rbtnPrintMultiple_CheckedChanged);
            // 
            // rbtnPrintSingle
            // 
            this.rbtnPrintSingle.AutoSize = true;
            this.rbtnPrintSingle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbtnPrintSingle.Location = new System.Drawing.Point(86, 70);
            this.rbtnPrintSingle.Name = "rbtnPrintSingle";
            this.rbtnPrintSingle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rbtnPrintSingle.Size = new System.Drawing.Size(153, 18);
            this.rbtnPrintSingle.TabIndex = 2;
            this.rbtnPrintSingle.Text = "Imprimir en cada insercion";
            this.rbtnPrintSingle.UseVisualStyleBackColor = true;
            // 
            // dgvItems
            // 
            this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCode,
            this.colDescription,
            this.colStock,
            this.colQuantity,
            this.colCost,
            this.colPrice,
            this.colClassificationCode,
            this.colPrintZPL,
            this.colLineNumber});
            this.dgvItems.Location = new System.Drawing.Point(12, 133);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.Size = new System.Drawing.Size(916, 414);
            this.dgvItems.TabIndex = 3;
            this.dgvItems.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvItems_CellBeginEdit);
            this.dgvItems.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvItems_CellFormatting);
            this.dgvItems.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvItems_DefaultValuesNeeded);
            this.dgvItems.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_RowValidated);
            this.dgvItems.EnabledChanged += new System.EventHandler(this.dgvItems_EnabledChanged);
            this.dgvItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItems_KeyDown);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.btnLoadOtherDocument);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnFinish);
            this.panel2.Location = new System.Drawing.Point(12, 553);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(916, 31);
            this.panel2.TabIndex = 4;
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(3, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "[F1] Buscar";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(836, 3);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(75, 23);
            this.btnFinish.TabIndex = 0;
            this.btnFinish.Text = "Finalizar";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(853, 103);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "Imprimir";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtDueDay);
            this.panel3.Controls.Add(this.rbtnPrintMultiple);
            this.panel3.Controls.Add(this.dtpDocumentDate);
            this.panel3.Controls.Add(this.rbtnPrintSingle);
            this.panel3.Controls.Add(this.lblDueDay);
            this.panel3.Controls.Add(this.lblDate);
            this.panel3.Location = new System.Drawing.Point(341, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(245, 115);
            this.panel3.TabIndex = 5;
            // 
            // txtDueDay
            // 
            this.txtDueDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDueDay.Location = new System.Drawing.Point(119, 30);
            this.txtDueDay.Name = "txtDueDay";
            this.txtDueDay.Size = new System.Drawing.Size(120, 20);
            this.txtDueDay.TabIndex = 4;
            // 
            // dtpDocumentDate
            // 
            this.dtpDocumentDate.CustomFormat = "dd-MM-yyy";
            this.dtpDocumentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDocumentDate.Location = new System.Drawing.Point(119, 4);
            this.dtpDocumentDate.Name = "dtpDocumentDate";
            this.dtpDocumentDate.Size = new System.Drawing.Size(120, 20);
            this.dtpDocumentDate.TabIndex = 2;
            // 
            // lblDueDay
            // 
            this.lblDueDay.AutoSize = true;
            this.lblDueDay.Location = new System.Drawing.Point(3, 33);
            this.lblDueDay.Name = "lblDueDay";
            this.lblDueDay.Size = new System.Drawing.Size(89, 13);
            this.lblDueDay.TabIndex = 1;
            this.lblDueDay.Text = "Dias Vencimiento";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(3, 6);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(110, 13);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "Fecha de Documento";
            // 
            // btnLoadOtherDocument
            // 
            this.btnLoadOtherDocument.Location = new System.Drawing.Point(609, 3);
            this.btnLoadOtherDocument.Name = "btnLoadOtherDocument";
            this.btnLoadOtherDocument.Size = new System.Drawing.Size(140, 23);
            this.btnLoadOtherDocument.TabIndex = 0;
            this.btnLoadOtherDocument.Text = "Cargar Otro Documento";
            this.btnLoadOtherDocument.UseVisualStyleBackColor = true;
            this.btnLoadOtherDocument.Click += new System.EventHandler(this.btnLoadOtherDocument_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(755, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Guardar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // colCode
            // 
            this.colCode.DataPropertyName = "Code";
            this.colCode.HeaderText = "Codigo";
            this.colCode.Name = "colCode";
            this.colCode.ReadOnly = true;
            // 
            // colDescription
            // 
            this.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDescription.DataPropertyName = "Description";
            this.colDescription.HeaderText = "Descripcion";
            this.colDescription.Name = "colDescription";
            // 
            // colStock
            // 
            this.colStock.DataPropertyName = "Stock";
            this.colStock.HeaderText = "Existen";
            this.colStock.Name = "colStock";
            this.colStock.ReadOnly = true;
            // 
            // colQuantity
            // 
            this.colQuantity.DataPropertyName = "Quantity";
            this.colQuantity.HeaderText = "Cantidad";
            this.colQuantity.Name = "colQuantity";
            // 
            // colCost
            // 
            this.colCost.DataPropertyName = "Cost";
            this.colCost.HeaderText = "Costo";
            this.colCost.Name = "colCost";
            // 
            // colPrice
            // 
            this.colPrice.DataPropertyName = "Price";
            this.colPrice.HeaderText = "Precio";
            this.colPrice.Name = "colPrice";
            // 
            // colClassificationCode
            // 
            this.colClassificationCode.DataPropertyName = "ClassificationCode";
            this.colClassificationCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colClassificationCode.HeaderText = "Clasificacion";
            this.colClassificationCode.Name = "colClassificationCode";
            // 
            // colPrintZPL
            // 
            this.colPrintZPL.DataPropertyName = "PrintZPL";
            this.colPrintZPL.HeaderText = "Imprimir?";
            this.colPrintZPL.Name = "colPrintZPL";
            // 
            // colLineNumber
            // 
            this.colLineNumber.DataPropertyName = "LineNumber";
            this.colLineNumber.HeaderText = "LineNumber";
            this.colLineNumber.Name = "colLineNumber";
            this.colLineNumber.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 594);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.panel1);
            this.Name = "Main";
            this.Text = "Saint Custom";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblWarehouse;
        private System.Windows.Forms.ComboBox cmbWarehouse;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.RadioButton rbtnPrintMultiple;
        private System.Windows.Forms.RadioButton rbtnPrintSingle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DateTimePicker dtpDocumentDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Button btnNewDocument;
        private System.Windows.Forms.TextBox txtDocumentNumber;
        private System.Windows.Forms.Label lblDocument;
        private System.Windows.Forms.TextBox txtDueDay;
        private System.Windows.Forms.Label lblDueDay;
        private System.Windows.Forms.Button btnLoadDocument;
        private System.Windows.Forms.Button btnLoadOtherDocument;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewComboBoxColumn colClassificationCode;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colPrintZPL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLineNumber;
    }
}

