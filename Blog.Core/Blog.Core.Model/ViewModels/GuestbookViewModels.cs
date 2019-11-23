using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Model.Models;

namespace Blog.Core.Model.ViewModels
{
    /// <summary>
    /// 留言信息展示VM
    /// </summary>
    public class GuestbookViewModels
    {
        /// <summary>
        /// 留言表
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 博客ID
        /// </summary>
        public int? BLogId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createdate { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 留言内容
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// ip地址
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 是否显示在前台，0否1是
        /// </summary>
        public bool Isshow { get; set; }
        public BlogArticle Blogarticle { get; set; }
    }
}
