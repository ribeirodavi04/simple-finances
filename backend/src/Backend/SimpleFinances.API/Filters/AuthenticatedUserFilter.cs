using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using SimpleFinances.Communication.Responses;
using SimpleFinances.Domain.Repositories.User;
using SimpleFinances.Domain.Security.Tokens;
using SimpleFinances.Exceptions;
using SimpleFinances.Exceptions.ExceptionsBase;

namespace SimpleFinances.API.Filters
{
    public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
    {
        private readonly IAccessTokenValidator _accessTokenValidator;
        private readonly IUserReadOnlyRepository _userRepository;

        public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator, IUserReadOnlyRepository userRepository)
        {
            _accessTokenValidator = accessTokenValidator;
            _userRepository = userRepository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenOnRequest(context);

                var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);

                var existUser = await _userRepository.ExistActiveUserWithIdentifier(userIdentifier);

                if (!existUser)
                    throw new SimpleFinancesException(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);

            }
            catch (SimpleFinancesException ex) 
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message));
            }
            catch (SecurityTokenExpiredException)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("Token is Expired")
                {
                    TokenExpired = true
                });
            }
            catch
            {
                throw new SimpleFinancesException(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
            }
        }


        private static string TokenOnRequest(AuthorizationFilterContext filterContext)
        {
            var authentication = filterContext.HttpContext.Request.Headers.Authorization.ToString();

            if (string.IsNullOrEmpty(authentication))
                throw new SimpleFinancesException(ResourceMessagesException.NO_TOKEN);

            return authentication["Bearer".Length..].Trim();
        }

   
    }
}
