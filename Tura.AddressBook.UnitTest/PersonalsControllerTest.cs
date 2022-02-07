using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tura.AddressBook.Api.Controllers;
using Tura.AddressBook.Domain.Models;
using Tura.AddressBook.Infrastructures.Exceptions;
using Tura.AddressBook.Services.Interfaces.Services;
using Xunit;

namespace Tura.AddressBook.UnitTest
{
    public class PersonalsControllerTest
    {
        private readonly Mock<IPersonalService> _mockService;
        private readonly PersonalsController _controller;
        private List<PersonalModel> personals;

        public PersonalsControllerTest()
        {
            _mockService = new Mock<IPersonalService>();
            _controller = new PersonalsController(_mockService.Object);
            personals = new List<PersonalModel>() { new PersonalModel { Id = new Guid( "49f8b087-bdb1-473e-ab79-1ff887b669af" ),  Firm = "Test Firması" , Name = "Engin" , LastName = "Turaman"  },
            new PersonalModel {  Id = new Guid( "0224604c-0e43-4ca2-87bb-ca2955ae1907" ) , Firm = "Test Firması 2" , Name = "Ahmet" , LastName = "Talha" }};
        }


        [Fact]
        public void GetPersonals_ActionExecutes_ReturnOkResultWithPersonal()
        {
            _mockService.Setup(x => x.Get()).Returns(personals);

            var result = _controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnPersonals = Assert.IsAssignableFrom<IEnumerable<PersonalModel>>(okResult.Value);

            Assert.Equal<int>(2, returnPersonals.ToList().Count);
        }

        [Theory]
        [InlineData("0224604c-0e43-4ca2-87bb-ca2955ae1907")]
        public void GetPersonal_IdInValid_ReturnNotFound(Guid personalId)
        {
            PersonalDetailModel personalModel = null;

            _mockService.Setup(x => x.GetById(personalId)).Returns(personalModel);

            var result = _controller.GetById(personalId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData("0224604c-0e43-4ca2-87bb-ca2955ae1907")]
        [InlineData("49f8b087-bdb1-473e-ab79-1ff887b669af")]
        public void GetPersonal_IdValid_ReturnOkResult(Guid personalId)
        {
            var personalDeteil = personals
                .Select(x => new PersonalDetailModel
                {
                    Id = x.Id,
                    Firm = x.Firm,
                    LastName = x.LastName,
                    Name = x.Name,
                })
                .First(x => x.Id == personalId);

            _mockService.Setup(x => x.GetById(personalId)).Returns(personalDeteil);

            var result = _controller.GetById(personalId);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnPersonalModel = Assert.IsType<PersonalDetailModel>(okResult.Value);

            Assert.Equal(personalId, returnPersonalModel.Id);
        }

        [Theory]
        [InlineData("0224604c-0e43-4ca2-87bb-ca2955ae1907")]
        public void PutPersonal_IdIsNotEqualPersonal_ReturnBadRequestResult(Guid personalId)
        {
            var personel = personals.First(x => x.Id == personalId);

            var result = _controller.Put(new Guid("abcdb087-bdb1-473e-ab79-1ff887b669af"), personel);

            var badRequestResult = Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData("0224604c-0e43-4ca2-87bb-ca2955ae1907")]
        public void PutPersonal_ActionExecutes_ReturnOkResult(Guid personelId)
        {
            var personal = personals.First(x => x.Id == personelId);

            _mockService.Setup(x => x.Put(personelId , personal));

            var result = _controller.Put(personelId, personal);

            _mockService.Verify(x => x.Put(personelId , personal), Times.Once);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void PostPersonal_ActionExecutes_ReturnCreatedAtAction()
        {
            var personal = personals.First();

            _mockService.Setup(x => x.Post(personal));

            var result = await _controller.Post(personal);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);

            _mockService.Verify(x => x.Post(personal), Times.Once);

            Assert.Equal("Get", createdAtActionResult.ActionName);
        }

        [Theory]
        [InlineData("0224604c-0e43-4ca2-87bb-ca2955ae1907")]
        public void DeletePersonal_IdInValid_ReturnNotFound(Guid personalId)
        {
            PersonalDetailModel personal = null;

            _mockService.Setup(x => x.GetById(personalId)).Returns(personal);

            var resultNotFound = _controller.Delete(personalId);

            Assert.IsType<NotFoundObjectResult>(resultNotFound);
        }

        [Theory]
        [InlineData("0224604c-0e43-4ca2-87bb-ca2955ae1907")]
        public void DeletePersonal_ActionExecute_ReturnNoContent(Guid personalId)
        {
            var personal = personals.Select(x => new PersonalDetailModel
            {
                Id = x.Id,
                Firm = x.Firm,
                LastName = x.LastName,
                Name = x.Name,
            }).First(x => x.Id == personalId);

            _mockService.Setup(x => x.GetById(personalId)).Returns(personal);
            _mockService.Setup(x => x.Delete(personalId));

            var result = _controller.Delete(personalId);

            _mockService.Verify(x => x.Delete(personalId), Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}