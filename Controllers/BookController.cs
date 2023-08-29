using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VektorelProje.Models;
using VektorelProje.Utility;

namespace VektorelProje.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookTypeRepository _bookTypeRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IBookRepository context, IBookTypeRepository bookTypeRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = context;
            _bookTypeRepository = bookTypeRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Ertu,Customer")]
        public IActionResult Index()
        {
            List<Book> objBookList = _bookRepository.GetAll(includeProps: "bookType").ToList();
            return View(objBookList);
        }

        [Authorize(Roles = UserRole.Role_Ertu)]
        public IActionResult AddUpdate(int? id)
        {
            IEnumerable<SelectListItem> BookTypeList = _bookTypeRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.Name,
                Value = k.Id.ToString()
            });
            ViewBag.BookTypeList = BookTypeList;
            if (id == null || id == 0)
            {
                return View();
            }
            else
            {
                Book? bookVt = _bookRepository.Get(u => u.Id == id);
                if (bookVt == null)
                {
                    return NotFound();
                }
                return View(bookVt);
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRole.Role_Ertu)]
        public IActionResult AddUpdate(Book book, IFormFile? file)
        {
            var errors=ModelState.Values.SelectMany(x => x.Errors);
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string bookPath = Path.Combine(wwwRootPath, @"img");

                if(file!=null)
                {
                    using (var fileStream = new FileStream(Path.Combine(bookPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    book.PictureUrl = @"\img\" + file.FileName;
                }

                if(book.Id == 0)
                {
                    _bookRepository.Add(book);
                    TempData["basarili"] = "kitap başarıyla oluşturuldu";

                }
                else
                {
                    _bookRepository.Update(book);
                    TempData["basarili"] = "kitap güncelleme başarılı";

                }

                _bookRepository.Save();
                return RedirectToAction("Index", "Book");
            }
            return View();
        }


        [Authorize(Roles = UserRole.Role_Ertu)]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Book? bookVt = _bookRepository.Get(u => u.Id == id);
            if (bookVt == null)
            {
                return NotFound();
            }
            return View(bookVt);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = UserRole.Role_Ertu)]
        public IActionResult DeletePost(int? id)
        {
            Book? book = _bookRepository.Get(u => u.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            _bookRepository.Delete(book);
            _bookRepository.Save();
            TempData["basarili"] = "kitap başarıyla silindi";
            return RedirectToAction("Index", "Book");
        }


    }
}
