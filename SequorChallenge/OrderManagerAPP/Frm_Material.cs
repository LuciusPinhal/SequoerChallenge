﻿using OrderManagerAPI.Models;
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
    public partial class Frm_Material : Base
    {
        private bool painelAberto = false;
        private Timer messageTimer;


        private string VarCodeMaterial = null;
        private string VarDescription = null;
        private List<Order> order = new List<Order>();
        private List<Order> Listorder = new List<Order>();
        private List<MaterialProduct> ListProductMaterial = new List<MaterialProduct>();

        public Frm_Material()
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
            TextInfo.Visible = false;
            Messagem.Visible = false;
            messageTimer.Stop();
        }

        public void SelectTxt(TextBox SeletTxt)
        {
            Color defaultColor = Color.White;
            Color selectedColor = Color.FromArgb(185, 205, 255);


            TextBox[] textBoxes = { TxtDescriptionL };


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
            SelectTxt(TxtDescriptionL);
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
       

        }
        private async void btnEdit_Click(object sender, EventArgs e)
        {
            AbrirPainel();
            await LoadProductAsync();
            await LoadProductMaterial(VarCodeMaterial);
            TitlePainel = "Editar "+ VarCodeMaterial;
            TxtDescriptionL.Text = VarDescription;          
        }

        private void Grid_Users_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                DataGridViewRow row = Grid_Users.Rows[e.RowIndex];

                VarCodeMaterial = row.Cells["CodeMaterial"].Value?.ToString();
                VarDescription = row.Cells["Description"].Value?.ToString();
            


                if (!string.IsNullOrEmpty(VarCodeMaterial))
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

            TxtMensagem.Text = "Material Selecionado: " + VarCodeMaterial;
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

                bool visible = row.Cells["CodeMaterial"].Value?.ToString().ToLower().Contains(searchValue) == true ||
                               row.Cells["Description"].Value?.ToString().ToLower().Contains(searchValue) == true;

                row.Visible = visible;
            }
        }

        private async Task LoadOrdersAsync()
        {
            string apiUrl = "http://localhost:5178/api/Material/GetMaterial";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Grid_Users.Rows.Clear();
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        var Materials = JsonSerializer.Deserialize<List<Material>>(jsonResponse, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                  
                        foreach (var material in Materials)
                        {
                            Grid_Users.Rows.Add(material.MaterialCode, material.MaterialDescription);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Erro ao buscar os Materiais: " + response.ReasonPhrase);
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

                   
                        var orders = JsonSerializer.Deserialize<List<Order>>(jsonResponse, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        Listorder = orders;

                        ListProductCheck.Items.Clear();

                        foreach (var os in orders)
                        {
                            ListProductCheck.Items.Add(os.ProductDescription);
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

        private async Task LoadProductMaterial(string code)
        {
            // / api / MaterialProduct / GetProductInMaterial

            string apiUrl = $"http://localhost:5178/api/MaterialProduct/GetProductInMaterial?MaterialCode={code}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
             
                        var materialProduct = JsonSerializer.Deserialize<List<MaterialProduct>>(jsonResponse, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        ListProductMaterial = materialProduct;

                        foreach (var order in ListProductMaterial)
                        {           
                            var matchingProduct = Listorder.FirstOrDefault(p => p.ProductCode == order.ProductCode);

                            if (matchingProduct != null)
                            {
                                for (int i = 0; i < ListProductCheck.Items.Count; i++)
                                {                              
                                    if (ListProductCheck.Items[i].ToString() == matchingProduct.ProductDescription)
                                    {                                      
                                        ListProductCheck.SetItemChecked(i, true);
                                    }
                                }

                            }
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

            } else if (TxtPainel.Text.Contains("Editar"))
            {
                await EditUserAsync();
            }

             ClearText();
            //fechar modal
            //Timer.Start();
            order = new List<Order>();
            UncheckAllItems();
            await LoadOrdersAsync();
        }
        private void ClearText()
        {       
            TxtDescriptionL.Text = "";         
        }
        private async Task AddUserAsync()
        {
            if (string.IsNullOrWhiteSpace(TxtDescriptionL.Text))
            {
                MessageBox.Show("Por favor, insira uma Descrição.", "Erro de Descrição", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Material material = new Material
            {
                MaterialCode = "",
                MaterialDescription = TxtDescriptionL.Text
            };

            if (order == null || !order.Any())
            {
                Order newOrder = new Order()
                {
                    OS = "",
                    Quantity = 0,
                    ProductCode = "",
                    ProductDescription = "",
                    Image = "",
                    CycleTime = 0,
                    Materials = new List<Material>()
                };
                newOrder.Materials.Add(material);
                order.Add(newOrder);
            }
            else
            {

                foreach (var existingOrder in order)
                {
                    existingOrder.Materials.Add(material);
                }
            }



            string jsonContent = JsonSerializer.Serialize(order);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = await client.PostAsync("http://localhost:5178/api/Material/SetMaterial", content);
                    if (response.IsSuccessStatusCode)
                    {
                        // Caso a resposta seja bem-sucedida, pode tratar a resposta aqui
                        string responseContent = await response.Content.ReadAsStringAsync();
                    
                        TxtMensagem.Text = "Material Criado: " + TxtDescriptionL.Text;
                        TxtMensagem.Visible = true;
                        Messagem.Visible = true;
                        messageTimer.Start();
                    }
                    else
                    {
                        // Caso a resposta não seja bem-sucedida
                        string errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Erro ao adicionar materiais: {errorContent}");
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
            if (string.IsNullOrWhiteSpace(TxtDescriptionL.Text))
            {
                MessageBox.Show("Por favor, insira uma Descrição.", "Erro de Descrição", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Material material = new Material
            {
                MaterialCode = VarCodeMaterial,
                MaterialDescription = TxtDescriptionL.Text
            };

            if (order == null || !order.Any())
            {
                Order newOrder = new Order()
                {
                    OS = "",
                    Quantity = 0,
                    ProductCode = "",
                    ProductDescription = "",
                    Image = "",
                    CycleTime = 0,
                    Materials = new List<Material>()
                };         
                newOrder.Materials.Add(material);
                order.Add(newOrder);
            }
            else
            {
 
                foreach (var existingOrder in order)
                {
                    existingOrder.Materials.Add(material);
                }
            }


            string jsonContent = JsonSerializer.Serialize(order);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PutAsync("http://localhost:5178/api/Material/UpdateMaterial", content);
                    if (response.IsSuccessStatusCode)
                    {
                        // Caso a resposta seja bem-sucedida, pode tratar a resposta aqui
                        string responseContent = await response.Content.ReadAsStringAsync();

                        TxtMensagem.Text = "Material Atualizado: " + TxtDescriptionL.Text;
                        TxtMensagem.Visible = true;
                        Messagem.Visible = true;
                        messageTimer.Start();
                    }
                    else
                    {
                        // Caso a resposta não seja bem-sucedida
                        string errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Erro ao Editar materiais: {errorContent}");
                    }

                }
            }
            catch (Exception ex)
            {
                UpdateMessageLabel($"Ocorreu um erro: {ex.Message}", "");
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
            textDelInfo.Text = VarDescription;

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

            string apiUrl = $"http://localhost:5178/api/Material/Delete/{VarCodeMaterial}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        TxtMensagem.Text = "Material deletado com sucesso: " + VarDescription;
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
            GetSelectedProduct();
        }

        private void GetSelectedProduct()
        {
            for (int i = 0; i < ListProductCheck.Items.Count; i++)
            {
                string productName = ListProductCheck.Items[i].ToString();
                bool isChecked = ListProductCheck.GetItemChecked(i);
                Order existingOrder = order.FirstOrDefault(o => o.ProductDescription == productName);

                if (isChecked)
                {
                    if (existingOrder == null)
                    {
                        Order orderL = Listorder.FirstOrDefault(o => o.ProductDescription == productName);
                        if (orderL != null)
                        {
                            Order newOrder = new Order()
                            {
                                OS = "",
                                Quantity = 0,
                                ProductCode = orderL.ProductCode,
                                ProductDescription = orderL.ProductDescription,
                                Image = "",
                                CycleTime = 0,
                                Materials = new List<Material>()
                            };
                            order.Add(newOrder);
                        }
                    }
                }
                else
                {
                    if (existingOrder != null)
                    {
                        order.Remove(existingOrder);
                    }
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


    }
}
