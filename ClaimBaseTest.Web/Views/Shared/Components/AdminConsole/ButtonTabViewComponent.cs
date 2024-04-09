using Microsoft.AspNetCore.Mvc;
using CodeGen.Repository.Repositories.Interfaces;
using CodeGen.Model.User;
using System.Security.Claims;

namespace AdConsole11.Web.Views.Shared.Components.AdminConsole
{
    public class ButtonTabViewComponent : ViewComponent
    {
        private readonly ISetPermissionServiceProvider _SetPermission;
        private readonly IConfiguration _config;
        public ButtonTabViewComponent(ISetPermissionServiceProvider SetPermission, IConfiguration Configuration)
        {
            _SetPermission = SetPermission;
            _config = Configuration;
        }

        public async Task<IViewComponentResult> InvokeAsync(string plinkValue, string btnid)
        {
            try
            {
                var tabid = "";
                if (Request.Query["Plink"].ToString().Length > 0)
                {

                    tabid = Request.Query["Tabid"].ToString();
                    plinkValue = Request.Query["Plink"].ToString();
                    btnid = Request.Query["Buttonid"].ToString();


                    if (btnid == "")
                    {
                        btnid = _SetPermission.GetButtondefaultTab(plinkValue);
                    }

                    if (tabid == "")
                    {
                        tabid = _SetPermission.GetdefaultTab(plinkValue, btnid);
                    }
                }

                //plinkValue = Request.Query["Plink"].ToString();
                //btnid=Request.Query["Buttonid"].ToString();
                //plinkValue = HttpContext.Request.Query["Plink"].ToString(); //both are same

                if(btnid!=null)
                {
                    var claimsPrincipal = HttpContext.User;
                    var intDesignationIdClaim = claimsPrincipal.FindFirst("INTUSERID")?.Value;
                    var userid = HttpContext.Session.GetInt32("userId");
                    var desgid = HttpContext.Session.GetInt32("DesgId");
                    var PId = _config.GetValue<int>("MySettings:ProjectId");
                    var results = _SetPermission.GetButtonWithTabAccess(plinkValue, btnid, Convert.ToInt32(intDesignationIdClaim));
                    ViewBag.Btnid = _SetPermission.GetButtondefaultTab(plinkValue);
                    ViewBag.Tabid = _SetPermission.GetdefaultTab(plinkValue, btnid);
                    ViewBag.BtnTab = results;
                    ViewBag.ClickTabId = tabid;
                    ViewBag.ClickBtnId = btnid;
                    var Data = results.ToList();
                    return View("ButtonWithTab", results);
                }
                else
                {
                    //var redirectToUrl = Url.Action("DenayPermission", "Home");
                    ////return View("Redirect1", (object)redirectToUrl);
                    //return View("/Views/Shared/Components/ButtonTab/Redirect1.cshtml");
                    var results = _SetPermission.GetButtonWithTabAccess(plinkValue, "", 0);
                    return View("ButtonWithTab", results);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}






#region Old Code by Anshuman
//using Microsoft.AspNetCore.Mvc;
//using CodeGen.Repository.Repositories.Interfaces;
//using CodeGen.Model.User;

//namespace ClaimBaseTest.Web.Views.Shared.Components.AdminConsole
//{
//    public class ButtonTabViewComponent : ViewComponent
//    {
//        private readonly ISetPermissionServiceProvider _SetPermission;
//        private readonly IConfiguration _config;
//        public ButtonTabViewComponent(ISetPermissionServiceProvider SetPermission, IConfiguration Configuration)
//        {
//            _SetPermission = SetPermission;
//            _config = Configuration;
//        }
//        public async Task<IViewComponentResult> InvokeAsync(string plinkValue, string btnid)
//        {
//            if (Request.Query["Plink"].ToString().Length > 0)
//            {
//                plinkValue = Request.Query["Plink"].ToString();
//                btnid = Request.Query["Buttonid"].ToString();
//                ViewBag.ClickBtnId= Request.Query["Buttonid"].ToString();
//                if (btnid == "")
//                {
//                    btnid = _SetPermission.GetButtondefaultTab(plinkValue);
//                }
//            }

//            //plinkValue = Request.Query["Plink"].ToString();
//            //btnid=Request.Query["Buttonid"].ToString();
//            //plinkValue = HttpContext.Request.Query["Plink"].ToString(); //both are same

//            var userid = HttpContext.Session.GetInt32("userId");
//            var desgid = HttpContext.Session.GetInt32("DesgId");
//            var PId = _config.GetValue<int>("MySettings:ProjectId");
//            var results = _SetPermission.GetButtonWithTabAccess(plinkValue, btnid);
//            ViewBag.BtnTab = results;
//            ViewBag.Btnid = _SetPermission.GetButtondefaultTab(plinkValue);

//            var Data = results.ToList();

//            //ViewBag.BtnTab = _userService.GetAllUsers(new User { ActionCode = "VF" });

//            return View("ButtonWithTab", results);
//        }
//    }
//}
#endregion