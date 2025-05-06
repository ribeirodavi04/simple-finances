using Moq;
using SimpleFinances.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TestUtilities.Repositories
{
    public class UnityOfWorkBuilder
    {
        public static IUnityOfWork Build()
        {
            var mock = new Mock<IUnityOfWork>();

            return mock.Object;
        }
    }
}
