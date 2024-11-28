namespace OrderManagerAPP
{
    partial class Frm_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Main));
            this.label1 = new System.Windows.Forms.Label();
            this.TLPnlMenu = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnOrder = new System.Windows.Forms.Button();
            this.BtnProduction = new System.Windows.Forms.Button();
            this.BtnProduct = new System.Windows.Forms.Button();
            this.BtnMaterial = new System.Windows.Forms.Button();
            this.BtnUser = new System.Windows.Forms.Button();
            this.TLPnlTop = new System.Windows.Forms.TableLayoutPanel();
            this.BtnMinimize = new System.Windows.Forms.Button();
            this.BtnMaximize = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.PnlNav = new System.Windows.Forms.Label();
            this.PnlFill = new System.Windows.Forms.Panel();
            this.PnlPage = new System.Windows.Forms.Panel();
            this.TLPnlMenu.SuspendLayout();
            this.TLPnlTop.SuspendLayout();
            this.PnlFill.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sans Serif Collection", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(33, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label1.Size = new System.Drawing.Size(246, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "Challenge Sequor";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // TLPnlMenu
            // 
            this.TLPnlMenu.ColumnCount = 1;
            this.TLPnlMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLPnlMenu.Controls.Add(this.panel1, 0, 0);
            this.TLPnlMenu.Controls.Add(this.BtnOrder, 0, 1);
            this.TLPnlMenu.Controls.Add(this.BtnProduction, 0, 2);
            this.TLPnlMenu.Controls.Add(this.BtnProduct, 0, 3);
            this.TLPnlMenu.Controls.Add(this.BtnMaterial, 0, 4);
            this.TLPnlMenu.Controls.Add(this.BtnUser, 0, 5);
            this.TLPnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.TLPnlMenu.Location = new System.Drawing.Point(0, 64);
            this.TLPnlMenu.Name = "TLPnlMenu";
            this.TLPnlMenu.RowCount = 7;
            this.TLPnlMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.TLPnlMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.TLPnlMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.TLPnlMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.TLPnlMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.TLPnlMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.TLPnlMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.TLPnlMenu.Size = new System.Drawing.Size(166, 977);
            this.TLPnlMenu.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(166, 2);
            this.panel1.TabIndex = 0;
            // 
            // BtnOrder
            // 
            this.BtnOrder.BackColor = System.Drawing.Color.Transparent;
            this.BtnOrder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnOrder.BackgroundImage")));
            this.BtnOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnOrder.FlatAppearance.BorderSize = 0;
            this.BtnOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.BtnOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnOrder.ForeColor = System.Drawing.Color.Transparent;
            this.BtnOrder.ImageIndex = 2;
            this.BtnOrder.Location = new System.Drawing.Point(5, 7);
            this.BtnOrder.Margin = new System.Windows.Forms.Padding(5);
            this.BtnOrder.Name = "BtnOrder";
            this.BtnOrder.Size = new System.Drawing.Size(156, 146);
            this.BtnOrder.TabIndex = 14;
            this.BtnOrder.Tag = "Order";
            this.BtnOrder.UseVisualStyleBackColor = false;
            this.BtnOrder.Click += new System.EventHandler(this.BtnOrder_Click);
            // 
            // BtnProduction
            // 
            this.BtnProduction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnProduction.BackgroundImage")));
            this.BtnProduction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnProduction.FlatAppearance.BorderSize = 0;
            this.BtnProduction.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.BtnProduction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnProduction.ImageIndex = 2;
            this.BtnProduction.Location = new System.Drawing.Point(5, 163);
            this.BtnProduction.Margin = new System.Windows.Forms.Padding(5);
            this.BtnProduction.Name = "BtnProduction";
            this.BtnProduction.Size = new System.Drawing.Size(156, 146);
            this.BtnProduction.TabIndex = 13;
            this.BtnProduction.Tag = "Produção";
            this.BtnProduction.UseVisualStyleBackColor = true;
            this.BtnProduction.Click += new System.EventHandler(this.BtnProduction_Click);
            // 
            // BtnProduct
            // 
            this.BtnProduct.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnProduct.BackgroundImage")));
            this.BtnProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnProduct.FlatAppearance.BorderSize = 0;
            this.BtnProduct.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.BtnProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnProduct.ImageIndex = 2;
            this.BtnProduct.Location = new System.Drawing.Point(5, 319);
            this.BtnProduct.Margin = new System.Windows.Forms.Padding(5);
            this.BtnProduct.Name = "BtnProduct";
            this.BtnProduct.Size = new System.Drawing.Size(156, 146);
            this.BtnProduct.TabIndex = 12;
            this.BtnProduct.Tag = "Produto";
            this.BtnProduct.UseVisualStyleBackColor = true;
            this.BtnProduct.Click += new System.EventHandler(this.BtnProduct_Click);
            // 
            // BtnMaterial
            // 
            this.BtnMaterial.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnMaterial.BackgroundImage")));
            this.BtnMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnMaterial.FlatAppearance.BorderSize = 0;
            this.BtnMaterial.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.BtnMaterial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMaterial.ImageIndex = 2;
            this.BtnMaterial.Location = new System.Drawing.Point(5, 475);
            this.BtnMaterial.Margin = new System.Windows.Forms.Padding(5);
            this.BtnMaterial.Name = "BtnMaterial";
            this.BtnMaterial.Size = new System.Drawing.Size(156, 146);
            this.BtnMaterial.TabIndex = 11;
            this.BtnMaterial.Tag = "Material";
            this.BtnMaterial.UseVisualStyleBackColor = true;
            this.BtnMaterial.Click += new System.EventHandler(this.BtnMaterial_Click);
            // 
            // BtnUser
            // 
            this.BtnUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnUser.BackgroundImage")));
            this.BtnUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnUser.FlatAppearance.BorderSize = 0;
            this.BtnUser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.BtnUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnUser.ImageIndex = 2;
            this.BtnUser.Location = new System.Drawing.Point(5, 631);
            this.BtnUser.Margin = new System.Windows.Forms.Padding(5);
            this.BtnUser.Name = "BtnUser";
            this.BtnUser.Size = new System.Drawing.Size(156, 146);
            this.BtnUser.TabIndex = 10;
            this.BtnUser.Tag = "Usuário";
            this.BtnUser.UseVisualStyleBackColor = true;
            this.BtnUser.Click += new System.EventHandler(this.BtnUser_Click);
            // 
            // TLPnlTop
            // 
            this.TLPnlTop.ColumnCount = 8;
            this.TLPnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLPnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.TLPnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.TLPnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TLPnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.TLPnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.TLPnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.TLPnlTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.TLPnlTop.Controls.Add(this.BtnMinimize, 5, 0);
            this.TLPnlTop.Controls.Add(this.BtnMaximize, 6, 0);
            this.TLPnlTop.Controls.Add(this.BtnExit, 7, 0);
            this.TLPnlTop.Controls.Add(this.label1, 2, 0);
            this.TLPnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.TLPnlTop.Location = new System.Drawing.Point(0, 0);
            this.TLPnlTop.Margin = new System.Windows.Forms.Padding(0);
            this.TLPnlTop.Name = "TLPnlTop";
            this.TLPnlTop.RowCount = 1;
            this.TLPnlTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLPnlTop.Size = new System.Drawing.Size(1904, 64);
            this.TLPnlTop.TabIndex = 0;
            this.TLPnlTop.Paint += new System.Windows.Forms.PaintEventHandler(this.TLPnlTop_Paint);
            // 
            // BtnMinimize
            // 
            this.BtnMinimize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnMinimize.BackgroundImage")));
            this.BtnMinimize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnMinimize.FlatAppearance.BorderSize = 0;
            this.BtnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.BtnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMinimize.Location = new System.Drawing.Point(1753, 0);
            this.BtnMinimize.Margin = new System.Windows.Forms.Padding(0);
            this.BtnMinimize.Name = "BtnMinimize";
            this.BtnMinimize.Size = new System.Drawing.Size(50, 64);
            this.BtnMinimize.TabIndex = 4;
            this.BtnMinimize.UseVisualStyleBackColor = true;
            this.BtnMinimize.Click += new System.EventHandler(this.BtnMinimize_Click);
            // 
            // BtnMaximize
            // 
            this.BtnMaximize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnMaximize.BackgroundImage")));
            this.BtnMaximize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnMaximize.FlatAppearance.BorderSize = 0;
            this.BtnMaximize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(205)))), ((int)(((byte)(255)))));
            this.BtnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMaximize.Location = new System.Drawing.Point(1803, 0);
            this.BtnMaximize.Margin = new System.Windows.Forms.Padding(0);
            this.BtnMaximize.Name = "BtnMaximize";
            this.BtnMaximize.Size = new System.Drawing.Size(50, 64);
            this.BtnMaximize.TabIndex = 3;
            this.BtnMaximize.UseVisualStyleBackColor = true;
            this.BtnMaximize.Click += new System.EventHandler(this.BtnMaximize_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnExit.BackgroundImage")));
            this.BtnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnExit.FlatAppearance.BorderSize = 0;
            this.BtnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExit.Location = new System.Drawing.Point(1853, 0);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(0);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(51, 64);
            this.BtnExit.TabIndex = 2;
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // PnlNav
            // 
            this.PnlNav.BackColor = System.Drawing.Color.White;
            this.PnlNav.Location = new System.Drawing.Point(22, 897);
            this.PnlNav.Name = "PnlNav";
            this.PnlNav.Size = new System.Drawing.Size(10, 90);
            this.PnlNav.TabIndex = 2;
            // 
            // PnlFill
            // 
            this.PnlFill.Controls.Add(this.PnlPage);
            this.PnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlFill.Location = new System.Drawing.Point(166, 64);
            this.PnlFill.Margin = new System.Windows.Forms.Padding(0);
            this.PnlFill.Name = "PnlFill";
            this.PnlFill.Size = new System.Drawing.Size(1738, 977);
            this.PnlFill.TabIndex = 3;
            // 
            // PnlPage
            // 
            this.PnlPage.BackColor = System.Drawing.Color.White;
            this.PnlPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlPage.Location = new System.Drawing.Point(0, 0);
            this.PnlPage.Margin = new System.Windows.Forms.Padding(0);
            this.PnlPage.Name = "PnlPage";
            this.PnlPage.Size = new System.Drawing.Size(1738, 977);
            this.PnlPage.TabIndex = 0;
            // 
            // Frm_Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(126)))), ((int)(((byte)(235)))));
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.PnlFill);
            this.Controls.Add(this.PnlNav);
            this.Controls.Add(this.TLPnlMenu);
            this.Controls.Add(this.TLPnlTop);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_Main";
            this.TLPnlMenu.ResumeLayout(false);
            this.TLPnlTop.ResumeLayout(false);
            this.TLPnlTop.PerformLayout();
            this.PnlFill.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel TLPnlMenu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel TLPnlTop;
        private System.Windows.Forms.Button BtnMinimize;
        private System.Windows.Forms.Button BtnMaximize;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnProduct;
        private System.Windows.Forms.Button BtnMaterial;
        private System.Windows.Forms.Button BtnUser;
        private System.Windows.Forms.Button BtnOrder;
        private System.Windows.Forms.Button BtnProduction;
        private System.Windows.Forms.Label PnlNav;
        private System.Windows.Forms.Panel PnlFill;
        private System.Windows.Forms.Panel PnlPage;
        private System.Windows.Forms.Panel panel1;
    }
}