using AutoMapper;
using MapperApp.Models;
using MapperApp.Models.DTOs.Incoming;
using MapperApp.Models.DTOs.Outgoing;

namespace MapperApp.Profiles
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<DriverForCreationDto, Driver>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.FirstName))
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(src => src.LastName))
                .ForMember(
                    dest => dest.DriverNumber,
                    opt => opt.MapFrom(src => src.DriverNumber))
                .ForMember(
                    dest => dest.WorldChampionship,
                    opt => opt.MapFrom(src => src.WorldChampionships))
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => 1))
                .ForMember(
                    dest => dest.DateAdded,
                    opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(
                    dest => dest.DateUpdated,
                    opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<Driver, DriverDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".ToUpper()))
                .ForMember(
                    dest => dest.DriverNumber,
                    opt => opt.MapFrom(src => src.DriverNumber))
                .ForMember(
                    dest => dest.WorldChampionships,
                    opt => opt.MapFrom(src => src.WorldChampionship));
        }
    }
}