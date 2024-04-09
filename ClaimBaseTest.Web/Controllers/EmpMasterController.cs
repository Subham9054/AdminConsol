using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using ClaimBaseTest.Model.Employee;
using ClaimBaseTest.Repository.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ClaimBaseTest.Web
{
    //[Authorize]
    public class EmpMasterController : Controller
    {
        public IConfiguration Configuration;
        private readonly IEmpMasterRepository _EmpMasterRepository;
        private IWebHostEnvironment _hostingEnvironment;
        public EmpMasterController(IConfiguration configuration, IEmpMasterRepository EmpMasterRepository, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _EmpMasterRepository = EmpMasterRepository;

            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [Authorize(Policy = "EmpMaster/Employee")]
        public IActionResult Employee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Employee(Employee TBL)
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
                if (TBL.EmployeeID == 0 || TBL.EmployeeID == null)
                {
                    var data = _EmpMasterRepository.Insert_Employee(TBL);
                    return Json(new { sucess = true, responseMessage = "Inserted Successfully.", responseText = "Success", data = data });
                }
                else
                {
                    var data = _EmpMasterRepository.Insert_Employee(TBL);
                    return Json(new { sucess = true, responseMessage = "Updated Successfully.", responseText = "Success", data = data });
                }

            }
        }

        [HttpGet]
        [Authorize(Policy = "EmpMaster/ViewEmployee")]
        public IActionResult ViewEmployee()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> Get_Employee()
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
                List<Employee> lst = await _EmpMasterRepository.Getall_Employee(new Employee());
                var jsonres = JsonConvert.SerializeObject(lst);

                return Json(jsonres);

            }

        }
        [HttpPost]
        public async Task<IActionResult> Search_Employee([FromBody] Employee BL)
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
                List<Employee> lst = await _EmpMasterRepository.Search_Employee(BL);
                var jsonres = JsonConvert.SerializeObject(lst);

                return Json(jsonres);

            }

        }

        [HttpDelete]

        public async Task<JsonResult> Delete_Employee(int Id)
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
                var data = _EmpMasterRepository.Delete_Employee(Id);
                return Json(new { sucess = true, responseMessage = "Action taken Successfully.", responseText = "Success", data = data });
            }
        }

        [HttpGet]

        public async Task<JsonResult> GetByID_Employee(int Id)
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
                Employee lst = await _EmpMasterRepository.GetEmployeeById(Id);
                var jsonres = JsonConvert.SerializeObject(lst);
                return Json(jsonres);
            }
        }

    }
}
