using OrderManagerAPI.Models;

namespace OrderManagerAPI.Models
{
    public class Production
    {
        public string Email { get; set; }
        public string Order { get; set; }
        public string ProductionDate { get; set; }  
        public string ProductionTime { get; set; }
        public double Quantity { get; set; }
        public string materialCode { get; set; }
        public double CycleTime { get; set; }

    }
}
