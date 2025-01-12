using Library.Clinic.DTO;
using Library.Clinic.Models;
using Newtonsoft.Json;
using PP.Library.Utilities;
using System.ComponentModel;

namespace Library.Clinic.Services
{
    public class PhysicianServiceProxy
    {
        private static object _lock = new object();
        public static PhysicianServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new PhysicianServiceProxy();
                    }
                }
                return instance;
            }
        }
        private static PhysicianServiceProxy? instance;
        private PhysicianServiceProxy()
        {
            instance = null;

            var physiciansData = new WebRequestHandler().Get("/Physician").Result;

            Physicians = JsonConvert.DeserializeObject<List<PhysicianDTO>>(physiciansData) ?? new List<PhysicianDTO>();
        }
        public int LastKey
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
        private List<PhysicianDTO> physicians;
        public List<PhysicianDTO> Physicians
        {
            get
            {
                return physicians;
            }

            private set
            {
                if (physicians != value)
                {
                    physicians = value;
                }
            }
        }
        public async Task<PhysicianDTO?> AddOrUpdatePhysician(PhysicianDTO physician)
        {
            {
                string endpoint;
                if (physician.Id == 0)
                {
                    endpoint = "/Physician";
                }
                else
                {
                    endpoint = "/Physician/UpdatePhysician";
                }

                var payload = await new WebRequestHandler().Post(endpoint, physician);
                var newPhysician = JsonConvert.DeserializeObject<PhysicianDTO>(payload);

                if (newPhysician != null)
                {
                    if (physician.Id == 0)
                    {
                        Physicians.Add(newPhysician);
                    }
                    else
                    {
                        var index = Physicians.FindIndex(p => p.Id == newPhysician.Id);
                        if (index >= 0)
                        {
                            Physicians[index] = newPhysician;
                        }
                    }
                }

                return newPhysician;
            }
            /*
            var payload = await new WebRequestHandler().Post("/Physician", physician);
            var newPhysician = JsonConvert.DeserializeObject<PhysicianDTO>(payload);
            if (newPhysician != null && newPhysician.Id > 0 && physician.Id == 0)
            {
                Physicians.Add(newPhysician);
            }
            else if (newPhysician != null && physician != null && physician.Id > 0 && physician.Id == newPhysician.Id)
            {
                var currentPhysician = Physicians.FirstOrDefault(p => p.Id == newPhysician.Id);
                var index = Physicians.Count;
                if (currentPhysician != null)
                {
                    index = Physicians.IndexOf(currentPhysician);
                    Physicians.RemoveAt(index);
                }
                Physicians.Insert(index, newPhysician);
            }
            return newPhysician;
            */
        }
        public async void RemovePhysician(int id)
        {
            var physicianToRemove = Physicians.FirstOrDefault(p => p.Id == id);
            if (physicianToRemove != null)
            {
                Physicians.Remove(physicianToRemove);
                await new WebRequestHandler().Delete($"/Physician/{id}");
            }
        }

        public void AddPhysicianSpecialization(int id, string specialization)
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

        public async Task<List<PhysicianDTO>> Search(string query)
        {
            string? physicianPayload;
            if (query == string.Empty)
            {
                physicianPayload = await new WebRequestHandler().Get("/Physician");
            }
            else
            {
                physicianPayload = await new WebRequestHandler()
                .Post($"/Physician/Search", new Query(query));
            }

            Physicians = JsonConvert.DeserializeObject<List<PhysicianDTO>>(physicianPayload)
                ?? new List<PhysicianDTO>();

            return Physicians;
        }

    }
}
