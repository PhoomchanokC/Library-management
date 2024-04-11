using Library_management.Data;
using Library_management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Library_management.Controllers
{
    
    public class BookController : Controller
    {
        private readonly ApplicationDBContext _db;

        public BookController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index() { 

          
            IEnumerable<Book> all_books = _db.books;
            if (all_books.IsNullOrEmpty())
            {
                return View();
            }
            return View(all_books);
        }
        public IActionResult Create() { 
            return View(); 
        }
        [HttpPost]
        public IActionResult Create(Book obj)
        {
            _db.books.Add(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Detail(string id)
        {
            Book book = _db.books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }



    }
}
