using Microsoft.AspNetCore.Mvc;
using Thera.Api.Models;
using Thera.Api.Services;

namespace Thera.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhysiciansController : ControllerBase
    {
        private readonly IPhysicianRepository _repository;

        public PhysiciansController(IPhysicianRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Physician>> GetAll()
        {
            var physicians = _repository.GetAll();
            return Ok(physicians);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Physician> GetById(int id)
        {
            var physician = _repository.GetById(id);
            if (physician == null)
                return NotFound();

            return Ok(physician);
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Physician>> Search([FromQuery] string q)
        {
            var results = _repository.Search(q);
            return Ok(results);
        }

        [HttpPost]
        public ActionResult<Physician> Create([FromBody] Physician physician)
        {
            var created = _repository.Create(physician);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Physician physician)
        {
            var success = _repository.Update(id, physician);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var success = _repository.Delete(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
