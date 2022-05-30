using ASPWebAPIFirstCode.Models;
using ASPWebAPIFirstCode.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ASPWebAPIFirstCode.Services
{
    public class EmployeeService : IEmployeeService
    {
        ResponseModel model = new ResponseModel();
        private EmpContext _context;

        public EmployeeService(EmpContext context)
        {
            _context = context;
        }
        public Employees Delete(int? id)
        {
            if (id == null)
            {
                model.IsSuccess = false;
                model.Message = "ID Employee Not Found";
            }

            var employee = _context.Employees
                .AsNoTracking()
                .FirstOrDefault(m => m.EmployeeId == id);
            if (employee == null)
            {
                model.IsSuccess = false;
                model.Message = "Employee Not Found";
            }

            return employee;
        }
        public ResponseModel DeleteEmployee(int employeeId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Employees _temp = GetEmployeeDetailsById(employeeId);
                if (_temp != null)
                {
                    _context.Remove<Employees>(_temp);
                    _context.SaveChanges();
                    model.IsSuccess = true;
                    model.Message = "Employee Deleted Successfully";
                }
                else
                {
                    model.IsSuccess = false;
                    model.Message = "Employee Not Found";
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }

        public Employees GetEmployeeDetailsById(int empId)
        {
            Employees emp;
            try
            {
                emp = _context.Find<Employees>(empId);
            }
            catch (Exception)
            {
                throw;
            }
            return emp;
        }

        public List<Employees> SortEmployeesList(string sortOrder)
        {
            List<Employees> empList;
            try
            {
                var employees = from s in _context.Employees
                                select s;
                if (sortOrder == "name_desc")
                {
                    employees = employees.OrderByDescending(s => s.EmployeeFirstName);
                }
                empList = employees.AsNoTracking().ToList();
            }
            catch (Exception e)
            {
                throw;
            }
            return empList;
        }

        public List<Employees> FindEmployees(string searchString)
        {
            List<Employees> empList;
            try
            {
                var employees = from s in _context.Employees
                                select s;
                employees = employees.Where(s => s.EmployeeLastName.Contains(searchString)
                                       || s.EmployeeFirstName.Contains(searchString));
                empList = employees.AsNoTracking().ToList();
            }
            catch (Exception e)
            {
                throw;
            }
            return empList;
        }

        public List<Employees> GetEmployeesList()
        {
            List<Employees> empList;
            try
            {
                empList = _context.Set<Employees>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return empList;
        }

        public ResponseModel CreateEmployee(Employees employee)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                if (!EmployeeExists(employee.EmployeeId))
                {
                    _context.Add(employee);
                    _context.SaveChanges();
                    model.IsSuccess = true;
                    model.Message = "Employee Inserted Successfully";
                }
            }
            catch (DbUpdateException ex)
            {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }

        public ResponseModel UpdateEmployee(Employees employee)
        {
            ResponseModel model = new ResponseModel();

            if (employee == null)
            {
                model.IsSuccess = false;
                model.Message = "Employee does not exist";
                return model;
            }
            try
            {
                _context.Update(employee);
                _context.SaveChanges();
                model.IsSuccess = true;
                model.Message = "Employee Updated Successfully";
            }
            catch (DbUpdateException ex)
            {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }

        public ResponseModel SaveEmployee(Employees employeeModel)
        {

            ResponseModel model = new ResponseModel();

            try
            {
                if (EmployeeExists(employeeModel.EmployeeId))
                {
                    _context.Update(employeeModel);
                    model.Message = "Employee Update Successfully";
                    Console.WriteLine("Employee Update Successfully");
                }
                else
                {
                    _context.Add<Employees>(employeeModel);
                    model.Message = "Employee Inserted Successfully";
                    Console.WriteLine("Employee Inserted Successfully");
                }

                _context.SaveChanges();
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
                Console.WriteLine("Error : " + ex.Message);
            }
            return model;
        }
        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
        private static void Dump(object o)
        {
            string json = JsonConvert.SerializeObject(o, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}
