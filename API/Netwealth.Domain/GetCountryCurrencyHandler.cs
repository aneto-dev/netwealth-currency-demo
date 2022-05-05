using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NetWealth.Data.Entities;
using NetWealth.Data.Models.Dto;
using NetWealth.Repositories;
using Netwealth.Services;

namespace NetWealth.Domain
{
    public class GetAllCountryCurrencyQuery : IRequest<List<CountryCurrencyDto>>
    {
        public class GetAllCountryCurrencyQueryHandler : IRequestHandler<GetAllCountryCurrencyQuery, List<CountryCurrencyDto>>
        {
            private readonly ICountryCurrencyRepository _countryCurrencyRepository;
            private readonly IMapper _mapper;
            private readonly ILogger<GetAllCountryCurrencyQueryHandler> _logger;

            public GetAllCountryCurrencyQueryHandler(ICountryCurrencyRepository  countryCurrencyRepository, IMapper mapper, ILogger<GetAllCountryCurrencyQueryHandler> logger)
            {
                _countryCurrencyRepository = countryCurrencyRepository;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<List<CountryCurrencyDto>> Handle(GetAllCountryCurrencyQuery query, CancellationToken cancellationToken)
            {
                var countryCurrencies =   await _countryCurrencyRepository.GetCountryCurrencyData();

                var mappedCountryCurrencies = _mapper.Map<List<CountryCurrencyDto>>(countryCurrencies);

                return mappedCountryCurrencies;
            }
        }
    }

}
