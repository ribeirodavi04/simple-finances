using Microsoft.EntityFrameworkCore;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Domain.Security.Tokens;
using SimpleFinances.Domain.Services.LoggedUser;
using SimpleFinances.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Infrastructure.Services.LoggedUser
{
    public class LoggedUser : ILoggedUser
    {
        private readonly SimpleFinancesDbContext _dbContext;
        private readonly ITokenProvider _tokenProvider;

        public LoggedUser(SimpleFinancesDbContext dbContext, ITokenProvider tokenProvider)
        {
            _dbContext = dbContext;
            _tokenProvider = tokenProvider;
        }

        public async Task<User> User()
        {
            var token = _tokenProvider.Value();

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

            var userIdentifier = Guid.Parse(identifier);

            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.UserGuid == userIdentifier);
        }
    }
}
