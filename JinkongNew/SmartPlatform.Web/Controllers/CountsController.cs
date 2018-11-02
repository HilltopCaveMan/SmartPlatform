using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperGPS.Controllers
{
    public class CountsController : Controller
    {
        //
        // GET: /Counts/
        /// <summary>
        /// 未付款车辆
        /// </summary>
        /// <returns></returns>
        public ActionResult UnPayCount()
        {
            return View();
        }
        /// <summary>
        /// 锁车统计
        /// </summary>
        /// <returns></returns>
        public ActionResult LockCarCount()
        {
            return View();
        }
	}
}