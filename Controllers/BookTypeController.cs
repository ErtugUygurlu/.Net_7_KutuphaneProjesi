using Microsoft.AspNetCore.Mvc;
using VektorelProje.Models;

namespace VektorelProje.Controllers
{
    public class BookTypeController : Controller
    {
        private readonly IBookTypeRepository _bookTypeRepository;

        public BookTypeController(IBookTypeRepository context)
        {
            _bookTypeRepository = context;
        }

        public IActionResult Index()
        {
            List<BookType> objBookTypeList = _bookTypeRepository.GetAll().ToList();
            return View(objBookTypeList);
        }

        public IActionResult AddOrUpdate(int? id)
        {
            if (id == null)
            {
                return View(new BookType());
            }

            BookType bookTypeVt = _bookTypeRepository.Get(u => u.Id == id);
            if (bookTypeVt == null)
            {
                return NotFound();
            }
            return View(bookTypeVt);
        }

        [HttpPost]
        public IActionResult AddOrUpdate(BookType bookType)
        {
            if (ModelState.IsValid)
            {
                if (bookType.Id == 0)
                {
                    _bookTypeRepository.Add(bookType);
                    TempData["basarili"] = "kitap türü başarıyla oluşturuldu";
                }
                else
                {
                    _bookTypeRepository.Update(bookType);
                    TempData["basarili"] = "kitap türü başarıyla güncellendi";
                }

                _bookTypeRepository.Save();
                return RedirectToAction("Index", "BookType");
            }
            return View(bookType);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            BookType bookTypeVt = _bookTypeRepository.Get(u => u.Id == id);
            if (bookTypeVt == null)
            {
                return NotFound();
            }
            return View(bookTypeVt);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            BookType bookType = _bookTypeRepository.Get(u => u.Id == id);
            if (bookType == null)
            {
                return NotFound();
            }
            _bookTypeRepository.Delete(bookType);
            _bookTypeRepository.Save();
            TempData["basarili"] = "kitap türü başarıyla silindi";
            return RedirectToAction("Index", "BookType");
        }
    }
}
