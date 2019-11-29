using Blog.Core.IRepository;
using Blog.Core.IServices;
using Blog.Core.Model.Models;
using Blog.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Services
{
    public class PasswordLibServices : BaseServices<PasswordLib>, IPasswordLibServices
    {
        IPasswordLibRepository m_Dal;

        public PasswordLibServices(IPasswordLibRepository dal)
        {
            this.m_Dal = dal;
            base.baseDal = dal;
        }
    }
}
