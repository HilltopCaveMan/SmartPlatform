using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GBLL.Car;
using GBLL.Basic;
using GModel.Car;

namespace SuperGPS.Controllers
{
    public class TerTypeController : BaseController
    {
        ProductsInfoBLL productsInfoBll = new ProductsInfoBLL();
        // GET: TerType
        public ActionResult Index()
        {
            return View();
        }

        //[OutputCache(CacheProfile = "ActionCacheProfile")]
        public string GetTerTypeList(string TypeMode)
        {
            IList<ProductsInfo> ictlist = null;

            if (CacheHelper.Get("TerTypeList") != null)
            {
                ictlist = (IList<ProductsInfo>)CacheHelper.Get("TerTypeList");
            }
            else
            {
                ProductsInfo pi = new ProductsInfo();
                ictlist = productsInfoBll.GetProductsInfoPage(pi);
                CacheHelper.Insert("TerTypeList", ictlist, 365 * 24 * 60);
                if (TypeMode == "true")
                {
                    ProductsInfo c = new ProductsInfo();
                    c.ProId = "";
                    c.ProModel = "--选择终端型号--";
                    c.ProName = "--选择终端类型--";
                    ictlist.Insert(0, c);
                }
            }

            string json = ConvertToJson(ictlist);
            return json;
        }
    }
}