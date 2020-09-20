using IdentityServer4.Models;
using IdentityServer4.Validation;
using MicService.Identoty.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MicService.Identoty.Api.Autentication
{
    public class SmsAuthCodeValidator : IExtensionGrantValidator
    {
        public string GrantType => "sms_auth_code";

        private readonly IAuthCodeService _authCodeService;
        private readonly IUserService _userService;
        public SmsAuthCodeValidator(IAuthCodeService authCodeService, IUserService userService)
        {
            _authCodeService = authCodeService;
            _userService = userService;
        }
        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var phone = context.Request.Raw["phone"];
            var code = context.Request.Raw["auth_code"];
            var ErrorResult = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(code))
            {
                context.Result = ErrorResult;
                return;
            }

            if (!_authCodeService.Validate(phone, code))
            {
                context.Result = ErrorResult;
                return;
            }
            var userindentity = await _userService.CheckOrCreate(phone);
            if (userindentity !=null)
            {
                context.Result = ErrorResult;
                return;
            }

            var claims = new Claim[] {
                new Claim("name",userindentity.Name??string.Empty),
                new Claim("avatar",userindentity.Avatar??string.Empty),
                new Claim("title",userindentity.Title??string.Empty),
                new Claim("company",userindentity.Company??string.Empty),
            };
            context.Result = new GrantValidationResult(userindentity.UserId.ToString(), GrantType, claims);
        }
    }
}
