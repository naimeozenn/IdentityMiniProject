﻿using Identity.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.CustomValidator
{
    public class CustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();


            if (password.ToLower().Contains(user.Name.ToLower()))
            {
                errors.Add(new IdentityError()
                {

                    Code = "Password ContainsUserName",
                    Description="Parola kullanıcı adı içeremez"
                });
            }
            if (errors.Count>0)
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            else
            {
                return Task.FromResult(IdentityResult.Success);
            }
        }
    }
}
