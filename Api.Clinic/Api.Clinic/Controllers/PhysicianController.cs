using Api.Clinic.Enterprise;
using Library.Clinic.DTO;
using Library.Clinic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhysicianController : ControllerBase
    {
        private readonly ILogger<PhysicianController> _logger;

        public PhysicianController(ILogger<PhysicianController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<Physician> Get()
        {
            return new PhysicianEC().Physicians;
        }
        /*
        [HttpGet("{id}")]
        public PhysicianDTO? GetById(int id)
        {
            return new PhysicianEC().GetById(id);
        }
        */
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            new PhysicianEC().Delete(id);
        }

        [HttpPost("UpdatePhysician")]
        public Physician? Update([FromBody] Physician physician)
        {
            return new PhysicianEC().Update(physician);
        }

        [HttpPost]
        public Physician? Create([FromBody] Physician physician)
        {
            return new PhysicianEC().Create(physician);
        }

        [HttpPost("Search")]
        public List<Physician> Search([FromBody] Query q)
        {
            return new PhysicianEC().Search(q?.Content ?? string.Empty)?.ToList() ?? new List<Physician>();
        }

    }
}
