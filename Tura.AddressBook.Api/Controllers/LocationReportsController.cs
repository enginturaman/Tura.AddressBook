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
    public class LocationReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public LocationReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }


        [HttpPost]
        public IActionResult Post()
        {
            _reportService.PushLocationReport();
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _reportService.GetLocationReports();
            return Ok(result);
        }

        [HttpPost("CreateLocationReport")]
        public IActionResult CreateLocationReport()
        {
            _reportService.CreateLocationReport();
            return Ok();
        }

    }
}
