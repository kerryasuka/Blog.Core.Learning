using Blog.Core.Model;
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
        Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFields);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression, int intPageIndex, int intPageSize, string strOrderByFields);
        Task<List<TEntity>> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFields);
        Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> expression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFields = null);
        Task<List<TResult>> QueryMuch<T1, T2, T3, TResult>(
            Expression<Func<T1, T2, T3, object[]>> joinExpression,
            Expression<Func<T1, T2, T3, TResult>> selectExpression,
            Expression<Func<T1, T2, T3, bool>> whereLambda = null
            ) where T1 : class, new();
    }
}
