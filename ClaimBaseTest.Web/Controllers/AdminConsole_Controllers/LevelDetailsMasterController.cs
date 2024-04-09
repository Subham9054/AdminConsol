using CodeGen.Repository.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeGen.Model.LevelDetailMasterModel;
namespace ClaimBaseTest.Web
{
    public class LevelDetailsMasterController : Controller
    {       
        public readonly IHierarchyServiceProviderRepository _hirerarchyServiceProvider; 
        public readonly  ILevelDetailsServiceProvider  _levelDetailsServiceProvider ;
        public readonly ILevelServiceProvider _levelServiceProvider ;
         LevelDetailMaster model = new LevelDetailMaster();

        public LevelDetailsMasterController(IHierarchyServiceProviderRepository hirerarchyServiceProvider,ILevelDetailsServiceProvider levelDetailsServiceProvider,ILevelServiceProvider levelServiceProvider)
        {
            _hirerarchyServiceProvider = hirerarchyServiceProvider;
            _levelDetailsServiceProvider=levelDetailsServiceProvider;
            _levelServiceProvider=levelServiceProvider;
        }
           [HttpGet]
          public async Task<IActionResult> AddLevelDetailsMaster(string Status)
          {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{

                model.HierarchyList = (await _levelDetailsServiceProvider.GetHierarchy()).HierarchyList.ToList(); //(await _hirerarchyServiceProvider.GetAllHierarchy(1)).HierarchyList.ToList();
               // model.INTCREATEDBY = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                return View(model);
            }
              // else
              // {
               // return RedirectToAction("Login", "Home");
              // }
           
        [HttpGet]
        public async Task<JsonResult> BindParentLevelByHierarchyId(int LId, int HId)
        {
            var result = (await _levelServiceProvider.GetAllLevelByHierarchyId(HId)).ParentLevelList.ToList();

            return Json(result);
        }
            [HttpGet]
        public async Task<JsonResult> BindParentLevelDetailsByHierarchyId(int HId, int PLId)
        {
            //var result = (await _levelDetailsServiceProvider.GetAllLevelDetailsByHierarchyId(PLId)).ParentLevelDetailsList.ToList();
            var result1 = (await _levelDetailsServiceProvider.GetAllLevelParentDetailsByHierarchyId(PLId, HId)).ParentLevelDetailsList.ToList();

            return Json(result1);
        }
        [HttpGet]
        public async Task<JsonResult> BindLevelByParentId(int PLId)
        {
            var result = (await _levelDetailsServiceProvider.GetLevelByParentId(PLId)).ParentLevelDetailsList.ToList();

            return Json(result);
        }
        [HttpGet]
        public async Task<JsonResult> BindParentLevelDetailsForEdit()
        {
            var result = (await _levelDetailsServiceProvider.GetAllLevelDetailsForEdit()).ParentLevelDetailsList.ToList();
            return Json(result);
        }
        #region Add  Level Details
        [HttpPost]
        public IActionResult AddLevelDetailsMaster(LevelDetailMaster objlevel)
        {
            try
            {
                if (objlevel.ParentPosition == 3)
                {
                    objlevel.INTPARENTID = objlevel.INTPARENTID_block;
                }
                else if (objlevel.ParentPosition == 4)
                {
                    objlevel.INTPARENTID = objlevel.INTPARENTID_gp;
                }
                else if (objlevel.ParentPosition == 5)
                {
                    objlevel.INTPARENTID = objlevel.INTPARENTID_level5;
                }
                else
                {
                    objlevel.INTPARENTID = objlevel.INTPARENTID;
                }
                var Result = _levelDetailsServiceProvider.AddLevelDetails(objlevel);
                //return RedirectToAction("ViewActiveLevelDetails", new { Status = "Record " + Result });
                return RedirectToAction("ViewActiveLevelDetails", new { Status = "Added Successfully." });

                //if (Result == "Save Successfully")
                //{
                //    return RedirectToAction("ViewActiveLevelDetails", new { Status = "Record " + Result });

                //}
                //else

                //{
                //    model.HierarchyList = (await _hirerarchyServiceProvider.GetAllHierarchy(1)).HierarchyList.ToList();
                //    model.INTHIERARCHYID = objlevel.INTHIERARCHYID;
                //    model.INTLEVELID = objlevel.INTLEVELID;
                //    model.INTPARENTID = objlevel.INTPARENTID;
                //    TempData["CommandStatus"] = "Record " + Result;
                //    return View(model);
                //}

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion
        #region view  Level Details data
        [HttpGet]
        public async Task<IActionResult> ViewActiveLevelDetails(string Status)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{


                try
                {
                    LevelDetailsMasterModel vmodel = new LevelDetailsMasterModel();
                    vmodel.LevelDetailsList = (await _levelDetailsServiceProvider.GetAllLevelDetails(1)).LevelDetailsList.ToList();
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
        public async Task<IActionResult> ViewInActiveLevelDetails(string Status)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{

                try
                {
                    LevelDetailsMasterModel vmodel = new LevelDetailsMasterModel();
                    vmodel.LevelDetailsList = (await _levelDetailsServiceProvider.GetAllLevelDetails(0)).LevelDetailsList.ToList();
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
        #region Get  Level Details by Id
        public async Task<IActionResult> EditActiveLevelDetails(int id)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{

                try
                {
                    //model.HierarchyList = (await _hirerarchyServiceProvider.GetAllHierarchy(1)).HierarchyList.ToList();
                    // model.ParentLevelDetailsList_Edit = (await _levelDetailsServiceProvider.GetAllLevelDetailsForEdit()).ParentLevelDetailsList_Edit.ToList();
                    model.HierarchyList = (await _levelDetailsServiceProvider.GetHierarchy()).HierarchyList.ToList();
                    var result = _levelDetailsServiceProvider.GetLevelDetailsById(id).Result;
                    model.INTHIERARCHYID = result.ToList()[0].INTHIERARCHYID;
                    model.INTLEVELID = result.ToList()[0].INTLEVELID;
                    model.INTPARENTID = result.ToList()[0].INTPARENTID;
                    model.NVCHLEVELNAME = result.ToList()[0].NVCHLEVELNAME;
                    model.VCHALIAS = result.ToList()[0].VCHALIAS;

                    model.VCHFAXNO = result.ToList()[0].VCHFAXNO;
                    model.VCHTELNO = result.ToList()[0].VCHTELNO;
                    model.INTLEVELDETAILID = result.ToList()[0].INTLEVELDETAILID;
                    model.ParentPosition = result.ToList()[0].ParentPosition;
                    model.INTPARENTID_block = result.ToList()[0].INTPARENTID_block;
                    model.INTPARENTID_gp = result.ToList()[0].INTPARENTID_gp;
                    model.INTPARENTID_level5 = result.ToList()[0].INTPARENTID_level5;
                    //model.INTCREATEDBY = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
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
        #region Update Level Details
        [HttpPost]
        public IActionResult EditLevelDetails(LevelDetailMaster objlevel)
        {
            try
            {
                if (objlevel.ParentPosition == 3)
                {
                    objlevel.INTPARENTID = objlevel.INTPARENTID_block;
                }
                else if (objlevel.ParentPosition == 4)
                {
                    objlevel.INTPARENTID = objlevel.INTPARENTID_gp;
                }
                else if (objlevel.ParentPosition == 5)
                {
                    objlevel.INTPARENTID = objlevel.INTPARENTID_level5;
                }
                else
                {
                    objlevel.INTPARENTID = objlevel.INTPARENTID;
                }
                var Result = _levelDetailsServiceProvider.EditLevelDetails(objlevel);
                return RedirectToAction("ViewActiveLevelDetails", new { Status = "Record " + Result });

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
                LevelDetailsMasterModel vmodel = new LevelDetailsMasterModel();
                string LevelId = string.Join(",", chkbox);
                var Result = String.Empty;
               // vmodel.INTUPDATEDBY = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

                foreach (string Id in chkbox)
                {
                    vmodel.INTLEVELDETAILID = Id;
                    Result = _levelDetailsServiceProvider.MarkInactive(vmodel);
                }
                return RedirectToAction("ViewActiveLevelDetails", new { Status = "Record " + Result });

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

                LevelDetailsMasterModel vmodel = new LevelDetailsMasterModel();
               // vmodel.INTUPDATEDBY = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                string LevelId = string.Join(",", chkbox);
                var Result = String.Empty;

                foreach (string Id in chkbox)
                {
                    vmodel.INTLEVELDETAILID = Id;
                    Result = _levelDetailsServiceProvider.MarkActive(vmodel);
                }
                return RedirectToAction("ViewActiveLevelDetails", new { Status = "Record " + Result });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
    }
    
 }
