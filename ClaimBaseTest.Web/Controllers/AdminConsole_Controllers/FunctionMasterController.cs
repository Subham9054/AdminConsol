using CodeGen.Repository.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CodeGen.Model.FunctionMasterModel;
using CodeGen.Model.CommonFunction;
using X.PagedList;
namespace ClaimBaseTest.Web
{
    public class FunctionMasterController : Controller
    {     // GET: /FunctionMaster/
        int intOut;
        FunctionMasterModel ObjFun = new FunctionMasterModel();
        private const int defaultPageSize =1000;
        private readonly IFuncServiceProvider _functionService;

        public FunctionMasterController(IFuncServiceProvider functionService)
        {

            _functionService = functionService;
        }
        
       public ActionResult AddFunctionMaster()
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{
                return View();
            //}
            //else
            //{
            //    return RedirectToAction("LogOut", "Home");
            //}
        }
        #region Add Function Master
        [HttpPost]
        public ActionResult AddFunctionMaster(IFormCollection Coll, FunctionMasterModel model, string Command)
        {
            if (Command == "Log")
            {
                //FileLogger fileLogger = new FileLogger();
                //string path = fileLogger.ShowLogFile();
                //ViewData["log"] = path;
                return View();
            }

            if (Command == "Submit")
            {
                try
                {
                    if (ModelState.IsValid)
                    {

                        model.ActionCode = "A";
                        model.FunctionName = Coll["FunctionName"].ToString();
                        model.FileName = Coll["FileName"].ToString();
                        model.Description = Coll["Description"].ToString();
                        if (Coll["CheckBoxAdd"].ToString() == "true,false")
                            model.FAdd = "Add";
                        else
                            model.FAdd = "N";
                        if (Coll["CheckBoxView"].ToString() == "true,false")
                            model.FView = "View";
                        else
                            model.FView = "N";
                        if (Coll["CheckBoxManage"].ToString() == "true,false")
                            model.FManage = "Manage";
                        else
                            model.FManage = "N";
                        model.MailR = Convert.ToInt16(Coll["MailR"]);
                        model.FreeBees = Convert.ToInt16(Coll["FreeBees"]);
                        model.PortletFile = Coll["PortletName"].ToString();
                        //model.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("UserId"));//Convert.ToInt16(Session["UserId"]);
                        intOut = _functionService.AddFunction(model);
                        ViewData["result"] = intOut;
                        ModelState.Clear();
                        //return View("AddFunctionMaster");
                        return RedirectToAction("ViewFunctionMasterActive", new { Status = "Sucessfully Added " });
                    }
                    else
                    {
                        return View();
                    }
                }
                catch (Exception)
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("AddFunctionMaster");
            }

        }
        #endregion
        #region View Active Function Master
        //[AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ViewFunctionMasterActive(IFormCollection Coll, int? pages, string Status)
        {
            //this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            //this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //this.Response.Cache.SetNoStore();
            //if (Session["UserId"] == null)
            //{
            //    return RedirectToAction("Default", "Login");
            //}
            ViewData["result"] = Status;
            FunctionMasterModel model = new FunctionMasterModel();
            model.FunctionName = Coll["fn"];
            string Funname = model.FunctionName;
            var pageNumber = pages ?? 1;
            model.ActionCode = "V";
            model.Flag = 1;
            if (model.FunctionName == "" || model.FunctionName == null)
            {
                model.FunctionName = string.Empty;
            }
            else
            {
                model.ActionCode = "V";
            }
            var Strmodel = _functionService.GetAllFunction(model).ToPagedList(pageNumber, defaultPageSize);
            ViewData["Funname"] = Funname;
            ViewBag.Strmodel = Strmodel;
            return View(Strmodel);

        }
        #endregion
        #region View Inactive Function Master
        //[AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ViewFunctionMasterInActive(IFormCollection Coll, int? page)
        {
            FunctionMasterModel model = new FunctionMasterModel();
            model.ActionCode = "V";
            model.FunctionName = Coll["fn"];
            string Funname = model.FunctionName;
            model.Flag = 0;
            var posts = _functionService.GetAllFunction(model);
            var pageNumber = page ?? 1;
            if (model.FunctionName == "" || model.FunctionName == null)
            {
                model.FunctionName = "";
            }

            var Strmodel = _functionService.GetAllFunction(model).ToPagedList(pageNumber, defaultPageSize); ;//.ToPagedList(pageNumber, defaultPageSize);
            ViewData["Funname"] = Funname;
            ViewBag.Strmodel = Strmodel;
            return View(Strmodel);


        }
        #endregion
        #region InActive Function Master
        //[HttpPost]
        public ActionResult InActiveFunctionMaster(params string[] checkResp)
        {
            try
            {
                foreach (string Id in checkResp)
                {
                    FunctionMasterModel model = new FunctionMasterModel();
                    model.ActionCode = "I";
                    // string strPids = string.Join(",", checkResp);
                    model.FunctionId = Id;
                    model.CreatedBy = 1; // Convert.ToInt16(Session["UserId"]);
                    intOut = _functionService.ActiveFunction(model);
                }
                //    FunctionMaster model = new FunctionMaster();
                //model.ActionCode = "I";
                //string strPids = string.Join(",", checkResp);
                //model.FunctionId = strPids;
                //model.CreatedBy = 1; // Convert.ToInt16(Session["UserId"]);
                //intOut = _functionService.ActiveFunction(model);

                return RedirectToAction("ViewFunctionMasterActive");

            }
            catch
            {
                return View();
            }
        }
     
        #endregion
        #region Active Function Master
        //[HttpPost]
        public ActionResult ActiveFunctionMaster(params string[] checkResp)
        {
            try
            {
                foreach (string Id in checkResp)
                {
                    FunctionMasterModel model = new FunctionMasterModel();
                    model.ActionCode = "T";
                    model.FunctionId = Id;
                    model.CreatedBy = 1;
                    intOut = _functionService.ActiveFunction(model);
                }
                //FunctionMaster model = new FunctionMaster();
                //model.ActionCode = "T";
                //string strPids = string.Join(",", checkResp);
                //model.FunctionId = strPids;
                //model.CreatedBy = 1;// Convert.ToInt16(Session["UserId"]);
                //intOut = _functionService.ActiveFunction(model);
                return RedirectToAction("ViewFunctionMasterInActive");
            }
            catch (Exception)
            {
                return View();
            }
        }
      
        #endregion

        public ActionResult EditActiveFunction(string id)
        {
            try
            {
                id = (CommonFunction.Decrypt(id));
                FunctionMasterModel model = new FunctionMasterModel();
                model.ActionCode = "E";
                model.FunctionId = id;
                model.Flag = 1;
                IList<FunctionMasterModel> objFunList = new List<FunctionMasterModel>();
                objFunList = _functionService.GetAllFunction(model);
                foreach (var i in objFunList)
                {
                    ObjFun.FunctionName = i.FunctionName;
                    ObjFun.FileName = i.FileName;
                    ViewData["Description"] = i.Description;
                    ViewData["DescriptionLen"] = 500 - Convert.ToInt16(i.Description.Length);
                    if (i.FAdd == "Y")
                        ViewData["FAdd"] = true;
                    else
                        ViewData["FAdd"] = false;
                    if (i.FView == "Y")
                        ViewData["FView"] = true;
                    else
                        ViewData["FView"] = false;
                    if (i.FManage == "Y")
                        ViewData["FManage"] = true;
                    else
                        ViewData["FManage"] = false;
                    ObjFun.MailR = i.MailR;
                    ObjFun.FreeBees = i.FreeBees;
                    ObjFun.PortletFile = i.PortletFile;
                }
                return View(ObjFun);

            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditActiveFunction(string id, IFormCollection Coll, string Command)
        {
            if (Command == "Update")
            {
                id = (CommonFunction.Decrypt(id));
                ObjFun.ActionCode = "U";
                ObjFun.FunctionId = id;
                ObjFun.FunctionName = Coll["FunctionName"].ToString();
                ObjFun.FileName = Coll["FileName"].ToString();
                ObjFun.Description = Coll["Description"].ToString();
                if (Coll["CheckBoxAdd"].ToString() == "true,false")
                    ObjFun.FAdd = "Add";
                else
                    ObjFun.FAdd = "N";
                if (Coll["CheckBoxView"].ToString() == "true,false")
                    ObjFun.FView = "View";
                else
                    ObjFun.FView = "N";
                if (Coll["CheckBoxManage"].ToString() == "true,false")
                    ObjFun.FManage = "Manage";
                else
                    ObjFun.FManage = "N";
                ObjFun.MailR = Convert.ToInt16(Coll["MailR"]);
                ObjFun.FreeBees = Convert.ToInt16(Coll["FreeBees"]);
                ObjFun.PortletFile = Coll["PortletFile"].ToString();
                //ObjFun.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("UserId"));// Convert.ToInt16(Session["UserId"]);
                intOut = _functionService.EditFunction(ObjFun);
                //ViewData["result"] = intOut;
                if (intOut == 2)
                {
                    //ViewData["result"] = intOut;
                    TempData["funresult"] = intOut;
                    TempData.Keep("funresult");
                    ModelState.Clear(); //clearing model
                                        //return View();
                    return RedirectToAction("ViewFunctionMasterActive", new { Status = TempData["result"] });
                }
                else
                {
                    TempData["funresult1"] = intOut;
                    TempData.Keep("funresult1");
                    ModelState.Clear(); //clearing model
                    //return View();
                    return RedirectToAction("ViewFunctionMasterActive", new { Status = TempData["result1"] });
                }
                //return RedirectToAction("ViewFunctionMasterActive", new { Status = TempData["result"] });
            }
            else
            {
                return View();
            }
        }



    
    }
    
 }
