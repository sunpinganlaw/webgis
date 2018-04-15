using System;
using System.Collections.Generic;
using System.Text;
using Zephyr.Core;
using Zephyr.Web.Areas.Mms.Common;

namespace Zephyr.Models
{
    [Module("Mms")]
    public class mms_pumpstationService : ServiceBase<mms_pumpstation>
    {
        //插入ProjectCode
        protected override bool OnBeforEditMaster(EditEventArgs arg)
        {
            arg.form["ProjectCode"] = MmsHelper.GetCurrentProject();
            return base.OnBeforEditMaster(arg);
        }


        public dynamic GetCountByGroupName(string name)
        {
            var result = new List<dynamic>();
            string sSql = string.Format(@"Select count(*) as countnumber,{0} from mms_pumpStation where {1} is not null  group by {2} ", name, name, name);
            using (var db = Db.Context("Mms"))
            {
                result = db.Sql(sSql).QueryMany<dynamic>();
            }

            return result;

        }


    }

    public class mms_pumpstation : ModelBase
     {

         [PrimaryKey]
         public string BillNo { get; set; }

         public string ApproveState { get; set; }
         public string ApprovePerson { get; set; }
         public DateTime? ApproveDate { get; set; }
         public string ApproveRemark { get; set; }
         public string CreatePerson { get; set; }
         public DateTime? CreateDate { get; set; }
         public string UpdatePerson { get; set; }
         public DateTime? UpdateDate { get; set; }
         public string ProjectCode { get; set; }

         public string OrgX { get; set; }
         public string OrgY { get; set; }

         public string X { get; set; }
         public string Y { get; set; }

         public string ObjectId { get; set; }

         public string IdentificationCode { get; set; }

         public string DrainageCode { get; set; }

         public string Name { get; set; }

         public string Address { get; set; }

         public string Category { get; set; }

         public string PumpNumber { get; set; }

         public string RainwaterCapacity { get; set; }

         public string SewageCapacity { get; set; }

         public string MinimumWaterLevel { get; set; }

         public string NurmalWaterLevel { get; set; }
         public string AlarmWaterLevel { get; set; }

         public string AreaCovered { get; set; }

         public string ServiceScope { get; set; }

         public string ServiceArea { get; set; }

         public string RainwaterCapacityNow { get; set; }

         public string SewageCapacityNow { get; set; }

         public string StormPeriod { get; set; }

         public string PowerSupply { get; set; }


         public string InstalledCapacity { get; set; }
         public string DischargeCode { get; set; }
         public string FrontPoolLength { get; set; }
         public string FrontPoolWidth { get; set; }
         public string FrontPoolDepth { get; set; }
      

         public string FacilityStatus { get; set; }

         public DateTime? CensusTime { get; set; }

         public string DataSources { get; set; }

         public string ReportingUnit { get; set; }

         public DateTime? reporttime { get; set; }

         public string Remark { get; set; }

         public string AdministrativeArea { get; set; }

         public string Purpose { get; set; }

        

     }
}