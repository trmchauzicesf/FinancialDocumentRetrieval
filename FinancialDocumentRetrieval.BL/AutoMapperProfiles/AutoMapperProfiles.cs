using AutoMapper;
using FinancialDocumentRetrieval.DAL.Identity;
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
            }
        }
    }
}
