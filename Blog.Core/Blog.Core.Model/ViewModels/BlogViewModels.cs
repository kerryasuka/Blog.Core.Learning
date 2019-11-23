using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.ViewModels
{
    /// <summary>
    /// 博客信息展示VM
    /// </summary>
    public class BlogViewModels
    {
        /// <summary>
        /// Id
        /// </summary>
        public int BId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string BSubmitter { get; set; }
        /// <summary>
        /// 博客标题
        /// </summary>
        public string BTitle { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Digest { get; set; }
        /// <summary>
        /// 上一篇
        /// </summary>
        public string Previous { get; set; }
        /// <summary>
        /// 上一篇Id
        /// </summary>
        public string PreviousId { get; set; }
        /// <summary>
        /// 下一篇
        /// </summary>
        public string Next { get; set; }
        /// <summary>
        /// 下一篇Id
        /// </summary>
        public string NextId { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string BCategory { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string BContent { get; set; }
        /// <summary>
        /// 访问量
        /// </summary>
        public int BTraffic { get; set; }
        /// <summary>
        /// 评论数量
        /// </summary>
        public int BCommentNum { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime BUpdateTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime BCreateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BRemark { get; set; }
    }
}
