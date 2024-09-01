using API.DTOs;
using API.Extensions;
using API.Models;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // from -> to
        // maps from ApplicationUser to MemberDto
        //CreateMap<ApplicationUser, MemberDto>();

        // d = destination (which member we want to configure)
        // o = options (where do we want to map from)
        // s = source (tell the source)
        // configuring the automapper, mapping PhotoUrl property in ApplicationUser class 
        // with the ImageUrl property in Photo class

        // Retrieve AppUser from Database and maps to MemberDto (this will be returned to client) check Repository and UserController
        CreateMap<ApplicationUser, MemberDto>()
            // d.Age => so it will not need to go and get the full app user objects
            .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.CalculateAge())) // comment out this and test - added when using AutoMapper queryable extensions
            .ForMember(d => d.PhotoUrl, o =>
                o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsProfilePhoto)!.ImageUrl));
        CreateMap<Photo, PhotoDto>();
        // Mapping from MemberUpdateDto to ApplicationUser
        CreateMap<MemberUpdateDto, ApplicationUser>();
    }
}


