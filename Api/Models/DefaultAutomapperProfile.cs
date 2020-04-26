using Api.Models.Users;
using AutoMapper;
using BusinessLogic.Models;

namespace Api.Models
{
    public class DefaultAutomapperProfile : Profile
    {
        public DefaultAutomapperProfile()
        {
            CreateMap<BusinessLogic.Models.User, UserViewModel>()
                    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                    .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Api.Models.Users.CreateViewModel, User>()
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                    .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Api.Models.Users.UpdateViewModel, User>()
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                    .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
