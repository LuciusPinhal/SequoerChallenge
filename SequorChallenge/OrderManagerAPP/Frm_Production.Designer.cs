﻿namespace OrderManagerAPP
{
    partial class Frm_Production
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Production));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TlPnlTop = new System.Windows.Forms.TableLayoutPanel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.PnlSearch = new System.Windows.Forms.Panel();
            this.LineSearch = new System.Windows.Forms.Label();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.picSearch = new System.Windows.Forms.PictureBox();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.ContainerPainel = new System.Windows.Forms.Panel();
            this.TextInfo = new System.Windows.Forms.RichTextBox();
            this.BtnConfirmar = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.BtnExit = new System.Windows.Forms.Button();
            this.TxtPainel = new System.Windows.Forms.Label();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.Hour = new System.Windows.Forms.DateTimePicker();
            this.ListMaterial = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtQuant = new System.Windows.Forms.TextBox();
            this.Date = new System.Windows.Forms.DateTimePicker();
            this.Quantidade = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textEmail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtOrder = new System.Windows.Forms.Label();
            this.TxtOrdem = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.PnlGrid = new System.Windows.Forms.Panel();
            this.pnlDel = new System.Windows.Forms.Panel();
            this.textDelInfo = new System.Windows.Forms.TextBox();
            this.bntConfirmDel = new System.Windows.Forms.Button();
            this.bntCancelDel = new System.Windows.Forms.Button();
            this.pictureDel = new System.Windows.Forms.PictureBox();
            this.textDel = new System.Windows.Forms.TextBox();
            this.Messagem = new System.Windows.Forms.Panel();
            this.TxtMensagem = new System.Windows.Forms.TextBox();
            this.Grid_Users = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TableHour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaterialCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CycleTimeProduction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TlPnlTop.SuspendLayout();
            this.PnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearch)).BeginInit();
            this.ContainerPainel.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.PnlGrid.SuspendLayout();
            this.pnlDel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureDel)).BeginInit();
            this.Messagem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Users)).BeginInit();
            this.SuspendLayout();
            // 
            // TlPnlTop
            // 
            this.TlPnlTop.ColumnCount = 10;
            this.TlPnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.TlPnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.TlPnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.TlPnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.TlPnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.TlPnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.TlPnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.TlPnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.TlPnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TlPnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TlPnlTop.Controls.Add(this.btnEdit, 3, 1);
            this.TlPnlTop.Controls.Add(this.PnlSearch, 9, 1);
            this.TlPnlTop.Controls.Add(this.BtnAdd, 1, 1);
            this.TlPnlTop.Controls.Add(this.btnDelete, 5, 1);
            this.TlPnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.TlPnlTop.Location = new System.Drawing.Point(2, 62);
            this.TlPnlTop.Margin = new System.Windows.Forms.Padding(0);
            this.TlPnlTop.Name = "TlPnlTop";
            this.TlPnlTop.RowCount = 2;
            this.TlPnlTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TlPnlTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.TlPnlTop.Size = new System.Drawing.Size(1714, 60);
            this.TlPnlTop.TabIndex = 8;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.btnEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.BackgroundImage")));
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.No;
            this.btnEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Location = new System.Drawing.Point(220, 20);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(110, 40);
            this.btnEdit.TabIndex = 13;
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // PnlSearch
            // 
            this.PnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(126)))), ((int)(((byte)(235)))));
            this.PnlSearch.Controls.Add(this.LineSearch);
            this.PnlSearch.Controls.Add(this.TxtSearch);
            this.PnlSearch.Controls.Add(this.picSearch);
            this.PnlSearch.Location = new System.Drawing.Point(1192, 20);
            this.PnlSearch.Margin = new System.Windows.Forms.Padding(0);
            this.PnlSearch.Name = "PnlSearch";
            this.PnlSearch.Size = new System.Drawing.Size(476, 40);
            this.PnlSearch.TabIndex = 10;
            // 
            // LineSearch
            // 
            this.LineSearch.BackColor = System.Drawing.Color.White;
            this.LineSearch.Location = new System.Drawing.Point(6, 33);
            this.LineSearch.Name = "LineSearch";
            this.LineSearch.Size = new System.Drawing.Size(395, 2);
            this.LineSearch.TabIndex = 10;
            this.LineSearch.Visible = false;
            // 
            // TxtSearch
            // 
            this.TxtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(126)))), ((int)(((byte)(235)))));
            this.TxtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearch.ForeColor = System.Drawing.Color.White;
            this.TxtSearch.Location = new System.Drawing.Point(10, 7);
            this.TxtSearch.Margin = new System.Windows.Forms.Padding(10);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(395, 24);
            this.TxtSearch.TabIndex = 2;
            this.TxtSearch.Text = "Pesquisar";
            this.TxtSearch.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TxtSearch_MouseClick);
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            this.TxtSearch.Leave += new System.EventHandler(this.TxtSearch_Leave);
            // 
            // picSearch
            // 
            this.picSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(126)))), ((int)(((byte)(235)))));
            this.picSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picSearch.BackgroundImage")));
            this.picSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.picSearch.Location = new System.Drawing.Point(423, 0);
            this.picSearch.Name = "picSearch";
            this.picSearch.Size = new System.Drawing.Size(53, 40);
            this.picSearch.TabIndex = 0;
            this.picSearch.TabStop = false;
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(126)))), ((int)(((byte)(235)))));
            this.BtnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnAdd.BackgroundImage")));
            this.BtnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnAdd.FlatAppearance.BorderSize = 0;
            this.BtnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.BtnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAdd.Location = new System.Drawing.Point(40, 20);
            this.BtnAdd.Margin = new System.Windows.Forms.Padding(0);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(140, 40);
            this.BtnAdd.TabIndex = 12;
            this.BtnAdd.UseVisualStyleBackColor = false;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.No;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(370, 20);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 40);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // ContainerPainel
            // 
            this.ContainerPainel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(126)))), ((int)(((byte)(235)))));
            this.ContainerPainel.Controls.Add(this.TextInfo);
            this.ContainerPainel.Controls.Add(this.BtnConfirmar);
            this.ContainerPainel.Controls.Add(this.panel4);
            this.ContainerPainel.Controls.Add(this.BtnCancelar);
            this.ContainerPainel.Controls.Add(this.tableLayoutPanel1);
            this.ContainerPainel.Controls.Add(this.panel3);
            this.ContainerPainel.Controls.Add(this.panel2);
            this.ContainerPainel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ContainerPainel.Location = new System.Drawing.Point(1366, 122);
            this.ContainerPainel.Margin = new System.Windows.Forms.Padding(40);
            this.ContainerPainel.Name = "ContainerPainel";
            this.ContainerPainel.Size = new System.Drawing.Size(350, 831);
            this.ContainerPainel.TabIndex = 9;
            // 
            // TextInfo
            // 
            this.TextInfo.BackColor = System.Drawing.Color.Red;
            this.TextInfo.ForeColor = System.Drawing.Color.Black;
            this.TextInfo.Location = new System.Drawing.Point(26, 614);
            this.TextInfo.Margin = new System.Windows.Forms.Padding(0, 3, 10, 3);
            this.TextInfo.Name = "TextInfo";
            this.TextInfo.ReadOnly = true;
            this.TextInfo.Size = new System.Drawing.Size(298, 135);
            this.TextInfo.TabIndex = 12;
            this.TextInfo.Text = "";
            this.TextInfo.Visible = false;
            // 
            // BtnConfirmar
            // 
            this.BtnConfirmar.BackColor = System.Drawing.Color.LimeGreen;
            this.BtnConfirmar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnConfirmar.BackgroundImage")));
            this.BtnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnConfirmar.FlatAppearance.BorderSize = 0;
            this.BtnConfirmar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.BtnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnConfirmar.Location = new System.Drawing.Point(174, 760);
            this.BtnConfirmar.Margin = new System.Windows.Forms.Padding(0);
            this.BtnConfirmar.Name = "BtnConfirmar";
            this.BtnConfirmar.Size = new System.Drawing.Size(140, 40);
            this.BtnConfirmar.TabIndex = 14;
            this.BtnConfirmar.UseVisualStyleBackColor = false;
            this.BtnConfirmar.Click += new System.EventHandler(this.BtnConfirmar_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(126)))), ((int)(((byte)(235)))));
            this.panel4.Controls.Add(this.BtnExit);
            this.panel4.Controls.Add(this.TxtPainel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 59);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(350, 77);
            this.panel4.TabIndex = 19;
            // 
            // BtnExit
            // 
            this.BtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnExit.BackgroundImage")));
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.FlatAppearance.BorderSize = 0;
            this.BtnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExit.Location = new System.Drawing.Point(276, -51);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(0);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(48, 128);
            this.BtnExit.TabIndex = 20;
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // TxtPainel
            // 
            this.TxtPainel.AutoSize = true;
            this.TxtPainel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtPainel.Font = new System.Drawing.Font("Sans Serif Collection", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPainel.ForeColor = System.Drawing.Color.White;
            this.TxtPainel.Location = new System.Drawing.Point(0, 0);
            this.TxtPainel.Margin = new System.Windows.Forms.Padding(0);
            this.TxtPainel.Name = "TxtPainel";
            this.TxtPainel.Padding = new System.Windows.Forms.Padding(20, 25, 0, 0);
            this.TxtPainel.Size = new System.Drawing.Size(186, 64);
            this.TxtPainel.TabIndex = 19;
            this.TxtPainel.Text = "Defaut Title";
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.BackColor = System.Drawing.Color.White;
            this.BtnCancelar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnCancelar.BackgroundImage")));
            this.BtnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancelar.FlatAppearance.BorderSize = 0;
            this.BtnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.BtnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancelar.Location = new System.Drawing.Point(35, 760);
            this.BtnCancelar.Margin = new System.Windows.Forms.Padding(0);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(139, 40);
            this.BtnCancelar.TabIndex = 13;
            this.BtnCancelar.UseVisualStyleBackColor = false;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.Hour, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.ListMaterial, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.TxtQuant, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.Date, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.Quantidade, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.textEmail, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.TxtOrder, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.TxtOrdem, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(26, 157);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 15;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(321, 451);
            this.tableLayoutPanel1.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Sans Serif Collection", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 300);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label4.Size = new System.Drawing.Size(160, 50);
            this.label4.TabIndex = 34;
            this.label4.Text = "Hora";
            // 
            // Hour
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.Hour, 2);
            this.Hour.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Hour.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.Hour.Location = new System.Drawing.Point(3, 353);
            this.Hour.Name = "Hour";
            this.Hour.Size = new System.Drawing.Size(286, 26);
            this.Hour.TabIndex = 33;
            // 
            // ListMaterial
            // 
            this.ListMaterial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(126)))), ((int)(((byte)(235)))));
            this.ListMaterial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel1.SetColumnSpan(this.ListMaterial, 2);
            this.ListMaterial.ColumnWidth = 4;
            this.ListMaterial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ListMaterial.Font = new System.Drawing.Font("Sans Serif Collection", 8.25F);
            this.ListMaterial.ForeColor = System.Drawing.Color.White;
            this.ListMaterial.FormattingEnabled = true;
            this.ListMaterial.Location = new System.Drawing.Point(3, 553);
            this.ListMaterial.Name = "ListMaterial";
            this.tableLayoutPanel1.SetRowSpan(this.ListMaterial, 4);
            this.ListMaterial.Size = new System.Drawing.Size(293, 150);
            this.ListMaterial.TabIndex = 32;
            this.ListMaterial.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ListMaterial_ItemCheck);
            this.ListMaterial.SelectedIndexChanged += new System.EventHandler(this.ListMaterial_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Sans Serif Collection", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 500);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label3.Size = new System.Drawing.Size(160, 50);
            this.label3.TabIndex = 30;
            this.label3.Text = "Relacionado Material";
            // 
            // TxtQuant
            // 
            this.TxtQuant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.TxtQuant, 2);
            this.TxtQuant.Location = new System.Drawing.Point(3, 453);
            this.TxtQuant.Name = "TxtQuant";
            this.TxtQuant.Size = new System.Drawing.Size(286, 26);
            this.TxtQuant.TabIndex = 29;
            this.TxtQuant.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TxtQuant_MouseClick);
            // 
            // Date
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.Date, 2);
            this.Date.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Date.Location = new System.Drawing.Point(3, 253);
            this.Date.Name = "Date";
            this.Date.Size = new System.Drawing.Size(286, 26);
            this.Date.TabIndex = 27;
            // 
            // Quantidade
            // 
            this.Quantidade.AutoSize = true;
            this.Quantidade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Quantidade.Font = new System.Drawing.Font("Sans Serif Collection", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Quantidade.ForeColor = System.Drawing.Color.White;
            this.Quantidade.Location = new System.Drawing.Point(0, 400);
            this.Quantidade.Margin = new System.Windows.Forms.Padding(0);
            this.Quantidade.Name = "Quantidade";
            this.Quantidade.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.Quantidade.Size = new System.Drawing.Size(160, 50);
            this.Quantidade.TabIndex = 26;
            this.Quantidade.Text = "Quantidade";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Sans Serif Collection", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 200);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label2.Size = new System.Drawing.Size(160, 50);
            this.label2.TabIndex = 24;
            this.label2.Text = "Data ";
            // 
            // textEmail
            // 
            this.textEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.textEmail, 2);
            this.textEmail.Location = new System.Drawing.Point(3, 153);
            this.textEmail.Name = "textEmail";
            this.textEmail.Size = new System.Drawing.Size(286, 26);
            this.textEmail.TabIndex = 23;
            this.textEmail.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textEmail_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Sans Serif Collection", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 100);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label1.Size = new System.Drawing.Size(160, 50);
            this.label1.TabIndex = 21;
            this.label1.Text = "Email";
            // 
            // TxtOrder
            // 
            this.TxtOrder.AutoSize = true;
            this.TxtOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtOrder.Font = new System.Drawing.Font("Sans Serif Collection", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtOrder.ForeColor = System.Drawing.Color.White;
            this.TxtOrder.Location = new System.Drawing.Point(0, 0);
            this.TxtOrder.Margin = new System.Windows.Forms.Padding(0);
            this.TxtOrder.Name = "TxtOrder";
            this.TxtOrder.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.TxtOrder.Size = new System.Drawing.Size(160, 50);
            this.TxtOrder.TabIndex = 20;
            this.TxtOrder.Text = "Order";
            // 
            // TxtOrdem
            // 
            this.TxtOrdem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.TxtOrdem, 2);
            this.TxtOrdem.Location = new System.Drawing.Point(3, 53);
            this.TxtOrdem.Name = "TxtOrdem";
            this.TxtOrdem.Size = new System.Drawing.Size(286, 26);
            this.TxtOrdem.TabIndex = 28;
            this.TxtOrdem.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TxtNameUser_MouseClick);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 821);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(350, 10);
            this.panel3.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(350, 59);
            this.panel2.TabIndex = 0;
            // 
            // Timer
            // 
            this.Timer.Interval = 500;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // PnlGrid
            // 
            this.PnlGrid.Controls.Add(this.pnlDel);
            this.PnlGrid.Controls.Add(this.Messagem);
            this.PnlGrid.Controls.Add(this.Grid_Users);
            this.PnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlGrid.Location = new System.Drawing.Point(2, 122);
            this.PnlGrid.Margin = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.PnlGrid.Name = "PnlGrid";
            this.PnlGrid.Padding = new System.Windows.Forms.Padding(40, 59, 60, 10);
            this.PnlGrid.Size = new System.Drawing.Size(1364, 831);
            this.PnlGrid.TabIndex = 10;
            // 
            // pnlDel
            // 
            this.pnlDel.AutoSize = true;
            this.pnlDel.BackColor = System.Drawing.Color.White;
            this.pnlDel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlDel.Controls.Add(this.textDelInfo);
            this.pnlDel.Controls.Add(this.bntConfirmDel);
            this.pnlDel.Controls.Add(this.bntCancelDel);
            this.pnlDel.Controls.Add(this.pictureDel);
            this.pnlDel.Controls.Add(this.textDel);
            this.pnlDel.Location = new System.Drawing.Point(560, 157);
            this.pnlDel.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDel.Name = "pnlDel";
            this.pnlDel.Size = new System.Drawing.Size(488, 310);
            this.pnlDel.TabIndex = 12;
            this.pnlDel.Visible = false;
            // 
            // textDelInfo
            // 
            this.textDelInfo.BackColor = System.Drawing.Color.White;
            this.textDelInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textDelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDelInfo.ForeColor = System.Drawing.Color.Black;
            this.textDelInfo.Location = new System.Drawing.Point(10, 178);
            this.textDelInfo.Margin = new System.Windows.Forms.Padding(10);
            this.textDelInfo.Name = "textDelInfo";
            this.textDelInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textDelInfo.Size = new System.Drawing.Size(456, 22);
            this.textDelInfo.TabIndex = 16;
            this.textDelInfo.Text = "Defaut";
            this.textDelInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textDelInfo.Visible = false;
            // 
            // bntConfirmDel
            // 
            this.bntConfirmDel.BackColor = System.Drawing.Color.Red;
            this.bntConfirmDel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bntConfirmDel.BackgroundImage")));
            this.bntConfirmDel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntConfirmDel.FlatAppearance.BorderSize = 0;
            this.bntConfirmDel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(43)))), ((int)(((byte)(40)))));
            this.bntConfirmDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntConfirmDel.Location = new System.Drawing.Point(268, 237);
            this.bntConfirmDel.Margin = new System.Windows.Forms.Padding(0);
            this.bntConfirmDel.Name = "bntConfirmDel";
            this.bntConfirmDel.Size = new System.Drawing.Size(140, 40);
            this.bntConfirmDel.TabIndex = 15;
            this.bntConfirmDel.UseVisualStyleBackColor = false;
            this.bntConfirmDel.Visible = false;
            this.bntConfirmDel.Click += new System.EventHandler(this.bntConfirmDel_Click);
            // 
            // bntCancelDel
            // 
            this.bntCancelDel.BackColor = System.Drawing.Color.White;
            this.bntCancelDel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bntCancelDel.BackgroundImage")));
            this.bntCancelDel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntCancelDel.FlatAppearance.BorderSize = 0;
            this.bntCancelDel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.bntCancelDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntCancelDel.Location = new System.Drawing.Point(96, 237);
            this.bntCancelDel.Margin = new System.Windows.Forms.Padding(0);
            this.bntCancelDel.Name = "bntCancelDel";
            this.bntCancelDel.Size = new System.Drawing.Size(139, 40);
            this.bntCancelDel.TabIndex = 14;
            this.bntCancelDel.UseVisualStyleBackColor = false;
            this.bntCancelDel.Visible = false;
            this.bntCancelDel.Click += new System.EventHandler(this.bntCancelDel_Click);
            // 
            // pictureDel
            // 
            this.pictureDel.BackColor = System.Drawing.Color.Transparent;
            this.pictureDel.Image = ((System.Drawing.Image)(resources.GetObject("pictureDel.Image")));
            this.pictureDel.InitialImage = null;
            this.pictureDel.Location = new System.Drawing.Point(205, 14);
            this.pictureDel.Name = "pictureDel";
            this.pictureDel.Size = new System.Drawing.Size(81, 81);
            this.pictureDel.TabIndex = 3;
            this.pictureDel.TabStop = false;
            this.pictureDel.Visible = false;
            // 
            // textDel
            // 
            this.textDel.BackColor = System.Drawing.Color.White;
            this.textDel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDel.ForeColor = System.Drawing.Color.Black;
            this.textDel.Location = new System.Drawing.Point(18, 117);
            this.textDel.Margin = new System.Windows.Forms.Padding(10);
            this.textDel.Multiline = true;
            this.textDel.Name = "textDel";
            this.textDel.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textDel.Size = new System.Drawing.Size(456, 51);
            this.textDel.TabIndex = 2;
            this.textDel.Text = "Você tem certeza que deseja \r\nexcluir este registro?";
            this.textDel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textDel.Visible = false;
            // 
            // Messagem
            // 
            this.Messagem.AutoSize = true;
            this.Messagem.BackColor = System.Drawing.Color.LimeGreen;
            this.Messagem.Controls.Add(this.TxtMensagem);
            this.Messagem.Location = new System.Drawing.Point(560, 8);
            this.Messagem.Margin = new System.Windows.Forms.Padding(0);
            this.Messagem.Name = "Messagem";
            this.Messagem.Size = new System.Drawing.Size(476, 40);
            this.Messagem.TabIndex = 11;
            this.Messagem.Visible = false;
            // 
            // TxtMensagem
            // 
            this.TxtMensagem.BackColor = System.Drawing.Color.LimeGreen;
            this.TxtMensagem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtMensagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMensagem.ForeColor = System.Drawing.Color.White;
            this.TxtMensagem.Location = new System.Drawing.Point(10, 8);
            this.TxtMensagem.Margin = new System.Windows.Forms.Padding(10);
            this.TxtMensagem.Name = "TxtMensagem";
            this.TxtMensagem.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtMensagem.Size = new System.Drawing.Size(456, 22);
            this.TxtMensagem.TabIndex = 2;
            this.TxtMensagem.Text = "Defaut Mensagem";
            this.TxtMensagem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtMensagem.Visible = false;
            // 
            // Grid_Users
            // 
            this.Grid_Users.AllowUserToResizeColumns = false;
            this.Grid_Users.AllowUserToResizeRows = false;
            this.Grid_Users.BackgroundColor = System.Drawing.Color.White;
            this.Grid_Users.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.Grid_Users.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(126)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(126)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grid_Users.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Grid_Users.ColumnHeadersHeight = 54;
            this.Grid_Users.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Grid_Users.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Email,
            this.Order,
            this.DateProduct,
            this.TableHour,
            this.Quantity,
            this.MaterialCode,
            this.CycleTimeProduction});
            this.Grid_Users.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid_Users.EnableHeadersVisualStyles = false;
            this.Grid_Users.GridColor = System.Drawing.Color.White;
            this.Grid_Users.Location = new System.Drawing.Point(40, 59);
            this.Grid_Users.Margin = new System.Windows.Forms.Padding(0);
            this.Grid_Users.MultiSelect = false;
            this.Grid_Users.Name = "Grid_Users";
            this.Grid_Users.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.Grid_Users.RowHeadersVisible = false;
            this.Grid_Users.RowHeadersWidth = 40;
            this.Grid_Users.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.Grid_Users.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Grid_Users.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.Grid_Users.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Grid_Users.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(126)))), ((int)(((byte)(235)))));
            this.Grid_Users.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(126)))), ((int)(((byte)(235)))));
            this.Grid_Users.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.Grid_Users.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grid_Users.RowTemplate.DividerHeight = 3;
            this.Grid_Users.RowTemplate.Height = 40;
            this.Grid_Users.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Grid_Users.Size = new System.Drawing.Size(1264, 762);
            this.Grid_Users.TabIndex = 0;
            this.Grid_Users.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_Users_CellClick);
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            // 
            // Email
            // 
            this.Email.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Email.HeaderText = "E-mail";
            this.Email.Name = "Email";
            // 
            // Order
            // 
            this.Order.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Order.HeaderText = "Ordem";
            this.Order.Name = "Order";
            // 
            // DateProduct
            // 
            this.DateProduct.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DateProduct.HeaderText = "Data ";
            this.DateProduct.Name = "DateProduct";
            // 
            // TableHour
            // 
            this.TableHour.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TableHour.HeaderText = "Hora";
            this.TableHour.Name = "TableHour";
            // 
            // Quantity
            // 
            this.Quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Quantity.HeaderText = "Quantidade";
            this.Quantity.Name = "Quantity";
            // 
            // MaterialCode
            // 
            this.MaterialCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MaterialCode.HeaderText = "Código Material";
            this.MaterialCode.Name = "MaterialCode";
            // 
            // CycleTimeProduction
            // 
            this.CycleTimeProduction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CycleTimeProduction.HeaderText = "Tempo";
            this.CycleTimeProduction.Name = "CycleTimeProduction";
            // 
            // Frm_Production
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1718, 955);
            this.Controls.Add(this.PnlGrid);
            this.Controls.Add(this.ContainerPainel);
            this.Controls.Add(this.TlPnlTop);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_Production";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Load += new System.EventHandler(this.Frm_User_Load);
            this.Controls.SetChildIndex(this.TlPnlTop, 0);
            this.Controls.SetChildIndex(this.ContainerPainel, 0);
            this.Controls.SetChildIndex(this.PnlGrid, 0);
            this.TlPnlTop.ResumeLayout(false);
            this.PnlSearch.ResumeLayout(false);
            this.PnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearch)).EndInit();
            this.ContainerPainel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.PnlGrid.ResumeLayout(false);
            this.PnlGrid.PerformLayout();
            this.pnlDel.ResumeLayout(false);
            this.pnlDel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureDel)).EndInit();
            this.Messagem.ResumeLayout(false);
            this.Messagem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Users)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel TlPnlTop;
        public System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel PnlSearch;
        private System.Windows.Forms.PictureBox picSearch;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Panel ContainerPainel;
        private System.Windows.Forms.Button BtnConfirmar;
        private System.Windows.Forms.Button BtnCancelar;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label TxtPainel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Label TxtOrder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Quantidade;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textEmail;
        private System.Windows.Forms.DateTimePicker Date;
        private System.Windows.Forms.Label LineSearch;
        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.Panel PnlGrid;
        private System.Windows.Forms.DataGridView Grid_Users;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel Messagem;
        private System.Windows.Forms.TextBox TxtMensagem;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.RichTextBox TextInfo;
        private System.Windows.Forms.TextBox TxtOrdem;
        private System.Windows.Forms.Panel pnlDel;
        private System.Windows.Forms.TextBox textDel;
        private System.Windows.Forms.PictureBox pictureDel;
        private System.Windows.Forms.TextBox textDelInfo;
        private System.Windows.Forms.Button bntConfirmDel;
        private System.Windows.Forms.Button bntCancelDel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtQuant;
        private System.Windows.Forms.CheckedListBox ListMaterial;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker Hour;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableHour;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaterialCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CycleTimeProduction;
    }
}