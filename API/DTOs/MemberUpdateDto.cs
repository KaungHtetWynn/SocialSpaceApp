using System;

namespace API.DTOs;

// Use AutoMapper to map MemberUpdateDto to ApplicationUser
public class MemberUpdateDto
{
    public string? MaritialStatus { get; set; }
    public string? Occupation { get; set; }
    public string? Description { get; set; }
    public string? Interests { get; set; }
    public string? LookingFor { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
}
