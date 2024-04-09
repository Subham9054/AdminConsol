using CodeGen.Model.PrimaryLink;
using CodeGen.Model.ProjectMaster;
using CodeGen.Model.Set_Permission;
using CodeGen.Model.User;
using CodeGen.Repository.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ClaimBaseTest.Web
{
   
  public class SetPermissionController : Controller
  {
        private readonly ISetPermissionServiceProvider _setpermissionservice;
        private readonly IUserServiceProvider _userService;
        private readonly IProjectLinkServiceProvider _projectLinkService;
        public IPriLinkServiceProvider _priLinkServiceProvider { get; }

        SetPermissionModel model = new SetPermissionModel();
        PrimaryModel pmodel = new PrimaryModel(); //Added by Subham
        public IActionResult Index()
        {

            return View();
        }
        public SetPermissionController(ISetPermissionServiceProvider setPermissionProvider, IUserServiceProvider userService, IProjectLinkServiceProvider projectLinkService, IPriLinkServiceProvider priLinkServiceProvider)
        {
            _setpermissionservice = setPermissionProvider;
            _userService = userService;
            _projectLinkService = projectLinkService;
            _priLinkServiceProvider = priLinkServiceProvider;

        }
       
        public async Task<IActionResult> SetPermission()
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{

                try
                {
                    ViewBag.Designation = _userService.GetDesignations();
                    ViewBag.UserDetails = _userService.GetAllUsers(new User { ActionCode = "VF" });
                    model.ProjectList = (await _priLinkServiceProvider.GetAllProjectLink()).ViewProjectLinkList.ToList();
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

        [HttpGet]
          public async Task<IActionResult> BindSetPermissionUser()
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{

                try
                {
                    ViewBag.Designation = _userService.GetDesignations();
                    ViewBag.UserDetails = _userService.GetAllUsers(new User { ActionCode = "VF" });
                    model.ProjectList = (await _priLinkServiceProvider.GetAllProjectLink()).ViewProjectLinkList.ToList();
                    return View("SetPermission",model);

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            //}
            //else
            //{
            //   return RedirectToAction("Login", "Home");
            //}

        }

        [HttpGet]
        public async Task<IActionResult>BindSetPermission()
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{

                try
                {
                    ViewBag.Designation = _userService.GetDesignations();
                    ViewBag.UserDetails = _userService.GetAllUsers(new User { ActionCode = "VF" });
                    model.ProjectList = (await _priLinkServiceProvider.GetAllProjectLink()).ViewProjectLinkList.ToList();
                    return View("SetPermission", model);

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
        public async Task<IActionResult> BindSetPermission(SetPermissionModel objpermission)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{
                ViewBag.Designation = _userService.GetDesignations();
                ViewBag.UserDetails = _userService.GetAllUsers(new User { ActionCode = "VF" });
                model.GlobalPrimaryList = (await _setpermissionservice.GetAllPrimaryLinkByGlobalLink(objpermission)).GlobalPrimaryList.ToList();
                model.ProjectList = (await _priLinkServiceProvider.GetAllProjectLink()).ViewProjectLinkList.ToList();
                return View("SetPermission", model);
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Home");
            //}

        }

        
        [HttpPost]
        public async Task<IActionResult> BindSetPermissionUser(SetPermissionModel objpermission)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{
                ViewBag.Designation = _userService.GetDesignations();
                ViewBag.UserDetails = _userService.GetAllUsers(new User { ActionCode = "VF" });
                model.GlobalPrimaryListUser = (await _setpermissionservice.GetAllPrimaryLinkByGlobalLinkUserwise(objpermission)).GlobalPrimaryList.ToList();
                model.ProjectList = (await _priLinkServiceProvider.GetAllProjectLink()).ViewProjectLinkList.ToList();
                //return View(model);
                return View("SetPermissionUser", model);
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Home");
            //}

        }
        //public async Task<IActionResult> BindSetPermission(int DId)
        //{

        //    try
        //    {
        //        ViewBag.Designation = _userService.GetDesignations();
        //         model.GlobalPrimaryList = (await _setpermissionservice.GetAllPrimaryLinkByGlobalLink()).GlobalPrimaryList.ToList();
        //        return View("SetPermission", model);



        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        #region Set permission
        [HttpPost]
        //public async Task<IActionResult> SetPermission(SetPermissionModel objpermission, string[] chkbox,string [] radio)
        public IActionResult SetPermissionData([FromBody] SetPermissionModel objpermission)
        {
            try
            {
                var Result = String.Empty;
                string[] permissionlist = objpermission.setpermissionList.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);

                var designations = _setpermissionservice.DeletePermissionLink_Designation(Convert.ToInt32(permissionlist[0].Split('|')[0]), Convert.ToInt32(permissionlist[0].Split('|')[1]));

                foreach (string item in permissionlist)
                {
                    SetPermissionModel s = new SetPermissionModel();
                    string[] permission = item.Split('|');
                    var projid = Convert.ToInt32(permission[1]);
                    Result = _setpermissionservice.SetPermissionLink_Designation(Convert.ToInt32(permission[0]), Convert.ToInt32(permission[2]), Convert.ToInt32(permission[3]), Convert.ToInt32(1), projid);

                }
                return Json(Result);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]

        public IActionResult SetPermissionDataUser([FromBody] SetPermissionModel objpermission)
        {
            try
            {
                var Result = String.Empty;
                string[] permissionlistUser = objpermission.setpermissionListUser.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                var userid = permissionlistUser[0].Split('|')[0];

                var s = _setpermissionservice.DeletePermissionLink_User(Convert.ToInt32(permissionlistUser[0].Split('|')[0]), Convert.ToInt32(permissionlistUser[0].Split('|')[1]));
                foreach (string item in permissionlistUser)
                {
                    string[] permission = item.Split('|');
                    var uid = Convert.ToInt32(permission[0]);
                    var projid = Convert.ToInt32(permission[1]);
                    var prilink = Convert.ToInt32(permission[2]);
                    var accessid = Convert.ToInt32(permission[3]);
                    Result = _setpermissionservice.SetPermissionLink_User(uid, prilink, accessid, Convert.ToInt32(1), projid);
                    //Result = _setpermissionservice.SetPermissionLink_User(Convert.ToInt32(permission[0]), Convert.ToInt32(permission[2]), Convert.ToInt32(permission[3]), Convert.ToInt32(HttpContext.Session.GetString("UserId")),Convert.ToInt32(permissionlistUser[1]));

                }
                return Json(Result);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion


        [HttpGet]
        public async Task<IActionResult> ViewUser(string Status)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{
                model.UserList = (await _setpermissionservice.GetAllUser()).UserList.ToList();
                TempData["CommandStatus"] = Status;
                return View("RemoveUserPermission", model);
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Home");
            //}
        }
        #region Remove User Permission List
        [HttpPost]
        public IActionResult MarkAsInActive(string[] chkbox)
        {
            try
            {
                var Result = String.Empty;
                foreach (string userId in chkbox)
                {
                    Result = _setpermissionservice.RemovePermissionList_User(Convert.ToInt32(userId), Convert.ToInt32(HttpContext.Session.GetString("UserId")));
                }
                return RedirectToAction("ViewUser", new { Status = Result });
                // return RedirectToAction("RemoveUserPermission");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Added By Subham For Set Permission User
        [HttpGet]
        public async Task<IActionResult> UserPermission(int Projectid, int globalLinkId, int primaryLinkId, int buttonId)
        {
            try
            {
                var userList= (await _setpermissionservice.GetAllUser()).UserList.ToList();
                var roleList=(await _setpermissionservice.GetAllRole()).RoleList.ToList();
                pmodel.ViewProjectLinkList = (await _priLinkServiceProvider.GetAllProjectLink()).ViewProjectLinkList.ToList();
                ViewBag.UserID = (await _setpermissionservice.GetAllUserID()).UserList.ToList();
               // ViewBag.GetAlldetails = (await _setpermissionservice.GetAllDetails(Projectid, globalLinkId, primaryLinkId, buttonId)).GlobalPrimaryList.ToList();

                var userlistall= (await _setpermissionservice.GetAllDetails(Projectid, globalLinkId, primaryLinkId, buttonId)).GlobalPrimaryList.ToList();
                foreach (var user in userlistall)
                {
                    var currentUsers = userList.ToSelectList("UserID", "UserName");
                    var currentRoles = roleList.ToSelectList("Roleid", "RoleName");
                    if(Projectid == 0 && globalLinkId == 0 && primaryLinkId == 0 && buttonId == 0)
                    {
                        user.Userlist = currentUsers;
                        user.Rolelist = currentRoles;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(user.UserIds) && user.UserIds.Length > 0)
                        {
                            user.UserIds = user.UserIds.Replace(" ", string.Empty);
                            var selectedUsers = user.UserIds.Split(',').Distinct().ToArray();
                            foreach (var user1 in currentUsers)
                            {
                                if (selectedUsers.Contains(user1.Value))
                                {
                                    user1.Selected = true;
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(user.RoleIds) && user.RoleIds.Length > 0)
                        {
                            user.RoleIds = user.RoleIds.Replace(" ", string.Empty);
                            var selectedRoles = user.RoleIds.Trim().Split(",").Distinct().ToArray();

                            foreach (var role1 in currentRoles)
                            {
                                if (selectedRoles.Contains(role1.Value))
                                {
                                    role1.Selected = true;
                                }
                            }
                        }
                        user.Userlist = currentUsers;
                        user.Rolelist = currentRoles;
                    }   
                }
                ViewBag.GetAlldetails = userlistall;

                ViewBag.Projectid = Projectid;
                ViewBag.globalLinkId = globalLinkId;
                ViewBag.primaryLinkId = primaryLinkId;
                ViewBag.buttonId = buttonId;

         
                return View(pmodel);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpGet]
        public async Task<JsonResult> BindGlobalLinkByProjectId(int ProjId)
        {
            var result = (await _priLinkServiceProvider.GetAllActiveGlobalLinkByProjectId(ProjId)).ViewGlobalLinkDetailsByProjectId.ToList();
            return Json(result);
        }
        public async Task<IActionResult> BindPrimaryLinkByGlobalID(int Globid)
        {
            var result = (await _setpermissionservice.GetPrimaryLinkByGlobalLink(Globid)).GlobalPrimaryList.ToList();
            return Json(result);
        }

        public async Task<IActionResult> GetButtonByPrimaryLink(int Primid)
        {
            var result = (await _setpermissionservice.GetButtonByPrimaryLink(Primid)).GlobalPrimaryList.ToList();
            return Json(result);
        }
        public async Task<IActionResult> Bindroleddl(int Projectid, int globalLinkId, int primaryLinkId, int buttonId,int tabid)
        {
            var roleListddl = (await _setpermissionservice.bindroleddl(Projectid, globalLinkId, primaryLinkId, buttonId, tabid)).RoleList.ToList();
            List<SelectListItem> rolesddl = new List<SelectListItem>();
            foreach (var role in roleListddl)
            {
                rolesddl.Add(new SelectListItem { Text = role.RoleName, Value = role.Roleid.ToString(), Selected = false });
            }
            ViewBag.Rolelistddl = rolesddl;
            return Json(rolesddl);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserdetail(UserSet user,int Projectid,int globalLinkId,int primaryLinkId,int buttonId)
        {
            try
            {
                var userData = HttpContext.Request.Form["UserSet"];
                var ResutUserData = JsonConvert.DeserializeObject<List<UserSet>>(userData);
                var userlistall = (await _setpermissionservice.GetUserId(Projectid, globalLinkId, primaryLinkId, buttonId)).GlobalPrimaryList.ToList();
                
                foreach (var item in ResutUserData)
                {
                  
                        var savedatares = await _setpermissionservice.UpdateUserdetail(item);
                         
                }
                return Json("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> RolePermission(string Status)
        {
            try
            {
                var roleListddl = (await _setpermissionservice.bindrole()).RoleList.ToList();
                List<SelectListItem> rolesddl = new List<SelectListItem>{new SelectListItem { Text = "--Select a Role--", Value = "" }};
                foreach (var role in roleListddl)
                {
                    rolesddl.Add(new SelectListItem { Text = role.RoleName, Value = role.Roleid.ToString() });
                }
                ViewBag.Rolelistddl = rolesddl; // Ensure this line is setting ViewBag correctly


                var functionlistddl = (await _setpermissionservice.bindFunction()).FunctionList.ToList();
                List<SelectListItem> functionddl = new List<SelectListItem> ();
                foreach (var role in functionlistddl)
                {
                    functionddl.Add(new SelectListItem { Text = role.FunctionName, Value = role.Functionid.ToString() });
                }
                ViewBag.Functionlistddl = functionddl;


                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public IActionResult AddRolePermission(RoleSet roleSet)
        {
            try
            {
                var userData = HttpContext.Request.Form["RoleSet"];
                var ResultRoleData = JsonConvert.DeserializeObject<List<RoleSet>>(userData);
                foreach (var item in ResultRoleData)
                {
                    var insdataress = _setpermissionservice.SaveRolefunction(item);

                }
                return Json("");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
    }
    
}


