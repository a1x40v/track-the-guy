using System.Security.Claims;
using Infrastructure.Security.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Security.Policies.Handlers
{
    public class CharacterCreatorRequirementHandler : AuthorizationHandler<CharacterCreatorRequirement>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CharacterCreatorRequirementHandler(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CharacterCreatorRequirement requirement)
        {
            string? idRouteValue = _httpContextAccessor?.HttpContext?.Request.RouteValues.SingleOrDefault(x => x.Key == "id").Value?.ToString();

            if (idRouteValue == null) return Task.CompletedTask;

            var characterId = Guid.Parse(idRouteValue);

            var character = _dbContext.Characters
                .AsNoTracking()
                .Include(x => x.Creator)
                .SingleOrDefaultAsync(x => x.Id == characterId && x.Creator.Id == context.User.FindFirstValue(ClaimTypes.NameIdentifier))
                .Result;

            if (character != null) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}