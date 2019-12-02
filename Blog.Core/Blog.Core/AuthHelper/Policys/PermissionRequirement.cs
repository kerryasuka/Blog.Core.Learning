using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;

namespace Blog.Core.AuthHelper.Policys
{
    /// <summary>
    /// 必要参数类，类似一个订单信息
    /// 继承IAuthorizationRequirement，用于设计自定义权限处理器PermissionHandler
    /// 因为AuthorizationHandler中的泛型参数TRequirement必须继承IAuthorizationRequirement
    /// </summary>
    public class PermissionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="deniedAction">拒绝请求的Url</param>
        /// <param name="permissions">权限集合</param>
        /// <param name="cliamType">声明类型</param>
        /// <param name="issuer">发行人</param>
        /// <param name="audience">订阅人</param>
        /// <param name="signingCredentials">签名验证实体</param>
        /// <param name="expiration">过期时间</param>
        public PermissionRequirement(string deniedAction, List<PermissionItem> permissions, string cliamType, string issuer, string audience, SigningCredentials signingCredentials, TimeSpan expiration)
        {
            this.Permissions = permissions;
            this.DeniedAction = deniedAction;
            this.ClaimType = cliamType;
            this.Issuer = issuer;
            this.Audience = audience;
            this.Expiration = expiration;
            this.SigningCredentials = signingCredentials;
        }

        /// <summary>
        /// 用户权限集合，一个订单包含了很多详情
        /// 同理，一个网站的认证发行中，也有很多权限详情(这里是Role和Url的关系)
        /// </summary>
        public List<PermissionItem> Permissions { get; set; }
        /// <summary>
        /// 无权限Action
        /// </summary>
        public string DeniedAction { get; set; }
        /// <summary>
        /// 认证授权类型
        /// </summary>
        public string ClaimType { internal get; set; }
        /// <summary>
        /// 请求路径
        /// </summary>
        public string LoginPath { get; set; } = "/api/Login";
        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 订阅人
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan Expiration { get; set; }
        /// <summary>
        /// 签名认证
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }
    }
}
