////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Web;
////using System.Web.Mvc;

////namespace Car.Controllers
////{
////    public class CompressFilter : Controller
////    {
////        // GET: CompressFilter
////        public ActionResult Index()
////        {
////            return View();
////        }
////    }
////}





//using System.Web;
//using System.Web.Mvc;




//using System;


//public class CacheFilterAttribute : ActionFilterAttribute
//{
//    /// <summary>
//    /// Gets or sets the cache duration in seconds. The default is 10 seconds.
//    /// </summary>
//    /// <value>The cache duration in seconds.</value>
//    public int Duration
//    {
//        get;
//        set;
//    }

//    public CacheFilterAttribute()
//    {
//        Duration = 10;
//    }

//    public override void OnActionExecuted(FilterExecutedContext filterContext)
//    {
//        if (Duration <= 0) return;

//        HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
//        TimeSpan cacheDuration = TimeSpan.FromSeconds(Duration);

//        cache.SetCacheability(HttpCacheability.Public);
//        cache.SetExpires(DateTime.Now.Add(cacheDuration));
//        cache.SetMaxAge(cacheDuration);
//        cache.AppendCacheExtension("must-revalidate, proxy-revalidate");
//    }
//}


//public class CompressFilter : ActionFilterAttribute
//{
//    public override void OnActionExecuting(FilterExecutingContext filterContext)
//    {
//        HttpRequestBase request = filterContext.HttpContext.Request;

//        string acceptEncoding = request.Headers["Accept-Encoding"];

//        if (string.IsNullOrEmpty(acceptEncoding)) return;

//        acceptEncoding = acceptEncoding.ToUpperInvariant();

//        HttpResponseBase response = filterContext.HttpContext.Response;

//        if (acceptEncoding.Contains("GZIP"))
//        {
//            response.AppendHeader("Content-encoding", "gzip");
//            response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
//        }
//        else if (acceptEncoding.Contains("DEFLATE"))
//        {
//            response.AppendHeader("Content-encoding", "deflate");
//            response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
//        }
//    }
//}