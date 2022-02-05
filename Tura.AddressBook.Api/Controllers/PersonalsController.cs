using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        // GET: api/<PersonalsController>
        [HttpGet]
        public IEnumerable<PersonalModel> Get()
        {
            IEnumerable<PersonalModel> result = _personalService.Get();

            return result;
        }

        // GET api/<PersonalsController>/5
        [HttpGet("{id}")]
        public PersonalDetailModel Get(Guid id)
        {
            PersonalDetailModel retval = _personalService.GetById(id);

            return retval;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonalModel model)
        {
            var result = await _personalService.Post(model);

            return Ok(result);
        }

        // PUT api/<PersonalsController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] PersonalModel model)
        {

            _personalService.Put(id, model);
        }

        // DELETE api/<PersonalsController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _personalService.Delete(id);

        }
    }
}
