using System;

namespace Blog.Core.Model
{
    /// <summary>
    /// 爱、死亡、机器人——蒂姆伯顿
    /// </summary>
    public class Love
    {        
        public virtual string SayLove()
        {
            return "I ❤ You.";
        }
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
    }
}
