using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //[Theory]
        //[InlineData(1)]
        //public void PutPersonal_ActionExecutes_ReturnNoContent(int PersonalId)
        //{
        //    var PersonalModel = personals.First(x => x.Id == PersonalId);

        //    _mockService.Setup(x => x.Update(Personal));

        //    var result = _controller.PutPersonal(PersonalId, Personal);

        //    _mockService.Verify(x => x.Update(Personal), Times.Once);

        //    Assert.IsType<NoContentResult>(result);
        //}

        //[Fact]
        //public async void PostPersonal_ActionExecutes_ReturnCreatedAtAction()
        //{
        //    var PersonalModel = personals.First();

        //    _mockService.Setup(x => x.Create(Personal)).Returns(Task.CompletedTask);

        //    var result = await _controller.PostPersonal(Personal);

        //    var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);

        //    _mockService.Verify(x => x.Create(Personal), Times.Once);

        //    Assert.Equal("GetPersonal", createdAtActionResult.ActionName);
        //}

        //[Theory]
        //[InlineData(0)]
        //public async void DeletePersonal_IdInValid_ReturnNotFound(int PersonalId)
        //{
        //    PersonalModel PersonalModel = null;

        //    _mockService.Setup(x => x.GetById(PersonalId)).ReturnsAsync(Personal);

        //    var resultNotFound = await _controller.DeletePersonal(PersonalId);

        //    Assert.IsType<NotFoundResult>(resultNotFound.Result);
        //}

        //[Theory]
        //[InlineData(1)]
        //public async void DeletePersonal_ActionExecute_ReturnNoContent(int PersonalId)
        //{
        //    var PersonalModel = personals.First(x => x.Id == PersonalId);
        //    _mockService.Setup(x => x.GetById(PersonalId)).ReturnsAsync(Personal);
        //    _mockService.Setup(x => x.Delete(Personal));

        //    var noContentResult = await _controller.DeletePersonal(PersonalId);

        //    _mockService.Verify(x => x.Delete(Personal), Times.Once);

        //    Assert.IsType<NoContentResult>(noContentResult.Result);
        //}
    }
}