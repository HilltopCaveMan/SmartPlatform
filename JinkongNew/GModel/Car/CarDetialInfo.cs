using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GModel.Car;
using System.Data;

namespace GModel.Car
{
     public class CarDetialInfo
     {
         public CarInfo CarInfoData { get; set; }
         public RegistrationInfo Register { get; set; }
         public IList<GetterpositionView> terminalInfo { get; set; }

         public DataTable InstallInfo { get; set; }

         public DataTable CreditInfo { get; set; }

         public DataTable CarOwnerInfo { get; set; }
    }
}
