using Library.Clinic.DTO;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Clinic.ViewModels
{
    public class AppointmentViewModel
    {
        public Appointment? Model { get; set; }
        public int Id
        {
            get
            {
                return Model?.Id ?? -1;
            }
            set
            {
                if (Model != null)
                {
                    Model.Id = value;
                }
            }
        }

        public string PatientName
        {
            get
            {
                if (Model != null && Model.PatientId > 0)
                {
                    if (Model.Patient == null)
                    {
                        Model.Patient = PatientServiceProxy.
                            Current.
                            Patients.
                            FirstOrDefault(p => p.Id == Model.PatientId);
                    }
                }
                return Model?.Patient?.Name ?? string.Empty;
            }
        }

        public PatientDTO? SelectedPatient {
            get
            {
                return Model?.Patient;
            }

            set
            {
                var selectedPatient = value;
                if(Model != null)
                {
                    Model.Patient = selectedPatient;
                    Model.PatientId = selectedPatient?.Id ?? 0;
                }
            }
        }
        public Treatment SelectedReason
        {
            get
            {
                return Model?.AppointmentReason ?? Treatment.Treatments[0];
            }
            set
            {
                if (Model != null)
                {
                    Model.AppointmentReason = value;
                }
            }
        }
        public ObservableCollection<Treatment> ApptReasons
        {
            get
            {
                return new ObservableCollection<Treatment>(Treatment.Treatments);
            }
        }
        public double TreatmentCost
        {
            get
            {
                if(Model != null && Model.AppointmentReason != null)
                {
                    return Model.AppointmentCost;
                }
                return 0;
            }
            set
            {
                if (Model != null)
                {
                    Model.AppointmentCost = value;
                }
            }
        }

        public ObservableCollection<PatientDTO> Patients { 
            get
            {
                return new ObservableCollection<PatientDTO>(PatientServiceProxy.Current.Patients);
            }
        }

        public string PhysicianName
        {
            get
            {
                if (Model != null && Model.PhysicianId > 0)
                {
                    if (Model.Physician == null)
                    {
                        Model.Physician = PhysicianServiceProxy.
                            Current.
                            Physicians.
                            FirstOrDefault(p => p.Id == Model.PhysicianId);
                    }
                }
                return Model?.Physician?.Name ?? string.Empty;
            }
        }

        public PhysicianDTO SelectedPhysician
        {
            get
            {
                return Model?.Physician;
            }

            set
            {
                var selectedPhysician = value;
                if (Model != null)
                {
                    Model.Physician = selectedPhysician;
                    Model.PhysicianId = selectedPhysician?.Id ?? 0;
                }
            }
        }

        public ObservableCollection<PhysicianDTO> Physicians
        {
            get
            {
                return new ObservableCollection<PhysicianDTO>(PhysicianServiceProxy.Current.Physicians);
            }
        }

        public DateTime StartDate {
            get
            {
                return Model?.AppointmentStartTime.Date ?? DateTime.Today;
            }
            set
            {
                if(Model != null)
                {
                    Model.AppointmentStartTime = value;
                    Model.AppointmentStartTime.Add(StartTime);
                    RefreshTime();
                }
            }
        }

        public DateTime EndDate
        {
            get
            {
                return Model?.AppointmentEndTime.Date ?? DateTime.Today;
            }
            set
            {
                if (Model != null)
                {
                    Model.AppointmentEndTime = value;
                    RefreshTime();
                }
            }
        }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime ApptStart
        {
            get
            {
                return Model?.AppointmentStartTime ?? DateTime.Now;
            }
        }
        public DateTime ApptEnd
        {
            get
            {
                return Model?.AppointmentEndTime ?? DateTime.Now;
            }
        }
        public DateTime MinStartDate
        {
            get
            {
                return DateTime.Today;
            }
        }

        public AppointmentViewModel() {
            Model = new Appointment();
        }
        public AppointmentViewModel(Appointment a)
        {
            Model = a;
        }

        public void AddOrUpdate()
        {
            if (Model != null)
            {
                AppointmentServiceProxy.Current.AddOrUpdate(Model);
            }
        }

        public void RefreshTime()
        {
            if (Model != null)
            {
                
                if (Model.AppointmentStartTime != null && Model.AppointmentEndTime != null)
                {
                    Model.AppointmentStartTime = StartDate;
                    Model.AppointmentStartTime = Model.AppointmentStartTime.Add(StartTime);

                    Model.AppointmentEndTime = EndDate;
                    Model.AppointmentEndTime = Model.AppointmentEndTime.Add(EndTime);
                }

            }
        }

    }
}
