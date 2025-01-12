using Library.Clinic.DTO;
using Library.Clinic.Services;
using System.Xml.Linq;

namespace Library.Clinic.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        private DateTime? appointment_start_time;
        private DateTime? appointment_end_time;
        private int? patient_id;
        private int? physician_id;
        private PatientDTO? patient;
        private PhysicianDTO? physician;
        public override string ToString()
        {
            return $"[{Id}] Physician: {physician_id} \nStart Time: {AppointmentStartTime} \nEnd Time: {AppointmentEndTime}";
        }
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
        public int PatientId
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
        public PatientDTO? Patient { get; set; }
        public PhysicianDTO? Physician { get; set; }
        public int PhysicianId
        {
            get
            {
                return physician_id ?? 0;
            }
            set
            {
                if (value >= 1)
                    physician_id = value;
            }
        }
        public Treatment? AppointmentReason { get; set; }
        public double AppointmentCost 
        {
            get
            {
                if (AppointmentReason != null && Patient != null)
                {
                    return CalculatePrice(AppointmentReason, Patient?.InsurancePlan ?? new Insurance());
                }
                return -1;
            }
            set
            {
                AppointmentCost = value;
            }
        }
        public double CalculatePrice(Treatment treatment, Insurance plan)
        {
            var treatmentPrice = treatment.Price * (1 - plan.CoveragePercentage);
            treatmentPrice = Math.Round(treatmentPrice, 2);
            return treatmentPrice;
        }
        public Appointment()
        {
            appointment_start_time = DateTime.MinValue;
            appointment_end_time = DateTime.MinValue;
        }


    }
}
