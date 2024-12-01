using OrderManagerAPP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace OrderManagerAPP
{
    public partial class Frm_Login : Form
    {


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private Timer messageTimer;

        public Frm_Login()
        {
            InitializeComponent();

            messageTimer = new Timer();
            messageTimer.Interval = 5000;
            messageTimer.Tick += MessageTimer_Tick;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MessageTimer_Tick(object sender, EventArgs e)
        {
            TxtMensagem.Visible = false;
            Messagem.Visible = false;
            messageTimer.Stop();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {

            var email = TxtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Por favor, insira um e-mail válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string apiUrl = $"http://localhost:5178/api/User/GetEmail/{email}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        var user = JsonSerializer.Deserialize<User>(jsonResponse, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (user != null && !string.IsNullOrEmpty(user.Email))
                        {
                            Messagem.BackColor = Color.LimeGreen;
                            TxtMensagem.BackColor = Color.LimeGreen;
                            TxtMensagem.Text = "Login realizado com sucesso! Bem-vindo, " + user.Name;
                            TxtMensagem.Visible = true;
                            Messagem.Visible = true;
                            messageTimer.Start();

                            await Task.Delay(2500);

                            Frm_Main frmMain = new Frm_Main
                            {
                                EmailUsuario = email
                            };


                        
                            string iconPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..",  "Icon.ico");

                            frmMain.Icon = new Icon(iconPath);

                            frmMain.Show();

                            this.Hide();
                        }
                        else
                        {
                            Messagem.BackColor = Color.LimeGreen;
                            TxtMensagem.BackColor = Color.LimeGreen;
                            Messagem.BackColor = Color.Red;
                            TxtMensagem.BackColor = Color.Red;
                            TxtMensagem.Text = "E-mail não encontrado. Verifique o e-mail fornecido.";
                            TxtMensagem.Visible = true;
                            Messagem.Visible = true;
                            messageTimer.Start();
                        }
                    }
                    else
                    {
                        Messagem.BackColor = Color.Red;
                        TxtMensagem.BackColor = Color.Red;
                        TxtMensagem.Text = "E-mail não encontrado. Verifique o e-mail fornecido.";
                        TxtMensagem.Visible = true;
                        Messagem.Visible = true;
                        messageTimer.Start();
                    }
                }
            }
            catch (Exception ex)
            {


                Messagem.BackColor = Color.Red;
                TxtMensagem.BackColor = Color.Red;
                TxtMensagem.Text = "Erro ao se conectar ao servidor: " + ex.Message;
                TxtMensagem.Visible = true;
                Messagem.Visible = true;
                messageTimer.Start();
            }
        }

        private void TxtCriar_Click(object sender, EventArgs e)
        {
            Frm_CreateUser User = new Frm_CreateUser
            {
                FormBorderStyle = FormBorderStyle.Sizable
            };

            User.Show();


        }

        private void TxtEmail_MouseClick(object sender, MouseEventArgs e)
        {

            if (TxtEmail.Text == "E-mail")
            {
                TxtEmail.Text = "";
            }
        }

        private void Frm_Login_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
    }    
    
}
