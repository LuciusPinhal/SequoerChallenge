using OrderManagerAPI.Models;
using OrderManagerAPP.Models;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace OrderManagerAPP
{
    public partial class Frm_Order : Base
    {
        private bool painelAberto = false;
        private Timer messageTimer;


        private string VarProductCode = null;
        private string VarQuantity = null;
        private string VarOrder = null;

        private string VarProductSelect = null;

        private Frm_Main frmMain;
        public Frm_Order(Frm_Main mainForm)
        {
            InitializeComponent();
            frmMain = mainForm;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;

            messageTimer = new Timer();
            messageTimer.Interval = 5000;
            messageTimer.Tick += MessageTimer_Tick;
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            frmMain.NavigateToProduction();

            var productionForm = frmMain.GetActiveForm<Frm_Production>();
            if (productionForm != null)
            {
                string order = VarOrder;
                productionForm.SetOrder(order);
            }
        }

        public string TitlePainel
        {
            get => TxtPainel.Text;
            set => TxtPainel.Text = value;
        }

        private void MessageTimer_Tick(object sender, EventArgs e)
        {
            TxtMensagem.Visible = false;           
            Messagem.Visible = false;
            messageTimer.Stop();
        }

        public void SelectTxt(TextBox SeletTxt)
        {
            Color defaultColor = Color.White;
            Color selectedColor = Color.FromArgb(185, 205, 255);


            TextBox[] textBoxes = { TxtQuantity };


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

        private void TxtQuantity_MouseClick(object sender, MouseEventArgs e)
        {
            SelectTxt(TxtQuantity);
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

        private async void BtnAdd_Click(object sender, EventArgs e)
        {
            ClearText();
            await LoadProductAsync();
            AbrirPainel();
            TitlePainel = "Adicionar";
            ListProductCheck.Visible = true;
   

        }
        private async void btnEdit_Click(object sender, EventArgs e)
        {
        
            AbrirPainel();
            await LoadProductAsync();
            TitlePainel = "Editar "+ VarOrder;
            TxtQuantity.Text = VarQuantity;

     
            for (int i = 0; i < ListProductCheck.Items.Count; i++)
            {
                if (ListProductCheck.Items[i].ToString() == VarProductSelect)
                {
                    ListProductCheck.SetItemChecked(i, true);
                    break;
                }
            }
        }

        private void Grid_Users_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                DataGridViewRow row = Grid_Users.Rows[e.RowIndex];

                VarProductCode = row.Cells["ProductCode"].Value?.ToString();
                VarOrder = row.Cells["Order"].Value?.ToString();
                VarQuantity = row.Cells["Quantity"].Value?.ToString();
                VarProductSelect = row.Cells["ProductCode"].Value?.ToString();

                if (!string.IsNullOrEmpty(VarProductCode))
                {
                    ReadyButtons();
                }
                else
                {
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnProduct.Enabled = false;
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

            btnProduct.Enabled = true;
            btnProduct.BackColor = Color.FromArgb(83, 126, 235);
            btnProduct.Cursor = Cursors.Hand;

            TxtMensagem.Text = "Order Selecionada: " + VarProductCode;
            TxtMensagem.Visible = true;
            Messagem.Visible = true;
            messageTimer.Start();

            
        }
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

                bool visible = row.Cells["ProductCode"].Value?.ToString().ToLower().Contains(searchValue) == true ||
                               row.Cells["Order"].Value?.ToString().ToLower().Contains(searchValue) == true ||
                               row.Cells["Quantity"].Value?.ToString().ToLower().Contains(searchValue) == true ||
                               row.Cells["ProductCode"].Value?.ToString().ToLower().Contains(searchValue) == true;

                row.Visible = visible;
            }
        }

        private async Task LoadOrdersAsync()
        {
            string apiUrl = "http://localhost:5178/api/Order/GetOrder";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Grid_Users.Rows.Clear();
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        var Products = JsonSerializer.Deserialize<List<Order>>(jsonResponse, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                  
                        foreach (var product in Products)
                        {
                            Grid_Users.Rows.Add(product.OS, product.Quantity, product.ProductCode);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Erro ao buscar as Ordens: " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private async Task LoadProductAsync()
        {
            string apiUrl = "http://localhost:5178/api/Product/GetProduct";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // Supondo que a resposta seja uma lista de objetos com "materialCode"
                        var Products = JsonSerializer.Deserialize<List<Order>>(jsonResponse, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        ListProductCheck.Items.Clear();

                        foreach (var product in Products)
                        {
                            ListProductCheck.Items.Add(product.ProductCode);
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

        private async void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (TxtPainel.Text == "Adicionar")
            {
               await AddUserAsync();
               ClearText();

            } else if (TxtPainel.Text.Contains("Editar"))
            {
                await EditUserAsync();
            }

            //fechar modal
            //Timer.Start();
           // material = new List<Material>();
            UncheckAllItems();
            await LoadOrdersAsync();
        }
        private void ClearText()
        {
            TxtQuantity.Text = "";
        }

        private async Task AddUserAsync()
        {

            if (!int.TryParse(TxtQuantity.Text, out int Qnt))
            {
                MessageBox.Show("Por favor, insira um valor numérico válido para o ciclo de tempo.", "Valor Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            Order order = new Order
            {
                OS = "",
                Quantity = Qnt,
                ProductCode = VarProductCode,
                ProductDescription = "",
                Image = "",
                CycleTime = 00,
                Materials = new List<Material>()
            };

            string jsonContent = JsonSerializer.Serialize(order);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = await client.PostAsync("http://localhost:5178/api/Order/SetOrder", content);
                    if (response.IsSuccessStatusCode)
                    {
                        // Caso a resposta seja bem-sucedida, pode tratar a resposta aqui
                        string responseContent = await response.Content.ReadAsStringAsync();

                        TxtMensagem.Text = "Ordem Criada ";
                        TxtMensagem.Visible = true;
                        Messagem.Visible = true;
                        messageTimer.Start();
                    }
                    else
                    {
                        // Caso a resposta não seja bem-sucedida
                        string errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Erro ao criar ordem: {errorContent}");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}", "");
            }
        }

        private async Task EditUserAsync()
        {
            if (!int.TryParse(TxtQuantity.Text, out int Qnt))
            {
                MessageBox.Show("Por favor, insira um valor numérico válido para o ciclo de tempo.", "Valor Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Order order = new Order
            {
                OS = VarOrder,
                Quantity = Qnt,
                ProductCode = VarProductCode,
                ProductDescription = "",
                Image = "",
                CycleTime = 00,
                Materials = new List<Material>()
            };

            string jsonContent = JsonSerializer.Serialize(order);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PutAsync("http://localhost:5178/api/Order/UpdateOrder", content);
                    if (response.IsSuccessStatusCode)
                    {
                        // Caso a resposta seja bem-sucedida, pode tratar a resposta aqui
                        string responseContent = await response.Content.ReadAsStringAsync();

                        TxtMensagem.Text = "Produto Atualizado: " + VarProductCode;
                        TxtMensagem.Visible = true;
                        Messagem.Visible = true;
                        messageTimer.Start();
                    }
                    else
                    {
                        // Caso a resposta não seja bem-sucedida
                        string errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Erro ao Editar Produto: {errorContent}");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}", "");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ModalCancel(true);
            textDelInfo.Text = VarOrder;

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

            string apiUrl = $"http://localhost:5178/api/Order/Delete/{VarOrder}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        TxtMensagem.Text = "Order deletada com sucesso: " + VarOrder;
                        TxtMensagem.Visible = true;
                        Messagem.Visible = true;
                        messageTimer.Start();
                    }
                    else
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Erro ao deletar o usuário: {responseContent}");
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

        private void UncheckAllItems()
        {
            for (int i = 0; i < ListProductCheck.Items.Count; i++)
            {
                ListProductCheck.SetItemChecked(i, false); 
            }
        }

        private void ListProductCheck_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {

                for (int i = 0; i < ListProductCheck.Items.Count; i++)
                {
                    if (i != e.Index)
                    {
                        ListProductCheck.SetItemChecked(i, false);
                    }
                }
            }
        }

        private void ListProductCheck_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (ListProductCheck.CheckedItems.Count > 0)
            {
                var selectedItem = ListProductCheck.CheckedItems[0];
                VarProductCode = selectedItem.ToString();

                TxtMensagem.Text = "Material selecionado: " + VarProductCode;
                TxtMensagem.Visible = true;
                Messagem.Visible = true;
                messageTimer.Start();

            }
        }
     
    }
}
