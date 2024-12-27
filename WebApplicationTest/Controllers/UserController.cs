using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTest.Entities;
using WebApplicationTest.Helpers;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    public class UserController : Controller
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IMapper _mapper;
        private readonly IHasher _hasher;

        public UserController(DataBaseContext dataBaseContext, IMapper mapper, IHasher hasher)
        {
            _dataBaseContext = dataBaseContext;
            _mapper = mapper;
            _hasher = hasher;
        }

        public IActionResult Index()
        {
            List<UserViewModels> users =
                _dataBaseContext.Users.ToList()
                .Select(x => _mapper.Map<UserViewModels>(x)).ToList();

            return View(users);
        }

        public IActionResult Create()
        {


            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                if (_dataBaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exists.");
                    return View(model);
                }

                User user = _mapper.Map<User>(model);
                user.Password = _hasher.DoMD5HashedString(user.Password);

                _dataBaseContext.Users.Add(user);
                _dataBaseContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Edit(Guid Id)
        {
            User user = _dataBaseContext.Users.Find(Id);
            EditUserModel model = _mapper.Map<EditUserModel>(user);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Guid Id, EditUserModel model)
        {
            if (ModelState.IsValid)
            {
                if (_dataBaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower() && x.Id != Id))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exists.");
                    return View(model);
                }

                User user = _dataBaseContext.Users.Find(Id);

                _mapper.Map(model, user);
                _dataBaseContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Delete(Guid Id)
        {

            User user = _dataBaseContext.Users.Find(Id);

            if (user != null)
            {
                _dataBaseContext.Users.Remove(user);
                _dataBaseContext.SaveChanges();
            }

            

            return RedirectToAction(nameof(Index));

        }

    }
}
