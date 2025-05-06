using Bogus;
using Common.TestUtilities.Cryptography;
using SimpleFinances.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TestUtilities.Entities
{
    public class UserBuilder
    {

        public User Build()
        {
            var passwordEncripter = PasswordEncripterBuilder.Build();

            var password = new Faker().Internet.Password();

            var user = new Faker<User>()
                .RuleFor(user => user.UserId, f => f.IndexFaker)
                .RuleFor(user => user.UserGuid, f => Guid.NewGuid())
                .RuleFor(user => user.UserName, f => f.Internet.UserName())
                .RuleFor(user => user.Name, f => f.Person.FullName)
                .RuleFor(user => user.Email, f => f.Internet.Email())
                .RuleFor(user => user.Password, f => passwordEncripter.Encrypt(password))
                .RuleFor(user => user.ProfilePhotoURL, f => "");

            return user;
        }
    }
}
