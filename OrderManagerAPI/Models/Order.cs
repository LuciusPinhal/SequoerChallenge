namespace OrderManagerAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double Quantity { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public string Imagem { get; set; }
        public double CycleTime { get; set; }
        public List<Material> Materials { get; set; }

    }
}