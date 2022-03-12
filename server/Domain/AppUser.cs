using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public bool IsAdmin { get; set; }
        public ICollection<Character> CreatedCharacters { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}