using System;
using System.Collections.Generic;
using System.Text;
using Zephyr.Core;
using Zephyr.Web.Areas.Mms.Common;

namespace Zephyr.Models
{
    [Module("Mms")]
    public class mms_realdataService : ServiceBase<mms_realdata>
    {
        //插入ProjectCode
        protected override bool OnBeforEditMaster(EditEventArgs arg)
        {
            arg.form["ProjectCode"] = MmsHelper.GetCurrentProject();
            return base.OnBeforEditMaster(arg);
        }

    }

    public class mms_realdata : ModelBase
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

         public string IdentificationCode { get; set; }

         public string DrainageCode { get; set; }

         public string RiversName { get; set; }

         public string Category { get; set; }

         public string Name { get; set; }

         public string GateOpening1 { get; set; }

         public string GateOpening2 { get; set; }

         public string GateOpening3 { get; set; }

         public string GateOpening4 { get; set; }

         public string GateOpening5 { get; set; }

         public string GateOpening6 { get; set; }

         public string GateOpening7 { get; set; }

         public string GateOpening8 { get; set; }

         public string UpstreamWaterLevel { get; set; }
         public string DownstreamWaterLevel { get; set; }

         public string ControlIP { get; set; }

         public string VideoIP { get; set; }

         public string Rainfall { get; set; }

         public string RiverWaterLevel { get; set; }

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