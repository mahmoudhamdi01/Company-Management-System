using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PL.ViewModels;

namespace PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var Roles = await _roleManager.Roles.ToListAsync();
                var MappedRole = _mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(Roles);
                return View(MappedRole);
            }
            else
            {
                var Users = await _roleManager.Roles
                    .Select(U => new RoleViewModel()
                    {
                        Id = U.Id,
                        RoleName = U.Name,
                    }).ToListAsync();
                return View(Users);
            }
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                var MappedRole = _mapper.Map<RoleViewModel, IdentityRole>(model);
                await _roleManager.CreateAsync(MappedRole);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var Role = await _roleManager.FindByIdAsync(id);
            if (Role is null)
                return NotFound();
            var MappedRole = _mapper.Map<IdentityRole, RoleViewModel>(Role);
            return View(ViewName, MappedRole);
        }

        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model, [FromRoute] string id)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            role.Name = model.RoleName;

            await _roleManager.UpdateAsync(role);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RoleViewModel model, [FromRoute] string id)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            role.Name = model.RoleName;

            await _roleManager.DeleteAsync(role);
            return RedirectToAction(nameof(Index));
        }
    }
}
