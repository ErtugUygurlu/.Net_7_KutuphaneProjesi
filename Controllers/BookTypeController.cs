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

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BookType bookType)
        {
            if (ModelState.IsValid)
            {
                _bookTypeRepository.Add(bookType);
                _bookTypeRepository.Save();
                TempData["basarili"] = "kitap türü başarıyla oluşturuldu";
                return RedirectToAction("Index", "BookType");
            }
            return View();
        }

        public IActionResult Update(int? id)
        {
            if(id == null || id==0)
            {
                return NotFound();
            }
            BookType? bookTypeVt = _bookTypeRepository.Get(u=>u.Id==id);
            if(bookTypeVt == null)
            {
                return NotFound();
            }
            return View(bookTypeVt);
        }

        [HttpPost]
        public IActionResult Update(BookType bookType)
        {
            if (ModelState.IsValid)
            {
                _bookTypeRepository.Update(bookType);
                _bookTypeRepository.Save();
                TempData["basarili"] = "kitap türü başarıyla güncellendi";
                return RedirectToAction("Index", "BookType");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            BookType? bookTypeVt = _bookTypeRepository.Get(u => u.Id == id);
            if (bookTypeVt == null)
            {
                return NotFound();
            }
            return View(bookTypeVt);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            BookType? bookType = _bookTypeRepository.Get(u => u.Id == id);
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
