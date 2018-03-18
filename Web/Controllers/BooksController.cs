using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using System.Linq.Dynamic;
using System.Collections;
using System.Web.Helpers;

namespace Web.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public async Task<ActionResult> Index(int page = 1, string sort = "Title", string sortdir = "asc", string search = "")
        {
            int pageSize = 5;
            int totalRecord = 0;
            if (page < 1) page = 1;
            int skip = (page * pageSize) - pageSize;
            var data = GetBooks(search, sort, sortdir, skip, pageSize, out totalRecord);
            ViewBag.TotalRows = totalRecord;
            ViewBag.search = search;
            return View(data);
        }

        public List<Book> GetBooks(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {

            {
                var v = (from a in db.Books
                         where
                                 a.Title.Contains(search) ||
                                 a.Authors.AuthorName.Contains(search) ||
                                 a.Categories.CateName.Contains(search) ||
                                 a.Price.ToString().Contains(search) ||
                                 a.Publishers.Name.Contains(search)

                         select a
                                );
                totalRecord = v.Count();
                //  data.OrderBy("Title desc") 
                string orderBy = sort + " " + sortdir;
                v = v.OrderBy(orderBy);
                if (pageSize > 0)
                {
                    v = v.Skip(skip).Take(pageSize);
                }
                return v.ToList();
            }
        }

       
        // GET: Books/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName");
            ViewBag.CateId = new SelectList(db.Categorys, "CateId", "CateName");
            ViewBag.PubId = new SelectList(db.Publishers, "PubId", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BookId,Title,CateId,AuthorId,PubId,Summary,ImgUrl,Price,Quantity,CreatedDate,ModifiedDate,ViewCount,IsActive")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName", book.AuthorId);
            ViewBag.CateId = new SelectList(db.Categorys, "CateId", "CateName", book.CateId);
            ViewBag.PubId = new SelectList(db.Publishers, "PubId", "Name", book.PubId);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName", book.AuthorId);
            ViewBag.CateId = new SelectList(db.Categorys, "CateId", "CateName", book.CateId);
            ViewBag.PubId = new SelectList(db.Publishers, "PubId", "Name", book.PubId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BookId,Title,CateId,AuthorId,PubId,Summary,ImgUrl,Price,Quantity,CreatedDate,ModifiedDate,ViewCount,IsActive")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName", book.AuthorId);
            ViewBag.CateId = new SelectList(db.Categorys, "CateId", "CateName", book.CateId);
            ViewBag.PubId = new SelectList(db.Publishers, "PubId", "Name", book.PubId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Book book = await db.Books.FindAsync(id);
            db.Books.Remove(book);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
