using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.User.Api.Models.Domain
{
    public class User
    {
        public User()
        {
            Properties = new List<UserProperty>();
            UserTags = new List<UserTag>();
        }

        public int Id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public byte Gender { get; set; }
        /// <summary>
        /// 公司电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 省id
        /// </summary>
        public string ProvinceId { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 市id
        /// </summary>
        public string CityId { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 名片地址
        /// </summary>
        public string NameCard { get; set; }
        /// <summary>
        /// 用户属性列表
        /// </summary>
        public List<UserProperty> Properties { get; set; }

        public List<UserTag> UserTags { get; set; }

        //public virtual BPFile BPFile { get; set; }
    }
}
