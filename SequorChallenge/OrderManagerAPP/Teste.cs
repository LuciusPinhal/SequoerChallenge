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
using OrderManagerAPP.Models;

namespace OrderManagerAPP
{
    public partial class Teste : Form
    {
        public Teste()
        {
            InitializeComponent();
        }

        private void listBoxOrders_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private async Task LoadOrdersAsync()
        {
            string apiUrl = "http://localhost:5178/api/Order/GetOrders";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // Supondo que a API retorne uma lista de objetos com "OS", "Quantity", etc.
                        var orders = JsonSerializer.Deserialize<List<Order>>(jsonResponse, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        // Limpa as linhas do DataGridView antes de adicionar novos dados
                        dataGridViewOrders.Rows.Clear();

                        // Adiciona as linhas ao DataGridView
                        foreach (var order in orders)
                        {
                            dataGridViewOrders.Rows.Add(order.OS, order.Quantity, order.ProductCode, order.ProductDescription);
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


   

        private async void Teste_Load_1(object sender, EventArgs e)
        {
            await LoadOrdersAsync();
        }

        private void dataGridViewOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
