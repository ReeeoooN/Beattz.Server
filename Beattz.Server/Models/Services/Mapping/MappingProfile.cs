using AutoMapper;
using Beattz.Server.Controllers.DTO;
using Beattz.Server.Models.Persistance.Entities;
using Beattz.Server.Models.Services.Domain;

namespace Beattz.Server.Models.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TrackEntity, DownloadTrack>()
            .ForMember(dest => dest.path, opt => opt.Ignore());

        CreateMap<TrackEntity, Track>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
            .ForMember(dest => dest.filename, opt => opt.MapFrom(src => src.filename))
            .ForMember(dest => dest.link, opt => opt.MapFrom(src => src.link))
            .ReverseMap();
        
        CreateMap<Track, TrackDTO>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
            .ForMember(dest => dest.filename, opt => opt.MapFrom(src => src.filename))
            .ForMember(dest => dest.link, opt => opt.MapFrom(src => src.link))
            .ReverseMap();
        
        CreateMap<DownloadTrack, DownloadTrackDTO>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
            .ForMember(dest => dest.filename, opt => opt.MapFrom(src => src.filename))
            .ForMember(dest => dest.link, opt => opt.MapFrom(src => src.link))
            .ForMember(dest=> dest.file, opt => opt.MapFrom(src => new FileStream(src.path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            .ReverseMap();
    }
}