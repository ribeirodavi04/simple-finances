using Microsoft.AspNetCore.Mvc;
using SimpleFinances.API.Filters;

namespace SimpleFinances.API.Attributes
{
    public class AuthenticatedUserAttribute : TypeFilterAttribute
    {
        public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter)) { }
    }
}
