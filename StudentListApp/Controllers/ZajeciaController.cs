using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentListApp.Data;
using StudentListApp.Models.Student;
using StudentListApp.Models.Zajecia;
using StudentListApp.Services;

namespace StudentListApp.Controllers
{
    public class ZajeciaController : Controller
    {

        private readonly DataContext _dataContext;

        public ZajeciaController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        // GET: Zajecia
        public async Task<IActionResult> Index()
        {
            var zajecia = await _dataContext.Zajecias.ToListAsync();
            return View(zajecia);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var zajecia = await _dataContext.Zajecias.Include(z => z.Students).FirstOrDefaultAsync(z => z.Id == id);
            return View(zajecia);
        }



        [HttpPost]
        public async Task<IActionResult> Add(ZajeciaAdd zajeciaAdd)
        {
            if (ModelState.IsValid)
            {
                var zajecia = new ZajeciaModel
                {
                    Name = zajeciaAdd.Name,
                    Description = zajeciaAdd.Description,
                    Ects = zajeciaAdd.Ects,
                };
                await _dataContext.Zajecias.AddAsync(zajecia);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Nie udało się dodać";
                return View(zajeciaAdd);
            }

        }
        [HttpGet]
        public async Task<IActionResult> AddStudents(int zajeciaId)
        {
            ViewBag.ZajeciaId = zajeciaId;
            var students = await _dataContext.Student.ToListAsync();
            return View(students);
        }


        [HttpPost]
        public async Task<IActionResult> AddStudents(int zajeciaId, List<int> selectedStudents)
        {
            var zajecia = await _dataContext.Zajecias.Include(z => z.Students).FirstOrDefaultAsync(z => z.Id == zajeciaId);
            Console.WriteLine(zajecia.Students);
            foreach (int id in selectedStudents)
            {
                var student = await _dataContext.Student.FindAsync(id);
                zajecia.Students.Add(student);

            }
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Details", new { id = zajeciaId });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteStudent(int id, int idStudent)
        {
            var zajecia = await _dataContext.Zajecias.Include(z => z.Students).FirstOrDefaultAsync(z => z.Id == id);
            var student = await _dataContext.Student.FindAsync(idStudent);
            zajecia.Students.Remove(student);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Details", new { id });
        }
    }


}

