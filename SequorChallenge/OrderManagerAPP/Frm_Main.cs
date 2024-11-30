using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OrderManagerAPP
{
    public partial class Frm_Main : Form
    { 

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);


        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;
        public Frm_Main()
        {
            InitializeComponent();
            SelectButton(BtnOrder);
            TLPnlTop.MouseDown += TLPnlTop_MouseDown;
        }

        public void SelectButton(Button selectedButton)
        {
            var i = selectedButton.Name;
            PnlPage.Controls.Clear();

            Color defaultColor = Color.FromArgb(83, 126, 235);
            Color selectedColor = Color.FromArgb(48, 73, 135);      


            // Lista de botões que precisam ser atualizados
            Button[] buttons = { BtnOrder, BtnProduction, BtnProduct, BtnMaterial, BtnUser };

          
            foreach (Button btn in buttons)
            {
                btn.BackColor = defaultColor;
    
            }

            // Ajusta apenas o botão selecionado
            selectedButton.BackColor = selectedColor;
            PnlNav.Top = selectedButton.Top;
            PnlNav.Height = selectedButton.Height;
            PnlNav.Location = new Point(selectedButton.Location.X, selectedButton.Location.Y + 65);

            Form selectedForm = null;
            switch (selectedButton.Name)
            {
                case "BtnOrder":
                    selectedForm = new Frm_Order(this);
                    break;
                case "BtnProduction":
                    selectedForm = new Frm_Production();
                    break;
                case "BtnProduct":
                    selectedForm = new Frm_Product();
                    break;
                case "BtnMaterial":
                    selectedForm = new Frm_Material();
                    break;
                case "BtnUser":
                    selectedForm = new Frm_User();
                    break;
            }

            if (selectedForm != null)
            {
                selectedForm.TopLevel = false;  
                selectedForm.FormBorderStyle = FormBorderStyle.None;  
                selectedForm.Dock = DockStyle.Fill;

                if (selectedForm is Base baseForm)
                {
                    baseForm.Title = selectedButton.Tag?.ToString() ?? "Sem título";
                }

                PnlPage.Controls.Add(selectedForm);
                selectedForm.Show();  
            }

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnMaximize_Click(object sender, EventArgs e)
        {
           if(WindowState == FormWindowState.Maximized)
            {
                WindowState= FormWindowState.Normal;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
            }
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void BtnOrder_Click(object sender, EventArgs e)
        {
            SelectButton(BtnOrder);
        }
        private void BtnProduction_Click(object sender, EventArgs e)
        {
            SelectButton(BtnProduction);
        }

        private void BtnProduct_Click(object sender, EventArgs e)
        {
            SelectButton(BtnProduct);
        }

        private void BtnMaterial_Click(object sender, EventArgs e)
        {
            SelectButton(BtnMaterial);
        }

        private void BtnUser_Click(object sender, EventArgs e)
        {
            SelectButton(BtnUser);
        }
        public void NavigateToProduction()
        {
            SelectButton(BtnProduction);

            var productionForm = PnlPage.Controls.OfType<Frm_Production>().FirstOrDefault();
            if (productionForm != null)
            {
                // Aguarda um pouco para garantir que o botão foi renderizado
                Task.Delay(100).ContinueWith(t =>
                {
                    productionForm.SimulateBtnAddClick();
                });
            }
        }

        public T GetActiveForm<T>() where T : Form
        {
            return PnlPage.Controls.OfType<T>().FirstOrDefault();
        }

        private void TLPnlTop_Paint(object sender, PaintEventArgs e)
        {

        }
        private void TLPnlTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
