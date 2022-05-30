using ASPWebAPIFirstCode.Models;
using ASPWebAPIFirstCode.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebAPIFirstCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService service)
        {
            _employeeService = service;
        }

        /// get all employess
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllEmployees()
        {
            try
            {
                var employees = _employeeService.GetEmployeesList();
                if (employees == null) return NotFound();
                return Ok(employees);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// get employess
        [HttpGet]
        [Route("[action]")]
        public IActionResult SortEmployees(string sortOrder)
        {
            try
            {
                var employees = _employeeService.SortEmployeesList(sortOrder);
                if (employees == null) return NotFound();
                return Ok(employees);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// get employess
        [HttpGet]
        [Route("[action]")]
        public IActionResult FindEmployees(string searchString)
        {
            try
            {
                var employees = _employeeService.FindEmployees(searchString);
                if (employees == null) return NotFound();
                return Ok(employees);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// get employee details by id
        [HttpGet]
        [Route("[action]/id")]
        public IActionResult GetEmployeesById(int id)
        {
            try
            {
                var employees = _employeeService.GetEmployeeDetailsById(id);
                if (employees == null) return NotFound();
                return Ok(employees);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// save employee
        [HttpPost]
        [Route("[action]")]
        public IActionResult SaveEmployees(Employees employee)
        {
            try
            {
                var model = _employeeService.SaveEmployee(employee);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// create employee
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateEmployees(Employees employee)
        {
            try
            {
                var model = _employeeService.CreateEmployee(employee);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// edit employee
        [HttpPost]
        [Route("[action]")]
        public IActionResult UpdateEmployees(Employees employee)
        {
            try
            {
                var model = _employeeService.UpdateEmployee(employee);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// delete employee
        [HttpGet]
        [Route("[action]/id")]
        public IActionResult Delete(int? id)
        {
            try
            {
                var employee = _employeeService.Delete(id);
                return Ok(employee);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("[action]/id")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                var model = _employeeService.DeleteEmployee(id);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
