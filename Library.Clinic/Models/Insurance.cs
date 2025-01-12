using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library.Clinic.Models
{
    public class Insurance
    {
        public override string ToString()
        {
            return PlanName ?? String.Empty; // Display the Name in the Picker
        }
        public static List<Insurance> InsurancePlans { get; } = new List<Insurance>() 
        { 
            new Insurance() { PlanName = "Blue Cross Blue Shield" , CoveragePercentage = 0.60}, 
            new Insurance() { PlanName = "Cigna" , CoveragePercentage = 0.70}, 
            new Insurance() { PlanName = "United Healthcare" , CoveragePercentage = 0.90} 
        };
        public string? PlanName { get; set; }
        public double CoveragePercentage { get; set; }
    }
}
