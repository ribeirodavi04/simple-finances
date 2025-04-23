using Moq;
using SimpleFinances.Domain.Entities;
using SimpleFinances.Domain.Services.LoggedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TestUtilities.LoggedUser
{
    public class LoggedUserBuilder
    {
        public static ILoggedUser Build(User user)
        {
            var mock = new Mock<ILoggedUser>();

            mock.Setup(x => x.User()).ReturnsAsync(user);

            return mock.Object;
        }
    }
}
