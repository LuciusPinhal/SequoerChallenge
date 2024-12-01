﻿using OrderManagerAPP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace OrderManagerAPP
{
    public partial class Frm_CreateUser : Form
    {
        private bool painelAberto = false;
        private Timer messageTimer;

        public Frm_CreateUser()
        {
            InitializeComponent();


            messageTimer = new Timer();
            messageTimer.Interval = 5000;
            messageTimer.Tick += MessageTimer_Tick;
        }
        public string TitlePainel
        {
            get => TxtPainel.Text;
            set => TxtPainel.Text = value;
        }

        private void MessageTimer_Tick(object sender, EventArgs e)
        {
            TxtMensagem.Visible = false;
            TextInfo.Visible = false;
            Messagem.Visible = false;
            messageTimer.Stop();
        }

        public void SelectTxt(TextBox SeletTxt)
        {
            Color defaultColor = Color.White;
            Color selectedColor = Color.FromArgb(185, 205, 255);


            TextBox[] textBoxes = { TxtNameUser, textEmail };


            foreach (TextBox Txt in textBoxes)
            {
                Txt.BackColor = defaultColor;
            }

            SeletTxt.BackColor = selectedColor;
        }

        private void AbrirPainel()
        {
            if (!painelAberto)
            {
                Timer.Start();         
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (ContainerPainel.Width == 350)
            {
                for (int i = 0; i < 10; i++)
                {
                    ContainerPainel.Width = ContainerPainel.Width - 35;
                }
                ContainerPainel.Visible = false;
                painelAberto = false;
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    ContainerPainel.Width = ContainerPainel.Width + 35;
                }
                ContainerPainel.Visible = true;
                painelAberto = true;
            }

            Timer.Stop();
        }
        private void TxtNameUser_MouseClick(object sender, MouseEventArgs e)
        {
            SelectTxt(TxtNameUser);
        }
        private void textEmail_MouseClick(object sender, MouseEventArgs e)
        {
            SelectTxt(textEmail);
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Timer.Start();
            ClearText();
        }
        private async void Frm_User_Load(object sender, EventArgs e)
        {
            ContainerPainel.Width = 0;
            await LoadOrdersAsync();
        }
        private void TxtSearch_MouseClick(object sender, MouseEventArgs e)
        {
            LineSearch.Visible = true;
            if (TxtSearch.Text == "Pesquisar")
            {
                TxtSearch.Text = "";
            }
        }
        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            LineSearch.Visible = false;

            if (TxtSearch.Text == "")
            {
                TxtSearch.Text = "Pesquisar";
            }
        }
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Timer.Start();
            ClearText();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ClearText();

            DateInitial.Format = DateTimePickerFormat.Custom;
            DateInitial.CustomFormat = "dd/MM/yyyy HH:mm:ss";

            EndDate.Format = DateTimePickerFormat.Custom;
            EndDate.CustomFormat = "dd/MM/yyyy HH:mm:ss";

            AbrirPainel();
            TitlePainel = "Adicionar";

        }

        private string EmailSelect = null;
        private string NameSelect = null;
        private string DateInitSelect = null;
        private string EndDateSelect = null;


        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchValue = TxtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchValue) || searchValue == "pesquisar")
            {
          
                foreach (DataGridViewRow row in Grid_Users.Rows)
                {
                    if (!row.IsNewRow) 
                        row.Visible = true;
                }
                return; 
            }

         
            foreach (DataGridViewRow row in Grid_Users.Rows)
            {
                if (row.IsNewRow)
                    continue;

                // Verifica se alguma célula contém o texto buscado
                bool visible = row.Cells["Email"].Value?.ToString().ToLower().Contains(searchValue) == true ||
                               row.Cells["NameUser"].Value?.ToString().ToLower().Contains(searchValue) == true ||
                               row.Cells["InitialDate"].Value?.ToString().ToLower().Contains(searchValue) == true ||
                               row.Cells["DateEnd"].Value?.ToString().ToLower().Contains(searchValue) == true;

                row.Visible = visible;
            }
        }

        private async Task LoadOrdersAsync()
        {
            string apiUrl = "http://localhost:5178/api/User/GetUser";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Grid_Users.Rows.Clear();
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        var Users = JsonSerializer.Deserialize<List<User>>(jsonResponse, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                  
                        foreach (var user in Users)
                        {
                            Grid_Users.Rows.Add(user.Name, user.Email, user.InitialDate, user.EndDate);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Erro ao buscar os pedidos: " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private async void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (TxtPainel.Text == "Adicionar")
            {
                await AddUserAsync();
                ClearText();

            } 
           
            await LoadOrdersAsync();
        }
        private void ClearText()
        {
            textEmail.Text = "";
            TxtNameUser.Text = "";
            DateInitial.Value = DateTime.Now;
            EndDate.Value = DateTime.Now;
        }

        private async Task AddUserAsync()
        {
            User user = new User
            {
                Email = textEmail.Text,
                Name = TxtNameUser.Text,
                InitialDate = DateInitial.Value,
                EndDate = EndDate.Value
            };

            string jsonContent = JsonSerializer.Serialize(user);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = await client.PostAsync("http://localhost:5178/api/User/SetUser", content);
                    string responseContent = await response.Content.ReadAsStringAsync();

                    string errorMessages = "";
                    string infoMessages = "";

                    if (response.IsSuccessStatusCode)
                    {
                        var responseObject = JsonSerializer.Deserialize<JsonElement>(responseContent);
                        if (responseObject.TryGetProperty("message", out JsonElement messageElement))
                        {
                            string message = messageElement.GetString();
                            if (message != "usuário validado com sucesso.")
                            {
                                ProcessResponseMessages(responseObject, ref errorMessages, ref infoMessages);
                                UpdateMessageLabel(errorMessages, infoMessages);
                            }
                        }
                      

                        TxtMensagem.Text = "Usuário Criado: " + user.Name;
                        TxtMensagem.Visible = true;
                        Messagem.Visible = true;
                        messageTimer.Start();
                    }
                    else
                    {
                        var responseObject = JsonSerializer.Deserialize<JsonElement>(responseContent);
                        ProcessResponseMessages(responseObject, ref errorMessages, ref infoMessages);

                        UpdateMessageLabel(errorMessages, infoMessages);
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateMessageLabel($"Ocorreu um erro: {ex.Message}", "");
            }

        }
      
        private void ProcessResponseMessages(JsonElement responseObject, ref string errorMessages, ref string infoMessages)
        {
            if (responseObject.TryGetProperty("errors", out JsonElement errors) && errors.GetArrayLength() > 0)
            {
                errorMessages = "Erros:\n" + string.Join("\n", errors.EnumerateArray().Select(e => e.GetString())) + "\n";
            }

            if (responseObject.TryGetProperty("info", out JsonElement info) && info.GetArrayLength() > 0)
            {
                infoMessages = "Informações:\n" + string.Join("\n", info.EnumerateArray().Select(e => e.GetString())) + "\n";
            }
        }

        private void UpdateMessageLabel(string errorMessages, string infoMessages)
        {
 
            TextInfo.Text = $"{errorMessages}\n{infoMessages}";     
            if (!string.IsNullOrEmpty(errorMessages))
            {
               TextInfo.BackColor = Color.Red;
               TextInfo.ForeColor = Color.White;
            }

            else if (!string.IsNullOrEmpty(infoMessages))
            {
                 TextInfo.BackColor = Color.Yellow;
                 TextInfo.ForeColor= Color.Black;
            }
       
            TextInfo.Visible = true;
            messageTimer.Start();
        }

    }
}
