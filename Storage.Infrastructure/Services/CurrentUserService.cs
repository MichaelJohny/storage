using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Storage.Application.Common.Interfaces;

namespace Storage.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    public string UserId { get; }
    
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}