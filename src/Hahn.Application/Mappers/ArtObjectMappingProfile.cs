using AutoMapper;
using Hahn.Application.DTOs;
using Hahn.Domain.Entities;

namespace Hahn.Application.Mappers
{
    public class ArtObjectMappingProfile : Profile
    {
        public ArtObjectMappingProfile()
        {
            CreateMap<ObjectDetailResponseDto, ArtObjectEntity>()
                .ForMember(dest => dest.ObjectId, opt => opt.MapFrom(src => src.ObjectID))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.ArtistDisplayName, opt => opt.MapFrom(src => src.ArtistDisplayName))
                .ForMember(dest => dest.Culture, opt => opt.MapFrom(src => src.Culture))
                .ForMember(dest => dest.Period, opt => opt.MapFrom(src => src.Period))
                .ForMember(dest => dest.Medium, opt => opt.MapFrom(src => src.Medium))
                .ForMember(dest => dest.Dimensions, opt => opt.MapFrom(src => src.Dimensions))
                .ForMember(dest => dest.ObjectDate, opt => opt.MapFrom(src => src.ObjectDate))
                .ForMember(dest => dest.ObjectURL, opt => opt.MapFrom(src => src.ObjectURL))
                .ForMember(dest => dest.IsPublicDomain, opt => opt.MapFrom(src => src.IsPublicDomain))
                .ForMember(dest => dest.MetadataDate, opt => opt.MapFrom(src => src.MetadataDate))
                .ForMember(dest => dest.Constituents, opt => opt.MapFrom(src => src.Constituents))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.AdditionalImages));

            CreateMap<ConstituentDto, ConstituentEntity>()
                .ForMember(dest => dest.ConstituentId, opt => opt.MapFrom(src => src.ConstituentID))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.WikidataURL, opt => opt.MapFrom(src => src.ConstituentWikidataURL))
                .ForMember(dest => dest.ULANURL, opt => opt.MapFrom(src => src.ConstituentULANURL));

            CreateMap<TagDto, TagEntity>()
                .ForMember(dest => dest.Term, opt => opt.MapFrom(src => src.Term))
                .ForMember(dest => dest.WikidataURL, opt => opt.MapFrom(src => src.WikidataURL))
                .ForMember(dest => dest.AATURL, opt => opt.MapFrom(src => src.AATURL));

            CreateMap<string, ImageEntity>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src));

            CreateMap<ArtObjectEntity, ObjectDetailResponseDto>()
                .ForMember(dest => dest.ObjectID, opt => opt.MapFrom(src => src.ObjectId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.ArtistDisplayName, opt => opt.MapFrom(src => src.ArtistDisplayName))
                .ForMember(dest => dest.Culture, opt => opt.MapFrom(src => src.Culture))
                .ForMember(dest => dest.Period, opt => opt.MapFrom(src => src.Period))
                .ForMember(dest => dest.Medium, opt => opt.MapFrom(src => src.Medium))
                .ForMember(dest => dest.Dimensions, opt => opt.MapFrom(src => src.Dimensions))
                .ForMember(dest => dest.ObjectDate, opt => opt.MapFrom(src => src.ObjectDate))
                .ForMember(dest => dest.ObjectURL, opt => opt.MapFrom(src => src.ObjectURL))
                .ForMember(dest => dest.IsPublicDomain, opt => opt.MapFrom(src => src.IsPublicDomain))
                .ForMember(dest => dest.MetadataDate, opt => opt.MapFrom(src => src.MetadataDate))
                .ForMember(dest => dest.Constituents, opt => opt.MapFrom(src => src.Constituents))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.AdditionalImages, opt => opt.MapFrom(src => src.Images.Select(i => i.Url).ToList()));

            CreateMap<ConstituentEntity, ConstituentDto>()
                .ForMember(dest => dest.ConstituentID, opt => opt.MapFrom(src => src.ConstituentId))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.ConstituentWikidataURL, opt => opt.MapFrom(src => src.WikidataURL))
                .ForMember(dest => dest.ConstituentULANURL, opt => opt.MapFrom(src => src.ULANURL));

            CreateMap<TagEntity, TagDto>()
                .ForMember(dest => dest.Term, opt => opt.MapFrom(src => src.Term))
                .ForMember(dest => dest.WikidataURL, opt => opt.MapFrom(src => src.WikidataURL))
                .ForMember(dest => dest.AATURL, opt => opt.MapFrom(src => src.AATURL));

            CreateMap<ImageEntity, string>()
                .ConvertUsing(src => src.Url);
        }
    }
}
