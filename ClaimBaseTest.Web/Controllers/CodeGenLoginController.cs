using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using CodeGen.Model.LoginModel;
using CodeGen.Model.forgetpassmodel;
using CodeGen.Repository.Repositories.Interfaces;
using ClientsideEncryption;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
//using CodeGen.Model.User;
//using Microsoft.AspNetCore.Authorization;

namespace ClaimBaseTest.Web
{
    public class CodeGenLoginController : Controller
    {
        public IConfiguration Configuration;
        private readonly ICodeGenLoginRepository _CodeGenLoginRepository;
        private readonly ISendSMSRepository _sms;
        public CodeGenLoginController(IConfiguration configuration, ICodeGenLoginRepository CodeGenLoginRepository, ISendSMSRepository sms)
        {
            Configuration = configuration;
            _CodeGenLoginRepository = CodeGenLoginRepository;
            _sms = sms;
        }


        public IActionResult UserLogin()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UserLogin(CodeGenLogin modelLogin)
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
                    modelLogin.UserName = AESEncrytDecry.DecryptStringAES(modelLogin.UserName);
                    modelLogin.Password = AESEncrytDecry.DecryptStringAES(modelLogin.Password);
                    var data = await _CodeGenLoginRepository.GetLoginDetails(modelLogin);
                    if (data != null)
                    {
                        //ViewData["ValidateMessage"] = true;
                        #region Claim Data
                        List<Claim> claims = new List<Claim>()
                                {
                                    new Claim(ClaimTypes.NameIdentifier, data.VCHFULLNAME),
                                    new Claim(ClaimTypes.Name, data.VCHEMAIL)
                                };

                        foreach (var role in data.NVCHDESIGNAME.Split(','))
                        {
                            // Dynamically add role claims based on ROLENAME retrieved from database
                            claims.Add(new Claim(ClaimTypes.Role, role.Trim()));
                        }
                        // Add additional claims for INTUSERID and INTDESIGNATIONID
                        claims.Add(new Claim("INTUSERID", data.INTUSERID.ToString()));
                        claims.Add(new Claim("INTDESIGNATIONID", data.INTDESIGNATIONID.ToString()));

                        //HttpContext.Session.SetInt32("userId", data.INTUSERID);
                        //if (data.INTDESIGNATIONID != 0)
                        //{
                        //    HttpContext.Session.SetInt32("DesgId", (int)data.INTDESIGNATIONID);
                        //}

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        AuthenticationProperties properties = new AuthenticationProperties()
                        {
                            AllowRefresh = true,
                            IsPersistent = modelLogin.KeepLoggedIn
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                        #endregion

                        HttpContext.Session.SetString("UserName", data.VCHUSERNAME);
                        HttpContext.Session.SetInt32("userId", data.INTUSERID);
                        if (data.INTDESIGNATIONID != 0)
                        {
                            HttpContext.Session.SetInt32("DesgId", (int)data.INTDESIGNATIONID);
                        }
                        return Json(new { sucess = true, responseMessage = "Action taken Successfully.", responseText = "Success", data = data });
                    }
                    else
                    {
                        //ViewData["ValidateMessage"] = "Invalid UserId Or password";
                        var message = "Invalid UserId Or password";
                        return Json(new { sucess = false, responseMessage = message, responseText = "Login Failed !", data = "" });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //CodeGenLogin vm = await _user.GetLoginAsync(modelLogin.Email, modelLogin.PassWord);

        //if (vm != null)
        //{
        //    if (modelLogin.Email == vm.VCHEMAIL && modelLogin.PassWord == vm.VCHPASSWORD)
        //    {
        //        List<Claim> claims = new List<Claim>()
        //        {
        //            new Claim(ClaimTypes.NameIdentifier, vm.VCHFULLNAME),
        //            new Claim(ClaimTypes.Name, vm.VCHEMAIL)
        //        };

        //        foreach (var role in vm.ROLENAME.Split(','))
        //        {
        //            // Dynamically add role claims based on ROLENAME retrieved from database
        //            claims.Add(new Claim(ClaimTypes.Role, role.Trim()));
        //        }

        //        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //        AuthenticationProperties properties = new AuthenticationProperties()
        //        {
        //            AllowRefresh = true,
        //            IsPersistent = modelLogin.KeepLoggedIn
        //        };

        //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

        //        return RedirectToAction("Index", "Home");
        //    }
        //}

        //ViewData["ValidateMessage"] = "User not found";
        //return View();



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin1(CodeGenLogin log)
        //public async Task<JsonResult> UserLogin(CodeGenLogin log)
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
                    log.UserName = AESEncrytDecry.DecryptStringAES(log.UserName);
                    log.Password = AESEncrytDecry.DecryptStringAES(log.Password);
                    var data = await _CodeGenLoginRepository.GetLoginDetails(log);
                    if (data != null)
                    {
                        #region claims
                        //if (log.UserName == data.VCHUSERNAME && log.Password == AESEncrytDecry.DecryptStringAES(data.VCHPASSWORD.Trim()))
                        //{
                        List<Claim> claims = new List<Claim>()
                            {
                                new Claim(ClaimTypes.NameIdentifier, data.VCHFULLNAME),
                                new Claim(ClaimTypes.Name, data.VCHEMAIL)
                            };

                        foreach (var role in data.NVCHDESIGNAME.Split(','))
                        {
                            // Dynamically add role claims based on ROLENAME retrieved from database
                            claims.Add(new Claim(ClaimTypes.Role, role.Trim()));
                        }
                        claims.Add(new Claim("INTUSERID", data.INTUSERID.ToString()));
                        claims.Add(new Claim("INTDESIGNATIONID", data.INTDESIGNATIONID.ToString()));
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        AuthenticationProperties properties = new AuthenticationProperties()
                        {
                            AllowRefresh = true,
                            IsPersistent = log.KeepLoggedIn
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                        #region Previous code Session Data
                        HttpContext.Session.SetInt32("userId", data.INTUSERID);
                        HttpContext.Session.SetString("UserName", data.VCHUSERNAME);
                        //if (data.ROLEID != 0)
                        //{
                        //    HttpContext.Session.SetInt32("roleId", (int)data.ROLEID);
                        //}

                        if (data.INTDESIGID != 0)
                        {
                            HttpContext.Session.SetInt32("roleId", (int)data.INTDESIGID);
                        }

                        //if (data.INTDESIGNATIONID != 0)
                        //{
                        //    HttpContext.Session.SetInt32("DesgId", (int)data.INTDESIGNATIONID);
                        //}
                        if (data.NVCHDESIGNAME != null)
                        {
                            HttpContext.Session.SetString("RoleName", data.NVCHDESIGNAME);
                        }
                        #endregion
                        //}
                        #endregion
                        return RedirectToAction("Index", "Home");
                        //return Json(new { sucess = true, responseMessage = "Action taken Successfully.", responseText = "Success", data = data });
                    }
                    else
                    {
                        var message = "Invalid UserId Or password";
                        return Json(new { sucess = false, responseMessage = message, responseText = "Login Failed !", data = "" });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            ViewBag.userID = HttpContext.Session.GetInt32("userId");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ChangePassword(CodeGenLogin Upd)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return Json(new { sucess = false, responseMessage = message, responseText = "Model State is invalid", data = "" });
            }
            else
            {
                var data = await _CodeGenLoginRepository.ChangePwd(Upd);
                if (data == 1)
                {
                    return Json(new { sucess = true, responseMessage = "Action taken Successfully.", responseText = "Update Successfully", data = data });

                }
                return Json(new { sucess = true, responseMessage = "Error.", responseText = "Current Password Invalid", data = data });
            }
        }
        #region Send SMS Code Added By Manoj
        public IActionResult Forgetpass()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Forgetpass(forgetpassmodel model)
        {
            var result = _CodeGenLoginRepository.GetLoginDetails(model).Result.FirstOrDefault();
            if (result != null)
            {
                ModelState.Clear();
                model.emailsent = true;
                int OTP = 0;
                Random rnd = new Random();
                OTP = rnd.Next(1000, 9999);
                model.otp = OTP;
                model.roleid = result.roleid;
                var result1 = _CodeGenLoginRepository.insertotp(model);
                sendemail(model.vchemail, model.otp);
                return Json(new { sucess = true, responseMessage = "OTP Send Succesfully.", responseText = "Success" });
            }
            else
            {
                return Json(new { sucess = false, responseMessage = "Invalid Emailid or Userid!", responseText = "error" });
            }

        }


        public string sendemail(string tomail, int otp)
        {
            var body = "Your otp for forget password is " + otp + "";
            bool Res = false;
            try
            {
                var SmtpHost = Configuration.GetValue<string>("Email:Host");// "smtp.zoho.com";
                var FromEmail = Configuration.GetValue<string>("Email:Username");
                var Password = Configuration.GetValue<string>("Email:Password");
                var toEmail = tomail;
                var Port = Configuration.GetValue<string>("Email:Port");
                var enbleSSl = true;
                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient(SmtpHost);
                mail.From = new MailAddress(FromEmail);

                mail.To.Add(toEmail);

                mail.Subject = "OTP Send For Forget Password";
                //mail.Subject = "OTP Send For marraige Soumya Ranjan Das";
                mail.Body = body;

                SmtpServer.Port = Convert.ToInt32(Port);

                SmtpServer.Credentials = new System.Net.NetworkCredential(FromEmail, Password);

                SmtpServer.EnableSsl = enbleSSl;

                mail.IsBodyHtml = true;

                SmtpServer.Send(mail);

                Res = true;

                return "Mail sent successfully";
            }
            catch (Exception ex)
            {
                var error = "";
                if (ex.InnerException.Message == null)
                {
                    error = ex.Message;
                }
                else
                {
                    error = ex.InnerException.Message;
                }
                Res = false;
                return error;
            }
        }

        [HttpPost]
        public JsonResult checkotp(forgetpassmodel model)
        {
            var result = _CodeGenLoginRepository.Otpcheck(model).Result;
            if (result.otp == model.otp)
            {
                return Json(new { sucess = true, responseMessage = "OTP Verified Succesfully.", responseText = "Success" });
            }
            else
            {
                return Json(new { sucess = false, responseMessage = "Invalid OTP!", responseText = "error" });
            }

        }

        public IActionResult Newpassword(string Domainname)
        {
            ViewBag.domain = Domainname;
            return View();
        }
        [HttpPost]
        public JsonResult Newpassword(forgetpassmodel model)
        {
            var result = _CodeGenLoginRepository.NewPassword(model).Result;
            if (result == 1)
            {
                return Json(new { sucess = true, responseMessage = "Password Changed Succesfully.", responseText = "Success" });
            }
            else
            {
                return Json(new { sucess = false, responseMessage = "Something wrong happened please try again!", responseText = "error" });
            }
        }



        public IActionResult SendSMS()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendSMS(CodeGenLogin model)
        {
            var result = _CodeGenLoginRepository.validateuser(model).Result.FirstOrDefault();
            if (result != null)
            {
                ModelState.Clear();
                var templateid = "1007643220090940278";
                var message = "Dear User, a new activity with target completion date of 20-May-2023 has been added for IT department. 5T Monitoring";
                var smsresult = _sms.Sendsms(model.vchmobno, message, templateid);
                //return Json("SMS Sent Successfully.");
                return Json(smsresult.Result);
            }
            else
            {
                return Json("Invalid User!!");
            }

        }
        #endregion
    }
}
