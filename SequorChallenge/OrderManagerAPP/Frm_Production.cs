using OrderManagerAPI.Models;
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
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace OrderManagerAPP
{
    public partial class Frm_Production : Base
    {
        private bool painelAberto = false;
        private Timer messageTimer;
        private Stopwatch stopwatch = new Stopwatch();

        private long IDProduction = 0;
        private string EmailSelect = null;
        private string OrderSelect = null;
        private string Dateselect = null;
        private string Hourselect = null;
        private string QuantitySelect = null;
        private int CycleTime = 0;
        private string MaterialSelect = null;

        public Frm_Production()
        {
            InitializeComponent();
            
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;

            messageTimer = new Timer();
            messageTimer.Interval = 5000;
            messageTimer.Tick += MessageTimer_Tick;

            tableLayoutPanel1.AutoScroll = true; 
            tableLayoutPanel1.HorizontalScroll.Enabled = false; 
            tableLayoutPanel1.HorizontalScroll.Visible = false; 
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


            TextBox[] textBoxes = { TxtOrdem, textEmail, TxtQuant };


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
            SelectTxt(TxtOrdem);
        }
        private void textEmail_MouseClick(object sender, MouseEventArgs e)
        {
            SelectTxt(textEmail);
        }
        private void TxtQuant_MouseClick(object sender, MouseEventArgs e)
        {
            SelectTxt(TxtQuant);
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

        public async void BtnAdd_Click(object sender, EventArgs e)
        {
            stopwatch.Reset();
            stopwatch.Start();
            ClearText();

            if(OrderSelect != null)
            {
                TxtOrdem.Text = OrderSelect;
            }

            AbrirPainel();
            TitlePainel = "Adicionar";
            await LoadMaterialAsync();

        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            stopwatch.Reset(); 
            stopwatch.Start();


            AbrirPainel();
            await LoadMaterialAsync();
            TitlePainel = "Editar";
            textEmail.Text = EmailSelect;
            TxtOrdem.Text = OrderSelect;       
            TxtQuant.Text = QuantitySelect;

            DateTime dateTime = DateTime.Parse(Dateselect);
            DateTime DateHour = DateTime.Parse(Hourselect);
            
            Date.Text = dateTime.ToShortDateString(); 
            Hour.Text = DateHour.ToShortTimeString(); 


            for (int i = 0; i < ListMaterial.Items.Count; i++)
            {            
                if (ListMaterial.Items[i].ToString() == MaterialSelect)
                {
                    ListMaterial.SetItemChecked(i, true);
                    break;
                }
            }
        }

        private void Grid_Users_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                DataGridViewRow row = Grid_Users.Rows[e.RowIndex];

                IDProduction = Convert.ToInt64(row.Cells["ID"].Value);
                EmailSelect = row.Cells["Email"].Value?.ToString();
                OrderSelect = row.Cells["Order"].Value?.ToString();
                QuantitySelect = row.Cells["Quantity"].Value?.ToString();
                MaterialSelect = row.Cells["MaterialCode"].Value?.ToString();

                string datePart = row.Cells["DateProduct"].Value?.ToString();
                string hourPart = row.Cells["TableHour"].Value?.ToString();

                if (DateTime.TryParse($"{datePart} {hourPart}", out DateTime fullDateTime))
                {
                    Dateselect = fullDateTime.ToString("yyyy-MM-dd");
                    Hourselect = fullDateTime.ToString("HH:mm:ss.fff");
                }

                if (!string.IsNullOrEmpty(EmailSelect))
                {
                    ReadyButtons();
                }
                else
                {
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
        }

        private void ReadyButtons()
        {
            btnEdit.Enabled = true;
            btnEdit.BackColor = Color.FromArgb(83, 126, 235);
            btnEdit.Cursor = Cursors.Hand;

            btnDelete.Enabled = true;
            btnDelete.BackColor = Color.FromArgb(83, 126, 235);
            btnDelete.Cursor = Cursors.Hand;

            TxtMensagem.Text = "Usuário Selecionado: " + OrderSelect;
            TxtMensagem.Visible = true;
            Messagem.Visible = true;
            messageTimer.Start();
        }
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchValue = TxtSearch.Text.Trim().ToLower();

            // Verifica se o campo está vazio ou contém apenas "pesquisar"
            if (string.IsNullOrEmpty(searchValue) || searchValue == "pesquisar")
            {
                // Torna todas as linhas visíveis
                foreach (DataGridViewRow row in Grid_Users.Rows)
                {
                    if (!row.IsNewRow) // Ignorar a nova linha não confirmada
                        row.Visible = true;
                }
                return; // Finaliza o método para evitar aplicar o filtro
            }

            // Aplica o filtro caso contrário
            foreach (DataGridViewRow row in Grid_Users.Rows)
            {
                if (row.IsNewRow)
                    continue;

                // Verifica se alguma célula contém o texto buscado
                bool visible = row.Cells["ID"].Value?.ToString().ToLower().Contains(searchValue) == true ||
                               row.Cells["Email"].Value?.ToString().ToLower().Contains(searchValue) == true ||
                               row.Cells["Order"].Value?.ToString().ToLower().Contains(searchValue) == true ||
                               row.Cells["DateProduct"].Value?.ToString().ToLower().Contains(searchValue) == true ||
                               row.Cells["TableHour"].Value?.ToString().ToLower().Contains(searchValue) == true ||                              
                               row.Cells["Quantity"].Value?.ToString().ToLower().Contains(searchValue) == true ||
                               row.Cells["MaterialCode"].Value?.ToString().ToLower().Contains(searchValue) == true;

                // Define a visibilidade da linha com base na busca
                row.Visible = visible;
            }
        }



        private async Task LoadOrdersAsync()
        {
            string email = "user1@example.com";
            string apiUrl = $"http://localhost:5178/api/Production/GetProduction?email={email}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Grid_Users.Rows.Clear();
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        var productions = JsonSerializer.Deserialize<List<Production>>(jsonResponse, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                  
                        foreach (var product in productions)
                        {
                            Grid_Users.Rows.Add(product.Id, product.Email, product.Order, product.ProductionDate, product.ProductionTime,product.Quantity, product.materialCode, product.CycleTime);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Erro ao buscar os produtos: " + response.ReasonPhrase);
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
            CycleTime = 0;

            if (TxtPainel.Text == "Adicionar")
            {              
                await AddUserAsync();
                ClearText();

            } else if (TxtPainel.Text == "Editar")
            {
                await EditUserAsync();
            }

            CycleTime = 0;
            await LoadOrdersAsync();
        }
        private void ClearText()
        {
            TxtOrdem.Text = "";
            TxtQuant.Text = "";
            textEmail.Text = "";
            TxtOrdem.Text = "";

            Date.Value = DateTime.Now;
            Hour.Value = DateTime.Now;
        }

        private async Task AddUserAsync()
        {

            if (!int.TryParse(TxtQuant.Text, out int quantity))
            {
                MessageBox.Show("Por favor, insira um valor numérico válido para o ciclo de tempo.", "Valor Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CycleTime = (int)stopwatch.Elapsed.TotalSeconds;

            string userDate = Date.Text; 
            string userTime = Hour.Text;


            Production production = new Production();

            if (DateTime.TryParse(userDate, out DateTime parsedDate) &&
                DateTime.TryParse(userTime, out DateTime parsedTime))
            {
                // Combina a data e a hora em um único DateTime
                DateTime parsedDateTime = new DateTime(
                    parsedDate.Year,
                    parsedDate.Month,
                    parsedDate.Day,
                    parsedTime.Hour,
                    parsedTime.Minute,
                    parsedTime.Second,
                    parsedTime.Millisecond
                );

                production = new Production()
                {
                    Id = 0,
                    Email = textEmail.Text,
                    Order = TxtOrdem.Text,
                    ProductionDate = parsedDateTime.ToString("yyyy-MM-dd"),
                    ProductionTime = parsedDateTime.ToString("HH:mm:ss.fff"),
                    Quantity = quantity,
                    materialCode = MaterialSelect,
                    CycleTime = CycleTime,
                };
            }
            else
            {             
                  MessageBox.Show("Data ou hora inválida. Verifique os valores inseridos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            string jsonContent = JsonSerializer.Serialize(production);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = await client.PostAsync("http://localhost:5178/api/Production/SetProduction", content);
                    string responseContent = await response.Content.ReadAsStringAsync();

                    string errorMessages = "";
                    string infoMessages = "";

                    if (response.IsSuccessStatusCode)
                    {
                        var responseObject = JsonSerializer.Deserialize<JsonElement>(responseContent);
                        if (responseObject.TryGetProperty("message", out JsonElement messageElement))
                        {
                            string message = messageElement.GetString();
                            if (message != "Produção validada com sucesso.")
                            {
                                ProcessResponseMessages(responseObject, ref errorMessages, ref infoMessages);
                                UpdateMessageLabel(errorMessages, infoMessages);
                            }
                        }
                      

                        TxtMensagem.Text = "Produção Criado para a O.S: " + TxtOrder.Text;
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

        private async Task EditUserAsync()
        {
            if (!int.TryParse(TxtQuant.Text, out int quantity))
            {
                MessageBox.Show("Por favor, insira um valor numérico válido para o ciclo de tempo.", "Valor Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CycleTime = (int)stopwatch.Elapsed.TotalSeconds;         

            string userDate = Date.Text;
            string userTime = Hour.Text;      
            Production production = new Production();

            if (DateTime.TryParse(userDate, out DateTime parsedDate) &&
                DateTime.TryParse(userTime, out DateTime parsedTime))
            {
                DateTime parsedDateTime = new DateTime(
                    parsedDate.Year,
                    parsedDate.Month,
                    parsedDate.Day,
                    parsedTime.Hour,
                    parsedTime.Minute,
                    parsedTime.Second,
                    parsedTime.Millisecond
                );

                production = new Production()
                {
                    Id = IDProduction,
                    Email = textEmail.Text,
                    Order = TxtOrdem.Text,
                    ProductionDate = parsedDateTime.ToString("yyyy-MM-dd"),
                    ProductionTime = parsedDateTime.ToString("HH:mm:ss.fff"),
                    Quantity = quantity,
                    materialCode = MaterialSelect,
                    CycleTime = CycleTime,
                };
            }
            else
            {
                MessageBox.Show("Data ou hora inválida. Verifique os valores inseridos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            string jsonContent = JsonSerializer.Serialize(production);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PutAsync("http://localhost:5178/api/Production/UpdateProduction", content);
                    string responseContent = await response.Content.ReadAsStringAsync();

                    string errorMessages = "";
                    string infoMessages = "";

                    if (response.IsSuccessStatusCode)
                    {
                        var responseObject = JsonSerializer.Deserialize<JsonElement>(responseContent);
                        if (responseObject.TryGetProperty("message", out JsonElement messageElement))
                        {
                            string message = messageElement.GetString();
                            if (message != "Produção alterada com sucesso!" && message != "Produção validada com sucesso.")
                            {
                                ProcessResponseMessages(responseObject, ref errorMessages, ref infoMessages);
                                UpdateMessageLabel(errorMessages, infoMessages);
                            }
                        }

                        TxtMensagem.Text = "Produção Atualizada O.S:" + TxtOrdem.Text;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ModalCancel(true);
            textDelInfo.Text = OrderSelect;

        }

        private void bntCancelDel_Click(object sender, EventArgs e)
        {
            ModalCancel(false);
        }

        private void ModalCancel(bool Active)
        {
            pnlDel.Visible = Active;
            pictureDel.Visible = Active;
            textDel.Visible = Active;
            textDelInfo.Visible = Active;
            bntCancelDel.Visible = Active;
            bntConfirmDel.Visible = Active;
        }
        private async void bntConfirmDel_Click(object sender, EventArgs e)
        {

            string apiUrl = $"http://localhost:5178/api/Production/Delete/{IDProduction}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        TxtMensagem.Text = "Produção deletada com sucesso: " + IDProduction;
                        TxtMensagem.Visible = true;
                        Messagem.Visible = true;
                        messageTimer.Start();                    
                    }
                    else
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Erro ao deletar a produção: {responseContent}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }

            await LoadOrdersAsync();

            ModalCancel(false);
        }

        private void ListMaterial_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
             
                for (int i = 0; i < ListMaterial.Items.Count; i++)
                {
                    if (i != e.Index) 
                    {
                        ListMaterial.SetItemChecked(i, false);
                    }
                }
            }
        }

        private async Task LoadMaterialAsync()
        {
            string apiUrl = "http://localhost:5178/api/Material/GetMaterial";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // Supondo que a resposta seja uma lista de objetos com "materialCode"
                        var materials = JsonSerializer.Deserialize<List<Material>>(jsonResponse, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        ListMaterial.Items.Clear();

                        foreach (var material in materials)
                        {
                            ListMaterial.Items.Add(material.MaterialCode);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Erro ao carregar materiais: " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void ListMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ListMaterial.CheckedItems.Count > 0)
            {
                var selectedItem = ListMaterial.CheckedItems[0];
                MaterialSelect = selectedItem.ToString();

                TxtMensagem.Text = "Material selecionado: " + MaterialSelect;
                TxtMensagem.Visible = true;
                Messagem.Visible = true;
                messageTimer.Start();

            }
             
        }
        public void SimulateBtnAddClick()
        {

            if (BtnAdd.InvokeRequired)
            {
                BtnAdd.Invoke(new Action(SimulateBtnAddClick)); 
            }
            else
            {
                BtnAdd.PerformClick();  
            }
        }

        public void SetOrder(string order)
        {
            if (TxtOrdem.InvokeRequired)
            {
                TxtOrdem.Invoke(new Action(() => TxtOrdem.Text = order));
            }
            else
            {
                OrderSelect = order;
            }
        }

    }
}
