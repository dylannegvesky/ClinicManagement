﻿using Library.Clinic.DTO;
using Library.Clinic.Models;
using Newtonsoft.Json;
using PP.Library.Utilities;

namespace Library.Clinic.Services
{
    public class PatientServiceProxy
    {
        private static object _lock = new object();
        public static PatientServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new PatientServiceProxy();
                    }
                }
                return instance;
            }
        }
        private static PatientServiceProxy? instance;
        private PatientServiceProxy()
        {
            instance = null;

            var patientsData = new WebRequestHandler().Get("/Patient").Result;

            Patients = JsonConvert.DeserializeObject<List<PatientDTO>>(patientsData) ?? new List<PatientDTO>();
        }
        public int LastKey
        {
            get
            {
                if (Patients.Any())
                {
                    return Patients.Select(x => x.Id).Max();
                }
                return 0;
            }
        }
        private List<PatientDTO> patients = new List<PatientDTO>();
        public List<PatientDTO> Patients
        {
            get
            {
                return patients;
            }

            private set
            {
                if (patients != value)
                {
                    patients = value;
                }
            }
        }

        public async Task<List<PatientDTO>> Search(string query)
        {
            var patientsPayload = await new WebRequestHandler()
                .Post($"/Patient/Search", new Query(query));

            Patients = JsonConvert.DeserializeObject<List<PatientDTO>>(patientsPayload) 
                ?? new List<PatientDTO>();

            return Patients;
        }

        public async Task<PatientDTO?> AddOrUpdatePatient(PatientDTO patient)
        {
            var payload = await new WebRequestHandler().Post("/Patient", patient);
            var newPatient = JsonConvert.DeserializeObject<PatientDTO>(payload);
            if (newPatient != null && newPatient.Id > 0 && patient.Id == 0)
            {
                Patients.Add(newPatient);
            }
            else if(newPatient != null && patient != null && patient.Id > 0 && patient.Id == newPatient.Id)
            {
                var currentPatient = Patients.FirstOrDefault(p => p.Id == newPatient.Id);
                var index = Patients.Count;
                if(currentPatient != null)
                {
                    index = Patients.IndexOf(currentPatient);
                    Patients.RemoveAt(index);
                }
                Patients.Insert(index, newPatient);
            }
            return newPatient;
        }
        public async void RemovePatient(int id)
        {
            var patientToRemove = Patients.FirstOrDefault(p => p.Id == id);
            if (patientToRemove != null)
            {
                Patients.Remove(patientToRemove);
                await new WebRequestHandler().Delete($"/Patient/{id}");
            }
        }

        public void AddPatientNotes(int id, string note)
        {

            if (id >= 1 && Patients.Any())
            {
                Patients[id - 1].MedicalNotes.Add(note);
                Console.WriteLine($"The following notes currently exist for patient {id}'s notes: ");
                foreach (var notes in Patients[id - 1].MedicalNotes)
                {
                    Console.WriteLine(notes);
                }
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("An error occurred when trying to add patient's note, please try again...");
            }
        }
    }
}
