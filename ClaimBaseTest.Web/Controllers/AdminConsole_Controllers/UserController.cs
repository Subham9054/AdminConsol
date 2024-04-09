using CodeGen.Model.CommonFunction;
using CodeGen.Model.User;
//using CodeGen.Model.Set_Permission;
using CodeGen.Repository.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CodeGen.Model.LoginModel;
namespace ClaimBaseTest.Web
{
  public class UserController : Controller
    {
        private readonly IUserServiceProvider _userService;
        private IWebHostEnvironment _hostingEnvironment;
        public IHierarchyServiceProviderRepository _hirerarchyServiceProvider { get; }
        public UserController(IUserServiceProvider userService, IWebHostEnvironment hostingEnvironment, IHierarchyServiceProviderRepository hirerarchyServiceProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _userService = userService;
            _hirerarchyServiceProvider = hirerarchyServiceProvider;
        }
        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{
                ViewBag.ParentLoc = (await _hirerarchyServiceProvider.GetAllHierarchy(1)).HierarchyList.ToList();
                // ViewBag.Location = _userService.GetLevelDetails();
                ViewBag.PhysicalLoc = _userService.GetLocation();
                ViewBag.Designation = _userService.GetDesignations();
                var data = CommonFunction.EncryptData("Pass#2021");
                return View("AddUser");
            //}
            //else
            //{
            //   return RedirectToAction("LogOut", "Home");
            //}
        }
        [HttpPost]
        public JsonResult AddUserData()
        {
            try
            {
                User user = new User();
                string ext = string.Empty;
                string Result = string.Empty;
                if (Request.Form.Files.Count == 0)
                {
                    user.UserImage = "";
                }
                else
                {
                    string ProcDocPath = _hostingEnvironment.WebRootPath + "\\UserImage";
                    if (!Directory.Exists(ProcDocPath))
                        Directory.CreateDirectory(ProcDocPath);

                    var file = Request.Form.Files;
                    var filename = Path.GetExtension(ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Trim('"'));
                    var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    var fullname = "User" + timestamp + "" + filename;
                    using (var stream = new FileStream(Path.Combine(ProcDocPath, fullname), FileMode.Create))
                    {
                        file[0].CopyTo(stream);
                    }
                    ext = Path.GetExtension(fullname);
                    user.UserImage = fullname;
                    //if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                    //{
                    //    //Result = "Please upload a valid document of type jpg/jpeg/png.!!!";
                    //    return Json(Result);
                    //}
                }
                user.ActionCode = "A";              
                user.UserName = HttpContext.Request.Form["UserName"].ToString();
                user.FullName = HttpContext.Request.Form["Name"].ToString();
                user.UserPasswaord = CommonFunction.EncryptData(HttpContext.Request.Form["Password"].ToString()).ToString();
                user.intLocation = Convert.ToInt32(HttpContext.Request.Form["PhyscalLocation"].ToString());
                user.LeveleID = Convert.ToInt32(HttpContext.Request.Form["Location"].ToString());
                if (HttpContext.Request.Form["DOJ"].ToString() == "")
                {
                    user.DateOfJoing = "";
                }
                else
                {
                    user.DateOfJoing = Convert.ToDateTime(HttpContext.Request.Form["DOJ"].ToString()).ToString("dd-MMM-yyyy");
                }
                if (HttpContext.Request.Form["DOB"].ToString() == "")
                {
                    user.DateOfBirth = "";
                }
                else
                {
                    user.DateOfBirth = Convert.ToDateTime(HttpContext.Request.Form["DOB"].ToString()).ToString("dd-MMM-yyyy");
                }
                user.DesignationID = HttpContext.Request.Form["Designation"].ToString();
                user.Gender = HttpContext.Request.Form["Gender"].ToString();
                user.email = HttpContext.Request.Form["Email"].ToString();
                user.Mobile = HttpContext.Request.Form["Mobile"].ToString();
                //user.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                Result = _userService.CreateUser(user).ToString();
                return Json(Result);
            }
            catch(Exception ex) {
                return Json(ex.Message);
            } 
            
        }
        [HttpPost]
        public JsonResult EditUserData()
        {

            try
            {
                User user = new User();
                string ProcDocPath = _hostingEnvironment.WebRootPath + "\\UserImage";
                string ext=string.Empty;
                if (!Directory.Exists(ProcDocPath))
                    Directory.CreateDirectory(ProcDocPath);
                var file = Request.Form.Files;
                if (file.Count > 0)
                {
                    var filename = Path.GetExtension(ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Trim('"'));
                    var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    var fullname = "User" + timestamp + "" + filename;
                    using (var stream = new FileStream(Path.Combine(ProcDocPath, fullname), FileMode.Create))
                    {
                        file[0].CopyTo(stream);
                    }
                    user.UserImage = fullname;
                    ext = Path.GetExtension(user.UserImage);
                }
                else
                {
                    user.UserImage = HttpContext.Request.Form["Image"].ToString();
                   ext = Path.GetExtension(user.UserImage);
                }
                if (ext== ".jpg" || ext == ".jpeg" || ext == ".png")//jpg/jpeg/png
                {
                    user.ActionCode = "U";
                    user.UserName = HttpContext.Request.Form["UserName"].ToString();
                    user.FullName = HttpContext.Request.Form["Name"].ToString();
                    user.intuserid = Convert.ToInt32(HttpContext.Request.Form["UserId"].ToString());
                    user.intLocation = Convert.ToInt32(HttpContext.Request.Form["PhyscalLocation"].ToString());
                    user.LeveleID = Convert.ToInt32(HttpContext.Request.Form["Location"].ToString());
                    user.DateOfJoing = Convert.ToDateTime(HttpContext.Request.Form["DOJ"].ToString()).ToString("dd-MMM-yyyy");
                    user.DateOfBirth = Convert.ToDateTime(HttpContext.Request.Form["DOB"].ToString()).ToString("dd-MMM-yyyy");
                    user.DesignationID = HttpContext.Request.Form["Designation"].ToString();
                    user.Gender = HttpContext.Request.Form["Gender"].ToString();
                    user.email = HttpContext.Request.Form["Email"].ToString();
                    user.Mobile = HttpContext.Request.Form["Mobile"].ToString();
                    //user.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("UserId")); ;
                    var result = _userService.EditUser(user);
                    TempData["userResult"] = result;
                    return Json(result);
                }
                else
                {
                    return Json("Please upload a valid document of type jpg/jpeg/png.!!!");
                }

               
              
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        [HttpGet]
        public JsonResult getLevelDetailsByParentId(int ParentId)
        {
            var result = _userService.GetLevelDetailsByParentId(ParentId);
            return Json(result);
        }
        [HttpGet]
        public JsonResult getleveldetailsbyhirarchyid(int hirarchyid)
        {
            var result = _userService.GetLevelDetails(hirarchyid);
            return Json(result);
        }
        [HttpGet]
        public IActionResult ViewUser(string msg)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{
                // TempData.Keep("userResult");
                TempData["userResult"] = msg;
                ViewBag.DesigId = 0;
                ViewBag.LocId = 0;
                ViewBag.Mob = "";
                ViewBag.Uname = "";
                ViewBag.PhysicalLoc = _userService.GetLocation();
                ViewBag.Designation = _userService.GetDesignations();
                ViewBag.UserDetails = _userService.GetAllUsers(new User { ActionCode = "VF" });
                return View("ViewUser");
            //}
            //else
            //{
            //    return RedirectToAction("LogOut", "Home");
            //}
        }
        [HttpPost]
        public IActionResult ViewSearchUser(User user)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{
                ViewBag.DesigId = user.intDesignationID;
                ViewBag.LocId = user.intLocation;
                ViewBag.Mob = user.vchmobtel;
                ViewBag.Uname = user.vchusername;
                ViewBag.PhysicalLoc = _userService.GetLocation();
                ViewBag.Designation = _userService.GetDesignations();
                ViewBag.UserDetails = _userService.GetAllUsers(new User { ActionCode = "VF", intDesignationID = user.intDesignationID, intLocation = user.intLocation, vchmobtel = user.vchmobtel, vchusername = user.vchusername });
                return View("ViewUser");
            //}
            //else
            //{
            //    return RedirectToAction("LogOut", "Home");
            //}
        }
        public async Task<IActionResult> EditUser(int Id)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{
                ViewBag.ParentLoc = (await _hirerarchyServiceProvider.GetAllHierarchy(1)).HierarchyList.ToList();
                // ViewBag.Location = _userService.GetLevelDetails(1);
                ViewBag.PhysicalLoc = _userService.GetLocation();
                ViewBag.Designation = _userService.GetDesignations();
                ViewBag.UserDetails = _userService.GetUserById(Id, "VS");
                return View("EditUser");
            //}
            //else
            //{
            //    return RedirectToAction("LogOut", "Home");
            //}
        }
        public IActionResult DeleteUser(int Id)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{
                TempData["userResult"] = _userService.DeleteUser(new User { ActionCode = "I", intuserid = Id, CreatedBy = 1 });
                return RedirectToAction("ViewUser", new { msg = TempData["userResult"] });
              //  return RedirectToAction("ViewUser");
            //}
            //else
            //{
            //    return RedirectToAction("LogOut", "Home");
            //}
        }
         
        #region For Login 
        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> UserLogin(CodeGenLogin log)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join("|", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage));
                    return Json(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
                }
                else
                {
                    var data = await _userService.GetLoginDetails(log);
                    if (data != null)
                    {
                        HttpContext.Session.SetInt32("userId", data.INTUSERID);
                        HttpContext.Session.SetInt32("roleId", (int)data.ROLEID);
                        HttpContext.Session.SetInt32("DesgId", (int)data.INTDESIGNATIONID);
                        HttpContext.Session.SetString("RoleName", data.ROLENAME);
                        HttpContext.Session.SetString("UserName", data.VCHUSERNAME);
                        HttpContext.Session.SetString("FullName", data.VCHFULLNAME);
                        return Json(new { sucess = true, responseMessage = "Login Successfully.", responseText = "Success", data = data });
                    }
                    else
                    {
                        return Json(new { sucess = true, responseMessage = "Login Successfully.", responseText = "Error", data = data });
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }

}


