using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using ClaimBaseTest.Model.City;
using ClaimBaseTest.Repository.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
//---------------------------------------
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace ClaimBaseTest.Web
{
  
    public class CityMasterController : Controller
    {
        public IConfiguration Configuration;
        private readonly ICityMasterRepository _CityMasterRepository;
        private IWebHostEnvironment _hostingEnvironment;
        public CityMasterController(IConfiguration configuration, ICityMasterRepository CityMasterRepository, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _CityMasterRepository = CityMasterRepository;

            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [Authorize(Policy = "CityMaster/City")]
        public IActionResult City()
        {
            return View();
        }   //City Add Method

        [HttpPost]
        public IActionResult City(City TBL)
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
                if (TBL.CityId == 0 || TBL.CityId == null)
                {
                    var data = _CityMasterRepository.Insert_City(TBL);
                    return Json(new { sucess = true, responseMessage = "Inserted Successfully.", responseText = "Success", data = data });
                }
                else
                {
                    var data = _CityMasterRepository.Insert_City(TBL);
                    return Json(new { sucess = true, responseMessage = "Updated Successfully.", responseText = "Success", data = data });
                }

            }
        }

        [HttpGet]
        [Authorize(Policy = "CityMaster/ViewCity")]
        public IActionResult ViewCity()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> Get_City()
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
                List<City> lst = await _CityMasterRepository.Getall_City(new City());
                var jsonres = JsonConvert.SerializeObject(lst);

                return Json(jsonres);

            }

        }

        [HttpPost]
        public async Task<IActionResult> Search_City([FromBody] City BL)
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
                List<City> lst = await _CityMasterRepository.Search_City(BL);
                var jsonres = JsonConvert.SerializeObject(lst);

                return Json(jsonres);

            }
        }

        [HttpDelete]
        public async Task<JsonResult> Delete_City(int Id)
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
                var data = _CityMasterRepository.Delete_City(Id);
                return Json(new { sucess = true, responseMessage = "Action taken Successfully.", responseText = "Success", data = data });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetByID_City(int Id)
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
                City lst = await _CityMasterRepository.GetCityById(Id);
                var jsonres = JsonConvert.SerializeObject(lst);
                return Json(jsonres);
            }
        }

    }
}
