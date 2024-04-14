using Library_management.Data;
using Library_management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.FileProviders;
using System;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Library_management.Controllers
{
    
    public class BookController : Controller
    {
        private readonly ApplicationDBContext _db;

        public BookController(ApplicationDBContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult Index(string search_bar,string categories)
        {
            IEnumerable<Book> all_books = _db.books;
            if (!search_bar.IsNullOrEmpty() && categories != "All")
            {
                all_books = all_books.Where(n => n.Title.Contains(search_bar) && n.Category.Contains(categories));
            }
            if (!search_bar.IsNullOrEmpty() && categories == "All")
            {
                all_books = all_books.Where(n => n.Title.Contains(search_bar));
            }
            if (search_bar.IsNullOrEmpty() && categories != "All")
            {
                all_books = all_books.Where(n => n.Category.Contains(categories));
            }
            if (search_bar.IsNullOrEmpty() && categories == "All")
            {
                all_books = all_books;
            }
            return View(all_books);
        }

        public IActionResult Index()
        {
            IEnumerable<Book> all_books = _db.books;
            return View(all_books);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create() {
            return View(); 
        }
        
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create(Book obj)
        {

            Random rnd = new Random();
            int num = rnd.Next();
            byte[] bytes = null;
            string str = obj.Title + Convert.ToString(num) + DateTime.Now;
            byte[] encoded = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(str));
            var value = BitConverter.ToUInt32(encoded, 0) % 1000000;
            obj.Id = Convert.ToString(value);
            obj.IS_borrow = false;
            obj.WhoBorrowNow = "";
            obj.Start = "";
            obj.Stop = "";
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

        [Authorize]
        public IActionResult Detail(string id)
        {
            Book book = _db.books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [Authorize]
        public IActionResult ConfirmBorrow(string id) {
            Book book = _db.books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Edit(string id)
        {
            byte[] bytes = null;
            System.Diagnostics.Debug.WriteLine(id);
            Book book = _db.books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);   
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Edit(string id,Book obj)
        {
            Book book = _db.books.Find(id);
            if (obj.ImageFile == null) {
            return View(book);
                    }
            byte[] bytes = null;

            using (Stream fs = obj.ImageFile.OpenReadStream())
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    bytes = br.ReadBytes((Int32)fs.Length);
                }
            }

            book.Id = obj.Id;
            book.Title = obj.Title;
            book.Author = obj.Author;
            book.Description = obj.Description;
            book.Category = obj.Category;
            book.Image = Convert.ToBase64String(bytes);
            book.Start = obj.Start;
            book.Stop = obj.Stop;
            book.WhoBorrowNow = obj.WhoBorrowNow;

           
            _db.SaveChanges();
            return LocalRedirect("/");
        }



        [HttpPost]
        public IActionResult Borrow(string id,string userid, string stop_date)
        {

            //save book status to db
            Book book = _db.books.FirstOrDefault(y => y.Id == id);
            book.Borrow_count++;
             book.WhoBorrowNow = userid;
            book.IS_borrow = !book.IS_borrow;
            book.Start = Convert.ToString(DateTime.Now.ToShortDateString());
            book.Stop = Convert.ToString(DateTime.Parse(stop_date).ToShortDateString());
           
            //save log borrow to db
            Borrow_log borrow = new Borrow_log();
            Random rnd = new Random();
            int num = rnd.Next();
            byte[] bytes = null;
            string str = book.Title + Convert.ToString(num) + userid;
            byte[] encoded = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(str));
            var value = BitConverter.ToUInt32(encoded, 0) % 10000000;
            borrow.borrow_id = Convert.ToString(value);
            borrow.userid = userid;
            borrow.book_id = book.Id;
            borrow.start = book.Start;
            borrow.end = book.Stop;

            _db.Update(book);
            _db.borrows.Add(borrow);
            _db.SaveChanges();


           
           
            //_db.SaveChanges();
            return RedirectToAction("Index");

        }
     
        public IActionResult Return(string id)
        {
            
           Book book = _db.books.FirstOrDefault(y => y.Id == id);
           book.WhoBorrowNow = "";
           book.IS_borrow = !book.IS_borrow;

            _db.Update(book);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        
        
        [Authorize(Roles = "admin")]
        public IActionResult BookList()
        {
            IEnumerable<Book> all_books = _db.books;
            return View(all_books);
        }
    }
}
