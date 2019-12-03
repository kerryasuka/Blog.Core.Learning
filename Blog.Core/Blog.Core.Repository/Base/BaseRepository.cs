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

        public async Task<bool> Delete(TEntity model);
        public async Task<bool> DeleteByIds(object[] lstIds);

        public async Task<bool> Update(TEntity model);
        public async Task<bool> Update(TEntity entity, string strWhere);
        public async Task<bool> Update(object operateAnonymousObjects);
        public async Task<bool> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "");

        public async Task<List<TEntity>> Query();
        public async Task<List<TEntity>> Query(string strWhere);
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression);
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression, string strOrderByFields);
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);
        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFields);
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression, int intTop, string strOrderByFields);
        public async Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFields);
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> expression, int intPageIndex, int intPageSize, string strOrderByFields);
        public async Task<List<TEntity>> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFields);
        public async Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> expression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFields = null);
        public async Task<List<TResult>> QueryMuch<T1, T2, T3, TResult>(
            Expression<Func<T1, T2, T3, object[]>> joinExpression,
            Expression<Func<T1, T2, T3, TResult>> selectExpression,
            Expression<Func<T1, T2, T3, bool>> whereLambda = null
            ) where T3 : class, new();
    }
}
