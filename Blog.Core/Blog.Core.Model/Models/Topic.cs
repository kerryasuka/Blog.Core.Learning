using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// TiBug类别
    /// </summary>
    public class Topic : RootEntity
    {
        public Topic()
        {
            this.TopicDetail = new List<TopicDetail>();
            this.TUpdatetime = DateTime.Now;
        }

        public string TLogo { get; set; }
        public string TName { get; set; }
        public string TDetail { get; set; }
        public string TAuthor { get; set; }
        public string TSectendDetail { get; set; }
        public bool TIsDelete { get; set; }
        public int TRead { get; set; }
        public int TCommend { get; set; }
        public int TGood { get; set; }
        public DateTime TCreatetime { get; set; }
        public DateTime TUpdatetime { get; set; }
        public virtual ICollection<TopicDetail> TopicDetail { get; set; }
    }
}
