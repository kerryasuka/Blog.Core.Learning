using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 用户留言
    /// </summary>
    public class GuestBook
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 博客Id
        /// </summary>
        public int BlogId { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime Createdate { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// QQ号
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 留言内容
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 是否现在是前台，0否1是
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 博客文章
        /// </summary>
        public BlogArticle BlogArticle { get; set; }
    }
}
