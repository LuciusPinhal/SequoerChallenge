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
    public partial class Frm_Product : Base
    {
        private bool painelAberto = false;
        private Timer messageTimer;


        private string VarProductCode = null;
        private string VarProductDescription = null;
        private string VarImage = null;
        private string VarCycleTime = null;
        private List<Material> material = new List<Material>();

        public Frm_Product()
        {
            InitializeComponent();

            btnEdit.Enabled = false;
            btnDelete.Enabled = false;

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
            Messagem.Visible = false;
            messageTimer.Stop();
        }

        public void SelectTxt(TextBox SeletTxt)
        {
            Color defaultColor = Color.White;
            Color selectedColor = Color.FromArgb(185, 205, 255);


            TextBox[] textBoxes = { TxtDescriptionL, textCycleTime };


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

        private void TxtDescriptionL_MouseClick(object sender, MouseEventArgs e)
        {
            SelectTxt(TxtDescriptionL);
        }
        private void textCycleTime_MouseClick(object sender, MouseEventArgs e)
        {
            SelectTxt(textCycleTime);
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
            await LoadMaterialAsync();
            AbrirPainel();
            TitlePainel = "Adicionar";
          

        }
        private async void btnEdit_Click(object sender, EventArgs e)
        {
            AbrirPainel();
            await LoadMaterialAsync();
            TitlePainel = "Editar "+ VarProductCode;
            TxtDescriptionL.Text = VarProductDescription;
            textCycleTime.Text = VarCycleTime;

            if (!string.IsNullOrEmpty(VarImage))
            {
                PicProduct.ImageLocation = VarImage;
            }
            else
            {
                PicProduct.Image = null;
            }


        

        }

        private void Grid_Users_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                DataGridViewRow row = Grid_Users.Rows[e.RowIndex];

                VarProductCode = row.Cells["ProductCode"].Value?.ToString();
                VarProductDescription = row.Cells["Description"].Value?.ToString();
                VarImage = row.Cells["Image"].Value?.ToString();
                VarCycleTime = row.Cells["CycleTime"].Value?.ToString();

                if (!string.IsNullOrEmpty(VarProductCode))
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

            TxtMensagem.Text = "Material Selecionado: " + VarProductCode;
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
                               row.Cells["Description"].Value?.ToString().ToLower().Contains(searchValue) == true ||
                               row.Cells["Image"].Value?.ToString().ToLower().Contains(searchValue) == true ||
                               row.Cells["CycleTime"].Value?.ToString().ToLower().Contains(searchValue) == true;

                row.Visible = visible;
            }
        }

        private async Task LoadOrdersAsync()
        {
            string apiUrl = "http://localhost:5178/api/Product/GetProduct";

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
                            Grid_Users.Rows.Add(product.ProductCode, product.ProductDescription, product.Image, product.CycleTime);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Erro ao buscar os Produtos: " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
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

                        ListProductCheck.Items.Clear();

                        foreach (var material in materials)
                        {
                            ListProductCheck.Items.Add(material.MaterialCode);
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
            material = new List<Material>();
            UncheckAllItems();
            await LoadOrdersAsync();
        }
        private void ClearText()
        {
            TxtDescriptionL.Text = "";
            textCycleTime.Text = "";
            PicProduct.Image = PicProduct.ErrorImage;
        }

        private async Task AddUserAsync()
        {
            if (!int.TryParse(textCycleTime.Text, out int cycleTime))
            {
                MessageBox.Show("Por favor, insira um valor numérico válido para o ciclo de tempo.", "Valor Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            if (VarImage == null && VarImage == "")
            {
                MessageBox.Show("Por favor, insira uma imagem.", "Erro de Imagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtDescriptionL.Text))
            {
                MessageBox.Show("Por favor, insira uma Descrição.", "Erro de Descrição", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (material == null || material.Count == 0)
            {
                material = new List<Material>();
               
            }
      
            Order order = new Order
            {
                OS = "",
                Quantity = 0,
                ProductCode = "",
                ProductDescription = TxtDescriptionL.Text,
                Image = VarImage,
                CycleTime = cycleTime,
                Materials = new List<Material>(material)
            };

            string jsonContent = JsonSerializer.Serialize(order);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = await client.PostAsync("http://localhost:5178/api/Product/SetProduct", content);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                    
                        TxtMensagem.Text = "Produto Criado: " + TxtDescriptionL.Text;
                        TxtMensagem.Visible = true;
                        Messagem.Visible = true;
                        messageTimer.Start();
                    }
                    else
                    {                    
                        string errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Erro ao adicionar materiais: {errorContent}");
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
            if (!int.TryParse(textCycleTime.Text, out int cycleTime))
            {
                MessageBox.Show("Por favor, insira um valor numérico válido para o ciclo de tempo.", "Valor Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (VarImage == null && VarImage == "")
            {
                MessageBox.Show("Por favor, insira uma imagem.", "Erro de Imagem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtDescriptionL.Text))
            {
                MessageBox.Show("Por favor, insira uma Descrição.", "Erro de Descrição", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (material == null || material.Count == 0)
            {
                material = new List<Material>();
            }

            Order order = new Order
            {
                OS = "",
                Quantity = 0,
                ProductCode = VarProductCode,
                ProductDescription = TxtDescriptionL.Text,
                Image = VarImage,
                CycleTime = cycleTime,
                Materials = new List<Material>(material)
            };

            string jsonContent = JsonSerializer.Serialize(order);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PutAsync("http://localhost:5178/api/Product/UpdateProduct", content);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();

                        TxtMensagem.Text = "Produto Atualizado: " + TxtDescriptionL.Text;
                        TxtMensagem.Visible = true;
                        Messagem.Visible = true;
                        messageTimer.Start();
                    }
                    else
                    {
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
            textDelInfo.Text = VarProductDescription;

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

            string apiUrl = $"http://localhost:5178/api/Product/Delete/{VarProductCode}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        TxtMensagem.Text = "Produto deletado com sucesso: " + VarProductDescription;
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

        private void ListProductCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSelectedMaterial();
        }

        private void GetSelectedMaterial()
        {
            foreach (var item in ListProductCheck.CheckedItems)
            {
                string code = item.ToString();        
                bool MaterialExists = material.Any(o => o.MaterialCode == code);
                
                if (!MaterialExists)
                {
                    Material newMaterial = new Material()
                    {
                        MaterialCode= code,
                        MaterialDescription = "",
                    };

                    material.Add(newMaterial);
                }
            }
        }

        private void UncheckAllItems()
        {
            for (int i = 0; i < ListProductCheck.Items.Count; i++)
            {
                ListProductCheck.SetItemChecked(i, false); 
            }
        }

        private void picOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*"; 
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                PicProduct.ImageLocation = openFileDialog.FileName; 
                VarImage = openFileDialog.FileName;
                PicProduct.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void PicProduct_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                PicProduct.ImageLocation = openFileDialog.FileName;
                VarImage = openFileDialog.FileName;
                PicProduct.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void txtProductRelated_Click(object sender, EventArgs e)
        {

        }
    }
}
