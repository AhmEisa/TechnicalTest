using AutoMapper;
using GET.Core.Application.Features.Lookups;
using GET.Core.Application.Features.ServiceRequests;
using GET.Core.Application.Features.Users;
using GET.Core.Application.Features.Users.Authentication;
using GET.Core.Application.Models.Authentication;
using GET.Core.Domain;
using System;

namespace GET.Core.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Service, ServiceVm>();

            CreateMap<User, UserVm>()
                .ForMember(d => d.UserName, m => m.MapFrom(s => $"{s.FirstName} {s.LastName}"));

            CreateMap<CreateServiceRequestCommand, ServiceRequest>();
            CreateMap<UpdateServiceRequestCommand, ServiceRequest>();


            CreateMap<RegisterUserCommand, User>()
                .ForMember(d => d.Id, map => map.MapFrom(s => Guid.NewGuid()));

            CreateMap<ServiceRequest, SeriveRequestVm>()
                .ForMember(c => c.UserName, map => map.MapFrom(s => $"{s.User.FirstName} {s.User.LastName}"))
                .ForMember(c => c.ServiceName, map => map.MapFrom(s => s.Service.Name))
                .ForMember(c => c.StatusName, map => map.MapFrom(s => s.ServiceStatus.Name));

            CreateMap<ServiceRequest, ServiceRequestExportDto>()
                .ForMember(c => c.RequestId, map => map.MapFrom(s => s.Id))
                .ForMember(c => c.UserName, map => map.MapFrom(s => $"{s.User.FirstName} {s.User.LastName}"))
                .ForMember(c => c.ServiceName, map => map.MapFrom(s => s.Service.Name))
                .ForMember(c => c.RequestStatus, map => map.MapFrom(s => s.ServiceStatus.Name));
        }
    }
}
