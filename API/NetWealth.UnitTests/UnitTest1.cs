using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Xunit;
using Moq;
using NetWealth.Data.Models.Query;
using NetWealth.Domain;
using Netwealth.Services;

namespace NetWealth.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public async  void CurrencyConverterCommand_ConvertToCurrency()
        {
            //Arange
            var mediator = new Mock<IMediator>();

            var currencyConverter = new Mock<ICurrencyConverterService>();
            var mapper = new Mock<IMapper>();

            CurrencyConverterCommand command = new CurrencyConverterCommand();
            command.FromCurrencyReference = "EUR";
            command.ToCurrencyReference = "USD";
            command.Amount = (decimal)1.00;
            CurrencyConverterHandler handler = new CurrencyConverterHandler(mapper.Object,currencyConverter.Object);

            //Act
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(x);

            //something like:
            //mediator.Verify(x => x.Publish(It.IsAny<CustomersChanged>()));
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
        {
            // Arrange
            var mockRepo = new Mock<ICurrencyConverterService>();



            ////var controller = new HomeController(mockRepo.Object);

            //// Act
            //var result = await controller.Index();

            //// Assert
            //var viewResult = Assert.IsType<ViewResult>(result);
            //var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
            //    viewResult.ViewData.Model);
            //Assert.Equal(2, model.Count());
        }
    }
}
