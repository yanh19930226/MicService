using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Identoty.Api.Services.Impletment
{
    public class TestAuthCodeService : IAuthCodeService
    {
        /// <summary>
        /// 测试短信验证码code,可替换成真实的短信验证码平台
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="authCode"></param>
        /// <returns></returns>
        public bool Validate(string phone, string authCode)
        {
            return true;
        }
    }
}
