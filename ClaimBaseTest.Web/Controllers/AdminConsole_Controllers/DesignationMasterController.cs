using CodeGen.Repository.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeGen.Model.DesignationMasterModel;
namespace ClaimBaseTest.Web
{
    public class DesignationMasterController : Controller
    {    public IDesignationServiceProvider _DesignationServiceProvider { get; }
        DesignationMaster model = new DesignationMaster();
        public DesignationMasterController(IDesignationServiceProvider DesignationServiceProvider)
        {
            _DesignationServiceProvider = DesignationServiceProvider;
        }   
            [HttpGet]
        public IActionResult AddDesignationMaster(string Status)
        {
           // if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{

                TempData["CommandStatus"] = Status;
                //ViewBag.CREATEDBY = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

                return View();
           // }
           // else
            //{
             //   return RedirectToAction("Login", "Home");
            //}
        }
        #region Add  DesignationMaster
        [HttpPost]
        public IActionResult AddDesignationMaster(DesignationMaster model)
        {
            try
            {
                var Result = _DesignationServiceProvider.AddDesignation(model);
                return RedirectToAction("ViewActiveDesignation", new { Status = Result });

                //return View();
                //return RedirectToAction("ViewActiveDesignation", new { Status = "Record " + Result });
                //if (Result == "Already Exist")
                //{
                //    TempData["CommandStatus"] = "Record " + Result;
                //    return View(model);
                //}
                //else
                //{
                //    return RedirectToAction("ViewActiveHirerarchy", new { Status = "Record " + Result });
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region view  DesignationMaster
        [HttpGet]
        public async Task<IActionResult> ViewActiveDesignation(string Status)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{

                try
                {
                    DesignationMasterModel vmodel = new DesignationMasterModel();
                    vmodel.DesignationMasterList = (await _DesignationServiceProvider.GetAllDesgination(1)).DesignationMasterList.ToList();
                    TempData["CommandStatus"] = Status;
                    return View(vmodel);

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            //}
            //else
            //{
                return RedirectToAction("Login", "Home");
            //}
        }
        [HttpGet]
        public async Task<IActionResult> ViewInActiveDesignation(string Status)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{
                try
                {
                    DesignationMasterModel vmodel = new DesignationMasterModel();
                    vmodel.DesignationMasterList = (await _DesignationServiceProvider.GetAllDesgination(0)).DesignationMasterList.ToList();
                    TempData["CommandStatus"] = Status;
                    return View(vmodel);

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

        #endregion
        #region Get  Designation by Id
        public async Task<IActionResult> EditActiveDesignation(int id)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{

                try
                {
                    DesignationMasterModel vmodel = new DesignationMasterModel();
                    vmodel.DesignationMasterList = (await _DesignationServiceProvider.GetById(id)).DesignationMasterList.ToList();
                    model.INTDESIGID = vmodel.DesignationMasterList[0].INTDESIGID;
                    model.NVCHDESIGNAME = vmodel.DesignationMasterList[0].NVCHDESIGNAME;
                    
                    model.NVCHALIASNAME = vmodel.DesignationMasterList[0].NVCHALIASNAME;
                  
                    //model.INTUPDATEDBY = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
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
        #endregion
        #region Update Designation
        [HttpPost]
        public IActionResult EditDesignation(DesignationMaster model)
        {
            try
            {

                var Result = _DesignationServiceProvider.EditDesignation(model);
                return RedirectToAction("ViewActiveDesignation", new { Status = "Record " + Result });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
        #region Mark As Inactive
        [HttpPost]
        public IActionResult MarkAsInactive(string[] chkbox)
        {
            try
            {


                //model.INTUPDATEDBY = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                string primarylinkId = string.Join(",", chkbox);
                var Result = String.Empty;
                foreach (string Id in chkbox)
                {
                    model.INTDESIGID = Convert.ToInt32(Id);
                    Result = _DesignationServiceProvider.MarkInactive(model);
                }

                return RedirectToAction("ViewActiveDesignation", new { Status = "Record " + Result });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
        #region Mark As Active
        [HttpPost]
        public IActionResult MarkAsActive(string[] chkbox)
        {
            try
            {
                //model.INTUPDATEDBY = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                string primarylinkId = string.Join(",", chkbox);
                var Result = String.Empty;
                foreach (string Id in chkbox)
                {
                    model.INTDESIGID = Convert.ToInt32(Id);
                    Result = _DesignationServiceProvider.MarkActive(model);
                }

                return RedirectToAction("ViewActiveDesignation", new { Status = "Record " + Result });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
    }
    
 }
