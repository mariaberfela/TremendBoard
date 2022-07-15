using AutoMapper;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Infrastructure.Services.DTOs;
using TremendBoard.Mvc.Models.ProjectViewModels;
using TremendBoard.Mvc.Models.RoleViewModels;
using TremendBoard.Mvc.Models.UserViewModels;

namespace TremendBoard.Mvc.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProjectDetailViewModel, ProjectDetailDTO>();
            CreateMap<ApplicationUser, UserDetailViewModel>()
                .ForMember(
                    dest => dest.Username,
                    memberOptions => memberOptions.MapFrom(
                        (src, dest, destMember, context) => destMember = src.UserName));
            CreateMap<ApplicationRole, ApplicationRoleDetailViewModel>()
                .ForMember(
                    dest => dest.RoleName,
                    memberOptions => memberOptions.MapFrom(
                        (src, dest, destMember, context) => destMember = src.Name));
            CreateMap<ProjectUserDetailDTO, ProjectUserDetailViewModel>();
        }
    }
}
