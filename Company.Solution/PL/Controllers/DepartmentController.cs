using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
	[Authorize]
	public class DepartmentController : Controller
	{
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
        }

		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index()
		{
			var Departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
			return View(Departments);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		//[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
			if(ModelState.IsValid)
			{
				await _unitOfWork.DepartmentRepository.AddAsync(department);
				int Result = await _unitOfWork.CompleteAsync();
				if(Result > 0)
				{
					TempData["Message"] = "Department is Created";
				}
				return RedirectToAction(nameof(Index));
			}
			return View(department);
		}

		public async Task<IActionResult> Details(int? id, string ViewName = "Details")
		{
			if (id is null)
				return BadRequest();
			var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id.Value);
			if(department is null)
				return NotFound();
			return View(ViewName, department);
		}


		public async Task<IActionResult> Edit(int? id)
		{
			return await Details(id, "Edit");
		}

		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Department department, [FromRoute]int id)
        {
			if(id != department.Id)
				return BadRequest();
			if(ModelState.IsValid)
			{
				try
				{
                    _unitOfWork.DepartmentRepository.Update(department);
					await _unitOfWork.CompleteAsync();
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View(department);
		}

		public async Task<IActionResult> Delete(int id)
		{
			return await Details(id, "Delete");
		}

		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Department department, [FromRoute]int id)
		{
			if (id != department.Id)
				return BadRequest();
			_unitOfWork.DepartmentRepository.Delete(department);
			await _unitOfWork.CompleteAsync();
			return RedirectToAction(nameof(Index));
        }
	}
}
