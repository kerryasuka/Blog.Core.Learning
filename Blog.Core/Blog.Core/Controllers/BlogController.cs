using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/blog")]
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class BlogController : ControllerBase
    {
        /// <summary>
        /// Sum接口
        /// </summary>
        /// <param name="i">参数i</param>
        /// <param name="j">参数j</param>
        /// <returns></returns>
        [HttpGet]
        public int Get(int i, int j)
        {
            return i + j;
        }
    }
}
