using CodeGen.Model.HirerachyMaster;
using CodeGen.Repository.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ClaimBaseTest.Web
{
    public class HirerachyMasterController : Controller
    {



        public IHierarchyServiceProviderRepository _hirerarchyServiceProvider { get; }
        Hierarchy model = new Hierarchy();

        public HirerachyMasterController(IHierarchyServiceProviderRepository hirerarchyServiceProvider)
        {
            _hirerarchyServiceProvider = hirerarchyServiceProvider;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddHirerarchy(string Status)
        {
            //    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //    {

            TempData["CommandStatus"] = Status;
            //ViewBag.CREATEDBY = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Home");
            //}
        }
        #region Add  Hierarchy
        [HttpPost]
        public IActionResult AddHirerarchy(Hierarchy objhierarchy)
        {

            try
            {

                var Result = _hirerarchyServiceProvider.AddHierarchy(objhierarchy);
                //return RedirectToAction("ViewActiveHirerarchy", new { Status = "sucessfully added " + Result });
                return RedirectToAction("ViewActiveHirerarchy", new { Status = "Sucessfully Added " });

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
        #region view  Hierarchy
        [HttpGet]
        public async Task<IActionResult> ViewActiveHirerarchy(string Status)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{ 

            try
            {
                HierarchyModel vmodel = new HierarchyModel();
                vmodel.HierarchyList = (await _hirerarchyServiceProvider.GetAllHierarchy(1)).HierarchyList.ToList();
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
        [HttpGet]
        public async Task<IActionResult> ViewInActiveHirerarchy(string Status)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{
                try
                {
                    HierarchyModel vmodel = new HierarchyModel();
                    vmodel.HierarchyList = (await _hirerarchyServiceProvider.GetAllHierarchy(0)).HierarchyList.ToList();
                    TempData["CommandStatus"] = Status;
                    return View(vmodel);

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            //else
            //{
            //    return RedirectToAction("Login", "Home");
            //}




        
        #endregion
        #region Get  Hierarchy by Id
        public async Task<IActionResult> EditActiveHierarchy(int id)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{ 

            try
            {
                HierarchyModel vmodel = new HierarchyModel();
                vmodel.HierarchyList = (await _hirerarchyServiceProvider.GetById(id)).HierarchyList.ToList();
                model.INTHIERARCHYID = vmodel.HierarchyList[0].INTHIERARCHYID;
                model.NVCHHIERARCHYNAME = vmodel.HierarchyList[0].NVCHHIERARCHYNAME;
                model.INTNOLEVEL = vmodel.HierarchyList[0].INTNOLEVEL;
                model.VCHHIERARCHYALIAS = vmodel.HierarchyList[0].VCHHIERARCHYALIAS;
                model.NVCHADDRESS = vmodel.HierarchyList[0].NVCHADDRESS;
                //model.INTUPDATEDBY = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                return View(model);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //else
        //{
        //    return RedirectToAction("Login", "Home");
        //}

        #endregion
        #region Update Hierarchy
        [HttpPost]
        public IActionResult EditHierarchy(Hierarchy objhierarchy)
        {
            try
            {

                var Result = _hirerarchyServiceProvider.EditHierarchy(objhierarchy);
                //return RedirectToAction("ViewActiveHirerarchy", new { Status = "Record " + Result });
                return RedirectToAction("ViewActiveHirerarchy", new { Status = "Updated Successfully " });
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


              //  model.INTUPDATEDBY = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                string primarylinkId = string.Join(",", chkbox);
                var Result = String.Empty;
                foreach (string Id in chkbox)
                {
                    model.INTHIERARCHYID = Convert.ToInt32(Id);
                    Result = _hirerarchyServiceProvider.MarkInactive(model);
                    
                }

                return RedirectToAction("ViewActiveHirerarchy", new { Status = "Record " + Result });

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
               // model.INTUPDATEDBY = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                string primarylinkId = string.Join(",", chkbox);
                var Result = String.Empty;
                foreach (string Id in chkbox)
                {
                    model.INTHIERARCHYID = Convert.ToInt32(Id);
                    Result = _hirerarchyServiceProvider.MarkActive(model);
                }

                return RedirectToAction("ViewActiveHirerarchy", new { Status = "Record " + Result });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
    }
    
 }
