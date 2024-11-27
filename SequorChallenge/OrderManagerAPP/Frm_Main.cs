using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderManagerAPP
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
            SelectButton(BtnOrder);
        }
        private void SelectButton(Button selectedButton)
        {
            UC_Page UC_Page = new UC_Page();
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
            PnlNav.Location = new Point(selectedButton.Location.X, selectedButton.Location.Y + 66);
            UC_Page.Title = selectedButton.Tag.ToString();
            PnlPage.Controls.Add(UC_Page);

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
    }
}
