﻿using System;
using System.Collections.Generic;
using System.Text;
using Zephyr.Core;
using Zephyr.Web.Areas.Mms.Common;

namespace Zephyr.Models
{
    [Module("Mms")]
    public class mms_draingrooveService : ServiceBase<mms_draingroove>
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
            string sSql = string.Format(@"Select count(*) as countnumber,{0} from mms_drainGroove where {1} is not null  group by {2} ", name, name, name);
            using (var db = Db.Context("Mms"))
            {
                result = db.Sql(sSql).QueryMany<dynamic>();
            }

            return result;

        }
    }

    public class mms_draingroove : ModelBase
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

         public string DrainageCode { get; set; }

         public string Address { get; set; }

         public string Start_stopPoint { get; set; }

         public string Category { get; set; }

         public string Type { get; set; }

         public string Length { get; set; }

         public string StartCode { get; set; }

         public string StopCode { get; set; }

         public string StartElevation { get; set; }

         public string StopElevation { get; set; }

         public string SectionForm { get; set; }

         public string Diameter { get; set; }

         public string Material { get; set; }

         public string Roughness { get; set; }

         public string Structure { get; set; }

         public string InterfaceForm { get; set; }

         public string Slope { get; set; }

         public string OwnershipUnit { get; set; }

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