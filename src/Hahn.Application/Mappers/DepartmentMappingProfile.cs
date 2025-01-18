using AutoMapper;
using Hahn.Application.DTOs;
using Hahn.Domain.Entities;

namespace Hahn.Application.Mappers
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<DepartmentResponseDto, DepartmentEntity>()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName ?? string.Empty));

            CreateMap<DepartmentEntity, DepartmentResponseDto>()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName));
        }
    }
}
