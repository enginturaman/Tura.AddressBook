using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Tura.AddressBook.Domain.Models;
using Tura.AddressBook.Services.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tura.AddressBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalContactsController : Controller
    {
        private readonly IPersonalContactService _personalContactService;

        public PersonalContactsController(IPersonalContactService personalContactService)
        {
            _personalContactService = personalContactService;
        }

        // GET: api/<PersonalsController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _personalContactService.Get();
            return Ok(result);
        }

        // GET api/<PersonalsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _personalContactService.GetById(id);

            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public IActionResult Post([FromBody] PersonalContactModel model)
        {
            var entityId = _personalContactService.Post(model);

            return Ok(entityId);

        }

        // PUT api/<PersonalsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] PersonalContactModel model)
        {

            _personalContactService.Put(id, model);
            return Ok();
        }

        // DELETE api/<PersonalsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _personalContactService.Delete(id);
            return Ok();

        }
    }
}
