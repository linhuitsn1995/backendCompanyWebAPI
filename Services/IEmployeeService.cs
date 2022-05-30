using ASPWebAPIFirstCode.Models;
using ASPWebAPIFirstCode.ViewModels;

namespace ASPWebAPIFirstCode.Services
{
    public interface IEmployeeService
    {
        /// get list of all employees
        List<Employees> GetEmployeesList();
        List<Employees> SortEmployeesList(string sortOrder);
        List<Employees> FindEmployees(string searchString);
        /// get employee details by employee id
        Employees GetEmployeeDetailsById(int empId);

        ///  add edit employee
        ResponseModel SaveEmployee(Employees employeeModel);

        ResponseModel CreateEmployee(Employees employee);
        ResponseModel UpdateEmployee(Employees employee);
        /// delete employees
        Employees Delete(int? id);
        ResponseModel DeleteEmployee(int employeeId);
    }
}
