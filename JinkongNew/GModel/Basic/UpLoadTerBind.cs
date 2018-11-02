using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GModel.Basic
{
    public class UpLoadTerBind
    {
        public string CarNo { get; set; }
        public string CarType { get; set; }
        public string TerNo { get; set; }
        public string SimCard { get; set; }
        public string TerType { get; set; }

        //新添加
        public string BusinessDivisionId { get; set; }
        public string CarAdminName { get; set; }
        public string CarAdminCardId { get; set; }

        public string CarColor { get; set; }
        public string OwerAddress { get; set; }

        public string OwerPhone { get; set; }

        public string InstallName { get; set; }

        public string InstallAddress { get; set; }
        public string InstallPhone { get; set; }
        public string InstallPlace { get; set; }
        public string InstallTime { get; set; }
        public string EntryName { get; set; }
        public string EntryPhone { get; set; }
        public string ContractNum { get; set; }
        public string SafeOrder { get; set; }
        public string LoanMoney { get; set; }
        public string LoanYear { get; set; }
        public string ServiceYear { get; set; }
        public string CarModel { get; set; }
        public string EngineNumber { get; set; }
        public string CarFrame { get; set; }
        public string Description { get; set; }

        public DateTime TerInnettime { get; set; }

        public List<GModel.Car.FieldValues> FieldValuesList = new List<Car.FieldValues>();
    }
}
