using Library.Clinic.DTO;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace App.Clinic.ViewModels
{
    public class PatientViewModel
    {
        public PatientDTO? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }

        public ObservableCollection<Insurance> InsurancePlans { get; set; }
        public Insurance SelectedInsurancePlan { get; set; }
        public int Id
        {
            get
            {
                if (Model == null)
                {
                    return -1; 
                }

                return Model.Id;
            }
            set
            {
                if(Model != null && Model.Id != value)
                {
                    Model.Id = value;
                }
            }
        }

        public string Name
        {
            get
            {
                return Model?.Name ?? string.Empty;
            }
            set
            {
                if (Model != null && Model.Name != value)
                {
                    Model.Name = value;
                }
            }
        }

        public void SetupCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as PatientViewModel));
        }

        private void DoDelete()
        {
            if (Id > 0)
            {
                PatientServiceProxy.Current.RemovePatient(Id);
                Shell.Current.GoToAsync("//Patients");
            }
        }
        private void DoEdit(PatientViewModel pvm)
        {
            if (pvm == null)
            {
                return;
            }
            var selectedPatientId = pvm?.Id ?? 0;
            Shell.Current.GoToAsync($"//PatientDetails?patientId={selectedPatientId}");
        }

        public PatientViewModel()
        {
            Model = new PatientDTO();
            InsurancePlans = new ObservableCollection<Insurance>(Insurance.InsurancePlans);
            SetupCommands();
        }

        public PatientViewModel(PatientDTO? _model)
        {
            if(_model != null)
            {
                Model = _model;
                if (Model.InsurancePlan != null)
                {
                    SelectedInsurancePlan = Model.InsurancePlan;
                }
            }
            SetupCommands();
        }

        public async void ExecuteAdd()
        {
            if (Model != null)
            {
                if (Model.InsurancePlan == null)
                {
                    Model.InsurancePlan = SelectedInsurancePlan;
                }
                await PatientServiceProxy.Current.AddOrUpdatePatient(Model);
            }

            await Shell.Current.GoToAsync("//Patients");
        }


    }
}
