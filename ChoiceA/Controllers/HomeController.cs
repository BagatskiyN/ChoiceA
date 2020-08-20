using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChoiceA.Models;
using ChoiceA.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using static ChoiceA.Data.ChoiceContext;
using Newtonsoft.Json;

namespace ChoiceA.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public List<CheckedModel> Discipline { get; set; } = new List<CheckedModel>();
        public List<Discipline> SelectedDiscipline { get; set; }
        private readonly ILogger<HomeController> _logger;
        private readonly ChoiceContext _context;
        public HomeController(ILogger<HomeController> logger, ChoiceContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
           
                var claim = User.Claims.FirstOrDefault(c => c.Type == "studentId");
                if (claim == null)
                    return View();
                return RedirectToAction("Select", new { id = Convert.ToInt32(claim.Value) });
        
        }
        public IActionResult Select(int? id)
        {
            Discipline.Clear();
            if (id == null)
            {
                return NotFound();
            }
            var student = _context.Students
                    .FirstOrDefault(m => m.Id == id);
            var students = _context.Students.Include(c => c.DisciplineStudents).ThenInclude(sc => sc.Discipline).ToList();
            SelectedDiscipline = students.FirstOrDefault(s => s.Id == student.Id).DisciplineStudents.Select(c => c.Discipline).ToList();

            var disciplines = _context.Disciplines.ToList().Except(SelectedDiscipline).ToList();
            NewDisceplines(SelectedDiscipline, true);
            NewDisceplines(disciplines, false);
            SelectViewModel selectViewModel = new SelectViewModel(student, Discipline);
            return View(selectViewModel);

        }
        public void NewDisceplines(List<Discipline> dis, bool IsChecked)
        {

            foreach (var item in dis)
            {
                CheckedModel checkedModel = new CheckedModel(item, IsChecked);
                Discipline.Add(checkedModel);

            }
        }
        [HttpPost]
        public IActionResult SelectAjax([FromBody] List<Testt> a)
        {

    
            if (ModelState.IsValid)
            {
                Student student = _context.Students.Include(s => s.DisciplineStudents).FirstOrDefault(s => s.Name ==User.Identity.Name);

                for (int i = 0; i <a.Count; i++)
                {
                    Discipline discipline = _context.Disciplines
                                .FirstOrDefault(c => c.Title == a[i].Title);

                    var studDis = student.DisciplineStudents.FirstOrDefault(sd => sd.DisciplineId == discipline.Id);

                    DisciplineStudent disciplineStudent = new DisciplineStudent() { DisciplineId = discipline.Id, StudentId = student.Id };
                    if (a[i].IsChecked == true)
                    {

                        if (!student.DisciplineStudents.Contains(studDis))
                        {
                            student.DisciplineStudents.Add(disciplineStudent);
                        }

                    }
                    else
                    {
                        if (student.DisciplineStudents.Contains(studDis))
                        {
                            student.DisciplineStudents.Remove(studDis);

                        }
                    }
                    _context.SaveChanges();
                }
                Discipline.Clear();


                var students = _context.Students.Include(c => c.DisciplineStudents).ThenInclude(sc => sc.Discipline).ToList();
                SelectedDiscipline = students.FirstOrDefault(s => s.Id == student.Id).DisciplineStudents.Select(c => c.Discipline).ToList();

                var disciplines = _context.Disciplines.ToList().Except(SelectedDiscipline).ToList();
                NewDisceplines(SelectedDiscipline, true);
                NewDisceplines(disciplines, false);
                return PartialView("~/Views/Home/SelectList.cshtml", Discipline);
            }
            return PartialView("~/Views/Home/SelectList.cshtml", Discipline);
        }
            
        

        //[HttpPost]
        //public IActionResult Select(SelectViewModel selectViewModel)
        //{
        //    Student student = _context.Students.Include(s => s.DisciplineStudents).FirstOrDefault(s => s.Id == selectViewModel.Student.Id);

        //    for (int i = 0; i < selectViewModel.SelectedDiscipline.Count; i++)
        //    {
        //        Discipline discipline = _context.Disciplines
        //                    .FirstOrDefault(c => c.Id == selectViewModel.SelectedDiscipline[i].Discipline.Id);

        //        var studDis = student.DisciplineStudents.FirstOrDefault(sd => sd.DisciplineId == discipline.Id);

        //        DisciplineStudent disciplineStudent = new DisciplineStudent() { DisciplineId = discipline.Id, StudentId = student.Id };
        //        if (selectViewModel.SelectedDiscipline[i].IsChecked == true)
        //        {

        //            if (!student.DisciplineStudents.Contains(studDis))
        //            {
        //                student.DisciplineStudents.Add(disciplineStudent);
        //            }

        //        }
        //        else
        //        {
        //            if (student.DisciplineStudents.Contains(studDis))
        //            {
        //                student.DisciplineStudents.Remove(studDis);

        //            }
        //        }
        //        _context.SaveChanges();
        //    }

        //    return Redirect("Index");

        //}
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
