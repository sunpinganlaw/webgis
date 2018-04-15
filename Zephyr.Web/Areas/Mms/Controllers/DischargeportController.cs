using Magicodes.ECharts;
using Magicodes.ECharts.Mvc;
using Magicodes.ECharts.Axis;
using Magicodes.ECharts.Components.Title;
using Magicodes.ECharts.Components.ToolTip;
using Magicodes.ECharts.Mvc.Results;
using Magicodes.ECharts.Series;
using Magicodes.ECharts.Series.Mark;
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
using Magicodes.ECharts.CommonDefinitions;

namespace Zephyr.Areas.Mms.Controllers
{
    public class DischargeportController : Controller
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
                    Form = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "DischargeportForm")),
                    AdministrativeArea = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "AdministrativeArea")),
                    FilmDoorMaterial = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "FilmDoorMaterial")),
                    FilmDoor = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "AdditionalFacilities")),
                    FacilityStatus = new sys_codeService().GetDynamicList(ParamQuery.Instance().Select("Value as value,Text as text").AndWhere("CodeType", "FacilityStatus"))
                },
                urls = new
                {
                    query = "/api/mms/dischargeport",
                    remove = "/api/mms/dischargeport/",
                    billno = "/api/mms/dischargeport/getnewbillno",
                    audit = "/api/mms/dischargeport/audit/",
                    edit = "/mms/dischargeport/edit/"
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
                    FilmDoor = "",
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
            var data = new DischargeportApiController().GetEditMaster(id);
            var codeService = new sys_codeService();

            var model = new
            {
                form = data.form,
                scrollKeys = data.scrollKeys,
                urls = new
                {
                    getdetail = "/api/mms/dischargeport/getdetail/",
                    getmaster = "/api/mms/dischargeport/geteditmaster/",
                    edit = "/api/mms/dischargeport/edit/",
                    audit = "/api/mms/dischargeport/audit/",
                    getrowid = "/mms/dischargeport/getnewrowid/"
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
                    Form = codeService.Decode("DischargeportForm"),
                    AdministrativeArea = codeService.Decode("AdministrativeArea"),
                    FilmDoorMaterial = codeService.Decode("FilmDoorMaterial"),
                    FilmDoor = codeService.Decode("AdditionalFacilities"),
                    FacilityStatus = codeService.Decode("FacilityStatus"),
                    warehouseItems = new mms_warehouseService().GetWarehouseItems(currentProject)
                },

                defaultForm = new mms_dischargeport().Extend(new
                {
                    BillNo = id,
                    OrgX="",
                    OrgY="",
                    IdentificationCode="",
                    DrainageCode="",
                    ReceivingCode="",
                    Address = "",
                    Category = codeService.GetDefaultCodeNew("InspectionWellCategory"),
                    Size="",
                    FilmDoor = codeService.GetDefaultCodeNew("AdditionalFacilities"),
                    FilmDoorMaterial = codeService.GetDefaultCodeNew("FilmDoorMaterial"),
                    FilmDoorDiameter = "",
                    FilmDoorToopElevation = "",
                    FilmDoorBottomElevation = "",
                    SubmergedWaterLevel = "",
                    TideCurve = "",
                    BottomElevation = "",
                    Form = codeService.GetDefaultCodeNew("DischargeportForm"),
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
            var getChartService = new mms_dischargeportService();

            List<dynamic> data = getChartService.GetCountByGroupName(id);
            foreach (var result in data)
            {
                foreach (var item in (IDictionary<string, object>)result)
                {
                    if (item.Key.Equals(id))
                    {
                        
                        if (id == "Category")
                        {
                            countname.Add(codeService.GetTextByCode(item.Value.ToString(), "InspectionWellCategory"));
                            titlename = "排放口类型统计图";
                        }
                        if (id == "AdministrativeArea")
                        {
                            countname.Add(codeService.GetTextByCode(item.Value.ToString(), "AdministrativeArea"));
                            titlename = "排放口区域分布统计图";

                        }
                        if (id == "Form")
                        {
                            countname.Add(codeService.GetTextByCode(item.Value.ToString(), "DischargeportForm"));
                            titlename = "排放口出流形式统计图";


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

    public class DischargeportApiController : ApiController
    {
        // GET api/mms/send/getdoperson
        public List<dynamic> GetDoPerson(string q)
        {
            var SendService = new mms_dischargeportService();
            var pQuery = ParamQuery.Instance().Select("top 10 DoPerson").AndWhere("DoPerson", q, Cp.StartWithPY);
            return SendService.GetDynamicList(pQuery);
        }

        public  string GetNewBillNo()
        {
            var service = new mms_dischargeportService();
            return service.GetNewKey("BillNo", "dateplus");
        }



        public  List<dynamic> GetBillNo(string q)
        {
            var service = new mms_dischargeportService();
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
        mms_dischargeport A
    </from>
    <where defaultForAll='true' defaultCp='equal' defaultIgnoreEmpty='true' >
        <field name='BillNo'          cp='equal'      ></field>
        <field name='Address'        cp='like'       ></field>
        <field name='Category'            cp='like'       ></field>
        <field name='Form'            cp='like'      ></field>
        <field name='FilmDoor'           cp='like'      ></field>
      
    </where>
</settings>");
            var service = new mms_dischargeportService();
            var pQuery = query.ToParamQuery().AndWhere("A.ProjectCode", MmsHelper.GetCurrentProject());
            return service.GetDynamicListWithPaging(pQuery);
        }

        
        public void Delete(string id)
        {
            var service = new mms_dischargeportService();
            var result = service.Delete(ParamDelete.Instance().AndWhere("BillNo", id));
            MmsHelper.ThrowHttpExceptionWhen(result <= 0, "信息删除失败[BillNo={0}]，请重试或联系管理员！", id);
        }

        [System.Web.Http.HttpPost]
        public void Audit(string id, dynamic data)
        {
          
            var status = data["status"].ToString();
            var comment = data["comment"].ToString();
            var result = new MmsService().Audit(typeof(mms_dischargeport).Name, id, status, comment);
            MmsHelper.ThrowHttpExceptionWhen(result < 0, "信息审核失败[BillNo={0}]，请重试或联系管理员！", id);
        }


        public dynamic GetEditMaster(string id)
        {

            var service = new mms_dischargeportService();
            return new
            {
                form = service.GetModel(ParamQuery.Instance().AndWhere("BillNo", id)),
                scrollKeys = service.ScrollKeys("BillNo", id, ParamQuery.Instance().AndWhere("ProjectCode", MmsHelper.GetCurrentProject()))
                
            };
        }

        public dynamic GetDetail(string id)
        {
            var ReceiveService = new mms_dischargeportService();
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

            var service = new mms_dischargeportService();
            var formWrapper = RequestWrapper.Instance().LoadSettingXmlString(@"
<settings>
    <table>mms_dischargeport</table>
    <where>
        <field name='BillNo' cp='equal'></field>
    </where>
</settings>");

            var result = service.Edit(formWrapper, null, data);
        }


    }

}
