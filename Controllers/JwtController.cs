using mandiri_project.Entities;
using mandiri_project.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using mandiri_project.Services;
using mandiri_project.RequestResponseModels.Responses;
using NuGet.Protocol.Plugins;
using mandiri_project.Settings;
using mandiri_project.RequestResponseModels.RequestModel.Jwt;
using System.Security.Cryptography;
using mandiri_project.Interfaces;

namespace mandiri_project.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        private AppDbContext _db;
        private readonly UserIdentityService _userIdentityService;
        private readonly IApplicationUser _applicationUser;
        private readonly JwtConfig _jwtTokenConfig;

        public JwtController(AppDbContext db, UserIdentityService userIdentityService, JwtConfig jwtTokenConfig, IApplicationUser applicationUser)
        {
            _db = db;
            _userIdentityService = userIdentityService;
            _jwtTokenConfig = jwtTokenConfig;
            _applicationUser = applicationUser;
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            // Validate user credentials (usually against a database)
            var user = await _db.ApplicationUsers
                .Where(q => q.Email.ToLower() == _userIdentityService.Email.ToLower())
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return Unauthorized();
            }

            user.Token = "";
            user.TokenExpireDate = DateTime.Now.AddDays(-1);
            user.ModifiedOn = DateTime.Now;
            user.ModifiedBy = user.UserId;

            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login( JwtLoginRequest request)
        {
            // Validate user credentials (usually against a database)
            var user = await _db.ApplicationUsers
                .Where(q => q.Email.ToLower() == request.Email.ToLower())
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return Unauthorized();
            }

            if (!_applicationUser.VerifyPassword(request.Password, user.Password))
            {
                return Problem(
                          title: "Wrong Password.",
                          detail: $"Password Salah.",
                          statusCode: StatusCodes.Status401Unauthorized,
                          instance: HttpContext.Request.Path
                      );
            }

            // Generate JWT token
            if (user.TokenExpireDate > DateTime.Now)
                return Ok(new { Token = user.Token});

            var token = await GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfig.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiredDate = DateTime.Now.AddMinutes(Convert.ToDouble(_jwtTokenConfig.AccessTokenExpiration));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Expired, expiredDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new Claim("BusinessAreaCode", user.BusinessAreaCode),
                new Claim("UserId", user.UserId),
            };

            var token = new JwtSecurityToken(
                issuer: _jwtTokenConfig.Issuer,
                audience: _jwtTokenConfig.Audience,
                claims: claims,
                expires: expiredDate,
                signingCredentials: credentials
            );

            #region httpContext
            var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(_jwtTokenConfig.AccessTokenExpiration),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                IssuedUtc = DateTime.Now
                // The time at which the authentication ticket was issued.

               // RedirectUri = "/Home/"
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            #endregion

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
            authProperties);

            var tokenToResult = new JwtSecurityTokenHandler().WriteToken(token);
            HttpContext.Session.SetString("Token", tokenToResult );

            user.Token = tokenToResult;
            user.TokenExpireDate = expiredDate;
            user.ModifiedOn = DateTime.Now;
            user.ModifiedBy = user.UserId;

            await _db.SaveChangesAsync();

            return tokenToResult;
        }

    }
    
}