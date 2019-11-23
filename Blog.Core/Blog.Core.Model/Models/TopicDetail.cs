using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// Tibug博文
    /// </summary>
    public class TopicDetail : RootEntity
    {
        public TopicDetail()
        {
            this.TdUpdatetime = DateTime.Now;
        }

        public int TopicId { get; set; }
        public string TdLogo { get; set; }
        public string TdName { get; set; }
        public string TdContent { get; set; }
        public string TdDetail { get; set; }
        public string TdSectendDetail { get; set; }
        public bool TdIsDelete { get; set; }
        public int TdRead { get; set; }
        public int TdCommend { get; set; }
        public int TdGood { get; set; }
        public DateTime TdCreatetime { get; set; }
        public DateTime TdUpdatetime { get; set; }
        public int TdTop { get; set; }
        public string TdAuthor { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
