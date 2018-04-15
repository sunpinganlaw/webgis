using System;
using Magicodes.ECharts;
using Magicodes.ECharts.Mvc;
using Magicodes.ECharts.Axis;
using Magicodes.ECharts.Components.Title;
using Magicodes.ECharts.Components.ToolTip;
using Magicodes.ECharts.Mvc.Results;
using Magicodes.ECharts.Series;
using Magicodes.ECharts.Series.Mark;
using Magicodes.ECharts.ValueTypes;
using Magicodes.ECharts.CommonDefinitions;
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
    public class DraingrooveController : Controller
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
                    Type = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "drainGrooveType")),
                    Material = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "drainGrooveMaterial")),
                    SectionForm = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "drainGrooveSectionForm")),
                    Structure = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "drainGrooveStructure")),
                    InterfaceForm = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "drainGrooveInterfaceForm")),
                    AdministrativeArea = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "AdministrativeArea")),
                    FacilityStatus = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "FacilityStatus"))
                },
                urls = new
                {
                    query = "/api/mms/draingroove",
                    remove = "/api/mms/draingroove/",
                    billno = "/api/mms/draingroove/getnewbillno",
                    audit = "/api/mms/draingroove/audit/",
                    edit = "/mms/draingroove/edit/"
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
                    Category="",
                    Type="",
                    Material=""

                   
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
            var data = new DraingrooveApiController().GetEditMaster(id);
            var codeService = new sys_codeService();

            var model = new
            {
                form = data.form,
                scrollKeys = data.scrollKeys,
                urls = new
                {
                    getdetail = "/api/mms/draingroove/getdetail/",
                    getmaster = "/api/mms/draingroove/geteditmaster/",
                    edit = "/api/mms/draingroove/edit/",
                    audit = "/api/mms/draingroove/audit/",
                    getrowid = "/mms/draingroove/getnewrowid/"
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
                    AdministrativeArea = codeService.Decode("AdministrativeArea"),
                    Type = codeService.Decode("drainGrooveType"),
                    Material = codeService.Decode("drainGrooveMaterial"),
                    SectionForm = codeService.Decode("drainGrooveSectionForm"),
                    Structure = codeService.Decode("drainGrooveStructure"),
                    InterfaceForm = codeService.Decode("drainGrooveInterfaceForm"),
                    FacilityStatus = codeService.Decode("FacilityStatus"),
                    warehouseItems = new mms_warehouseService().GetWarehouseItems(currentProject)
                },

                defaultForm = new mms_draingroove().Extend(new
                {
                    BillNo = id,
                    IdentificationCode="",
                    DrainageCode="",
                    Address = "",
                    Start_stopPoint="",
                    Category = codeService.GetDefaultCodeNew("InspectionWellCategory"),
                    Type = codeService.GetDefaultCodeNew("drainGrooveType"),
                    Length="",
                    StartCode="",
                    StopCode="",
                    StartElevation="",
                    StopElevation="",
                    SectionForm = codeService.GetDefaultCodeNew("drainGrooveSectionForm"),
                    Diameter="",
                    Material = codeService.GetDefaultCodeNew("PipeMaterial"),
                    Roughness="",
                    Structure = codeService.GetDefaultCodeNew("drainGrooveStructure"),
                    InterfaceForm = codeService.GetDefaultCodeNew("drainGrooveInterfaceForm"),
                    Slope="",
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

        #region 图表展示
        [System.Web.Http.HttpGet]
        public JavaScriptJsonResult Echart(string id)
        {

            if (id == "Category")
            {
                var name = new List<object>();
                name.Add("总长");
                name.Add("合流");
                name.Add("雨水");
                name.Add("污水");


                var data_fix = new List<object>() {
                     new {value=310.415, name= "排水沟(渠)总长"},
                    new {value=227.175, name="合流沟(渠)总长"},
                    new {value=82.619, name="雨水沟(渠)总长"},
                    new {value=0.621, name="污水沟(渠)总长"}
                   
                                };
                var chartOptions_fix = new EChartsOption
                {
                    Title = new Title("排水沟(渠)长度统计(km)") { Left = new AlignValue(Align.center) },
                    Tooltip = new Tooltip(),
                    Series = new Series[]
                {
                    new BarSeries
                    {
                        Name ="排水沟(渠)长度统计(km)",
                        Data = data_fix,
                        MarkPoint = new MarkPoint
                        {
                            Data = new List<MarkData>
                            {
                                new MarkData {Type = MarkPointDataTypes.max, Name = "最大值" },
                                new MarkData {Type = MarkPointDataTypes.min, Name = "最小值"}
                            }
                        },
                        MarkLine = new MarkLine
                        {
                            Data = new List<MarkData>
                            {

                            }
                        }
                    }
                },
                    XAxis = new XAxis[1] { new XAxis { Type = AxisTypes.category, Data = name } },
                    YAxis = new YAxis[1] { new YAxis { Type = AxisTypes.value } }
                };
                return this.ToEChartResult(chartOptions_fix);

            }











            string titlename = "";
            var countnumber = new List<object>();
            var countname = new List<object>();
            var codeService = new sys_codeService();
            var getChartService = new mms_draingrooveService();

            List<dynamic> data = getChartService.GetCountByGroupName(id);
            foreach (var result in data)
            {
                foreach (var item in (IDictionary<string, object>)result)
                {
                    if (item.Key.Equals(id))
                    {
                        //codeService.GetTextByCode(item.Value.ToString(), "InspectionWellCategory");
                        if (id == "Category")
                        {
                            countname.Add(codeService.GetTextByCode(item.Value.ToString(), "InspectionWellCategory"));
                            titlename = "排水渠类型统计图";
                        }
                        if (id == "AdministrativeArea")
                        {
                            countname.Add(codeService.GetTextByCode(item.Value.ToString(), "AdministrativeArea"));
                            titlename = "排水渠区域分布统计图";

                        }
                        if (id == "OwnershipUnit")
                        {
                            countname.Add(item.Value.ToString());
                            titlename = "排水渠权属单位分布统计图";


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

    public class DraingrooveApiController : ApiController
    {
        // GET api/mms/send/getdoperson
        public List<dynamic> GetDoPerson(string q)
        {
            var SendService = new mms_draingrooveService();
            var pQuery = ParamQuery.Instance().Select("top 10 DoPerson").AndWhere("DoPerson", q, Cp.StartWithPY);
            return SendService.GetDynamicList(pQuery);
        }

        public  string GetNewBillNo()
        {
            var service = new mms_draingrooveService();
            return service.GetNewKey("BillNo", "dateplus");
        }



        public  List<dynamic> GetBillNo(string q)
        {
            var service = new mms_draingrooveService();
            var pQuery = ParamQuery.Instance().Select("top 10 BillNo").AndWhere("BillNo", q, Cp.StartWith);
            return service.GetDynamicList(pQuery);
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
        mms_draingroove A
    </from>
    <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
        <field name='BillNo'          cp='equal'      ></field>
        <field name='Address'        cp='like'       ></field>
        <field name='IdentificationCode'            cp='like'       ></field>
        <field name='Category'            cp='like'       ></field>
        <field name='Type'            cp='like'       ></field>
        <field name='Material'            cp='like'       ></field>
        <field name='Structure'            cp='like'       ></field>
        <field name='InterfaceForm'            cp='like'       ></field>
        <field name='SectionForm'            cp='like'       ></field>
        <field name='OwnershipUnit'            cp='like'       ></field>
        <field name='Diameter'            cp='like'       ></field>
    </where>
</settings>");
            var service = new mms_draingrooveService();
            var pQuery = query.ToParamQuery().AndWhere("A.ProjectCode", MmsHelper.GetCurrentProject());
            return service.GetDynamicListWithPaging(pQuery);
        }

        
        public void Delete(string id)
        {
            var service = new mms_draingrooveService();
            var result = service.Delete(ParamDelete.Instance().AndWhere("BillNo", id));
            MmsHelper.ThrowHttpExceptionWhen(result <= 0, "信息删除失败[BillNo={0}]，请重试或联系管理员！", id);
        }

        [System.Web.Http.HttpPost]
        public void Audit(string id, dynamic data)
        {
          
            var status = data["status"].ToString();
            var comment = data["comment"].ToString();
            var result = new MmsService().Audit(typeof(mms_draingroove).Name, id, status, comment);
            MmsHelper.ThrowHttpExceptionWhen(result < 0, "信息审核失败[BillNo={0}]，请重试或联系管理员！", id);
        }


        public dynamic GetEditMaster(string id)
        {

            var service = new mms_draingrooveService();
            return new
            {
                form = service.GetModel(ParamQuery.Instance().AndWhere("BillNo", id)),
                scrollKeys = service.ScrollKeys("BillNo", id, ParamQuery.Instance().AndWhere("ProjectCode", MmsHelper.GetCurrentProject()))
                
            };
        }

        public dynamic GetDetail(string id)
        {
            var ReceiveService = new mms_draingrooveService();
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

            var service = new mms_draingrooveService();
            var formWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
<settings>
    <table>mms_draingroove</table>
    <where>
        <field name='BillNo' cp='equal'></field>
    </where>
</settings>");

            var result = service.Edit(formWrapper, null, data);
        }


    }

}
