﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.CustomValidator
{
    public class CustomIdentityValidator:IdentityErrorDescriber
    {

        //IPasswordValidator<AppUSer>
        //IUserValidator<AppUser>
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = "PasswordTooShort",
                Description = $"Parola en az {length} karakter olmalıdır"
            };
        }
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresNonAlphanumeric",
                Description = "Parola Alfanümerik (!.?, vs) bir karakter içermelidir"
            };
        }
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "DuplicateUserName",
                Description=$"İlgili kullanıcı adı ({userName}) sistemde kayıtlı"

            };
        }
    }
}
