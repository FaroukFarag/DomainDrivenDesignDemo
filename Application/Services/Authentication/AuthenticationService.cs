﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationResult Login(string email, string password)
        {
            return new AuthenticationResult(
                Guid.NewGuid(),
                "firstName",
                "lastname",
                email,
                "token");
        }

        public AuthenticationResult Register(string firstName, string lastname, string email, string password)
        {
            return new AuthenticationResult(
                Guid.NewGuid(),
                firstName,
                lastname,
                email,
                "token");
        }
    }
}
