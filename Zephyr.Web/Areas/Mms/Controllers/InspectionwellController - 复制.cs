using Magicodes.ECharts;
using Magicodes.ECharts.Mvc;
using Magicodes.ECharts.Axis;
using Magicodes.ECharts.CommonDefinitions;
using Magicodes.ECharts.Components.Title;
using Magicodes.ECharts.Mvc.Results;
using Magicodes.ECharts.Series;
using Magicodes.ECharts.ValueTypes;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Zephyr.Core;
using Zephyr.Models;
using Zephyr.Web.Areas.Mms.Common;
using Magicodes.ECharts.Series.Mark;
using System.Collections;
using Magicodes.ECharts.Components.ToolTip;


namespace Zephyr.Areas.Mms.Controllers
{

    public class InspectionwellController : Controller
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
                    Category = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "InspectionWellCategory")),
                    Type = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "InspectionWellType")),
                    Form = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "InspectionWellForm")),
                    AdministrativeArea = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "AdministrativeArea")),
                    CoverMaterial = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "InspectionWellCoverMaterial")),
                    CoverSharp = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "InspectionWellCoverSharp")),
                    WellType = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "InspectionWellwelltype")),
                    BottomType = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "InspectionWellBottomType")),
                    AntifallNet = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "AdditionalFacilities")),
                    AntifallNetStatus = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "AdditionalFacilitiesStatus")),
                    WellLevel = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "InspectionWellLevel")),
                    FacilityStatus = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "FacilityStatus"))
                },
                urls = new
                {
                    query = "/api/mms/inspectionwell",
                    querycount = "/api/mms/inspectionwell/getcount",
                    remove = "/api/mms/inspectionwell/",
                    billno = "/api/mms/inspectionwell/getnewbillno",
                    audit = "/api/mms/inspectionwell/audit/",
                    edit = "/mms/inspectionwell/edit/"
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
                    Category = "",
                    Type = "",
                    Form = ""
                   
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
            var data = new InspectionwellApiController().GetEditMaster(id);
            var codeService = new sys_codeService();

            var model = new
            {
                form = data.form,
                scrollKeys = data.scrollKeys,
                urls = new
                {
                    getdetail = "/api/mms/inspectionwell/getdetail/",
                    getmaster = "/api/mms/inspectionwell/geteditmaster/",
                    edit = "/api/mms/inspectionwell/edit/",
                    audit = "/api/mms/inspectionwell/audit/",
                    getrowid = "/mms/inspectionwell/getnewrowid/",
                    report = "inspectionwell"
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
                    Category = codeService.Decode("InspectionWellCategory"),
                    Type = codeService.Decode("InspectionWellType"),
                    Form = codeService.Decode("InspectionWellForm"),
                    AdministrativeArea = codeService.Decode("AdministrativeArea"),
                    CoverMaterial = codeService.Decode("InspectionWellCoverMaterial"),
                    CoverSharp = codeService.Decode("InspectionWellCoverSharp"),
                    WellType = codeService.Decode("InspectionWellwelltype"),
                    BottomType = codeService.Decode("InspectionWellBottomType"),
                    AntifallNet = codeService.Decode("AdditionalFacilities"),
                    AntifallNetStatus = codeService.Decode("AdditionalFacilitiesStatus"),
                    WellLevel = codeService.Decode("InspectionWellLevel"),
                    FacilityStatus = codeService.Decode("FacilityStatus"),
                    warehouseItems = new mms_warehouseService().GetWarehouseItems(currentProject)
                },

                defaultForm = new mms_inspectionwell().Extend(new
                {
                    BillNo = id,
                    OrgX="",
                    OrgY="",
                    IdentificationCode="",
                    DrainageCode="",
                    Address = "",
                    Category = codeService.GetDefaultCodeNew("InspectionWellCategory"),
                    Type = codeService.GetDefaultCodeNew("InspectionWellType"),
                    Form = codeService.GetDefaultCodeNew("InspectionWellForm"),
                    Depth="",
                    GroundElevation="",
                    CoverMaterial = codeService.GetDefaultCodeNew("InspectionWellCoverMaterial"),
                    CoverSharp = codeService.GetDefaultCodeNew("InspectionWellCoverSharp"),
                    CoverSize="",
                    WellType = codeService.GetDefaultCodeNew("InspectionWellwelltype"),
                    WellLength="",
                    WellWidth="",
                    WellDiameter="",
                    WellHeight="",
                    OrgWaterDepth="",
                    OrgDirtDepth="",
                    BottomType = codeService.GetDefaultCodeNew("InspectionWellBottomType"),
                    AntifallNet = codeService.GetDefaultCodeNew("AdditionalFacilities"),
                    AntifallNetStatus = codeService.GetDefaultCodeNew("AdditionalFacilitiesStatus"),
                    WellLevel = codeService.GetDefaultCodeNew("InspectionWellLevel"),
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

        #region 图表展示
        [System.Web.Http.HttpGet]
        public JavaScriptJsonResult Echart(string id)
        {
            string titlename = "";
            var countnumber = new List<object>();
            var countname = new List<object>();
            var codeService = new sys_codeService();
            var getChartService = new mms_inspectionwellService();
            
            List<dynamic> data = getChartService.GetCountByGroupName(id);
            foreach (var result in data)
            {
                foreach (var item in (IDictionary<string, object>)result)
                {
                    if(item.Key.Equals(id))
                    {
                        //codeService.GetTextByCode(item.Value.ToString(), "InspectionWellCategory");
                        if (id == "Category")
                        {
                            countname.Add(codeService.GetTextByCode(item.Value.ToString(), "InspectionWellCategory"));
                            titlename = "检查井类型统计图";
                        }
                        if (id == "AdministrativeArea")
                        {
                            countname.Add(codeService.GetTextByCode(item.Value.ToString(), "AdministrativeArea"));
                            titlename = "检查井区域分布统计图";

                        }
                        if (id == "Type")
                        {
                            countname.Add(codeService.GetTextByCode(item.Value.ToString(), "InspectionWellType"));
                            titlename = "检查井类型统计图";


                        }

                    }
                    if (item.Key.Equals("countnumber"))
                    {
                        countnumber.Add(item.Value.ToString());
                    }
                   
                }

            }
            
           
           
var chartOptions = new EChartsOption
            {
                Title = new Title(titlename) { Left = new AlignValue(Align.center) },
                Tooltip=new Tooltip(),
                Series = new  Series[]
                {
new BarSeries
                    {
                        Name =titlename,
                         Data = countnumber,
                        MarkPoint = new MarkPoint
                        {
                            Data = new List<MarkData>
                            {
new MarkData {Type = MarkPointDataTypes.max, Name = "最大值"},
new MarkData {Type = MarkPointDataTypes.min, Name = "最小值"}
                            }
                        },
                        MarkLine = new MarkLine
                        {
                            Data = new List<MarkData>
                            {
//new MarkData {Type = MarkPointDataTypes.average, Name = "平均值"}
                            }
                        }
                    }
                },
                XAxis = new XAxis[1] { new XAxis { Type = AxisTypes.category, Data = countname } },
                YAxis = new YAxis[1] { new YAxis { Type = AxisTypes.value } }
            };
return this.ToEChartResult(chartOptions);



        }

        #endregion



    }

    public class InspectionwellApiController : ApiController
    {
        // GET api/mms/send/getdoperson
        public List<dynamic> GetDoPerson(string q)
        {
            var SendService = new mms_inspectionwellService();
            var pQuery = ParamQuery.Instance().Select("top 10 DoPerson").AndWhere("DoPerson", q, Cp.StartWithPY);
            return SendService.GetDynamicList(pQuery);
        }

        public  string GetNewBillNo()
        {
            var service = new mms_inspectionwellService();
            return service.GetNewKey("BillNo", "dateplus");
        }



        public  List<dynamic> GetBillNo(string q)
        {
            var service = new mms_inspectionwellService();
            var pQuery = ParamQuery.Instance().Select("top 10 BillNo").AndWhere("BillNo", q, Cp.StartWith);
            return service.GetDynamicList(pQuery);
        }



        // 查询主表：GET api/mms/send
        public dynamic GetCount(RequestWrapper query)
        {
            query.LoadSettingXmlString(@"
<settings >
    <select>
      COUNT(*) AS OrgWaterDepth
    </select>
    <from>
        mms_inspectionwell A
    </from>
    <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
        <field name='BillNo'          cp='equal'      ></field>
        <field name='Address'        cp='like'       ></field>
        <field name='Type'            cp='like'       ></field>
        <field name='Form'            cp='like'      ></field>
        <field name='Depth'           cp='like'      ></field>
        <field name='GroundElevation'        cp='like'  ></field>
    </where>
</settings>");
            var service = new mms_inspectionwellService();
            var pQuery = query.ToParamQuery().AndWhere("A.ProjectCode", MmsHelper.GetCurrentProject());
            return service.GetDynamicListWithPaging(pQuery);
        }




        // 查询主表：GET api/mms/send
        public dynamic Get(RequestWrapper query)
        {
           
            query.LoadSettingXmlString(@"
<settings defaultOrderBy='BillNo'>
    <select>
     A.*
    </select>
    <from>
        mms_inspectionwell A
    </from>
    <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
        <field name='BillNo'          cp='equal'      ></field>
        <field name='Address'        cp='like'       ></field>
        <field name='Type'            cp='like'       ></field>
        <field name='Form'            cp='like'      ></field>
        <field name='Depth'           cp='like'      ></field>
        <field name='GroundElevation'        cp='like'  ></field>
    </where>
</settings>");
            var service = new mms_inspectionwellService();
            var pQuery = query.ToParamQuery().AndWhere("A.ProjectCode", MmsHelper.GetCurrentProject());
            return service.GetDynamicListWithPaging(pQuery);
        }






        public void Delete(string id)
        {
            var service = new mms_inspectionwellService();
            var result = service.Delete(ParamDelete.Instance().AndWhere("BillNo", id));
            MmsHelper.ThrowHttpExceptionWhen(result <= 0, "信息删除失败[BillNo={0}]，请重试或联系管理员！", id);
        }

        [System.Web.Http.HttpPost]
        public void Audit(string id, dynamic data)
        {
          
            var status = data["status"].ToString();
            var comment = data["comment"].ToString();
            var result = new MmsService().Audit(typeof(mms_inspectionwell).Name, id, status, comment);
            MmsHelper.ThrowHttpExceptionWhen(result < 0, "信息审核失败[BillNo={0}]，请重试或联系管理员！", id);
        }


        public dynamic GetEditMaster(string id)
        {
            
            var service = new mms_inspectionwellService();
            return new
            {
                form = service.GetModel(ParamQuery.Instance().AndWhere("BillNo", id)),
                scrollKeys = service.ScrollKeys("BillNo", id, ParamQuery.Instance().AndWhere("ProjectCode", MmsHelper.GetCurrentProject()))
                
            };
        }

        public dynamic GetDetail(string id)
        {
            var ReceiveService = new mms_inspectionwellService();
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

            var service = new mms_inspectionwellService();
            var formWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
<settings>
    <table>mms_inspectionwell</table>
    <where>
        <field name='BillNo' cp='equal'></field>
    </where>
</settings>");

            var result = service.Edit(formWrapper, null, data);
        }


    }

}
