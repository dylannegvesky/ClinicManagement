﻿using Library.Clinic.DTO;
using Library.Clinic.Services;
using System.Net;
using System.Runtime.Intrinsics.X86;

namespace Library.Clinic.Models
{
    public class Physician
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
        public Physician()
        {
            Name = string.Empty;
            License = string.Empty;
            Graduation = DateTime.MinValue;
        }
        public Physician(PhysicianDTO p)
        {
            Id = p.Id;
            Name = p.Name;
            Graduation = p.Graduation;
            License = p.License;
        }
    }
}
