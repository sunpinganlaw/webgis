using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Zephyr.Core;
using Zephyr.Models;
using Zephyr.Web.Areas.Mms.Common;

namespace Zephyr.Areas.Mms.Controllers
{
    public class FloodareaController : Controller
    {
     
        // GET: /Mms/Inspectionwell/
        #region 查询页面
        public ActionResult Index()
        {
             var currentProject = MmsHelper.GetCurrentProject();
            var model = new
            {
                dataSource = new
                {
                    warehouseItems = new mms_warehouseService().GetWarehouseItems(currentProject),
                    AdministrativeArea = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "AdministrativeArea")),
                    FacilityStatus = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "FacilityStatus"))
                },
                urls = new
                {
                    query = "/api/mms/floodarea",
                    remove = "/api/mms/floodarea/",
                    billno = "/api/mms/floodarea/getnewbillno",
                    audit = "/api/mms/floodarea/audit/",
                    edit = "/mms/floodarea/edit/"
                },
                resx = new
                {
                    detailTitle = "信息维护",
                    noneSelect = "请先选择条信息！",
                    deleteConfirm = "确定要删除选中的信息吗？",
                    deleteSuccess = "删除成功！",
                    auditSuccess = "信息已审核！"
                },
                form = new
                {
                    IdentificationCode = "",
                    Address = "",
                    AdministrativeArea = "",
                    Type=""
                   

                   
                }
            };
            return View(model);
        }
        #endregion

        #region 编辑页面
        public ActionResult Edit(string id)
        {
            var userName = MmsHelper.GetUserName();
            var currentProject = MmsHelper.GetCurrentProject();
            var data = new FloodareaApiController().GetEditMaster(id);
            var codeService = new sys_codeService();

            var model = new
            {
                form = data.form,
                scrollKeys = data.scrollKeys,
                urls = new
                {
                    getdetail = "/api/mms/floodarea/getdetail/",
                    getmaster = "/api/mms/floodarea/geteditmaster/",
                    edit = "/api/mms/floodarea/edit/",
                    audit = "/api/mms/floodarea/audit/",
                    getrowid = "/mms/floodarea/getnewrowid/"
                },
                resx = new
                {
                    rejected = "已撤消修改！",
                    editSuccess = "保存成功！",
                    auditReject = "信息已取消审核！",
                    auditPassed = "信息已通过审核"
                },
                dataSource = new
                {
                   
                    AdministrativeArea = codeService.Decode("AdministrativeArea"),
                    FacilityStatus = codeService.Decode("FacilityStatus"),
                    warehouseItems = new mms_warehouseService().GetWarehouseItems(currentProject)
                },

                defaultForm = new mms_drainpipe().Extend(new
                {
                    BillNo = id,
                    IdentificationCode="",
                    Region = "",
                    Address = "",
                    Type = "",
                    ReportedArea = "",
                    ActualArea = "",
                    MaximumWaterDepth = "",
                    Watertime = "",
                    WaterLong = "",
                    WaterCause = "",
                    InfluenceDegree = "",
                    RemediationMeasures = "",
                    PlannedCompletionTime = "",
                    PlanInvestment = "",
                    OwnershipUnit="",
                    FacilityStatus = codeService.GetDefaultCodeNew("FacilityStatus"),
                    DataSources = codeService.GetDefaultCodeNew("DataSources"),
                    ReportingUnit="",
                    Remark="",
                    AdministrativeArea = codeService.GetDefaultCodeNew("AdministrativeArea")
                  
                }),
               
                setting = new
                {
                    postFormKeys = new string[] { "BillNo" }
                  
                }
            };

            return View(model);
        }
        #endregion

    }

    public class FloodareaApiController : ApiController
    {
        // GET api/mms/send/getdoperson
        public List<dynamic> GetDoPerson(string q)
        {
            var SendService = new mms_floodareaService();
            var pQuery = ParamQuery.Instance().Select("top 10 DoPerson").AndWhere("DoPerson", q, Cp.StartWithPY);
            return SendService.GetDynamicList(pQuery);
        }

        public  string GetNewBillNo()
        {
            var service = new mms_floodareaService();
            return service.GetNewKey("BillNo", "dateplus");
        }



        public  List<dynamic> GetBillNo(string q)
        {
            var service = new mms_floodareaService();
            var pQuery = ParamQuery.Instance().Select("top 10 BillNo").AndWhere("BillNo", q, Cp.StartWith);
            return service.GetDynamicList(pQuery);
        }



        // 查询主表：GET api/mms/send
        public dynamic Get(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
<settings defaultOrderBy='BillNo'>
    <select>
     A.*,(select SUM (FacilityStatus)  from  mms_floodarea b where b.BillNo=A.BillNo) as totals
    </select>
    <from>
        mms_floodarea A
    </from>
    <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
        <field name='BillNo'          cp='equal'      ></field>
        <field name='Address'        cp='like'       ></field>
        <field name='IdentificationCode'            cp='like'       ></field>
        <field name='OwnershipUnit'            cp='like'       ></field>
      
    </where>
</settings>");
            var service = new mms_floodareaService();
            var pQuery = query.ToParamQuery().AndWhere("A.ProjectCode", MmsHelper.GetCurrentProject());
            return service.GetDynamicListWithPaging(pQuery);
        }

        
        public void Delete(string id)
        {
            var service = new mms_floodareaService();
            var result = service.Delete(ParamDelete.Instance().AndWhere("BillNo", id));
            MmsHelper.ThrowHttpExceptionWhen(result <= 0, "信息删除失败[BillNo={0}]，请重试或联系管理员！", id);
        }

        [System.Web.Http.HttpPost]
        public void Audit(string id, dynamic data)
        {
          
            var status = data["status"].ToString();
            var comment = data["comment"].ToString();
            var result = new MmsService().Audit(typeof(mms_floodarea).Name, id, status, comment);
            MmsHelper.ThrowHttpExceptionWhen(result < 0, "信息审核失败[BillNo={0}]，请重试或联系管理员！", id);
        }


        public dynamic GetEditMaster(string id)
        {

            var service = new mms_floodareaService();
            return new
            {
                form = service.GetModel(ParamQuery.Instance().AndWhere("BillNo", id)),
                scrollKeys = service.ScrollKeys("BillNo", id, ParamQuery.Instance().AndWhere("ProjectCode", MmsHelper.GetCurrentProject()))
                
            };
        }

        public dynamic GetDetail(string id)
        {
            var ReceiveService = new mms_floodareaService();
            var query = RequestWrapper
                .InstanceFromRequest()
                .SetRequestData("CustomerId", id)
                .LoadSettingXmlString(@"
<settings defaultOrderBy='UpdateDate desc'>
    <select>
        A.*
    </select>
    <from>
        psi_customerContract A
    </from>
    <where>
        <field name='CustomerId' cp='equal'></field>
    </where>
</settings>");

            var pQuery1 = query.ToParamQuery();

            query.LoadSettingXmlString(@"
<settings defaultOrderBy='VisitId desc'>
    <select>
        A.*
    </select>
    <from>
        psi_customerVisit A
    </from>
    <where>
        <field name='CustomerId' cp='equal'></field>
    </where>
</settings>");

            var pQuery2 = query.ToParamQuery();
            var result = ReceiveService.GetDynamicListWithPaging(pQuery2);
            return result;
        }

        [System.Web.Http.HttpPost]
        public void Edit(dynamic data)
        {

            var service = new mms_floodareaService();
            var formWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
<settings>
    <table>mms_drainpipe</table>
    <where>
        <field name='BillNo' cp='equal'></field>
    </where>
</settings>");

            var result = service.Edit(formWrapper, null, data);
        }


    }

}
