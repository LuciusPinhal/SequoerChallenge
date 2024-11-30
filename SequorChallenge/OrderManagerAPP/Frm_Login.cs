using OrderManagerAPP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace OrderManagerAPP
{
    public partial class Frm_Login : Form
    {
        public Frm_Login()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

                        // Deserializa o JSON recebido para o modelo de usuário
                        var user = JsonSerializer.Deserialize<User>(jsonResponse, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        // Verifica se o usuário foi encontrado
                        if (user != null && !string.IsNullOrEmpty(user.Email))
                        {
                            MessageBox.Show($"Bem-vindo, {user.Name}!", "Login bem-sucedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Frm_Main frmMain = new Frm_Main
                            {
                                EmailUsuario = email
                            };

                            frmMain.Show();

                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("E-mail não encontrado. Verifique o e-mail fornecido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Erro ao validar o e-mail: " + response.ReasonPhrase, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao se conectar ao servidor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
    }
}
