using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Tura.AddressBook.Api.Controllers;
using Tura.AddressBook.Domain.Models;
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
            personals = new List<PersonalModel>() { new PersonalModel {  Company = "Test Firması" , Name = "Engin" , SurName = "Turaman"  },
            new PersonalModel { Company = "Test Firması 2" , Name = "Ahmet" , SurName = "Talha" }};
        }


        [Fact]
        public void GetPersonals_ActionExecutes_ReturnOkResultWithPersonal()
        {
            _mockService.Setup(x => x.Get()).Returns(personals);

            var result = _controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnProducts = Assert.IsAssignableFrom<IEnumerable<PersonalModel>>(okResult.Value);

            Assert.Equal<int>(2, returnProducts.ToList().Count);
        }

        //[Theory]
        //[InlineData(0)]
        //public async void GetProduct_IdInValid_ReturnNotFound(int productId)
        //{
        //    Product product = null;

        //    _mockService.Setup(x => x.GetById(productId)).ReturnsAsync(product);

        //    var result = await _controller.GetProduct(productId);

        //    Assert.IsType<NotFoundResult>(result);
        //}

        //[Theory]
        //[InlineData(1)]
        //[InlineData(2)]
        //public async void GetProduct_IdValid_ReturnOkResult(int productId)
        //{
        //    var product = personals.First(x => x.Id == productId);

        //    _mockService.Setup(x => x.GetById(productId)).ReturnsAsync(product);

        //    var result = await _controller.GetProduct(productId);

        //    var okResult = Assert.IsType<OkObjectResult>(result);

        //    var returnProduct = Assert.IsType<Product>(okResult.Value);

        //    Assert.Equal(productId, returnProduct.Id);
        //    Assert.Equal(product.Name, returnProduct.Name);
        //}

        //[Theory]
        //[InlineData(1)]
        //public void PutProduct_IdIsNotEqualProduct_ReturnBadRequestResult(int productId)
        //{
        //    var product = personals.First(x => x.Id == productId);

        //    var result = _controller.PutProduct(2, product);

        //    var badRequestResult = Assert.IsType<BadRequestResult>(result);
        //}

        //[Theory]
        //[InlineData(1)]
        //public void PutProduct_ActionExecutes_ReturnNoContent(int productId)
        //{
        //    var product = personals.First(x => x.Id == productId);

        //    _mockService.Setup(x => x.Update(product));

        //    var result = _controller.PutProduct(productId, product);

        //    _mockService.Verify(x => x.Update(product), Times.Once);

        //    Assert.IsType<NoContentResult>(result);
        //}

        //[Fact]
        //public async void PostProduct_ActionExecutes_ReturnCreatedAtAction()
        //{
        //    var product = personals.First();

        //    _mockService.Setup(x => x.Create(product)).Returns(Task.CompletedTask);

        //    var result = await _controller.PostProduct(product);

        //    var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);

        //    _mockService.Verify(x => x.Create(product), Times.Once);

        //    Assert.Equal("GetProduct", createdAtActionResult.ActionName);
        //}

        //[Theory]
        //[InlineData(0)]
        //public async void DeleteProduct_IdInValid_ReturnNotFound(int productId)
        //{
        //    Product product = null;

        //    _mockService.Setup(x => x.GetById(productId)).ReturnsAsync(product);

        //    var resultNotFound = await _controller.DeleteProduct(productId);

        //    Assert.IsType<NotFoundResult>(resultNotFound.Result);
        //}

        //[Theory]
        //[InlineData(1)]
        //public async void DeleteProduct_ActionExecute_ReturnNoContent(int productId)
        //{
        //    var product = personals.First(x => x.Id == productId);
        //    _mockService.Setup(x => x.GetById(productId)).ReturnsAsync(product);
        //    _mockService.Setup(x => x.Delete(product));

        //    var noContentResult = await _controller.DeleteProduct(productId);

        //    _mockService.Verify(x => x.Delete(product), Times.Once);

        //    Assert.IsType<NoContentResult>(noContentResult.Result);
        //}
    }
}