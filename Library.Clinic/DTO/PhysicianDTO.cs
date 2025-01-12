using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.DTO
{
    public class PhysicianDTO
    {
        public override string ToString()
        {
            return Display;
        }
        public string Display
        {
            get
            {
                return $"[{Id}] {Name}";
            }
        }
        public List<string> Specializations { get; set; } = new List<string>();
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Graduation { get; set; }
        public string? License { get; set; }
        public PhysicianDTO()
        {
            Name = string.Empty;
            License = string.Empty;
            Graduation = DateTime.MinValue;
        }
        public PhysicianDTO(Physician p)
        {
            Id = p.Id;
            Name = p.Name;
            Graduation = p.Graduation;
            License = p.License;
        }
    }
}
