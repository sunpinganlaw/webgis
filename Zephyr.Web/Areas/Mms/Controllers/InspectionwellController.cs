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
                    Junc_Category = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "InspectionWellCategory")),
                    Junc_Type = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "InspectionWellType")),
                    Junc_Style = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "InspectionWellForm")),
                    AdministrativeArea = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "AdministrativeArea")),
                    Cov_Material = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "InspectionWellCoverMaterial")),
                    Cov_Shape = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "InspectionWellCoverSharp")),
                    Chamber_Type = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "InspectionWellwelltype")),
                    Bottom_Style = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "InspectionWellBottomType")),
                    Anti_FallingNet = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "AdditionalFacilities")),
                    Anti_Net_Status = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "AdditionalFacilitiesStatus")),
                    Junc_Class = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "InspectionWellLevel")),
                    Status = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "FacilityStatus"))
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
                    NodeID = "",
                    Lane_Way = "",
                    AdministrativeArea = "",
                    Junc_Category = "",
                    Junc_Type = "",
                    Junc_Style = ""
                   
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
                    Junc_Category = codeService.Decode("InspectionWellCategory"),
                    Junc_Type = codeService.Decode("InspectionWellType"),
                    Junc_Style = codeService.Decode("InspectionWellForm"),
                    AdministrativeArea = codeService.Decode("AdministrativeArea"),
                    Cov_Material = codeService.Decode("InspectionWellCoverMaterial"),
                    Cov_Shape = codeService.Decode("InspectionWellCoverSharp"),
                    Chamber_Type = codeService.Decode("InspectionWellwelltype"),
                    Bottom_Style = codeService.Decode("InspectionWellBottomType"),
                    Anti_FallingNet = codeService.Decode("AdditionalFacilities"),
                    Anti_Net_Status = codeService.Decode("AdditionalFacilitiesStatus"),
                    Junc_Class = codeService.Decode("InspectionWellLevel"),
                    Status = codeService.Decode("FacilityStatus"),
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
                    Junc_Category = codeService.GetDefaultCodeNew("InspectionWellCategory"),
                    Junc_Type = codeService.GetDefaultCodeNew("InspectionWellType"),
                    Junc_Style = codeService.GetDefaultCodeNew("InspectionWellForm"),
                    Depth="",
                    GroundElevation="",
                    Cov_Material = codeService.GetDefaultCodeNew("InspectionWellCoverMaterial"),
                    Cov_Shape = codeService.GetDefaultCodeNew("InspectionWellCoverSharp"),
                    CoverSize="",
                    Chamber_Type = codeService.GetDefaultCodeNew("InspectionWellwelltype"),
                    WellLength="",
                    WellWidth="",
                    WellDiameter="",
                    WellHeight="",
                    OrgWaterDepth="",
                    OrgDirtDepth="",
                    Bottom_Style = codeService.GetDefaultCodeNew("InspectionWellBottomType"),
                    Anti_FallingNet = codeService.GetDefaultCodeNew("AdditionalFacilities"),
                    Anti_Net_Status = codeService.GetDefaultCodeNew("AdditionalFacilitiesStatus"),
                    Junc_Class = codeService.GetDefaultCodeNew("InspectionWellLevel"),
                    Status = codeService.GetDefaultCodeNew("FacilityStatus"),
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
                    if (item.Key.Equals(id))
                    {
                        //codeService.GetTextByCode(item.Value.ToString(), "InspectionWellCategory");
                        if (id == "Junc_Category")
                        {
                            countname.Add(codeService.GetTextByCode(item.Value.ToString(), "InspectionWellCategory"));
                            titlename = "检查井类型统计图";
                        }
                        if (id == "AdministrativeArea")
                        {
                            countname.Add(codeService.GetTextByCode(item.Value.ToString(), "AdministrativeArea"));
                            titlename = "检查井区域分布统计图";

                        }
                        if (id == "Junc_Type")
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
                            Tooltip = new Tooltip(),
                            Series = new Series[]
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
            var pQuery = ParamQuery.Instance().Select("top 10 DoPerson").AndWhere("DoPerson", q, Cp.StartWithPY).From("MANHOLE");
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
            var pQuery = ParamQuery.Instance().Select("top 10 BillNo").AndWhere("BillNo", q, Cp.StartWith).From("MANHOLE"); ;
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
        MANHOLE A
    </from>
     <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
        <field name='BillNo'          cp='equal'      ></field>
        <field name='Lane_Way'        cp='like'       ></field>
        <field name='Junc_Type'            cp='like'       ></field>
        <field name='Junc_Style'            cp='like'      ></field>
        <field name='Depth'           cp='like'      ></field>
    </where>
</settings>");
            var service = new mms_inspectionwellService();
            var pQuery = query.ToParamQuery().AndWhere("A.ProjectCode", MmsHelper.GetCurrentProject()).From("MANHOLE");
            return service.GetDynamicListWithPaging(pQuery);
        }




        // 查询主表：GET api/mms/send
        public dynamic Get(RequestWrapper query)
        {
           
            query.LoadSettingXmlString(@"
<settings defaultOrderBy='BillNo'>
    <select>
      *
    </select>
    <from>
        MANHOLE
    </from>
    <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
        <field name='BillNo'          cp='equal'      ></field>
        <field name='Lane_Way'        cp='like'       ></field>
        <field name='Junc_Type'            cp='like'       ></field>
        <field name='Junc_Style'            cp='like'      ></field>
        <field name='Depth'           cp='like'      ></field>
    </where>
</settings>");
            var service = new mms_inspectionwellService();
            var pQuery = query.ToParamQuery().From("MANHOLE");
            return service.GetDynamicListWithPaging(pQuery);
        }






        public void Delete(string id)
        {
            var service = new mms_inspectionwellService();
            var result = service.Delete(ParamDelete.Instance().AndWhere("BillNo", id).From("MANHOLE"));
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
                scrollKeys = service.ScrollKeys("BillNo", id, ParamQuery.Instance().AndWhere("ProjectCode", MmsHelper.GetCurrentProject()).From("MANHOLE"))
                
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
    <table>MANHOLE</table>
    <where>
        <field name='BillNo' cp='equal'></field>
    </where>
</settings>");

            var result = service.Edit(formWrapper, null, data);
        }


    }

}
