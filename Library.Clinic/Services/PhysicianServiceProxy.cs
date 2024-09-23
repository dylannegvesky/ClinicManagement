using Library.Clinic.Models;

namespace Library.Clinic.Services
{
    public static class PhysicianServiceProxy
    {
        public static int LastKey
        {
            get
            {
                if (Physicians.Any())
                {
                    return Physicians.Select(x => x.Id).Max();
                }
                return 0;
            }
        }
        public static List<Physician> Physicians { get; private set; } = new List<Physician>();
        public static void AddPhysician(Physician physician)
        {
            if (physician.Id <= 0)
            {
                physician.Id = LastKey + 1;
            }
            Physicians.Add(physician);
        }
        public static void RemovePhysician(int id)
        {
            var physicianToRemove = Physicians.FirstOrDefault(p => p.Id == id);
            if (physicianToRemove != null)
            {
                Physicians.Remove(physicianToRemove);
            }
        }

        public static void AddPhysicianSpecialization(int id, string specialization)
        {

            if (id >= 1 && id <= LastKey && Physicians.Any())
            {
                Physicians[id - 1].Specializations.Add(specialization);
                Console.WriteLine($"The following notes currently exist for physician {id}'s notes: ");
                foreach (var spec
                    in Physicians[id - 1].Specializations)
                {
                    Console.WriteLine(spec);
                }
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("An error occurred when trying to add physician's note, please try again...");
            }

        }

        public static void printList()
        {
            Console.WriteLine("Current Physician List: \n");
            foreach (var physician in Physicians)
            {
                Console.WriteLine($"Physician ID: {physician.Id}");
                Console.WriteLine($"Physician License: #{physician.License}");
                Console.WriteLine($"Physician Name: {physician.Name}");
                Console.WriteLine($"Physician Graduation Date: {physician.Graduation}\n");
            }
        }


    }
}
