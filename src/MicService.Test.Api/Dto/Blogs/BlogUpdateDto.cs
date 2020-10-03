using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.Test.Api.Dto.Blogs
{
    public class BlogUpdateDto
    {
        /// <summary>
        /// Id
        [Required(ErrorMessage = "{0}不能为空")]
        public long Id { get; set; }
        /// <summary>
        /// Name
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(11, ErrorMessage = "{0}最多{1}个字符")]
        public string Name { get; set; }
        /// <summary>
        /// Url
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(11, ErrorMessage = "{0}最多{1}个字符")]
        public string Url { get; set; }
    }
}
