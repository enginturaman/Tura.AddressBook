using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;
using Tura.AddressBook.Domain.Models;
using Tura.AddressBook.Services.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tura.AddressBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalsController : ControllerBase
    {
        private readonly IPersonalService _personalService;

        public PersonalsController(IPersonalService personalService)
        {
            _personalService = personalService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Log.Information("Test MongoDB Information Log");
            var result = _personalService.Get();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _personalService.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonalModel model)
        {
            var result = await _personalService.Post(model);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] PersonalModel model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest();
            }
            _personalService.Put(id, model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _personalService.Delete(id);

            return Ok();
        }
    }
}
