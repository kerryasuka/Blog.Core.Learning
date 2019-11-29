using Blog.Core.IRepository;
using Blog.Core.IServices;
using Blog.Core.Model.Models;
using Blog.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class TopicDetailServices : BaseServices<TopicDetail>, ITopicDetailServices
    {
        ITopicDetailRepository m_Dal;

        public TopicDetailServices(ITopicDetailRepository dal)
        {
            this.m_Dal = dal;
            base.baseDal = dal;
        }

        public async Task<List<TopicDetail>> GetTopicDetails()
        {
            return await base.Query(a => !a.TdIsDelete && a.TdSectendDetail == "tbug");
        }
    }
}
