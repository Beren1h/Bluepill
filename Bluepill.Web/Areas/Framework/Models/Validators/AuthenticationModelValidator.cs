using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bluepill.Web.Gateways;

namespace Bluepill.Web.Areas.Application.Models.Validators
{
    public class AuthenticationModelValidator: AbstractValidator<AuthenticationModel>
    {
        private IAuthenticationGateway _authenticationGateway;
        
        public AuthenticationModelValidator(IAuthenticationGateway authenticationGateway)
        {
            _authenticationGateway = authenticationGateway;
            
            Custom(x => {

                if (!_authenticationGateway.Authenticate(x.UserName, x.Password))
                        return new ValidationFailure("credentials", "invalid credentials");

                return null;
            
            });
                  
        }
    }
}