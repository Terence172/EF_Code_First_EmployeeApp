using Assignment4_EmployeeData.DAL;
using Assignment4_EmployeeData.Models;
using Assignment4_EmployeeData.Models.DBEntities;
using Microsoft.AspNetCore.Mvc;

namespace Assignment4_EmployeeData.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _context;

        // GET
        public EmployeeController(EmployeeDbContext context)
        {
            this._context = context;
        }

        
        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            List<EmployeeViewModel> employeeList = new List<EmployeeViewModel>();

            if (employees != null)
            {
                
                foreach (var employee in employees)
                {
                    var EmployeeViewModel = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DateOfBirth = employee.DateOfBirth,
                        Email = employee.Email,
                        Salary = employee.Salary
                    };
                    employeeList.Add(EmployeeViewModel);
                }
                return View(employeeList);
            }
            return View(employeeList);
        }   
        

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = new Employee()
                    {
                        FirstName = employeeViewModel.FirstName,
                        LastName = employeeViewModel.LastName,
                        DateOfBirth = employeeViewModel.DateOfBirth,
                        Email = employeeViewModel.Email,
                        Salary = employeeViewModel.Salary
                    };
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Employee Created";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Model Data is not Valid";
                    return View();
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
            
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var employee = _context.Employees.SingleOrDefault(x => x.Id == id);
                if (employee != null)
                {
                    var employeeViewModel = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DateOfBirth = employee.DateOfBirth,
                        Email = employee.Email,
                        Salary = employee.Salary
                    };
                    return View(employeeViewModel);
                }

                else
                {
                    TempData["errorMessage"] = $"Employee not found with ID : {id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel employeeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = new Employee()
                    {
                        
                        Id= employeeViewModel.Id,
                        FirstName = employeeViewModel.FirstName,
                        LastName = employeeViewModel.LastName,
                        DateOfBirth = employeeViewModel.DateOfBirth,
                        Email = employeeViewModel.Email,
                        Salary = employeeViewModel.Salary

                    };
                    
                    _context.Employees.Update(employee);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Employee Updated";
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["errorMessage"] = "Model Data is not Valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var employee = _context.Employees.SingleOrDefault(x => x.Id == id);
                if (employee != null)
                {
                    var employeeViewModel = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DateOfBirth = employee.DateOfBirth,
                        Email = employee.Email,
                        Salary = employee.Salary
                    };
                    return View(employeeViewModel);
                }

                else
                {
                    TempData["errorMessage"] = $"Employee not found with ID : {id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeViewModel)
        {
            try
            {
                var employee = _context.Employees.SingleOrDefault(x => x.Id == employeeViewModel.Id);

                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Employee Deleted Successfully";
                    return RedirectToAction("Index");
                }

                else
                {
                    TempData["errorMessage"] = $"Employee not found with ID : {employeeViewModel.Id}";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

    }
}
