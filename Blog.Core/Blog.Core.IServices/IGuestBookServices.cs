using Blog.Core.IServices.Base;
using Blog.Core.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.IServices
{
    public interface IGuestBookServices : IBaseServices<GuestBook>
    {
        Task<bool> TestTranInRepository();
        Task<bool> TestTranInRepositoryAOP();
    }
}
