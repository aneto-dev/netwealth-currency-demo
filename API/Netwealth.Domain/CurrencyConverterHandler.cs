using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NetWealth.Data.Models.Command;
//using NetWealth.Data.Models.Command;
using NetWealth.Data.Models.Query;
using Netwealth.Services;

namespace NetWealth.Domain
{
    public class CurrencyConverterHandler : IRequestHandler<CurrencyConverterCommand, CurrencyConverterResponse>
    {
        
        private readonly IMapper _mapper;
        private readonly ICurrencyConverterService _currencyConverterService;

        public CurrencyConverterHandler(IMapper  mapper, ICurrencyConverterService currencyConverterService)
        {
            _mapper = mapper;
            _currencyConverterService = currencyConverterService;
        }


        public async Task<CurrencyConverterResponse> Handle(CurrencyConverterCommand request, CancellationToken cancellationToken)
        {
            var currencyConverterResponse = await  _currencyConverterService.GetCurrencyData(request.ToCurrencyReference, request.FromCurrencyReference,
                request.Amount);

            //var mappedCurrencyConverterResponse  = _mapper.Map<CurrencyConverterDto>(currencyConverterResponse);

            return currencyConverterResponse;
        }
    }

    public class CurrencyConverterCommand : CommandBase<CurrencyConverterResponse>
    {
        public CurrencyConverterCommand()
        {

        }
        public decimal Amount { get; set; }
        public string FromCurrencyReference { get; set; }
        public string ToCurrencyReference { get; set; }
    }

   
}
