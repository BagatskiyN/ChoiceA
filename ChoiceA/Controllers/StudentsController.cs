using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChoiceA.Data;
using ChoiceA.Models;
using static ChoiceA.Data.ChoiceContext;
using Microsoft.AspNetCore.Authorization;

namespace ChoiceA.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly ChoiceContext _context;
        public List<CheckedModel> Discipline { get; set; } = new List<CheckedModel>();
        public List<Discipline> SelectedDiscipline { get; set; }
        public StudentsController(ChoiceContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Group")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
        public IActionResult Select(string id)
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
        [HttpPost]
        public IActionResult Select(SelectViewModel selectViewModel)
        {
            Student student = _context.Students.Include(s => s.DisciplineStudents).FirstOrDefault(s => s.Id == selectViewModel.Student.Id);

            for (int i = 0; i < selectViewModel.SelectedDiscipline.Count; i++)
            {
                Discipline discipline = _context.Disciplines
                            .FirstOrDefault(c => c.Id == selectViewModel.SelectedDiscipline[i].Discipline.Id);

                var studDis = student.DisciplineStudents.FirstOrDefault(sd => sd.DisciplineId == discipline.Id);

                DisciplineStudent disciplineStudent = new DisciplineStudent() { DisciplineId = discipline.Id, StudentId = student.Id };
                if (selectViewModel.SelectedDiscipline[i].IsChecked == true)
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

            return Redirect("Index");

        }

    
        public void NewDisceplines(List<Discipline> dis, bool IsChecked)
        {

            foreach (var item in dis)
            {
                CheckedModel checkedModel = new CheckedModel(item, IsChecked);
                Discipline.Add(checkedModel);

            }
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Group")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
