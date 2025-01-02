using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTest.Entities;
using WebApplicationTest.Helpers;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    public class MemberController : Controller
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IMapper _mapper;
        private readonly IHasher _hasher;

        public MemberController(DataBaseContext dataBaseContext, IMapper mapper, IHasher hasher)
        {
            _dataBaseContext = dataBaseContext;
            _mapper = mapper;
            _hasher = hasher;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MemberListPartial()
        {
            List<UserViewModel> users =
                _dataBaseContext.Users.ToList()
                .Select(x => _mapper.Map<UserViewModel>(x)).ToList();

            return PartialView("_MemberlistPartial", users);
        }


        public IActionResult AddNewUserPartial()
        {
            return PartialView("_AddNewUserPartial", new CreateUserViewModel());
        }

        [HttpPost]
        public IActionResult AddNewUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_dataBaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exists.");
                    return PartialView("_AddNewUserPartial", model);
                }

                User user = _mapper.Map<User>(model);
                user.Password = _hasher.DoMD5HashedString(model.Password);

                _dataBaseContext.Users.Add(user);
                _dataBaseContext.SaveChanges();

                return PartialView("_AddNewUserPartial", new CreateUserViewModel { Done = "User added." });
            }

            return PartialView("_AddNewUserPartial", model);
        }

        public IActionResult EditUserPartial(Guid Id)
        {
            User user = _dataBaseContext.Users.Find(Id);
            EditUserViewModel model = _mapper.Map<EditUserViewModel>(user);

            return PartialView("_EditUserPartial", model);
        }

        [HttpPost]
        public IActionResult EditUser(Guid Id, EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_dataBaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower() && x.Id != Id))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exists.");
                    return PartialView("_EditUserPartial", model);
                }

                User user = _dataBaseContext.Users.Find(Id);

                _mapper.Map(model, user);
                _dataBaseContext.SaveChanges();

                return PartialView("_EditUserPartial", new EditUserViewModel { Done = "User added." });
            }

            return PartialView("_EditUserPartial", model);
        }

        public IActionResult DeleteUser(Guid Id)
        {
            User user = _dataBaseContext.Users.Find(Id);

            if (user != null)
            {
                _dataBaseContext.Users.Remove(user);
                _dataBaseContext.SaveChanges();
            }

            return MemberListPartial();
        }
    }
}
