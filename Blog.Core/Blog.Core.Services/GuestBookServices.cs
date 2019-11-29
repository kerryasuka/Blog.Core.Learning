using Blog.Core.IRepository;
using Blog.Core.IRepository.UnitOfWork;
using Blog.Core.IServices;
using Blog.Core.Model.Models;
using Blog.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class GuestBookServices : BaseServices<GuestBook>, IGuestBookServices
    {
        private readonly IGuestBookRepository m_Dal;
        private readonly IUnitOfWork m_UnitOfWork;
        private readonly IPasswordLibRepository m_PasswordLibRepository;

        public GuestBookServices(IUnitOfWork unitOfWork, IGuestBookRepository dal, IPasswordLibRepository passwordLibRepository)
        {
            this.m_Dal = dal;
            this.baseDal = dal;
            m_UnitOfWork = unitOfWork;
            m_PasswordLibRepository = passwordLibRepository;
        }

        public async Task<bool> TestTranInRepository()
        {
            try
            {
                Console.WriteLine($"");
                Console.WriteLine($"Begin Transaction");
                m_UnitOfWork.BeginTran();

                var passwords = await m_PasswordLibRepository.Query();
                Console.WriteLine($"First time: the count of password is: {passwords.Count}.");

                Console.WriteLine($"Insert a data into the table PasswordLib now.");
                var insertPassword = await m_PasswordLibRepository.Add(new PasswordLib()
                {
                    IsDeleted = false,
                    PLAccountName = "aaa",
                    PLCreateTime = DateTime.Now
                });

                passwords = await m_PasswordLibRepository.Query(d => d.IsDeleted == false);
                Console.WriteLine($"Second time: the count of passwords is: {passwords.Count}.");

                var guestbooks = await m_Dal.Query();
                Console.WriteLine($"\nFirst time: the count of guestbooks is: {guestbooks.Count}.");

                int ex = 0;
                Console.WriteLine($"\nThere's an exception!");
                int throwEx = 1 / ex;

                Console.WriteLine($"Insert a data into the table Guestbook now.");
                var insertGuestBook = await m_Dal.Add(new GuestBook()
                {
                    UserName = "bbb",
                    BlogId = 1,
                    Createdate = DateTime.Now,
                    IsShow = true,
                });

                guestbooks = await m_Dal.Query();
                Console.WriteLine($"Second time: the count of guestbooks is: {guestbooks.Count}.");

                m_UnitOfWork.CommitTran();
                return true;
            }
            catch (Exception)
            {
                m_UnitOfWork.RollBackTran();
                var passwords = await m_PasswordLibRepository.Query();
                Console.WriteLine($"Third time: the count of passwords is: {passwords.Count}.");

                var guestBooks = await m_Dal.Query();
                Console.WriteLine($"Third time: the count of guestbooks is: {guestBooks.Count}.");
                return false;
            }
        }

        public async Task<bool> TestTranInRepositoryAOP()
        {
            var passwords = await m_PasswordLibRepository.Query();
            Console.WriteLine($"First time: the count of passwords is: {passwords.Count}.");

            Console.WriteLine($"Insert a data into the table PasswordLib now.");
            var insertPassword = await m_PasswordLibRepository.Add(new PasswordLib()
            {
                IsDeleted = false,
                PLAccountName = "aaa",
                PLCreateTime = DateTime.Now,
            });

            passwords = await m_PasswordLibRepository.Query(d => d.IsDeleted == false);
            Console.WriteLine($"Second time: the count of passwords is: {passwords.Count}.");

            var guestbooks = await m_Dal.Query();
            Console.WriteLine($"\nFirst time: the count of guestbooks is: {guestbooks.Count}.");

            int ex = 0;
            Console.WriteLine($"\nThere's an exception!");
            int throwEx = 1 / ex;

            Console.WriteLine($"Insert a data into the table Gestbook now.");
            var insertGuestbook = await m_Dal.Add(new GuestBook()
            {
                UserName = "bbb",
                BlogId = 1,
                Createdate = DateTime.Now,
                IsShow = true,
            });

            guestbooks = await m_Dal.Query();
            Console.WriteLine($"Second time: the count of guestbooks is: {guestbooks.Count}.");

            return true;
        }
    }
}
