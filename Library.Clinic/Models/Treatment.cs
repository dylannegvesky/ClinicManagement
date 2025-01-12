using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
{
    public class Treatment
    {
        public override string ToString()
        {
            return $"{Name} - Price Without Insurance: {Price}";
        }
        public string? Name { get; set; }
        public double Price { get; set; }

        public static List<Treatment> Treatments { get; set; } = new List<Treatment>()
        {
            new Treatment(){Name = "Consultation", Price = 100 },
            new Treatment(){Name = "X-Ray", Price = 200 },
            new Treatment(){Name = "Surgery", Price = 1000 },
            new Treatment(){Name = "Medication", Price = 50 },
            new Treatment(){Name = "Physiotherapy", Price = 150 },
            new Treatment(){Name = "Blood Test", Price = 80 }
        };
    }
}
