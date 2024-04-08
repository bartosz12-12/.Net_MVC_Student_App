using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentListApp.Data;
using StudentListApp.Models.Student;
using StudentListApp.Services;
using System;

namespace StudentListApp.Controllers
{
    public class StudentListController : Controller
    {
        private static List<StudentModel> studnetList = new List<StudentModel>{
            new StudentModel{Id = 1, Name = "Dawid", LastName="Grygorowicz",NrIndex = 1234},
            new StudentModel{Id = 2, Name = "Mateusz", LastName="Kurski",NrIndex = 1223},
            new StudentModel{Id = 3, Name = "Alicja", LastName="Lipińska",NrIndex = 4445},
            new StudentModel{Id = 4, Name = "Emilia", LastName="Lipińska",NrIndex = 7678}
        };

        private readonly DataContext _dataContext;
        private readonly IStudent _studentService;
        public StudentListController(DataContext dataContext, IStudent studentService)
        {
            _dataContext = dataContext;
            _studentService = studentService;
        }


        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAll();

            return View(students);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Oceny()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudent addStudent)
        {
            if (ModelState.IsValid)
            {
                var student = new StudentModel
                {
                    Name = addStudent.Name,
                    LastName = addStudent.LastName,
                    NrIndex = addStudent.NrIndex,
                };

                await _studentService.AddStudent(student);
                return RedirectToAction("Index", "StudentList");
            }
            else
            {
                TempData["ErrorMessage"] = "Nie udało się dodać studenta. Proszę poprawić wprowadzone dane.";

                return View(addStudent);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _dataContext.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {
                var student = await _dataContext.Student.FindAsync(studentModel.Id);
                if (student is not null)
                {
                    await _studentService.EditStudent(student, studentModel);
                    return RedirectToAction("Index", "StudentList");
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                Console.WriteLine("Bład");
                return View(studentModel);
            }


        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _dataContext.Student.FindAsync(id);
            if (student is not null)
            {
                await _studentService.DeleteStudent(student);
            }

            return RedirectToAction("Index", "StudentList");
        }
    }
}
