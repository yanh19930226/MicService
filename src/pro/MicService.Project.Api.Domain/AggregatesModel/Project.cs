using MicService.Project.Api.Domain.Events;
using MicService.Project.Api.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicService.Project.Api.Domain.AggregatesModel
{
    public class Project : Entity, IAggregateRoot
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 项目logo
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 原始BP文件
        /// </summary>
        public string OriginBPFile { get; set; }
        /// <summary>
        /// 转化后BP文件
        /// </summary>
        public string FormatBPFile { get; set; }
        /// <summary>
        /// 是否显示敏感信息
        /// </summary>
        public bool ShowSecurityInfo { get; set; }
        /// <summary>
        /// 省Id
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 市Id
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 区域Id
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 成立时间
        /// </summary>
        public DateTime RegisterTime { get; set; }
        /// <summary>
        /// 融资金额
        /// </summary>
        public int FinMoney { get; set; }
        /// <summary>
        /// 收入
        /// </summary>
        public int Income { get; set; }
        /// <summary>
        /// 利润
        /// </summary>
        public int Revenue { get; set; }
        /// <summary>
        /// 估值
        /// </summary>
        public int Valuation { get; set; }
        /// <summary>
        /// 佣金分配方式
        /// </summary>
        public int BrokerageOptions { get; set; }
        /// <summary>
        /// 是否委托平台
        /// </summary>
        public bool OnPlatform { get; set; }
        /// <summary>
        /// 可见范围
        /// </summary>
        public virtual ProjectVisibleRule VisibleRule { get; set; }
        /// <summary>
        /// 根Id
        /// </summary>
        public int SourceId { get; set; }
        /// <summary>
        /// 上级引用Id
        /// </summary>
        public int ReferenceId { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// 项目属性
        /// </summary>
        public virtual List<ProjectProperty> Properties { get; set; } = new List<ProjectProperty>();
        /// <summary>
        /// 查看者
        /// </summary>
        public virtual List<ProjectViewer> Viewers { get; set; } = new List<ProjectViewer>();
        /// <summary>
        /// 贡献者
        /// </summary>
        public virtual List<ProjectContributor> Contributors { get; set; } = new List<ProjectContributor>();

        public Project()
        {
            this.AddDomainEvent(new ProjectCreatedEvent { Project = this });
        }
        /// <summary>
        /// 添加项目查看者
        /// </summary>
        /// <param name="viewer"></param>
        public void AddView(int userId, string userName, string avatar)
        {
            var viewer = new ProjectViewer()
            {
                UserId = userId,
                UserName = userName,
                Avatar = avatar,
                CreateTime = DateTime.Now
            };
            if (!Viewers.Any(v => v.UserId == userId))
            {
                Viewers.Add(viewer);
            }
            AddDomainEvent(new ProjectViewedEvent { Viewer = viewer });
        }
        /// <summary>
        /// 添加项目贡献者
        /// </summary>
        /// <param name="contributor"></param>
        public void AddContributor(ProjectContributor contributor)
        {
            if (!Contributors.Any(v => v.UserId == contributor.UserId))
            {
                Contributors.Add(contributor);
            }
            AddDomainEvent(new ProjectJoinedEvent { Contributor = contributor });
        }
    }
}
