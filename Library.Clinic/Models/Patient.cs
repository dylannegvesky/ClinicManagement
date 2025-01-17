﻿using Library.Clinic.DTO;

namespace Library.Clinic.Models
{
    public class Patient
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
        public List<string> MedicalNotes { get; set; } = new List<string>();
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Birthday { get; set; }
        public string? Address { get; set; }
        public string? SSN { get; set; }
        public Insurance? InsurancePlan { get; set; }
        public Patient()
        {
            Name = string.Empty;
            Address = string.Empty;
            Birthday = DateTime.MinValue;
            SSN = string.Empty;
        }

        public Patient(PatientDTO p)
        {
            Id = p.Id;
            Name = p.Name;
            Address = p.Address;
            Birthday = p.Birthday;
            SSN = p.SSN;
            InsurancePlan = p.InsurancePlan;
        }
    }
}
