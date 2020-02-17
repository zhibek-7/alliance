using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Car.Controllers
{

    [Serializable]
    public class NotModifiedResult : Controller
    {
        // GET: NotModifiedResult
        public ActionResult Index()
        {
            return View();
        }
        //public override void ExecuteResult(ControllerContext context)
        //{
        //    var response = context.HttpContext.Response;
        //    response.StatusCode = 304;
        //    response.StatusDescription = "Not Modified";
        //    response.SuppressContent = true;
        //    return View();
        //}
    }


    public static class CacheStorage
    {

        internal static object Null = new Object();

        public static object Get(string index)
        {
            return HttpContext.Current.Cache.Get(index);
        }

        public static CacheItem<T> Get<T>(string key)
        {
            CacheItem<T> result = new CacheItem<T>
            {
                Cached = false,
                Item = default(T)
            };

            object obj = Get(key);

            if (obj != null)
            {
                result.Cached = true;
                result.Item = obj == CacheStorage.Null ? default(T) : (T)obj;
            }

            return result;
        }

        public static void Put(string key, object value)
        {
            if (value == null)
            {
                value = CacheStorage.Null;
            }

            int expirationMinutes = 2;

            HttpContext.Current.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(expirationMinutes), TimeSpan.Zero);
        }
    }

    public class CacheItem<T>
    {
        public T Item { get; set; }
        public bool Cached { get; set; }
    }

    public static class QueryResultCache
    {

                    public static List<T> FromCache<T>(this IQueryable<T> query,
                    params object[] parameters)
                        {
                            string key = QueryResultCache.GetKey(parameters);

                            List<T> result = CacheStorage.Get(key) as List<T>;

                            if (result == null)
                            {
                                result = query.ToList();
                                CacheStorage.Put(key, result);
                            }

                            return result;
                        }




        public static T FirstOrDefaultFromCache<T>(this IQueryable<T> query,
      params object[] parameters)
        {
            string key = QueryResultCache.GetKey(parameters);

            CacheItem<T> result = CacheStorage.Get<T>(key);

            if (!result.Cached)
            {
                result.Item = query.FirstOrDefault();
                CacheStorage.Put(key, result.Item);
            }

            return result.Item;
        }



        private static string GetKey(object[] parameters)
        {
            return GetMD5(string.Join(string.Empty, parameters.Select(x => x.ToString()).ToArray()));
        }

        private static string GetMD5(string input)
        {
            MD5 md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }




    }