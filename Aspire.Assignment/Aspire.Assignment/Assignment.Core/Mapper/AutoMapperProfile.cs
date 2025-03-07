using AutoMapper;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;

namespace Assignment.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<App, AppDTO>();
            CreateMap<Users, UsersDTO>();
            CreateMap<UpdateUsersDTO, Users>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
        }
    }

