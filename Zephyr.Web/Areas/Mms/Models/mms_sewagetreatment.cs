using System;
using System.Collections.Generic;
using System.Text;
using Zephyr.Core;
using Zephyr.Web.Areas.Mms.Common;

namespace Zephyr.Models
{
    [Module("Mms")]
    public class mms_sewagetreatmentService : ServiceBase<mms_sewagetreatment>
    {
        //插入ProjectCode
        protected override bool OnBeforEditMaster(EditEventArgs arg)
        {
            arg.form["ProjectCode"] = MmsHelper.GetCurrentProject();
            return base.OnBeforEditMaster(arg);
        }

    }

    public class mms_sewagetreatment : ModelBase
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

         public string Sequence { get; set; }
         public string OrgX { get; set; }
         public string OrgY { get; set; }

         public string ObjectId { get; set; }

         public string IdentificationCode { get; set; }

         public string DrainageCode { get; set; }

         public string Name { get; set; }

         public string Address { get; set; }

         public string AreaCovered { get; set; }

         public string ServiceScope { get; set; }

         public string PlanningCapacity { get; set; }

         public string ActualCapacity { get; set; }

         public string WaterQuality { get; set; }

         public string DischargeWater { get; set; }
         public string DischargeNumber { get; set; }

         public string DischargeLoacation { get; set; }

         public string OperatingCondition { get; set; }

         public string TreatmentCost { get; set; }

         public string FundsSources { get; set; }

         public string OwnershipUnit { get; set; }

      

         public string FacilityStatus { get; set; }

         public DateTime? CensusTime { get; set; }

         public string DataSources { get; set; }

         public string ReportingUnit { get; set; }

       

         public string Remark { get; set; }

         public string AdministrativeArea { get; set; }

         public string Purpose { get; set; }

        

     }
}