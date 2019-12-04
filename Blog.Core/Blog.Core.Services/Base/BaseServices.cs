using Blog.Core.IRepository.Base;
using Blog.Core.IServices.Base;
using Blog.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Services.Base
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {
        //public IBaseRepository<TEntity> baseDal = new BaseRepository<TEntity>();
        //通过在子类的构造函数中注入，这里是基类，不用构造函数
        public IBaseRepository<TEntity> baseDal;

        /// <summary>
        /// 根据Id查询一条数据
        /// </summary>
        /// <param name="objId">Id(必须指定主键特性[SugarColumn(IsPrimaryKey=true)])，如果是联合主键，请使用where条件</param>
        /// <returns>数据实体</returns>
        public async Task<TEntity> QueryById(object objId)
        {
            return await baseDal.QueryById(objId);
        }

        /// <summary>
        /// 根据Id查询一条数据
        /// </summary>
        /// <param name="objId">Id(必须指定主键特性[SugarColumn(IsPrimaryKey=true)])，如果是联合主键，请使用where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        public async Task<TEntity> QueryById(object objId, bool blnUseCache = false)
        {
            return await baseDal.QueryById(objId, blnUseCache);
        }

        /// <summary>
        /// 根据Id查询数据
        /// </summary>
        /// <param name="lstIds">Id(必须指定主键特性[SugarColumn(IsPrimaryKey=true)])，如果是联合主键，请使用where条件</param>
        /// <returns>数据实体列表</returns>
        public async Task<List<TEntity>> QueryByIds(object[] lstIds)
        {
            return await baseDal.QueryByIds(lstIds);
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="">博文实体类</param>
        /// <returns></returns>
        public async Task<int> Add(TEntity entity)
        {
            return await baseDal.Add(entity);
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity)
        {
            return await baseDal.Update(entity);
        }

        public async Task<bool> Update(TEntity entity, string strWhere)
        {
            return await baseDal.Update(entity, strWhere);
        }

        public async Task<bool> Update(object operateAnonymousObjects)
        {
            return await baseDal.Update(operateAnonymousObjects);
        }

        public async Task<bool> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "")
        {
            return await baseDal.Update(entity, lstColumns, lstIgnoreColumns, strWhere);
        }

        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Delete(TEntity entity)
        {
            return await baseDal.Delete(entity);
        }

        /// <summary>
        /// 删除指定Id的一条数据
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            return await baseDal.DeleteById(id);
        }

        /// <summary>
        /// 删除指定Id集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键Id集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await baseDal.DeleteByIds(ids);
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query()
        {
            return await baseDal.Query();
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await baseDal.Query(strWhere);
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression)
        {
            return await baseDal.Query(expression);
        }

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <param name="orderByExpression">排序字段，如name asc，age desc</param>
        /// <param name="isAsc">数据列表</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await baseDal.Query(expression, orderByExpression, isAsc);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression, string strOrderByFields)
        {
            return await baseDal.Query(expression, strOrderByFields);
        }

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFields">排序字段，如name asc，age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFields)
        {
            return await baseDal.Query(strWhere, strOrderByFields);
        }

        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <param name="intTop">取出条数</param>
        /// <param name="strOrderByFields">排序字段，如name asc，age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression, int intTop, string strOrderByFields)
        {
            return await baseDal.Query(expression, intTop, strOrderByFields);
        }

        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intTop">取出条数</param>
        /// <param name="strOrderByFields">排序字段，如name asc，age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFields)
        {
            return await baseDal.Query(strWhere, intTop, strOrderByFields);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">单页容量</param>
        /// <param name="strOrderByFields">排序字段，如name asc，age desc</param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression, int intPageIndex, int intPageSize, string strOrderByFields)
        {
            return await baseDal.Query(expression, intPageIndex, intPageSize, strOrderByFields);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">单页容量</param>
        /// <param name="strOrderByFields">排序字段，如name asc，age desc</param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFields)
        {
            return await baseDal.Query(strWhere, intPageIndex, intPageSize, strOrderByFields);
        }

        public async Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> expression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFields = null)
        {
            return await baseDal.QueryPage(expression, intPageIndex, intPageSize, strOrderByFields);
        }

        public async Task<List<TResult>> QueryMuch<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, object[]>> joinExpression, Expression<Func<T1, T2, T3, TResult>> selectExpression, Expression<Func<T1, T2, T3, bool>> whereLambda = null) where T1 : class, new()
        {
            return await baseDal.QueryMuch(joinExpression, selectExpression, whereLambda);
        }
    }
}
