using Blog.Core.IRepository;
using Blog.Core.IRepository.UnitOfWork;
using Blog.Core.Model.Models;
using Blog.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Repository
{
    public class GuestBookRepository : BaseRepository<GuestBook>, IGuestBookRepository
    {
        public GuestBookRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
