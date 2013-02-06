using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation.Results;
using Web.Gateways;

namespace Web.Areas.Authentication.Models.Validators
{
    public class SignOnModelValidator : AbstractValidator<SignOnModel>
    {
        private IAuthenticationGateway _authenticationGateway;
        
        public SignOnModelValidator(IAuthenticationGateway authenticationGateway)
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