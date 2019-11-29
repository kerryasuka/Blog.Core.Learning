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
    public class TopicServices: BaseServices<Topic>, ITopicServices
    {
        ITopicRepository m_Dal;

        public TopicServices(ITopicRepository dal)
        {
            this.m_Dal = dal;
            base.baseDal = dal;
        }

        public async Task<List<Topic>> GetTopics()
        {
            return await base.Query(a => !a.TIsDelete && a.TSectendDetail == "tbug");
        }
    }
}
