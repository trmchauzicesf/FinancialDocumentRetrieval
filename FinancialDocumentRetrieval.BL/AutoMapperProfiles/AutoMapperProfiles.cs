using AutoMapper;
using FinancialDocumentRetrieval.DAL.Identity;
using FinancialDocumentRetrieval.Models.Common.Validation;
using FinancialDocumentRetrieval.Models.DTOs.RequestDTOs;
using FinancialDocumentRetrieval.Models.DTOs.ResponseDTOs;
using FinancialDocumentRetrieval.Models.Entity;
using FinancialDocumentRetrieval.Models.Users;

namespace FinancialDocumentRetrieval.BL.AutoMapperProfiles
{
    public static class AutoMapperProfiles
    {
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<ApiUserDto, ApiUser>().ReverseMap();
                CreateMap<FinancialDocumentRequestDTO, FinancialDocumentValidation>();
                CreateMap<Client, CompanyDTO>();
            }
        }
    }
}