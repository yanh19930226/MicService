using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Test.Api.Dto
{
    public class TestDto
    {
        /// <summary>
        /// 手机号
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(11, ErrorMessage = "{0}最多{1}个字符")]
        public string Mobile { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(6, ErrorMessage = "{0}最多{1}个字符")]
        public string Code { get; set; }
    }
}
