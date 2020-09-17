using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Identoty.Api.Services
{
    public interface IAuthCodeService
    {
        /// <summary>
        /// 根据手机号验证码验证
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="authCode">验证码</param>
        /// <returns></returns>
        bool Validate(string phone, string authCode);
    }
}
