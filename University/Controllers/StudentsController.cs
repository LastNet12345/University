using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using University.Core;
using University.Data.Data;
using University.Filters;
using University.Models;

namespace University.Controllers
{
    public class StudentsController : Controller
    {
        private readonly UniversityContext _context;
        private readonly IMapper mapper;
        private readonly Faker faker = new Faker("sv");

        public StudentsController(UniversityContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            //var viewModel = await _context.Student.Select(s => new StudentIndexViewModel
            //{
            //    Id = s.Id,
            //    Avatar = s.Avatar,
            //    FirstName = s.FirstName,
            //    LastName = s.LastName,
            //    AddressStreet = s.Address.Street
            //}).ToListAsync();

            var viewModel = await mapper.ProjectTo<StudentIndexViewModel>(_context.Student)
                .OrderByDescending(s => s.Id)
                .ToListAsync();

            return View(viewModel);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await mapper.ProjectTo<StudentDetailsViewModel>(_context.Student)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelValidationFilter]
        public async Task<IActionResult> Create(StudentCreateViewModel viewModel)
        {
            //if (ModelState.IsValid)
            //{
                //var student = new Student
                //{
                //    FirstName= viewModel.FirstName,
                //    LastName= viewModel.LastName,
                //    Email= viewModel.Email,
                //    Avatar = faker.Internet.Avatar(),
                //    Address = new Address
                //    {
                //        Street = viewModel.AddressStreet,
                //        City = viewModel.AddressCity,
                //        ZipCode = viewModel.AddressZipCode
                //    }
                //};

                var student = mapper.Map<Student>(viewModel);
                student.Avatar = faker.Internet.Avatar();

                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(viewModel);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await mapper.ProjectTo<StudentEditViewModel>(_context.Student)
                                      .FirstOrDefaultAsync(s => s.Id == id);


            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var student = await _context.Student.Include(s => s.Address)
                        .FirstOrDefaultAsync(s => s.Id == id);

                    mapper.Map(viewModel, student);

                    //_context.Entry(student).Property("Edited").CurrentValue = DateTime.Now;

                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(viewModel.Id))
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
            return View(viewModel);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set 'UniversityContext.Student'  is null.");
            }
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Student?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> CheckIfEmailIsUnique(string email) //<< Name must match name of property 
        {
            if (await _context.Student.AnyAsync(s => s.Email == email))
            {
                return Json("There is already a student with that email!");
            }
            return Json(true);
        } 

    }
}
