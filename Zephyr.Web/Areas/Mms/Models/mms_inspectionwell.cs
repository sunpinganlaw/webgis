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
            string sSql = string.Format(@"Select count(*) as countnumber,{0} from MANHOLE where {1} is not null  group by {2} ", name, name, name);
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

         public string NodeID { get; set; }

         public string Lane_Way { get; set; }
         public string X_Coor { get; set; }

         public string Y_Coor { get; set; }

         public string Junc_Category { get; set; }

         public string Junc_Type { get; set; }

         public string Junc_Style { get; set; }

         public string Depth { get; set; }

         public string Surface_Ele { get; set; }

         public string Anci_Fac { get; set; }

         public string Cov_Material { get; set; }

         public string Cov_Shape { get; set; }

         public string Cov_Dimen1 { get; set; }

         public string Cov_Dimen2 { get; set; }
         public string Chamber_Type { get; set; }

         public string Chamber_Length { get; set; }

         public string Chamber_Width { get; set; }

         public string Chamber_Hight { get; set; }

         public string Survey_WaterDeep { get; set; }

         public string Survey_SediDeep { get; set; }

         public string Bottom_Style { get; set; }

         public string Anti_FallingNet { get; set; }

         public string Anti_Net_Status { get; set; }

         public string Junc_Class { get; set; }

         public string Status { get; set; }
         public string DataSource { get; set; }

         public DateTime? Record_Date { get; set; }

         public string ReportDept { get; set; }

         public DateTime? ReportDate { get; set; }

         public string Remark { get; set; }

         public string AdministrativeArea { get; set; }

         public string Purpose { get; set; }

        

     }
}