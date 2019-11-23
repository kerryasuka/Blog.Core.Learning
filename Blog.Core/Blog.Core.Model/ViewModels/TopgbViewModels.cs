using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Model.ViewModels
{
    /// <summary>
    /// 留言排名展示VM
    /// </summary>
    public class TopgbViewModels
    {
        /// <summary>
        /// 博客ID
        /// </summary>
        public int? BlogId { get; set; }
        /// <summary>
        /// 评论数量
        /// </summary>
        public int Counts { get; set; }
        /// <summary>
        /// 博客标题
        /// </summary>
        public string Btitle { get; set; }
    }
}
