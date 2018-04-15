using System;
using System.Collections.Generic;
using System.Text;
using Zephyr.Core;
using Zephyr.Web.Areas.Mms.Common;

namespace Zephyr.Models
{
    [Module("Mms")]
    public class mms_inspectionwellService : ServiceBase<mms_inspectionwell>
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
            string sSql = string.Format(@"Select count(*) as countnumber,{0} from mms_inspectionWell where {1} is not null  group by {2} ", name, name, name);
            using (var db = Db.Context("Mms"))
            {
                result = db.Sql(sSql).QueryMany<dynamic>();
            }
            
            return result;

        }





    }

     public class mms_inspectionwell : ModelBase
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

         public string Category { get; set; }

         public string Type { get; set; }

         public string Form { get; set; }

         public string Depth { get; set; }

         public string GroundElevation { get; set; }

         public string CoverMaterial { get; set; }

         public string CoverSharp { get; set; }
         public string CoverSize { get; set; }

         public string WellType { get; set; }

         public string WellLength { get; set; }

         public string WellWidth { get; set; }

         public string WellDiameter { get; set; }

         public string WellHeight { get; set; }

         public string OrgWaterDepth { get; set; }

         public string OrgDirtDepth { get; set; }

         public string BottomType { get; set; }

         public string AntifallNet { get; set; }

         public string AntifallNetStatus { get; set; }

         public string WellLevel { get; set; }

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