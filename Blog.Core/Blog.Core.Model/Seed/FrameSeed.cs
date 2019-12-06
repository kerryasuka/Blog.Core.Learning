using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Seed
{
    public class FrameSeed
    {
        /// <summary>
        /// 生成Model层
        /// </summary>
        /// <param name="myContext"></param>
        /// <returns></returns>
        public static bool CreateModels(MyContext myContext)
        {
            try
            {
                myContext.CreateModelByDbTable(@"C:\my-file\Blog.Core.Model", "Blog.Core.Model.Models", new string[] { }, "");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 生成IRepository层
        /// </summary>
        /// <param name="myContext"></param>
        /// <returns></returns>
        public static bool CreateIRepository(MyContext myContext)
        {
            try
            {
                myContext.CreateIRepositoryByDbTable(@"C:\my-file\Blog.Core.IRepository", "Blog.Core.IRepository", new string[] { }, "");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 生成IServices层
        /// </summary>
        /// <param name="myContext"></param>
        /// <returns></returns>
        public static bool CreateIServices(MyContext myContext)
        {
            try
            {
                myContext.CreateIServicesByDbTable(@"C:\my-file\Blog.Core.IServices", "Blog.Core.IServices", new string[] { "Module" }, "");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 生成Repository层
        /// </summary>
        /// <param name="myContext"></param>
        /// <returns></returns>
        public static bool CreateRepository(MyContext myContext)
        {
            try
            {
                myContext.CreateRepositoryByDbTable(@"C:\my-file\Blog.Core.Repository", "Blog.Core.Repository", new string[] { "Module" }, "");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 生成Services层
        /// </summary>
        /// <param name="myContext"></param>
        /// <returns></returns>
        public static bool CreateServices(MyContext myContext)
        {
            try
            {
                myContext.CreateServicesByDbTable(@"C:\my-file\Blog.Core.Services", "Blog.Core.Services", new string[] { "Module" }, "");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
