using Blog.Core.IRepository.Base;
using Blog.Core.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.IRepository
{
    public interface IGuestBookRepository : IBaseRepository<GuestBook>
    {
    }
}
