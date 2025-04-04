using AutoMapper;
using SimpleFinances.Communication.Requests;
using SimpleFinances.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToDomain();
            DomainToResponse();
        }


        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<RequestCardJson, Domain.Entities.Card>();
            CreateMap<RequestIncomeJson, Domain.Entities.Income>();
            
        }

        private void DomainToResponse()
        {
            CreateMap< Domain.Entities.Card, ResponseCardJson>();
            CreateMap< Domain.Entities.Income, ResponseIncomeJson>();
        }
    }
}
