using Library.Clinic.Models;

namespace Library.Clinic.Services
{
    public static class PatientServiceProxy
    {
        public static int LastKey
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
        public static List<Patient> Patients { get; private set; } = new List<Patient>();
        public static void AddPatient(Patient patient)
        {
            if (patient.Id <= 0)
            {
                patient.Id = LastKey + 1;
            }
            Patients.Add(patient);
        }
        public static void RemovePatient(int id)
        {
            var patientToRemove = Patients.FirstOrDefault(p => p.Id == id);
            if (patientToRemove != null)
            {
                Patients.Remove(patientToRemove);
            }
        }

        public static void AddPatientNotes(int id, string note)
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

        public static void printList()
        {
            Console.WriteLine("Current Patient List: \n");
            foreach (var patient in Patients)
            {
                Console.WriteLine($"Patient ID: {patient.Id}");
                Console.WriteLine($"Patient Name: {patient.Name}");
                Console.WriteLine($"Patient Birthday: {patient.Birthday}");
                Console.WriteLine($"Patient SSN: {patient.SSN}");
                Console.WriteLine($"Patient Address: {patient.Address}\n");
            }
        }


    }
}
