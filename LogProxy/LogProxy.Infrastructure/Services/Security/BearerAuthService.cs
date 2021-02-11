using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using LogProxy.Application.Services.Security;
using LogProxy.Domain.Entities.Identity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogProxy.Infrastructure.Services.Security
{
    public class BearerAuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimService _userClaimService;

        public BearerAuthService(UserManager<ApplicationUser> userManager, IUserClaimService userClaimService)
        {
            _userManager = userManager;
            _userClaimService = userClaimService;
        }

        public async Task<string> GenerateJwtToken(string username, string password, string jwtSecret, DateTime expiryDate, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(username).ConfigureAwait(false);
            if (user == null)
                throw new ApplicationException($"User with username: {username} is not found");

            if (!await _userManager.CheckPasswordAsync(user, password).ConfigureAwait(false))
                throw new ApplicationException("Invalid password!");

            var claims = await _userClaimService.GetClaims(user, cancellationToken).ConfigureAwait(false);

            var key = Encoding.ASCII.GetBytes(jwtSecret);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiryDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
