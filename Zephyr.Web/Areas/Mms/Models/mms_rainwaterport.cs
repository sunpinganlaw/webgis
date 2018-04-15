using System;
using System.Collections.Generic;
using System.Text;
using Zephyr.Core;
using Zephyr.Web.Areas.Mms.Common;

namespace Zephyr.Models
{
    [Module("Mms")]
    public class mms_rainwaterportService : ServiceBase<mms_rainwaterport>
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
            string sSql = string.Format(@"Select count(*) as countnumber,{0} from mms_rainWaterPort where {1} is not null  group by {2} ", name, name, name);
            using (var db = Db.Context("Mms"))
            {
                result = db.Sql(sSql).QueryMany<dynamic>();
            }

            return result;

        }

    }

    public class mms_rainwaterport : ModelBase
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

         public string ObjectId { get; set; }
         public string OrgX { get; set; }

         public string OrgY { get; set; }

         public string X { get; set; }

         public string Y { get; set; }

         public string IdentificationCode { get; set; }

         public string DrainageCode { get; set; }

         public string Address { get; set; }

         public string Form { get; set; }

         public string MaximumDepth { get; set; }

         public string GroundElevation { get; set; }

         public string MaximumFlowCapacity { get; set; }

         public string Width { get; set; }
         public string Length { get; set; }

         public string RainGrateMaterial { get; set; }

         public string RainGrateLength { get; set; }

         public string RainGrateWidth { get; set; }

         public string GarbageInterceptionDevice { get; set; }

         public string DeodorizationDevice { get; set; }

         public string RainwaterInterceptionDevice { get; set; }

         public string RetentionCapacity { get; set; }

        
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