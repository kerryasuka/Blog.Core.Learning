using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Core.IRepository.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> QueryById(object objId);
        Task<TEntity> QueryById(object objId, bool blnUseCache = false);
        Task<List<TEntity>> QueryByIds(object[] lstIds);

        Task<int> Add(TEntity model);

        Task<bool> DeleteById(object id);
        Task<bool> Delete(TEntity model);
        Task<bool> DeleteByIds(object[] lstIds);

        Task<bool> Update(TEntity model);
        Task<bool> Update(TEntity entity, string strWhere);
        Task<bool> Update(object operateAnonymousObjects);
        Task<bool> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "");

        Task<List<TEntity>> Query();
        Task<List<TEntity>> Query(string strWhere);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression, string strOrderByFields);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);
        Task<List<TEntity>> Query(string strWhere, string strOrderByFields);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression, int intTop, string strOrderByFields);
    }
}
