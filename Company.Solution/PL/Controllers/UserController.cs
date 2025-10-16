using AutoMapper;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PL.ViewModels;

namespace PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<AppUser> userManager, IMapper mapper)
		{
			_userManager = userManager;
            _mapper = mapper;
        }
      
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var Users = await _userManager.Users
                    .Select(U => new UserViewModel()
                    {
                        Id = U.Id,
                        FName = U.FName,
                        LName = U.LName,
                        Email = U.Email,
                        PhoneNumber = U.PhoneNumber,
                        Roles = _userManager.GetRolesAsync(U).Result
                    })
                    .ToListAsync();

                return View(Users);
            }
            else
            {
                var Users = await _userManager.Users
                    .Where(U => U.FName.Contains(SearchValue) || U.LName.Contains(SearchValue))
                    .Select(U => new UserViewModel()
                    {
                        Id = U.Id,
                        FName = U.FName,
                        LName = U.LName,
                        Email = U.Email,
                        PhoneNumber = U.PhoneNumber,
                        Roles = _userManager.GetRolesAsync(U).Result
                    })
                    .ToListAsync();

                return View(Users);
            }
        }

        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var User = await _userManager.FindByIdAsync(id);
            if(User is null)
                return NotFound();
            var MappedUser = _mapper.Map<AppUser, UserViewModel>(User);
            return View(ViewName, MappedUser);
        }

        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model, [FromRoute] string id)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            user.PhoneNumber = model.PhoneNumber;
            user.FName = model.FName;
            user.LName = model.LName;

            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }


        public Task<IActionResult> Delete(string id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserViewModel model, [FromRoute] string id)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid) 
                return View(model);

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }

    }
}
