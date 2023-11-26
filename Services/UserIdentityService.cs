using System.Security.Claims;

namespace mandiri_project.Services
{
    public class UserIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            var temp = _httpContextAccessor.HttpContext
                                        .User
                                        .Claims;
        }

        /// <summary>
        /// Gets the user account ID based on claim type <seealso cref="ClaimTypes.NameIdentifier"/>.
        /// </summary>
        public string Email => _httpContextAccessor.HttpContext
                                            .User
                                            .Claims
                                            .FirstOrDefault(x => x.Type == ClaimTypes.Email)
                                            //.FirstOrDefault(Q => Q.Type == OpenIddictConstants.Claims.Subject)
                                            ?.Value;

        public string Role => _httpContextAccessor.HttpContext
            .User
            .Claims
            //.FirstOrDefault(Q => Q.Type == OpenIddictConstants.Claims.Role)?
            .FirstOrDefault(Q => Q.Type == ClaimTypes.Role)?
            .Value;

        

        public string BusinessAreaCode => _httpContextAccessor.HttpContext
           .User
           .Claims
           .FirstOrDefault(Q => Q.Type == "BusinessAreaCode")?
           .Value;

        public string Name => _httpContextAccessor.HttpContext
          .User
          .Claims
          .FirstOrDefault(Q => Q.Type == ClaimTypes.Name)?
          .Value;

        public string UserId => _httpContextAccessor.HttpContext
          .User
          .Claims
          .FirstOrDefault(Q => Q.Type == "UserId")?
          .Value;

        public string TokenExpireDate => _httpContextAccessor.HttpContext
          .User
          .Claims
          .FirstOrDefault(Q => Q.Type == ClaimTypes.Expired)?
          .Value;
    }

}
