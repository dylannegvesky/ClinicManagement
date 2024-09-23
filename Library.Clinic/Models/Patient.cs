namespace Library.Clinic.Models
{
    public class Patient
    {
        public List<string> MedicalNotes { get; set; } = new List<string>();
        public int Id { get; set; }
        private string? name;
        private string? address;
        private string? ssn;
        private DateTime? birthday;
        public string Name
        {
            get
            {
                return name ?? string.Empty;
            }
            set
            {
                name = value;
            }
        }
        public DateTime Birthday
        {
            get
            {
                return birthday ?? DateTime.MinValue;
            }
            set
            {
                birthday = value;
            }
        }
        public string Address
        {
            get
            {
                return address ?? string.Empty;
            }
            set
            {
                address = value;
            }
        }
        public string SSN
        {
            get
            {
                return ssn ?? string.Empty;
            }
            set
            {
                ssn = value;
            }
        }
        public Patient()
        {
            Name = string.Empty;
            Address = string.Empty;
            Birthday = DateTime.MinValue;
            SSN = string.Empty;
        }
    }
}
