using Api.Clinic.Database;
using Library.Clinic.DTO;
using Library.Clinic.Models;

namespace Api.Clinic.Enterprise
{
    public class PhysicianEC
    {
        public PhysicianEC() { }

        public IEnumerable<Physician> Physicians
        {
            get
            {
                var physicians = new MsSqlContext().GetPhysicians();
                return physicians;
            }
        }
        public IEnumerable<Physician>? Search(string query)
        {
            return new MsSqlContext().SearchPhysicians(query);
        }

        public Physician Update(Physician physician)
        {
            new MsSqlContext().UpdatePhysician(physician);
            return physician;
        }

        public Physician Create(Physician physician)
        {
            return new MsSqlContext().CreatePhysician(physician);
        }

        public void Delete(int id)
        {
            new MsSqlContext().DeletePhysician(id);
            return;
        }
    }
}
