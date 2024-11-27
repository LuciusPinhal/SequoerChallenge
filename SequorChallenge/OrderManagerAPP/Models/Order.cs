using System.Collections.Generic;

namespace OrderManagerAPP.Models
{
    public class Order
    {
        public string OS { get; set; }
        public double Quantity { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public string Image { get; set; }
        public double CycleTime { get; set; }
        public List<Material> Materials { get; set; }

    }
}