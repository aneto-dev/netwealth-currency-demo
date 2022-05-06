using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using NetWealth.Currency.API.Controllers;
using NetWealth.Currency.API.Mapping;
using NetWealth.Data.Entities;
using NetWealth.Data.Models.Dto;
using NetWealth.Domain;
using NetWealth.Repositories;
using Netwealth.Services;
using NetWealth.UnitTests.Mocks;
using Xunit;
using Shouldly;

namespace NetWealth.UnitTests
{
    public class CurrencyControllerTests
    {
        private readonly Mock<ICountryCurrencyRepository> _mockCountryCurrencyRepository;
        private readonly Mock<ICurrencyConverterService> mockCurrencyConverterService;
        private readonly Mock<ILogger<GetAllCountryCurrencyQuery.GetAllCountryCurrencyQueryHandler>> _mockLogger;
        private readonly IMapper _mapper;
        private readonly CurrencyController _controller;

        public CurrencyControllerTests()
        {
            _mockCountryCurrencyRepository = MockCountryCurrencyRepository.GetCountryCurrencyData();
            var mediator = new Mock<IMediator>();
            var logger = new Mock<ILogger<CurrencyController>>();

            _mockLogger = new Mock<ILogger<GetAllCountryCurrencyQuery.GetAllCountryCurrencyQueryHandler>>();    

            _controller = new CurrencyController(mediator.Object,logger.Object);


            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<DtoMappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public void GetCountriesCurrencies_ActionExecutes_ReturnsViewForGetCountriesCurrencies()
        {
            var result = _controller.GetCountriesCurrencies();
            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void ConvertToCurrency_ActionExecutes_ReturnsViewForConvertToCurrency()
        {
            var result = _controller.ConvertToCurrency(new CurrencyConverterCommand());
            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void ConvertToCurrency_ActionExecutes_ReturnsViewForConvertToCurrency_FromCurrencyReference_ShouldReturnBadRequestStatus()
        {
            var result = _controller.ConvertToCurrency(new CurrencyConverterCommand() { Amount = Convert.ToDecimal(200.00), ToCurrencyReference = "USD"});
            Assert.IsType<Task<IActionResult>>(result);

            Assert.Equal(HttpStatusCode.BadRequest.GetHashCode(), (result.Result as ObjectResult)?.StatusCode);
        }

        [Fact]
        public void ConvertToCurrency_ActionExecutes_ReturnsViewForConvertToCurrency_toCurrencyReference_ShouldReturnBadRequestStatus()
        {
            var result = _controller.ConvertToCurrency(new CurrencyConverterCommand() { Amount = Convert.ToDecimal(300.00), FromCurrencyReference = "USD" });
            Assert.IsType<Task<IActionResult>>(result);

            Assert.Equal(HttpStatusCode.BadRequest.GetHashCode(), (result.Result as ObjectResult)?.StatusCode);
        }

        [Fact]
        public void ConvertToCurrency_ActionExecutes_ReturnsViewForConvertToCurrency_FromCurrencyReference_ShouldReturnOkStatus()
        {
            var result = _controller.ConvertToCurrency(new CurrencyConverterCommand() { Amount = Convert.ToDecimal(200.00), FromCurrencyReference="EUR", ToCurrencyReference = "USD" });
            Assert.IsType<Task<IActionResult>>(result);

            Assert.Equal(HttpStatusCode.OK.GetHashCode(), (result.Result as ObjectResult)?.StatusCode);
        }


        [Fact]
        public async void GetCountriesCurrencies_ActionExecutes_ReturnsExactNumberOfCurrencyCountries()
        {

            var handler =
                new GetAllCountryCurrencyQuery.GetAllCountryCurrencyQueryHandler(_mockCountryCurrencyRepository.Object,
                    _mapper,_mockLogger.Object);
            var result = await handler.Handle(new GetAllCountryCurrencyQuery(), CancellationToken.None);

            result.Count.ShouldBe(4);

            result.ShouldBeOfType<List<CountryCurrencyDto>>();
        }
        
        [Fact]
        public void GetCountriesCurrencies_ActionExecutes_Verify()
        {
            //Arrange
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GetAllCountryCurrencyQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<List<CountryCurrencyDto>>());

            //Act
             mediator.Object.Send(new GetAllCountryCurrencyQuery());

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<GetAllCountryCurrencyQuery>(), It.IsAny<CancellationToken>()));

            mediator
                .Setup(m => m.Send(It.IsAny<GetAllCountryCurrencyQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<CountryCurrencyDto>()) //<-- return Task to allow await to continue
                .Verifiable("Query was not sent.");

            mediator.Verify(x => x.Send(It.IsAny<GetAllCountryCurrencyQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
