using System.Security.Claims;
using Identity.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Identity.Policies.Handlers
{
    public class ReviewCreatorRequirementHandler : AuthorizationHandler<ReviewCreatorRequirement>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ReviewCreatorRequirementHandler(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReviewCreatorRequirement requirement)
        {
            string? idRouteValue = _httpContextAccessor?.HttpContext?.Request.RouteValues.SingleOrDefault(x => x.Key == "id").Value?.ToString();

            if (idRouteValue == null) return Task.CompletedTask;

            var reviewId = Guid.Parse(idRouteValue);

            var review = _dbContext.Reviews
               .AsNoTracking()
               .Include(x => x.Author)
               .SingleOrDefaultAsync(x => x.Id == reviewId && x.Author.Id == context.User.FindFirstValue(ClaimTypes.NameIdentifier))
               .Result;

            if (review != null) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}