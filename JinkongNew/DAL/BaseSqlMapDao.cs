using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using IBatisNet.Common.Exceptions;
using IBatisNet.Common.Pagination;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Scope;
using IBatisNet.DataMapper.Configuration.Statements;

namespace GDAL
{
    /// <summary>
    /// Summary description for BaseSqlMapDao.
    /// </summary>
    public class BaseSqlMapDao
    {
        #region 属性——设置局部数据显示特征


        #endregion

        /// <summary>
        /// Looks up the parent DaoManager, gets the local transaction
        /// (which should be a SqlMapDaoTransaction) and returns the
        /// SqlMap associated with this DAO.
        /// </summary>
        /// <returns>The SqlMap instance for this DAO.</returns>
        protected ISqlMapper GetLocalSqlMap()
        {
            return SqlMapper.Instance();
        }

        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// Executes the query for a generic object list.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        protected IList<T> ExecuteQueryForList<T>(string statementName, object parameterObject)
        {
            ISqlMapper sqlMap = GetLocalSqlMap();         
                try
                {
                    IMappedStatement im = sqlMap.GetMappedStatement("RealtimeData.SelectCarMonitor");
                    string aa = GetSql("RealtimeData.SelectCarMonitor", parameterObject, im);
                    string bb = aa.Substring(0, 10);
                }
                catch (Exception e)
                { }
                try
                {
                    return sqlMap.QueryForList<T>(statementName, parameterObject);
                }
                catch (Exception)
                {
                    return null;
                }
                

        }

        protected IList<T> ExecuteQueryForListTrans<T>(string statementName, object parameterObject, ISqlMapper sqlMap)
        {
       
                try
                {
                    //IMappedStatement im = sqlMap.GetMappedStatement("DeptInfo.SelectAllChildrenDeptInfo");
                    //string aa = GetSql("DeptInfo.SelectAllChildrenDeptInfo", parameterObject, im);
                    //string bb = aa.Substring(0, 10);
                }
                catch (Exception e)
                { }
                return sqlMap.QueryForList<T>(statementName, parameterObject);
        }





        protected ArrayList ExecuteQueryForList(string statementName, object parameterObject)
        {
            ISqlMapper sqlMap = GetLocalSqlMap();

            try
            {
                return (ArrayList)sqlMap.QueryForList(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for object.  Cause: " + e.Message, e);
            }
        }



        /// <summary>
        /// 获取sql执行的语句
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="paramObject"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public string GetSql(string tag, object paramObject, IMappedStatement p)
        {
            ISqlMapper _sqlMap = GetLocalSqlMap();
            IStatement statement = _sqlMap.GetMappedStatement(tag).Statement;
            RequestScope request = statement.Sql.GetRequestScope(p, paramObject, new SqlMapSession(_sqlMap));
            return request.PreparedStatement.PreparedSql;
        }

        protected IList<T> ExecuteQueryForList<T>(string statementName, object parameterObject, int skipResults, int maxResults)
        {
            ISqlMapper sqlMap = GetLocalSqlMap();
            try
            {
                return sqlMap.QueryForList<T>(statementName, parameterObject, skipResults, maxResults);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        protected ArrayList ExecuteQueryForArrayList(string statementName, object parameterObject)
        {
            ISqlMapper sqlMap = GetLocalSqlMap();
            try
            {
                return (ArrayList)sqlMap.QueryForList(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        protected IList ExecuteQueryForList(string statementName, object parameterObject, int skipResults, int maxResults)
        {
            ISqlMapper sqlMap = GetLocalSqlMap();
            try
            {
                
                return sqlMap.QueryForList(statementName, parameterObject, skipResults, maxResults);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }

        #region Query For a IPaginatedList
        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        protected IPaginatedList ExecuteQueryForPaginatedList(string statementName, object parameterObject, int pageSize)
        {
            ISqlMapper sqlMap = GetLocalSqlMap();
            try
            {
                return sqlMap.QueryForPaginatedList(statementName, parameterObject, pageSize);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for paginated list.  Cause: " + e.Message, e);
            }
        }

        //protected IPaginatedList ExecuteQueryForPaginatedList(string statementName, object parameterObject, int currentPage, int pageSize)
        //{
        //    /* 获得分页后的IPaginatedList 
        //     * 直接传入当前页与页面大小
        //     */
        //    ISqlMapper sqlMap = GetLocalSqlMap();
        //    try
        //    {

        //        return sqlMap.QueryForPaginatedList(statementName, parameterObject, currentPage, pageSize);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new IBatisNetException("Error executing query '" + statementName + "' for paginated list.  Cause: " + e.Message, e);
        //    }
        //}


        //protected IPaginatedList ExecuteQueryForPaginatedList(string statementName, object parameterObject, PageInfo thePageInfo)
        //{
        //    ISqlMapper sqlMap = GetLocalSqlMap();
        //    try
        //    {
        //        return sqlMap.QueryForPaginatedList(statementName, parameterObject, thePageInfo.getCurrentPage(), thePageInfo.getNumPerPage());
        //    }
        //    catch (Exception e)
        //    {
        //        throw new IBatisNetException("Error executing query '" + statementName + "' for paginated list.  Cause: " + e.Message, e);
        //    }
        //}
        #endregion

        /// <summary>
        /// Executes the query for a generic object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statementName">Name of the statement.</param>
        /// <param name="parameterObject">The parameter object.</param>
        /// <returns></returns>
        protected T ExecuteQueryForObject<T>(string statementName, object parameterObject)
        {
            ISqlMapper sqlMap = GetLocalSqlMap();

            try
            {
                return sqlMap.QueryForObject<T>(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for object.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        protected object ExecuteQueryForObject(string statementName, object parameterObject)
        {
            ISqlMapper sqlMap = GetLocalSqlMap();

            try
            {
                IMappedStatement im = sqlMap.GetMappedStatement("TerminalInfoView.SelectTerminalInfoViewByTerNosCount");
                string aa = GetSql("TerminalInfoView.SelectTerminalInfoViewByTerNosCount", parameterObject, im);
                string bb = aa.Substring(0, 10);
            }
            catch (Exception e)
            {

            }

            try
            {
                return sqlMap.QueryForObject(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for object.  Cause: " + e.Message, e);
                //return false;
            }
        }

        protected object ExecuteQueryForObjectTrans(string statementName, object parameterObject,ISqlMapper sqlMap)
        {
            try
            {
                //IMappedStatement im = sqlMap.GetMappedStatement("DeptInfo.SelectMaxCodeDeptInfo");
                //string aa = GetSql("DeptInfo.SelectMaxCodeDeptInfo", parameterObject, im);
                //string bb = aa.Substring(0, 10);
            }
            catch (Exception e)
            { 

            }

            return sqlMap.QueryForObject(statementName, parameterObject);
        }

        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        protected int ExecuteUpdate(string statementName, object parameterObject)
        {
            try
            {
                ISqlMapper sqlMap = GetLocalSqlMap();
                int k =sqlMap.Update(statementName, parameterObject);
                return k;
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for update.  Cause: " + e.Message, e);
            }
        }

        protected int ExecuteUpdateTrans(string statementName, object parameterObject, ISqlMapper sqlMap)
        {
            return sqlMap.Update(statementName, parameterObject);
        }

        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        protected object ExecuteInsert(string statementName, object parameterObject)
        {
            ISqlMapper sqlMap = GetLocalSqlMap();
            //try
            //{
                return sqlMap.Insert(statementName, parameterObject);          
            //}
            //catch (Exception e)
            //{
            //    throw new IBatisNetException("Error executing query '" + statementName + "' for insert.  Cause: " + e.Message, e);
            //}
        }

        protected object ExecuteInsertTrans(string statementName, object parameterObject,ISqlMapper sqlMap)
        {
            return sqlMap.Insert(statementName, parameterObject);
        }

        protected string GetSql(string statementName, object paramObject)
        {
            ISqlMapper mapper = GetLocalSqlMap();
            IMappedStatement statement = mapper.GetMappedStatement(statementName);
            if (!mapper.IsSessionStarted)
            {
                mapper.OpenConnection();
            }
            RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, paramObject, mapper.LocalSession);
            return scope.PreparedStatement.PreparedSql;
        }

        #region Query For a DataSet
        /// <summary>
        /// Query for a DataSet.
        /// </summary>
        /// <param name="statementName">Name of the statement.</param>
        /// <param name="paramObject">The parameter object.</param>
        /// <returns>DataSet.</returns>
        protected DataSet QueryForDataSet(string statementName, object paramObject)
        {
            try
            {
                DataSet ds = new DataSet();
                ISqlMapper mapper = GetLocalSqlMap();
                IMappedStatement statement = mapper.GetMappedStatement(statementName);
                if (!mapper.IsSessionStarted)
                {
                    mapper.OpenConnection();
                }
                RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, paramObject, mapper.LocalSession);
                statement.PreparedCommand.Create(scope, mapper.LocalSession, statement.Statement, paramObject);
                IDbCommand dc = mapper.LocalSession.CreateCommand(scope.IDbCommand.CommandType);
                dc.CommandText = scope.IDbCommand.CommandText;
                
                foreach (IDbDataParameter para in scope.IDbCommand.Parameters)
                {
                    dc.Parameters.Add(para);
                }   
            
                IDbDataAdapter dda = mapper.LocalSession.CreateDataAdapter(dc);
                dda.Fill(ds);

                mapper.CloseConnection();
                
                return ds;
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// 只返回部分符合条件的数据
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="paramObject"></param>
        /// <param name="thePageInfo"></param>
        /// <returns></returns>
        //protected DataSet QueryForDataSet(string statementName, object paramObject, PageInfo thePageInfo)
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        ISqlMapper mapper = GetLocalSqlMap();
        //        IMappedStatement statement = mapper.GetMappedStatement(statementName);
        //        if (!mapper.IsSessionStarted)
        //        {
        //            mapper.OpenConnection();
        //        }
        //        RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, paramObject, mapper.LocalSession);
        //        statement.PreparedCommand.Create(scope, mapper.LocalSession, statement.Statement, paramObject);

        //        ///
        //        /// 生成标准SQL语句
        //        IDbCommand dc = mapper.LocalSession.CreateCommand(scope.IDbCommand.CommandType);
        //        string mySQL = scope.IDbCommand.CommandText;
        //        if (thePageInfo == null)
        //            dc.CommandText = mySQL;
        //        else
        //            dc.CommandText = thePageInfo.GetQueryStringPageSplit(mySQL);

        //        foreach (IDbDataParameter para in scope.IDbCommand.Parameters)
        //        {
        //            dc.Parameters.Add(para);
        //        }
        //        IDbDataAdapter dda = mapper.LocalSession.CreateDataAdapter(dc);
        //        dda.Fill(ds);

        //        return ds;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new IBatisNetException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
        //    }
        //}

        protected DataSet QueryForDataSet(string statementName, object paramObject, int currentPage, int pageSize)
        {
            /* 获得分页后的DataSet 
             * 直接传入当前页与页面大小
             */
            try
            {
                DataSet ds = new DataSet();
                ISqlMapper mapper = GetLocalSqlMap();
                IMappedStatement statement = mapper.GetMappedStatement(statementName);
                if (!mapper.IsSessionStarted)
                {
                    mapper.OpenConnection();
                }
                RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, paramObject, mapper.LocalSession);
                statement.PreparedCommand.Create(scope, mapper.LocalSession, statement.Statement, paramObject);
                IDbCommand dc = mapper.LocalSession.CreateCommand(scope.IDbCommand.CommandType);
                string mySQL = scope.IDbCommand.CommandText;

                /* 拼出分页语句 */
                if (currentPage < 1 ||
                    pageSize < 1)
                    dc.CommandText = mySQL;
                else
                    dc.CommandText = GetPagedSQLForOracle(mySQL, currentPage, pageSize);

                foreach (IDbDataParameter para in scope.IDbCommand.Parameters)
                {
                    dc.Parameters.Add(para);
                }
                IDbDataAdapter dda = mapper.LocalSession.CreateDataAdapter(dc);
                dda.Fill(ds);

                mapper.CloseConnection();

                return ds;
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }

        private string GetPagedSQLForOracle(string sqlSource, int currentPage, int pageSize)
        {
            /* 分页SQL 
             * 追加分页的SQL语句到源语句
             */

            if (string.IsNullOrEmpty(sqlSource))
                return "";

            /* 首末行数据 */
            int startIndex = (currentPage - 1) * pageSize;
            int lastIndex = currentPage * pageSize;

            /* 生成对应的SQL语句 */
            string resultSQL = string.Format(
            @"select * from 
            ( select op_a.*, rownum rn from ({0}) op_a ) op_b
            where op_b.rn > {1} 
            and op_b.rn <= {2} "
            , sqlSource, startIndex, lastIndex);
            return resultSQL;
        }


        #endregion

        #region 查询符合要求的数据行数

        /// <summary>
        /// 返回符合条件的数据行数
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="paramObject"></param>
        /// <returns></returns>
        //protected int QueryForListCount(string statementName, object paramObject)
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        ISqlMapper mapper = GetLocalSqlMap();
        //        IMappedStatement statement = mapper.GetMappedStatement(statementName);
        //        if (!mapper.IsSessionStarted)
        //        {
        //            mapper.OpenConnection();
        //        }
        //        RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, paramObject, mapper.LocalSession);
        //        statement.PreparedCommand.Create(scope, mapper.LocalSession, statement.Statement, paramObject);

        //        ///
        //        /// 生成标准SQL语句
        //        IDbCommand dc = mapper.LocalSession.CreateCommand(scope.IDbCommand.CommandType);
        //        string mySQL = scope.IDbCommand.CommandText;
        //        //PageInfo myPageInfo = new PageInfo();
        //        dc.CommandText = myPageInfo.GetQueryStringListCount(mySQL);

        //        foreach (IDbDataParameter para in scope.IDbCommand.Parameters)
        //        {
        //            dc.Parameters.Add(para);
        //        }
        //        IDbDataAdapter dda = mapper.LocalSession.CreateDataAdapter(dc);
        //        dda.Fill(ds);

        //        ///
        //        /// 返回数据
        //        if (ds.Tables.Count < 1) return 0;
        //        if (ds.Tables[0].Rows.Count < 1) return 0;
        //        return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        //    }
        //    catch (Exception e)
        //    {
        //        throw new IBatisNetException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
        //    }
        //}
        #endregion
    }
}
