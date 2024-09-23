using Library.Clinic.Services;

namespace Library.Clinic.Models
{
    public class Physician
    {
        public List<string> Specializations { get; set; } = new List<string>();
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public int Id { get; set; }
        private string? name;
        private string? license;
        private DateTime? graduation;
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
        public DateTime Graduation
        {
            get
            {
                return graduation ?? DateTime.MinValue;
            }
            set
            {
                graduation = value;
            }
        }
        public string License
        {
            get
            {
                return license ?? string.Empty;
            }
            set
            {
                license = value;
            }
        }
        public Physician()
        {
            Name = string.Empty;
            License = string.Empty;
            Graduation = DateTime.MinValue;
        }
        
        //ChatGPT code
        public bool IsDoubleBooked(DateTime newStartTime, DateTime newEndTime)
        {
            foreach (var appointment in Appointments)
            {
                if (newStartTime < appointment.AppointmentEndTime && newEndTime > appointment.AppointmentStartTime)
                {
                    return true;
                }
            }
            return false;
        }
        public void AddAppointment(int patient_id, Appointment new_appt)
        {
            if (patient_id >= 1)
            {
                if (new_appt.AppointmentStartTime.DayOfWeek != DayOfWeek.Saturday && new_appt.AppointmentStartTime.DayOfWeek != DayOfWeek.Sunday)
                {
                    if (new_appt.AppointmentStartTime.Hour >= 8 && new_appt.AppointmentStartTime.Hour <= 17 && new_appt.AppointmentEndTime.Hour >= 8 && new_appt.AppointmentEndTime.Hour <= 17)
                    {
                        if (!IsDoubleBooked(new_appt.AppointmentStartTime, new_appt.AppointmentEndTime))
                        {
                            Appointments.Add(new_appt);
                            return;
                        }
                    }
                }
            }
            Console.WriteLine("Failed to add appointment, please check all inputs and try again...");
        }
        public void RemoveAppointment(int physicianId, int apptToDelete)
        {
            PhysicianServiceProxy.Physicians[physicianId].Appointments.RemoveAt(apptToDelete);
        }
        public void printAppointmentList()
        {
            int counter = 0;
            Console.WriteLine("Current Appointment List: \n");
            foreach (var appt in Appointments)
            {
                counter++;
                Console.WriteLine($"Appointment {counter}:");
                Console.WriteLine($"Start time: {appt.AppointmentStartTime}");
                Console.WriteLine($"End time: {appt.AppointmentEndTime}\n");
            }
        }
    }
}
