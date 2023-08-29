using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Data;
using VektorelProje.Models;
using VektorelProje.Utility;

namespace VektorelProje.Controllers
{
    [Authorize(Roles = UserRole.Role_Ertu)]
    public class HireController : Controller
    {
        private readonly IHireRepository _hireRepository;
        private readonly IBookRepository _bookRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public HireController(IHireRepository hireRepository, IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment)
        {
            _hireRepository = hireRepository;
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Hire> objHireList = _hireRepository.GetAll(includeProps: "Book").ToList();
            return View(objHireList);
        }

        public IActionResult AddUpdate(int? id)
        {
            IEnumerable<SelectListItem> BookList = _bookRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.BookName,
                Value = k.Id.ToString()
            });
            ViewBag.BookList = BookList;
            if (id == null || id == 0)
            {
                return View();
            }
            else
            {
                Hire? hireVt = _hireRepository.Get(u => u.Id == id);
                if (hireVt == null)
                {
                    return NotFound();
                }
                return View(hireVt);
            }
        }

        [HttpPost]
        public IActionResult AddUpdate(Hire hire)
        {
            var errors=ModelState.Values.SelectMany(x => x.Errors);
            if (ModelState.IsValid)
            {
                

                if(hire.Id == 0)
                {
                    _hireRepository.Add(hire);
                    TempData["basarili"] = "Kiralama işlemi başarıyla oluşturuldu";

                }
                else
                {
                    _hireRepository.Update(hire);
                    TempData["basarili"] = "Kiralama güncelleme işlemi başarılı";

                }

                _hireRepository.Save();
                return RedirectToAction("Index", "Hire");
            }
            return View();
        }


        public IActionResult Delete(int? id)
        {
            IEnumerable<SelectListItem> BookList = _bookRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.BookName,
                Value = k.Id.ToString()
            });
            ViewBag.BookList = BookList;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Hire? hireVt = _hireRepository.Get(u => u.Id == id);
            if (hireVt == null)
            {
                return NotFound();
            }
            return View(hireVt);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Hire? hire = _hireRepository.Get(u => u.Id == id);
            if (hire == null)
            {
                return NotFound();
            }
            _hireRepository.Delete(hire);
            _hireRepository.Save();
            TempData["basarili"] = "Kiralama kaydı başarıyla silindi";
            return RedirectToAction("Index", "Hire");
        }


    }
}
