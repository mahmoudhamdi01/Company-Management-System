using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Helpers;
using PL.ViewModels;

namespace PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> Employees;
            if(string.IsNullOrEmpty(SearchValue))
            {
                 Employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                 Employees = _unitOfWork.EmployeeRepository.GetEmpByName(SearchValue);
            }
                var MappedEmployee = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
                return View(MappedEmployee);
            
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if(ModelState.IsValid)
            {
                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                await _unitOfWork.EmployeeRepository.AddAsync(MappedEmployee);
                var Result = await _unitOfWork.CompleteAsync();
                if (Result > 0)
                {
                    TempData["Message"] = "Employee is Created";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            if(employee is null)    
                return NotFound();
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(ViewName, MappedEmployee);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeVM, [FromRoute]int Id)
        {
            if(Id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                if (employeeVM.Image is not null)
                {
                    employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");
                }
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitOfWork.EmployeeRepository.Update(MappedEmployee);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM)
        {
            if(ModelState.IsValid)
            {
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitOfWork.EmployeeRepository.Delete(MappedEmployee);
                int Result = await _unitOfWork.CompleteAsync();
                if (Result > 0 && employeeVM.ImageName is not null)
                {
                    DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }
    }

}
