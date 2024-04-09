using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using ClaimBaseTest.Model.Company;
using ClaimBaseTest.Repository.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ClaimBaseTest.Web
{
    //[Authorize]
    public class CompanyMasterController : Controller
    {
        public IConfiguration Configuration;
        private readonly ICompanyMasterRepository _CompanyMasterRepository;
        private IWebHostEnvironment _hostingEnvironment;
        public CompanyMasterController(IConfiguration configuration, ICompanyMasterRepository CompanyMasterRepository, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _CompanyMasterRepository = CompanyMasterRepository;

            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [Authorize(Policy = "CompanyMaster/Company")]
        public IActionResult Company()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Company(Company TBL)
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
                if (TBL.CompanyID == 0 || TBL.CompanyID == null)
                {
                    var data = _CompanyMasterRepository.Insert_Company(TBL);
                    return Json(new { sucess = true, responseMessage = "Inserted Successfully.", responseText = "Success", data = data });
                }
                else
                {
                    var data = _CompanyMasterRepository.Insert_Company(TBL);
                    return Json(new { sucess = true, responseMessage = "Updated Successfully.", responseText = "Success", data = data });
                }

            }
        }

        [HttpGet]
        [Authorize(Policy = "CompanyMaster/ViewCompany")]
        public IActionResult ViewCompany()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> Get_Company()
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
                List<Company> lst = await _CompanyMasterRepository.Getall_Company(new Company());
                var jsonres = JsonConvert.SerializeObject(lst);

                return Json(jsonres);

            }

        }
        [HttpPost]
        public async Task<IActionResult> Search_Company([FromBody] Company BL)
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
                List<Company> lst = await _CompanyMasterRepository.Search_Company(BL);
                var jsonres = JsonConvert.SerializeObject(lst);

                return Json(jsonres);

            }

        }

        [HttpDelete]
        public async Task<JsonResult> Delete_Company(int Id)
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
                var data = _CompanyMasterRepository.Delete_Company(Id);
                return Json(new { sucess = true, responseMessage = "Action taken Successfully.", responseText = "Success", data = data });
            }
        }

        [HttpGet]

        public async Task<JsonResult> GetByID_Company(int Id)
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
                Company lst = await _CompanyMasterRepository.GetCompanyById(Id);
                var jsonres = JsonConvert.SerializeObject(lst);
                return Json(jsonres);
            }
        }

    }
}
