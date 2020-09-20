using MicService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Identoty.Api.Services
{
    public interface IUserService
    {
        /// <summary>
        /// 检查手机号是否存在不存在就创建
        /// </summary>
        /// <param name=""></param>
        Task<UserIdentity> CheckOrCreate(string phone);
    }
}
