using System;
using System.Collections.Generic;
using System.Text;
using Zephyr.Core;
using Zephyr.Web.Areas.Mms.Common;

namespace Zephyr.Models
{
    [Module("Mms")]
    public class mms_floodareaService : ServiceBase<mms_floodarea>
    {
        //插入ProjectCode
        protected override bool OnBeforEditMaster(EditEventArgs arg)
        {
            arg.form["ProjectCode"] = MmsHelper.GetCurrentProject();
            return base.OnBeforEditMaster(arg);
        }

    }

    public class mms_floodarea : ModelBase
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
         public string Sequence { get; set; }

         public string IdentificationCode { get; set; }

         public string Region { get; set; }

         public string Address { get; set; }

         public string Type { get; set; }

         public string ReportedArea { get; set; }

         public string ActualArea { get; set; }

         public string MaximumWaterDepth { get; set; }

         public string Watertime { get; set; }

         public string WaterLong { get; set; }

         public string WaterCause { get; set; }
         public string InfluenceDegree { get; set; }

         public string RemediationMeasures { get; set; }

         public string PlannedCompletionTime { get; set; }

         public string PlanInvestment { get; set; }

         public string OwnershipUnit { get; set; }

         public string FacilityStatus { get; set; }

         public DateTime? CensusTime { get; set; }

         public string DataSources { get; set; }

         public string ReportingUnit { get; set; }

         public string Remark { get; set; }

         public string AdministrativeArea { get; set; }

         public string Purpose { get; set; }

         public string totals { get; set; }

        

     }
}