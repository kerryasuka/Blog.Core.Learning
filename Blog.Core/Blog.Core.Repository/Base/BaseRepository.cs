using Blog.Core.IRepository.Base;
using Blog.Core.IRepository.UnitOfWork;
using Blog.Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Repository.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly ISqlSugarClient m_db;
        private readonly IUnitOfWork m_unitOfWork;

        internal ISqlSugarClient Db
        {
            get { return m_db; }
        }

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            m_unitOfWork = unitOfWork;
            m_db = unitOfWork.GetDbClient();
            //DbContext.Init(BaseDbConfig.ConnectionString, (DbType)BaseDbConfig.DbType);
        }

        public async Task<TEntity> QueryById(object objId)
        {
            //return await Task.Run(() => m_db.Queryable<TEntity>().InSingle(objId));
            return await m_db.Queryable<TEntity>().In(objId).SingleAsync();
        }

        /// <summary>
        /// 根据Id查询一条数据
        /// </summary>
        /// <param name="objId">id(必须指定主键特性[SugarColumn(IsPrimaryKey=true)])，如果是联合主键，请使用where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        public async Task<TEntity> QueryById(object objId, bool blnUseCache = false)
        {
            //return await Task.Run(() => m_db.Queryable<TEntity>().WithCacheIF(blnUseCache).InSingle(objId));
            return await m_db.Queryable<TEntity>().WithCacheIF(blnUseCache).In(objId).SingleAsync();
        }

        /// <summary>
        /// 根据Id查询数据
        /// </summary>
        /// <param name="lstIds">id(必须指定主键特性[SugarColumn(IsPrimaryKey=true)])，如果是联合主键，请使用where条件</param>
        /// <returns>数据实体列表</returns>
        public async Task<List<TEntity>> QueryByIds(object[] lstIds)
        {
            //return await Task.Run(() => m_db.Queryable<TEntity>().In(lstIds).ToList());
            return await m_db.Queryable<TEntity>().In(lstIds).ToListAsync();
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="model">博文实体类</param>
        /// <returns></returns>
        public async Task<int> Add(TEntity model)
        {
            //var i = await Task.Run(() => m_db.Insertable(model).ExecuteReturnBigIdentity());
            ////返回的i是long类型，这里可根据自身业务进行处理
            //return (int)i;

            var insert = m_db.Insertable(model);
            return await insert.ExecuteReturnIdentityAsync();
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="insertColumn">指定只插入列</param>
        /// <returns>返回自增量列</returns>
        public async Task<int> Add(TEntity entity, Expression<Func<TEntity, object>> insertColumns = null)
        {
            var insert = m_db.Insertable(entity);
            if (insertColumns == null)
            {
                return await insert.ExecuteReturnIdentityAsync();
            }
            else
            {
                return await insert.InsertColumns(insertColumns).ExecuteReturnIdentityAsync();
            }
        }

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="listEntity">实体集合</param>
        /// <returns>影响行数</returns>
        public async Task<int> Add(List<TEntity> listEntity)
        {
            return await m_db.Insertable(listEntity.ToArray()).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除指定Id数据
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            //var i = await Task.Run(() => m_db.Deleteable<TEntity>(id).ExecuteCommand());
            //return i > 0;
            return await m_db.Deleteable<TEntity>(id).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 删除指定实体数据
        /// </summary>
        /// <param name="model">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Delete(TEntity entity)
        {
            //var i = await Task.Run(() => m_db.Deleteable(entity).ExecuteCommand());
            //return i > 0;

            return await m_db.Deleteable(entity).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 批量删除指定Id集合的数据
        /// </summary>
        /// <param name="lstIds">主键Id集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] lstIds)
        {
            //var i = await Task.Run(() => m_db.Deleteable<TEntity>().In(lstIds).ExecuteCommand());
            //return i > 0;

            return await m_db.Deleteable<TEntity>().In(lstIds).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity)
        {
            //var i = await Task.Run(() => m_db.Updateable(entity).ExecuteCommand());
            //return i > 0;

            return await m_db.Updateable(entity).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> Update(TEntity entity, string strWhere)
        {
            //return await Task.Run(() => m_db.Updateable(entity).Where(strWhere).ExecuteCommand() > 0);
            return await m_db.Updateable(entity).Where(strWhere).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> Update(string strSql, SugarParameter[] parameters = null)
        {
            //return await Task.Run(() => m_db.Ado.ExecuteCommand(strSql, parameters) > 0);
            return await m_db.Ado.ExecuteCommandAsync(strSql, parameters) > 0;
        }

        public async Task<bool> Update(object operateAnonymousObjects)
        {
            return await m_db.Updateable<TEntity>(operateAnonymousObjects).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 更新某些列
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="lstColumns">列集合</param>
        /// <param name="lstIgnoreColumns">忽略的列集合</param>
        /// <param name="strWhere">筛选条件</param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "")
        {
            //IUpdateable<TEntity> up = await Task.Run(() => m_db.Updateable(entity));
            //if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
            //{
            //    up = await Task.Run(() => up.IgnoreColumns(it => lstIgnoreColumns.Contains(it)));
            //}
            //if (lstColumns != null && lstColumns.Count > 0)
            //{
            //    up = await Task.Run(() => up.UpdateColumns(it => lstColumns.Contains(it)));
            //}
            //if (!string.IsNullOrEmpty(strWhere))
            //{
            //    up = await Task.Run(() => up.Where(strWhere));
            //}
            //return await Task.Run(() => up.ExecuteCommand()) > 0;

            IUpdateable<TEntity> up = m_db.Updateable(entity);
            if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
            {
                up = up.IgnoreColumns(lstIgnoreColumns.ToArray());
            }
            if (lstColumns != null && lstColumns.Count > 0)
            {
                up = up.UpdateColumns(lstColumns.ToArray());
            }
            if (!string.IsNullOrEmpty(strWhere))
            {
                up = up.Where(strWhere);
            }
            return await up.ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query()
        {
            return await m_db.Queryable<TEntity>().ToListAsync();
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="strWhere">数据列表</param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(string strWhere)
        {
            //return await Task.Run(() => m_db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList());
            return await m_db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToListAsync();
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression)
        {
            return await m_db.Queryable<TEntity>().WhereIF(expression != null, expression).ToListAsync();
        }

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <param name="strOrderByFields">排序字段，如name asc，age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression, string strOrderByFields)
        {
            //return await Task.Run(() => m_db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields).WhereIF(expression!= null, expression).ToList());

            return await m_db.Queryable<TEntity>().WhereIF(expression != null, expression).OrderByIF(strOrderByFields != null, strOrderByFields).ToListAsync();
        }

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <param name="orderByExpression">排序字段，如name asc，age desc</param>
        /// <param name="isAsc"></param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            //return await Task.Run(() => m_db.Queryable<TEntity>().OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(expression != null, expression).ToList());
            return await m_db.Queryable<TEntity>().OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(expression != null, expression).ToListAsync();
        }

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="strWhere">条件表达式</param>
        /// <param name="strOrderByFields">排序字段，如name asc，age desc</param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFields)
        {
            //return await Task.Run(() => m_db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList());
            return await m_db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToListAsync();
        }

        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <param name="intTop">取出条数</param>
        /// <param name="strOrderByFields">排序字段，如name asc，age desc</param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression, int intTop, string strOrderByFields)
        {
            //return await Task.Run(() => m_db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields).WhereIF(expression != null, expression).Take(intTop).ToList());
            return await m_db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields).WhereIF(expression != null, expression).Take(intTop).ToListAsync();
        }

        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="strWhere">条件表达式</param>
        /// <param name="intTop">取出条数</param>
        /// <param name="strOrderByFields">排序字段，如name asc，age desc</param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFields)
        {
            //return await Task.Run(() => m_db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList());
            return await m_db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToListAsync();
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
            //return await Task.Run(() => m_db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields).WhereIF(expression != null, expression).ToPageList(intPageIndex, intPageSize));
            return await m_db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields).WhereIF(expression != null, expression).ToPageListAsync(intPageIndex, intPageSize);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="strWhere">条件表达式</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">单页容量</param>
        /// <param name="strOrderByFields">排序字段，如name asc，age desc</param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFields)
        {
            //return await Task.Run(() => m_db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToPageList(intPageIndex, intPageSize));
            return await m_db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToPageListAsync(intPageIndex, intPageSize);
        }

        /// <summary>
        /// 分页查询，当前使用版本
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">单页容量</param>
        /// <param name="strOrderByFields">排序字段，如name asc，age desc</param>
        /// <returns></returns>
        public async Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> expression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFields = null)
        {
            RefAsync<int> totalCount = 0;
            var list = await m_db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFields), strOrderByFields).WhereIF(expression != null, expression).ToPageListAsync(intPageIndex, intPageSize);

            int pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal())).ObjToInt();
            return new PageModel<TEntity>()
            {
                DataCount = totalCount,
                PageCount = pageCount,
                Page = intPageIndex,
                PageSize = intPageSize,
                Data = list,
            };
        }

        /// <summary>
        /// 多表查询
        /// </summary>
        /// <typeparam name="T1">实体1</typeparam>
        /// <typeparam name="T2">实体2</typeparam>
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1, join2) => new object[] { JoinType.Left, join1.UserNo == join2.UserNo } </param>
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id1 = s1.UserNo, Id2 = s2.UserNo } </param>
        /// <param name="whereLambda">查询表达式 (w1, w2) => w1.UserNo == "" </param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryMuch<T1, T2, T3, TResult>(
            Expression<Func<T1, T2, T3, object[]>> joinExpression,
            Expression<Func<T1, T2, T3, TResult>> selectExpression,
            Expression<Func<T1, T2, T3, bool>> whereLambda = null
            ) where T1 : class, new()
        {
            if (whereLambda == null)
            {
                return await m_db.Queryable(joinExpression).Select(selectExpression).ToListAsync();
            }
            return await m_db.Queryable(joinExpression).Where(whereLambda).Select(selectExpression).ToListAsync();
        }
    }
}
