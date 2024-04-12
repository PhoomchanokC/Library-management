using Library_management.Data;
using Library_management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.FileProviders;
using System;
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
            Random rnd = new Random();
            int num = rnd.Next();
            byte[] bytes = null;
            string str = obj.Title + Convert.ToString(num);
            byte[] encoded = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(str));
            var value = BitConverter.ToUInt32(encoded, 0) % 1000000;
            obj.Id = Convert.ToString(value);
            obj.IS_borrow = false;
            obj.Borrow_count = 0;
          

            using (Stream fs = obj.ImageFile.OpenReadStream())
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    bytes = br.ReadBytes((Int32)fs.Length);
                }
            }
            obj.Image = Convert.ToBase64String(bytes);
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

        public IActionResult ConfirmBorrow(string id) {
            Book book = _db.books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        public IActionResult Borrow(string id)
        {
            Book book = _db.books.FirstOrDefault(y => y.Id == id);
            if (!book.IS_borrow)
            {
                book.Borrow_count++;
            }
            book.IS_borrow = !book.IS_borrow;
            _db.Update(book);
            _db.SaveChanges(); 
            return RedirectToAction("Index");
        }


    }
}
