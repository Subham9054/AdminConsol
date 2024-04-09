//using AdConsole11.Repository.USP_DIST;
using CodeGen.Model.GlobalLink;
using CodeGen.Model.PrimaryLink;
using CodeGen.Model.Set_Permission;
using CodeGen.Model.User;
using CodeGen.Repository.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

namespace ClaimBaseTest.Web.Controllers.AdminConsole_Controllers
{
    //Anshuman New --Full page
    public class MenueManageController : Controller
    {
        public IPriLinkServiceProvider _priLinkServiceProvider { get; }
        private readonly IGblLinkServiceProvider _gblLinkService;
        private readonly IMenueManageProvider _menueManageProvider;
        ButtonTab model = new ButtonTab();
        PrimaryModel modelPl = new PrimaryModel();
        public MenueManageController(IPriLinkServiceProvider priLinkServiceProvider, IGblLinkServiceProvider gblLinkService, IProjectLinkServiceProvider projectLinkService, IMenueManageProvider menueManageProvider)
        {
            _priLinkServiceProvider = priLinkServiceProvider;
            _gblLinkService = gblLinkService;
            _menueManageProvider = menueManageProvider;
        }

        [HttpGet]
        public async Task<IActionResult> AddButtonMaster(int intsortnum)
        {
            //Added by Subham
            ViewBag.maxbtnid = _menueManageProvider.Getbuttonmax(intsortnum);
            return View();
            //model.ViewProjectLinkList = (await _priLinkServiceProvider.GetAllProjectLink()).ViewProjectLinkList.ToList();
            //model.GlobalList = (await _gblLinkService.GetAllActiveGlobalLink()).ViewGlobalLinkDetailsmodel.ToList();
            //model.FunctionList = (await _menueManageProvider.GetAllFunctionMaster()).FunctionList.ToList();
            //return View(model);
        }
        [HttpGet]
        public async Task<JsonResult> GetProject()
        {
            var result = (await _priLinkServiceProvider.GetAllProjectLink()).ViewProjectLinkList.ToList();
            var jsonres = JsonConvert.SerializeObject(result);
            return Json(jsonres);
        }

        [HttpGet]
        public async Task<JsonResult> BindGlobalLinkByProjectId(int INTPROJECTID)
        {
            var result = (await _priLinkServiceProvider.GetAllActiveGlobalLinkByProjectId(INTPROJECTID)).ViewGlobalLinkDetailsByProjectId.ToList();
            var jsonres = JsonConvert.SerializeObject(result);
            return Json(jsonres);
        }
        [HttpGet]
        public async Task<JsonResult> BindPrimaryLinkByGlobalLinkId(int INTPLINKID)
        {
            List<ButtonTab> lst = await _menueManageProvider.GetAllPrimaryLinkByGlobalLink(INTPLINKID);
            var jsonres = JsonConvert.SerializeObject(lst);
            return Json(jsonres);
        }

        [HttpGet]
        public async Task<JsonResult> BindFunction()
        {
            var result = (await _menueManageProvider.GetAllFunctionMaster()).FunctionList.ToList();
            var jsonres = JsonConvert.SerializeObject(result);
            return Json(jsonres);
        }

        [HttpPost]
        public IActionResult AddButtonMaster(ButtonTab btn)
        {
            try
            {
                btn.INTCREATEDBY = HttpContext.Session.GetInt32("userId") ?? 0;
                var Result = _menueManageProvider.AddButtonMaster(btn);
                var jsonres = JsonConvert.SerializeObject(Result);
                return Json(jsonres);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public IActionResult UpdateButtonMasterDetails(ButtonTab btn)
        {
            try
            {
                btn.INTUPDATEDBY = HttpContext.Session.GetInt32("userId") ?? 0;
                var Result = _menueManageProvider.UpdateButtonMaster(btn);
                var jsonres = JsonConvert.SerializeObject(Result);
                return Json(jsonres);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddTabMaster(int intsortnum)
        {
            ViewBag.TabMaxSortNo = _menueManageProvider.GetTabMaxSortNo(intsortnum);
            return View();
        }

        [HttpPost]
        public IActionResult AddTabMaster(ButtonTab btn)
        {
            try
            {
                btn.INTCREATEDBY = HttpContext.Session.GetInt32("userId") ?? 0;
                var Result = _menueManageProvider.AddTabMaster(btn);
                var jsonres = JsonConvert.SerializeObject(Result);
                return Json(jsonres);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public IActionResult UpdateTabMasterDetails(ButtonTab btn)
        {
            try
            {
                btn.INTUPDATEDBY = HttpContext.Session.GetInt32("userId") ?? 0;
                var Result = _menueManageProvider.UpdateTabMaster(btn);
                var jsonres = JsonConvert.SerializeObject(Result);
                return Json(jsonres);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<JsonResult> BindButton()
        {
            var result = (await _menueManageProvider.GetAllButton()).ButtonList.ToList();
            var jsondata = JsonConvert.SerializeObject(result);
            return Json(jsondata);
        }

        //public IActionResult ViewTabMaster1()
        //{
        //    return View();
        //}

        [HttpGet]
        public async Task<IActionResult> ViewTabMaster()
        {
            //ViewBag.ViewTabMaxSortNo = _menueManageProvider.GetTabMaxSortNo();
            ViewBag.TabData = await _menueManageProvider.GetAllTabMaster();
            return View();
        }

        //[HttpGet]
        //public Task<IActionResult> ViewTabMaster()
        //{
        //    //ViewBag.ViewTabMaxSortNo = _menueManageProvider.GetTabMaxSortNo();
        //    ViewBag.TabData = _menueManageProvider.GetAllTabMaster(); // Assuming GetAllTabMaster() returns Task<IEnumerable<TabMaster>> or similar

        //    return Task.FromResult<IActionResult>(View());
        //}

        [HttpGet]
        public async Task<JsonResult> EditTabMaster(int TABID)
        {
            List<ButtonTab> lst = await _menueManageProvider.GetAllTabMasterById(TABID);
            var jsonres = JsonConvert.SerializeObject(lst);
            return Json(jsonres);
        }

        [HttpDelete]
        //DeleteTabMaster
        public IActionResult DeleteTabMaster(int TABID)
        {
            try
            {
                var Result = _menueManageProvider.DeleteTabMaster(TABID);
                var jsonres = JsonConvert.SerializeObject(Result);
                return Json(jsonres);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //This is for Tab serial No
        [HttpPost]
        public IActionResult SlnoModifyData([FromBody] ButtonTab objGlobalModel)
        {
            try
            {
                var Result = String.Empty;
                string[] Globalslnolist = objGlobalModel.slnomodifyList.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in Globalslnolist)
                {
                    string[] slno = item.Split('|');
                    int userId = HttpContext.Session.GetInt32("userId") ?? 0;
                    Result = _menueManageProvider.ModifySlnoTabMaster(Convert.ToInt32(slno[0]), Convert.ToInt32(slno[1]), userId);
                }
                return Json(Result);
            }
            catch (Exception ex)
            {

                throw ex; 
            }
        }

        #region Added by Subham for Serial No Set for Button
        [HttpPost]
        public IActionResult SlnoModifyDataForButton([FromBody] ButtonTab objBtnModel)
        {
            try
            {
                var Result = String.Empty;
                string[] Globalbtnlist = objBtnModel.slnomodifyList.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in Globalbtnlist)
                {
                    string[] slno = item.Split('|');
                    Result = _menueManageProvider.ModifySlnoButtonLink(Convert.ToInt32(slno[0]), Convert.ToInt32(slno[1]), Convert.ToInt32(HttpContext.Session.GetString("UserId")));
                }

                return Json(Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //---------------------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> ViewButtonMaster()
        {
            ViewBag.data = await _menueManageProvider.GetAllButtonMaster();
            return View();
        }

        [HttpGet]
        //async Task<JsonResult> BindPrimaryLinkByGlobalLinkId(int INTPLINKID)
        public async Task<JsonResult> EditButtonMaster(int BUTTONID)
        {
            List<ButtonTab> lst = await _menueManageProvider.GetAllButtonMasterById(BUTTONID);
            var jsonres = JsonConvert.SerializeObject(lst);
            return Json(jsonres);
        }

        [HttpDelete]
        //DeleteButtonMaster
        public IActionResult DeleteButtonMaster(int BUTTONID)
        {
            try
            {
                var Result = _menueManageProvider.DeleteButtonMaster(BUTTONID);
                var jsonres = JsonConvert.SerializeObject(Result);
                return Json(jsonres);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<IActionResult> EditButtonMaster1(int id)
        //{
        //    try
        //    {
        //        ButtonTab model = new ButtonTab();
        //        ViewBag.result = await _menueManageProvider.GetAllButtonMasterById(id);
        //        model = ViewBag.result;
        //        var jsonres = JsonConvert.SerializeObject(model);
        //        return (jsonres);
        //        //return View("AddButtonMaster");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public async Task<IActionResult> ViewButtonMaster(ButtonTab btn)
        //{
        //    return View();
        //}
        //[HttpGet]
        //public IActionResult ViewTabMaster()
        //{
        //    return View();
        //}
    }
}
