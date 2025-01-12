using Library.Clinic.DTO;
using Library.Clinic.Models;

namespace Library.Clinic.Services
{
    public class AppointmentServiceProxy
    {
        private static object _lock = new object();
        public static AppointmentServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new AppointmentServiceProxy();
                    }
                }
                return instance;
            }
        }
        private static AppointmentServiceProxy? instance;
        private AppointmentServiceProxy()
        {
            instance = null;

            Appointments = new List<Appointment>();
        }
        public int LastKey
        {
            get
            {
                if (Appointments.Any())
                {
                    return Appointments.Select(x => x.Id).Max();
                }
                return 0;
            }
        }
        private List<Appointment> appointments;
        public List<Appointment> Appointments
        {
            get
            {
                return appointments;
            }

            private set
            {
                if (appointments != value)
                {
                    appointments = value;
                }
            }
        }
        //ChatGPT code
        public bool IsDoubleBooked(PhysicianDTO phys, DateTime newStartTime, DateTime newEndTime)
        {
            foreach (var appointment in Appointments.Where(p => p.Physician == phys))
            {
                if (newStartTime < appointment.AppointmentEndTime && newEndTime > appointment.AppointmentStartTime)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsDoubleBooked(PatientDTO pat, DateTime newStartTime, DateTime newEndTime)
        {
            foreach (var appointment in Appointments.Where(p => p.Patient == pat))
            {
                if (newStartTime < appointment.AppointmentEndTime && newEndTime > appointment.AppointmentStartTime)
                {
                    return true;
                }
            }
            return false;
        }
        public void AddOrUpdate(Appointment new_appt)
        {
            bool isAdd = false;
            if (new_appt.Id <= 0)
            {
                new_appt.Id = LastKey + 1;
                isAdd = true;
            }

            if (isAdd)
            {
                if (new_appt.AppointmentStartTime.DayOfWeek != DayOfWeek.Saturday && new_appt.AppointmentStartTime.DayOfWeek != DayOfWeek.Sunday)
                {
                    if (new_appt.AppointmentStartTime.Hour >= 8 && new_appt.AppointmentStartTime.Hour <= 17 && new_appt.AppointmentEndTime.Hour >= 8 && new_appt.AppointmentEndTime.Hour <= 17)
                    {
                        if (!IsDoubleBooked(new_appt.Physician, new_appt.AppointmentStartTime, new_appt.AppointmentEndTime))
                        {
                            if(!IsDoubleBooked(new_appt?.Patient, new_appt.AppointmentStartTime, new_appt.AppointmentEndTime))
                            {
                                Appointments.Add(new_appt);
                            }
                        }
                    }
                }
            }
       
        }
        public void RemoveAppointment(int apptId)
        {
            var apptToRemove = Appointments.FirstOrDefault(p => p.Id == apptId);
            if (apptToRemove != null)
            {
                Appointments.Remove(apptToRemove);
            }

        }
    }   
}
