namespace Library.Clinic.Models
{
    public class Appointment
    {
        private DateTime? appointment_start_time;
        private DateTime? appointment_end_time;
        private int? patient_id;
        public DateTime AppointmentStartTime
        {
            get
            {
                return appointment_start_time ?? DateTime.MinValue;
            }
            set
            {
                appointment_start_time = value;
            }
        }
        public DateTime AppointmentEndTime
        {
            get
            {
                return appointment_end_time ?? DateTime.MinValue;
            }
            set
            {
                appointment_end_time = value;
            }
        }
        public int PatientID
        {
            get
            {
                return patient_id ?? 0;
            }
            set
            {
                if (value >= 1)
                    patient_id = value;
            }
        }
        public Appointment(DateTime startTime, DateTime endTime)
        {
            appointment_start_time = startTime;
            appointment_end_time = endTime;
        }


    }
}
