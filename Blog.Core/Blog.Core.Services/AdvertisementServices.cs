using Blog.Core.IRepository;
using Blog.Core.IServices;
using Blog.Core.Model.Models;
using Blog.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Services
{
    public class AdvertisementServices : BaseServices<Advertisement>, IAdvertisementServices
    {
        IAdvertisementRepository m_dal;

        public AdvertisementServices(IAdvertisementRepository dal)
        {
            this.m_dal = dal;
            base.baseDal = dal;
        }

        public void ReturnExp()
        {
            int a = 1;
            int b = 0;

            int c = a / b;
        }
    }
}
