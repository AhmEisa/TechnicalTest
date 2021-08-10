
using Get.Core.Application.Contracts.Persistence;
using GET.Core.Application.Models.Authentication;
using GET.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GET.Infrastructure.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly JwtSettings _jwtSettings;

        public UserRepository(GETDbContext dbContext, IOptions<JwtSettings> jwtSettings) : base(dbContext)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<User> FindByEmailAsync(string email)
        {
            return await _dbContext.User.Include(c => c.UserRoles).ThenInclude(c => c.Role).FirstOrDefaultAsync(c => c.Email == email);
        }

        public AuthenticationResponse AuthenticateAsync(User user)
        {
            JwtSecurityToken jwtSecurityToken = GenerateToken(user);

            AuthenticationResponse response = new AuthenticationResponse
            {
                Id = user.Id.ToString(),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = string.Concat(user.FirstName, " ", user.LastName)
            };

            return response;
        }
        private JwtSecurityToken GenerateToken(User user)
        {
            var roles = user.UserRoles != null && user.UserRoles.Any() ? user.UserRoles.Select(r => r.Role.Name).ToList() : new List<string>();

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString())
            }
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
