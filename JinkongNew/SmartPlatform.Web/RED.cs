using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Redis;
using System.Configuration;
using System.Data;
//using NUnit.Framework;

namespace SuperGPS
{
    public class RED
    {
        //实际上只用了一个IpportString的配置队列
        private string RedisPath = ConfigurationManager.ConnectionStrings["IpportString"].ConnectionString;

        #region -- 连接信息 --
        public PooledRedisClientManager prcm = null;
        //   public  PooledRedisClientManager prcm = CreateManager(new string[] { RedisPath }, new string[] { RedisPath2 });
        public PooledRedisClientManager CreateManager()
        {
            //// 支持读写分离，均衡负载 
            //return new PooledRedisClientManager(readWriteHosts, readOnlyHosts, new RedisClientManagerConfig
            //{
            //    MaxWritePoolSize = 5, // “写”链接池链接数
            //    MaxReadPoolSize  = 5, // “读”链接池链接数
            //    AutoStart = true,
            //});
            prcm = new PooledRedisClientManager(new string[] 
        {
              //如果是Redis集群则配置多个{IP地址:端口号}即可
              //例如: "10.0.0.1:6379","10.0.0.2:6379","10.0.0.3:6379"
            RedisPath
        });
            return prcm;
        }
        #endregion

        #region -- Item --
        /// <summary>
        /// 设置单体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public bool Item_Set<T>(string key, T t)
        {
            try
            {
                using (IRedisClient redis = prcm.GetClient())
                {
                    return redis.Set<T>(key, t, new TimeSpan(1, 0, 0));
                }
            }
            catch (Exception ex)
            {
                // LogInfo
            }
            return false;
        }

        /// <summary>
        /// 获取单体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Item_Get<T>(string key) where T : class
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.Get<T>(key);
            }
        }

        /// <summary>
        /// 移除单体
        /// </summary>
        /// <param name="key"></param>
        public bool Item_Remove(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.Remove(key);
            }
        }

        #endregion

        #region -- List_Table--
        /// <summary>
        /// 添加一个数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void List_Table_Add<T>(T t)
        {
            if (prcm != null)
            {
                using (IRedisClient redis = prcm.GetClient())
                {
                    if (redis != null)
                    {
                        var redisTypedClient = redis.GetTypedClient<T>();
                        redisTypedClient.Store(t);
                    }
                }
            }
        }
        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void List_Table_DeleteAll<T>()
        {
            if (prcm != null)
            {

                using (IRedisClient redis = prcm.GetClient())
                {
                    if (redis != null)
                    {
                        var redisTypedClient = redis.GetTypedClient<T>();
                        if (redisTypedClient.GetAll().Count > 0)
                            redisTypedClient.DeleteAll();
                    }
                }
            }
        }
        /// <summary>
        /// 申请一个Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public long List_Table_GetNextSequence<T>()
        {
            if (prcm != null)
            {
                using (IRedisClient redis = prcm.GetClient())
                {
                    if (redis != null)
                    {
                        var redisTypedClient = redis.GetTypedClient<T>();
                        return redisTypedClient.GetNextSequence();
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="wheres"></param>
        /// <returns></returns>
        public IEnumerable<T> List_Table_GetData<T>(Func<T, bool> wheres)
        {
            if (prcm != null)
            {
                using (IRedisClient redis = prcm.GetClient())
                {
                    if (redis != null)
                    {
                        var redisTypedClient = redis.GetTypedClient<T>();
                        //var userKeyList = redisTypedClient.GetAllKeys();
                        //  string strtt = userKeyList.ToString();

                        //   var listResult = redisTypedClient.GetAll().Where<T>(wheres);
                        var listResult = redisTypedClient.GetAll().Where<T>(wheres);
                        //foreach (T Curt in tlist)
                        //{

                        //}
                        //var at = redisTypedClient.GetValue("29");
                        return listResult;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="start">开始行</param>
        /// <param name="rows">获取数据行数</param>
        /// <param name="rowcount">总行数</param>
        /// <returns></returns>
        public List<T> List_Table_GetData<T>(int pageIndex, int pageSize, ref int rowcount)
        {
            rowcount = 0;
            int start = (pageIndex - 1) * 10 + 1;//从0行开始
            int end = pageIndex * pageSize;

            List<T> returnList = new List<T>();
            if (prcm != null)
            {
                using (IRedisClient redis = prcm.GetClient())
                {
                    if (redis != null)
                    {
                        var redisTypedClient = redis.GetTypedClient<T>();
                        IEnumerable<T> listResult = redisTypedClient.GetAll();
                        T[] tModel = listResult.ToArray<T>();
                        rowcount = listResult.Count();
                        if (end > rowcount)
                        { end = rowcount; }
                        if (listResult != null)
                        {
                            //int List_Count = 0;
                            for (int i = start; i <= end; i++)
                            {
                                returnList.Add(tModel[i]);
                            }
                            //foreach (T t in listResult)
                            //{
                            //    List_Count = List_Count + 1;
                            //    if (List_Count >= start && List_Count <= end)
                            //    {
                            //        returnList.Add(t);
                            //    }
                            //    else if (List_Count > end)
                            //    {
                            //        break;
                            //    }
                            //}
                        }
                    }
                }
            }
            return returnList;
        }
        public List<T> SortedSet_GetList<T>(string key, int pageIndex, int pageSize)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                var list = redis.GetRangeFromSortedSet(key, (pageIndex - 1) * pageSize, pageIndex * pageSize - 1);
                if (list != null && list.Count > 0)
                {
                    List<T> result = new List<T>();
                    foreach (var item in list)
                    {
                        var data = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(item);
                        result.Add(data);
                    }
                    return result;
                }
            }
            return null;
        }
        #endregion

        #region -- List --
        /// <summary>
        /// //////////////////////////////////////////
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        public void List_Add<T>(string key, T t)
        {
            if (prcm != null)
            {
                using (IRedisClient redis = prcm.GetClient())
                {
                    if (redis != null)
                    {
                        //var redisTypedClient = redis.GetTypedClient<T>();

                        ////redisTypedClient.SetItemInList()
                        //redisTypedClient.AddItemToList(redisTypedClient.Lists[key], t);
                        var redisTypedClient = redis.GetTypedClient<T>();
                        redisTypedClient.AddItemToList(redisTypedClient.Lists[key], t);
                    }
                }
            }
        }
        public bool List_Remove<T>(string key, T t)
        {
            if (prcm != null)
            {
                using (IRedisClient redis = prcm.GetClient())
                {
                    if (redis != null)
                    {
                        var redisTypedClient = redis.GetTypedClient<T>();
                        return redisTypedClient.RemoveItemFromList(redisTypedClient.Lists[key], t) > 0;
                    }
                }
            }
            return false;
        }
        public void List_RemoveAll<T>(string key)
        {
            if (prcm != null)
            {
                using (IRedisClient redis = prcm.GetClient())
                {
                    if (redis != null)
                    {
                        var redisTypedClient = redis.GetTypedClient<T>();
                        redisTypedClient.Lists[key].RemoveAll();
                    }
                }
            }
        }

        public int List_Count(string key)
        {
            if (prcm != null)
            {

                using (IRedisClient redis = prcm.GetClient())
                {
                    if (redis != null)
                    {
                        return redis.GetListCount(key);
                    }
                }
            }
            return -1;
        }

        public List<T> List_GetRange<T>(string key, int start, int count)
        {
            if (prcm != null)
            {
                using (IRedisClient redis = prcm.GetClient())
                {
                    if (redis != null)
                    {
                        var c = redis.GetTypedClient<T>();
                        return c.Lists[key].GetRange(start, start + count - 1);
                    }
                }
            }
            return null;
        }


        public List<T> List_GetList<T>(string key)
        {
            if (prcm != null)
            {
                using (IRedisClient redis = prcm.GetClient())
                {
                    if (redis != null)
                    {
                        var c = redis.GetTypedClient<T>();
                        return c.Lists[key].GetRange(0, c.Lists[key].Count);
                    }
                }
            }
            return null;
        }

        public List<T> List_GetList<T>(string key, int pageIndex, int pageSize)
        {
            int start = pageSize * (pageIndex - 1);
            return List_GetRange<T>(key, start, pageSize);
        }

        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datetime"></param>
        public void List_SetExpire(string key, DateTime datetime)
        {
            if (prcm != null)
            {
                using (IRedisClient redis = prcm.GetClient())
                {
                    if (redis != null)
                    {
                        redis.ExpireEntryAt(key, datetime);
                    }
                }
            }
        }
        #endregion

        #region -- Set --
        public void Set_Add<T>(string key, T t)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                var redisTypedClient = redis.GetTypedClient<T>();
                redisTypedClient.Sets[key].Add(t);
            }
        }
        public bool Set_Contains<T>(string key, T t)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                var redisTypedClient = redis.GetTypedClient<T>();
                return redisTypedClient.Sets[key].Contains(t);
            }
        }
        public bool Set_Remove<T>(string key, T t)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                var redisTypedClient = redis.GetTypedClient<T>();
                return redisTypedClient.Sets[key].Remove(t);
            }
        }
        #endregion

        #region -- Hash --
        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool Hash_Exist<T>(string key, string dataKey)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.HashContainsEntry(key, dataKey);
            }
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool Hash_Set<T>(string key, string dataKey, T t)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                string value = ServiceStack.Text.JsonSerializer.SerializeToString<T>(t);
                return redis.SetEntryInHash(key, dataKey, value);
            }
        }
        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool Hash_Remove(string key, string dataKey)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.RemoveEntryFromHash(key, dataKey);
            }
        }
        /// <summary>
        /// 移除整个hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool Hash_Remove(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.Remove(key);
            }
        }
        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public T Hash_Get<T>(string key, string dataKey)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                string value = redis.GetValueFromHash(key, dataKey);
                return ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(value);
            }
        }
        /// <summary>
        /// 获取整个hash的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> Hash_GetAll<T>(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                var list = redis.GetHashValues(key);
                if (list != null && list.Count > 0)
                {
                    List<T> result = new List<T>();
                    foreach (var item in list)
                    {
                        var value = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(item);
                        result.Add(value);
                    }
                    return result;
                }
                return null;
            }
        }
        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datetime"></param>
        public void Hash_SetExpire(string key, DateTime datetime)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                redis.ExpireEntryAt(key, datetime);
            }
        }
        #endregion

        #region -- SortedSet --
        /// <summary>
        ///  添加数据到 SortedSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="score"></param>
        public bool SortedSet_Add<T>(string key, T t, double score)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                string value = ServiceStack.Text.JsonSerializer.SerializeToString<T>(t);
                return redis.AddItemToSortedSet(key, value, score);
            }
        }
        /// <summary>
        /// 移除数据从SortedSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool SortedSet_Remove<T>(string key, T t)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                string value = ServiceStack.Text.JsonSerializer.SerializeToString<T>(t);
                return redis.RemoveItemFromSortedSet(key, value);
            }
        }
        /// <summary>
        /// 修剪SortedSet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="size">保留的条数</param>
        /// <returns></returns>
        public int SortedSet_Trim(string key, int size)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.RemoveRangeFromSortedSet(key, size, 9999999);
            }
        }
        /// <summary>
        /// 获取SortedSet的长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int SortedSet_Count(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.GetSortedSetCount(key);
            }
        }

        /// <summary>
        /// 获取SortedSet的分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        //public List<T> SortedSet_GetList<T>(string key, int pageIndex, int pageSize)
        //{
        //    using (IRedisClient redis = prcm.GetClient())
        //    {
        //        var list = redis.GetRangeFromSortedSet(key, (pageIndex - 1) * pageSize, pageIndex * pageSize - 1);
        //        if (list != null && list.Count > 0)
        //        {
        //            List<T> result = new List<T>();
        //            foreach (var item in list)
        //            {
        //                var data = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(item);
        //                result.Add(data);
        //            }
        //            return result;
        //        }
        //    }
        //    return null;
        //}


        /// <summary>
        /// 获取SortedSet的全部数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<T> SortedSet_GetListALL<T>(string key)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                var list = redis.GetRangeFromSortedSet(key, 0, 9999999);
                if (list != null && list.Count > 0)
                {
                    List<T> result = new List<T>();
                    foreach (var item in list)
                    {
                        var data = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(item);
                        result.Add(data);
                    }
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datetime"></param>
        public void SortedSet_SetExpire(string key, DateTime datetime)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                redis.ExpireEntryAt(key, datetime);
            }
        }

        //public static double SortedSet_GetItemScore<T>(string key,T t)
        //{
        //    using (IRedisClient redis = prcm.GetClient())
        //    {
        //        var data = ServiceStack.Text.JsonSerializer.SerializeToString<T>(t);
        //        return redis.GetItemScoreInSortedSet(key, data);
        //    }
        //    return 0;
        //}
        /// <summary>
        /// 往队列中加入值
        /// </summary>
        /// <param name="listId">主键</param>
        /// <param name="item">项</param>
        public void EnqueueItemOnList(string listId, string item)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                redis.EnqueueItemOnList(listId, item);
            }
        }

        /// <summary>
        /// 往队列中取出值数据
        /// </summary>
        /// <param name="listId">主键</param>
        /// <returns>第一个进入队列的项</returns>
        public string DequeueItemFromList(string listId)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.DequeueItemFromList(listId);
            }
        }

        /// <summary>
        /// 链表中数据容量
        /// </summary>
        /// <param name="listId">主键</param>
        /// <returns></returns>
        public int NameItemCount(string listId)
        {
            using (IRedisClient redis = prcm.GetClient())
            {
                return redis.GetListCount(listId);
            }
        }
        #endregion
    }
}
