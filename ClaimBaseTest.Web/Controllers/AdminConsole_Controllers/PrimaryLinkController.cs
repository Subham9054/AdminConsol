using CodeGen.Model.PrimaryLink;
using CodeGen.Repository.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ClaimBaseTest.Web
{
    public class PrimaryLinkController : Controller
    {



        //
       //
        // GET: /PrimaryLink/
        
        Primary objPrimary = new Primary();
       // int intOut;
        public static string StrOL;
        private const int defaultPageSize = 10;

      //  private int currentUserId;
        public IPriLinkServiceProvider _priLinkServiceProvider { get; }
        private readonly IGblLinkServiceProvider _gblLinkService;
        private readonly IProjectLinkServiceProvider _projectLinkService;
        PrimaryModel model = new PrimaryModel();

        public PrimaryLinkController(IPriLinkServiceProvider priLinkServiceProvider, IGblLinkServiceProvider gblLinkService, IProjectLinkServiceProvider projectLinkService)
        {
            _priLinkServiceProvider = priLinkServiceProvider;
            _gblLinkService = gblLinkService;
            //currentUserId = 1;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddPrimaryLink(string Status)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{
           model.ViewProjectLinkList = (await _priLinkServiceProvider.GetAllProjectLink()).ViewProjectLinkList.ToList();
           model.GlobalList = (await _gblLinkService.GetAllActiveGlobalLink()).ViewGlobalLinkDetailsmodel.ToList();
            model.FunctionList = (await _priLinkServiceProvider.GetAllFunctionMaster()).FunctionList.ToList();
            //model.CreatedBy =Convert.ToInt32( HttpContext.Session.GetString("UserId"));

            //model.GlobalModelList = (await _gblLinkService.GetAllGlobalLink(1)).ViewGlobalLinkDetailsmodel.ToList();
                return View(model);
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Home");
            //}
        }
        [HttpGet]
        public async Task<JsonResult> BindGlobalLinkByProjectId(int ProjId)
        {
            var result = (await _priLinkServiceProvider.GetAllActiveGlobalLinkByProjectId(ProjId)).ViewGlobalLinkDetailsByProjectId.ToList();
            return Json(result);
        }
        [HttpGet]
        public JsonResult BindMaxId(int GId)
        {
            var result = _priLinkServiceProvider.GetPrimaryLinkMaxId(GId); //(await _priLinkServiceProvider.GetPrimaryLinkMaxId(HId)).ToList();
            return Json(result);
        }
        #region Add Primary Link
        [HttpPost]
        public IActionResult AddPrimaryLink(PrimaryModel objPrimary)
        {
            try
            {
                PrimaryModel obj = new PrimaryModel();
                obj.GlinkId = objPrimary.GlinkId;
                var Result = _priLinkServiceProvider.AddPrimaryLink(objPrimary);
                return RedirectToAction("ViewActivePrimaryLink", new { Status = Result,id = obj.GlinkId , projectId = objPrimary.INTPROJECTLINKID});
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> ViewActivePrimaryLink(string Status, int id,int projectId)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{
            try
            {
                    model.ViewProjectLinkList = (await _priLinkServiceProvider.GetAllProjectLink()).ViewProjectLinkList.ToList();
                   model.GlobalList = (await _gblLinkService.GetAllActiveGlobalLink()).ViewGlobalLinkDetailsmodel.ToList();
                if (id > 0)
                {
                    PrimaryModel obj = new PrimaryModel();
                    model.GlinkId = id;
                    model.PrimaryLinkList = (await _priLinkServiceProvider.GetAllPrimaryLinkByGlobalLink(model)).PrimaryLinkList.ToList();
                    model.INTPROJECTLINKID = projectId;
                }
               
                if (Status == "1")
                {
                    TempData["CommandStatus_S"] = Status;

                }
                else if (Status == "2")
                {
                    TempData["CommandStatus_U"] = Status;

                }
                else if (Status == "4")
                {
                    TempData["CommandStatus_A"] = Status;

                }
                else if (Status == "5")
                {
                    TempData["CommandStatus_AC"] = Status;
                    TempData["CommandStatus"] = "Record Marked As Active Successfully";

                    }
                else if (Status == "6")
                {
                    //TempData["CommandStatus_INA"] = Status;
                    TempData["CommandStatus"] = "Record Marked As InActive Successfully";

                }
                else if (Status == "7")
                {
                    //TempData["CommandStatus_AU"] = Status;
                    TempData["CommandStatus"] = "Record Already in Use";

                }
                // TempData["CommandStatus"] = Status;
                return View(model);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Home");
            //}



        }
        public async Task<IActionResult> EditActivePrimaryLink(int id)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{ 
            try
            {
               model.ViewProjectLinkList = (await _priLinkServiceProvider.GetAllProjectLink()).ViewProjectLinkList.ToList();
               model.GlobalList = (await _gblLinkService.GetAllActiveGlobalLink()).ViewGlobalLinkDetailsmodel.ToList();
                model.FunctionList = (await _priLinkServiceProvider.GetAllFunctionMaster()).FunctionList.ToList();
                var primaryresult = _priLinkServiceProvider.GetPrimaryLinkById(id).Result;
                model.INTPLINKID = primaryresult.ToList()[0].INTPLINKID;
                model.NVCHPLINKNAME = primaryresult.ToList()[0].NVCHPLINKNAME;
                model.INTGLINKID = primaryresult.ToList()[0].INTGLINKID;
                model.INTFUNCTIONID = primaryresult.ToList()[0].INTFUNCTIONID;
                model.INTSLNO = primaryresult.ToList()[0].INTSLNO;
               //model.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                    model.INTPROJECTLINKID = primaryresult.ToList()[0].INTPROJECTLINKID;
                    return View(model);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Home");
            //}
        }
        [HttpPost]

        public IActionResult SlnoModifyData([FromBody] PrimaryModel objPrimaryModel)
        {
            try
            {
                var Result = String.Empty;
                var Gid = 0;
                string[] primarylnolist = objPrimaryModel.slnomodifyList.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in primarylnolist)
                {
                    string[] slno = item.Split('|');
                    Gid = Convert.ToInt32(slno[2]);
                    //Result = _priLinkServiceProvider.ModifySlnoPrimaryLink(Convert.ToInt32(slno[0]), Convert.ToInt32(slno[1]), Convert.ToInt32(HttpContext.Session.GetString("UserId")));
                }
                return Json(new { Response = Result,Id= Gid });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public IActionResult MarkAsInactive(string[] chkbox)
        {
            try
            {
                //model.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                string primarylinkId = string.Join(",", chkbox);
                var Result=String.Empty;
                foreach (string Id in chkbox)
                {
                    model.INTPLINKID = Convert.ToInt32(Id);
                     Result = _priLinkServiceProvider.MarkInactivePrimaryLink(model);
                }
                return RedirectToAction("ViewActivePrimaryLink", new { Status = Result });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> ViewInActivePrimaryLink(string Status)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{
            try
            {
                model.PrimaryLinkList = (await _priLinkServiceProvider.GetAllPrimaryLink(0)).PrimaryLinkList.ToList();
                TempData["CommandStatus"] = Status;
                return View(model);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Home");
            //}



        }
        [HttpPost]
        public IActionResult MarkAsActive(string[] chkbox)
        {
            try
            {
                //model.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                string primarylinkId = string.Join(",", chkbox);
                var Result = String.Empty;
                foreach (string Id in chkbox)
                {
                    model.INTPLINKID = Convert.ToInt32(Id);
                    Result = _priLinkServiceProvider.MarkActivePrimaryLink(model);
                }
                return RedirectToAction("ViewActivePrimaryLink", new { Status = Result });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SearchPrimaryLink(PrimaryModel obj)
        {
            try
            {
                model.ViewProjectLinkList = (await _priLinkServiceProvider.GetAllProjectLink()).ViewProjectLinkList.ToList();
                model.GlobalList = (await _priLinkServiceProvider.GetAllActiveGlobalLinkByProjectId(obj.INTPROJECTLINKID)).ViewGlobalLinkDetailsByProjectId.ToList();
                if (obj.GlinkId==0)
                {
                    model.PrimaryLinkList = (await _priLinkServiceProvider.GetAllPrimaryLink(1)).PrimaryLinkList.ToList();
                }
                else
                {
                    model.PrimaryLinkList = (await _priLinkServiceProvider.GetAllPrimaryLinkByGlobalLink(obj)).PrimaryLinkList.ToList();
                }
                model.GlinkId = obj.GlinkId;
                return View("ViewActivePrimaryLink", model);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
        #region Update Primary Link
        [HttpPost]
        public IActionResult EditPrimaryLink(PrimaryModel objprimary)
        {
            try
            {
             
               var GlinkId = objprimary.INTGLINKID;
                var Result = _priLinkServiceProvider.EditPrimaryLink(objprimary);
                return RedirectToAction("ViewActivePrimaryLink", new { Status =Result, id = GlinkId , projectId = objprimary.INTPROJECTLINKID });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
        

}
}


