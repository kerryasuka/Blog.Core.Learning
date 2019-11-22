using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 博客文章
    /// </summary>
    public class BlogArticle
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int BId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string BSubmitter { get; set; }
        /// <summary>
        /// 标题blog
        /// </summary>
        public string BTitle { get; set; }
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
        /// <summary>
        /// 逻辑删除
        /// </summary>
        public bool? IsDeleted { get; set; }
    }
}
