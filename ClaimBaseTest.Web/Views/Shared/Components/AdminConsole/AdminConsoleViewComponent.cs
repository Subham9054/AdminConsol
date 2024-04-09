using Microsoft.AspNetCore.Mvc;
using CodeGen.Repository.Repositories.Interfaces;
namespace ClaimBaseTest.Web.Views.Shared.Components.AdminConsole
{
    public class AdminConsoleViewComponent : ViewComponent
    {
        private readonly ISetPermissionServiceProvider _SetPermission;
        private readonly IConfiguration _config;

        public AdminConsoleViewComponent(ISetPermissionServiceProvider SetPermission, IConfiguration Configuration)
        {
            _SetPermission = SetPermission;
            _config = Configuration;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //Commented (This is only for Basic Login) 
            //var userid = HttpContext.Session.GetInt32("userId");
            //var desgid = HttpContext.Session.GetInt32("DesgId");

            // Retrieve INTUSERID and INTDESIGNATIONID claims from the current user's identity
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "INTUSERID");
            var desgIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "INTDESIGNATIONID");

            // Check if claims exist and convert them to integers
            int? userId = userIdClaim != null ? Convert.ToInt32(userIdClaim.Value) : null;
            int? desgId = desgIdClaim != null ? Convert.ToInt32(desgIdClaim.Value) : null;


            var PId = _config.GetValue<int>("MySettings:ProjectId");
            var results = _SetPermission.GetLinkAccessByUserId(Convert.ToInt32(userId), PId, Convert.ToInt32(desgId));
          /*  var plinkValue = HttpContext.Request.Query["Plink"].ToString();
            var btn= _SetPermission.GetButtondefaultTab(plinkValue);
            ViewBag.Btnid = Convert.ToUInt32(btn);
            var btnid= _SetPermission.GetButtondefaultTab(plinkValue);
            ViewBag.Tabid = _SetPermission.GetdefaultTab(plinkValue, btnid);*/
            var Data = results.ToList();
            if (HttpContext.Request.Query["Glink"].ToString() != "")
            {
                Data.ForEach(c => c.Aglinkid = Convert.ToInt32(HttpContext.Request.Query["Glink"]));
                //TempData["Glink"] = HttpContext.Request.Query["Glink"].ToString();
                //TempData.Keep();
            }
            if (HttpContext.Request.Query["Plink"].ToString() != "")
            {
                Data.ForEach(c => c.Aplinkid = Convert.ToInt32(HttpContext.Request.Query["Plink"]));
                //TempData["Plink"] = HttpContext.Request.Query["Plink"].ToString();
                //TempData.Keep();
            }
            return View("default", results);
        }
    }
}
