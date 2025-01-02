using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTest.Entities;
using WebApplicationTest.Helpers;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    public class StudentController : Controller
    {
        

        private readonly DataBaseContext _dataBaseContext;
        private readonly IMapper _mapper;
        private readonly IHasher _hasher;

        public StudentController(DataBaseContext dataBaseContext, IMapper mapper, IHasher hasher)
        {
            _dataBaseContext = dataBaseContext;
            _mapper = mapper;
            _hasher = hasher;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult StudentListPartial()
        {
            List<StudentViewModel> student =
                _dataBaseContext.StudentInformations.ToList()
                .Select(x => _mapper.Map<StudentViewModel>(x)).ToList();

            return PartialView("_StudentlistPartial", student);
        }


        public IActionResult AddNewStudentPartial()
        {
            return PartialView("_AddNewStudentPartial", new CreateStudentViewModel());
        }

        [HttpPost]
        public IActionResult AddNewStudent(CreateStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_dataBaseContext.StudentInformations.Any(x => x.FullName.ToLower() == model.FullName.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.FullName), "Student is already exists.");
                    return PartialView("_AddNewStudentPartial", model);
                }

                StudentInfo student = _mapper.Map<StudentInfo>(model);
                student.Password = _hasher.DoMD5HashedString(model.Password);

                _dataBaseContext.StudentInformations.Add(student);
                _dataBaseContext.SaveChanges();

                return PartialView("_AddNewStudentPartial", new CreateStudentViewModel { Done = "Student added." });
            }

            return PartialView("_AddNewStudentPartial", model);
        }

        public IActionResult EditStudentPartial(Guid Id)
        {
            StudentInfo student = _dataBaseContext.StudentInformations.Find(Id);
            EditStudentViewModel model = _mapper.Map<EditStudentViewModel>(student);

            return PartialView("_EditStudentPartial", model);
        }

        [HttpPost]
        public IActionResult EditStudent(Guid Id, EditStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_dataBaseContext.StudentInformations.Any(x => x.FullName.ToLower() == model.FullName.ToLower() && x.Id != Id))
                {
                    ModelState.AddModelError(nameof(model.FullName), "This student is already exists.");
                    return PartialView("_EditStudentPartial", model);
                }

                StudentInfo student = _dataBaseContext.StudentInformations.Find(Id);

                _mapper.Map(model, student);
                _dataBaseContext.SaveChanges();

                return PartialView("_EditStudentPartial", new EditStudentViewModel { Done = "Student added." });
            }

            return PartialView("_EditStudentPartial", model);
        }

        public IActionResult DeleteStudent(Guid Id)
        {
            StudentInfo student = _dataBaseContext.StudentInformations.Find(Id);

            if (student != null)
            {
                _dataBaseContext.StudentInformations.Remove(student);
                _dataBaseContext.SaveChanges();
            }

            return StudentListPartial();
        }
    }
}
