using AutoMapper;
using Blog.Core.IRepository;
using Blog.Core.IServices;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels;
using Blog.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class BlogArticleServices : BaseServices<BlogArticle>, IBlogArticleServices
    {
        IBlogArticleRepository m_dal;
        IMapper m_mapper;

        public BlogArticleServices(IBlogArticleRepository dal, IMapper mapper)
        {
            this.m_dal = dal;
            base.baseDal = dal;
            this.m_mapper = mapper;
        }

        /// <summary>
        /// 获取视图博客详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BlogViewModels> GetBlogDetails(int id)
        {
            var blogList = await base.Query(a => a.IsDeleted == false, a => a.BId);
            var blogArticle = (await base.Query(a => a.BId == id)).FirstOrDefault();

            BlogViewModels models = null;

            if (blogArticle != null)
            {
                BlogArticle prevBlog;
                BlogArticle nextBlog;

                int blogIndex = blogList.FindIndex(index => index.BId == id);
                if (blogIndex >= 0)
                {
                    try
                    {
                        prevBlog = blogIndex > 0 ? blogList[blogIndex - 1] : null;
                        nextBlog = blogIndex + 1 < blogList.Count() ? blogList[blogIndex + 1] : null;

                        models = m_mapper.Map<BlogViewModels>(blogArticle);

                        if(nextBlog != null)
                        {
                            models.Next = nextBlog.BTitle;
                            models.NextId = nextBlog.BId;
                        }

                        if(prevBlog != null)
                        {
                            models.Previous = prevBlog.BTitle;
                            models.PreviousId = prevBlog.BId;
                        }

                        var entity2ViewModel = m_mapper.Map<BlogArticle>(models);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }

                blogArticle.BTraffic += 1;
                //await base.Update(blogArticle, new List<string> { "BTraffic" });
            }

            return models;
        }

        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<BlogArticle>> GetBlogs()
        {
            var blogList = await base.Query(a => a.BId > 0, a => a.BId);
            return blogList;
        }
    }
}
