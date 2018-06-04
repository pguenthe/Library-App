using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryApp.Models;

namespace LibraryApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult BookList()
        {
            LibraryEntities db = new LibraryEntities();
            List<book> books = db.books.ToList();
            ViewBag.Books = books;

            ViewBag.Statuses = db.Statuses.ToList();

            return View();
        }

        //filter by status
        public ActionResult BookListByStatus(int status)
        {
            LibraryEntities db = new LibraryEntities();
            //LINQ Query
            List<book> books = (from b in db.books
                               where b.Status == status
                               select b).ToList();
            ViewBag.Books = books;
            
            ViewBag.Statuses = db.Statuses.ToList();

            return View("BookList");
        }

        public ActionResult BookListByAuthor(string author)
        {
            LibraryEntities db = new LibraryEntities();
            //LINQ Query
            List<book> books = (from b in db.books
                                where b.Author.Contains(author)
                                select b).ToList();
            ViewBag.Books = books;
            
            ViewBag.Statuses = db.Statuses.ToList();

            return View("BookList");
        }

        public ActionResult BookListSorted(string column)
        {
            LibraryEntities db = new LibraryEntities();
            //LINQ Query
            if (column == "Title")
            {
                ViewBag.Books = (from b in db.books
                                orderby b.Title
                                select b).ToList();
            }
            else if (column == "Author")
            {
                ViewBag.Books = (from b in db.books
                                 orderby b.Author
                                 select b).ToList();
            }
            else if (column == "Status")
            {
                ViewBag.Books = (from b in db.books
                                 orderby b.Status
                                 select b).ToList();
            }
            else if (column == "YearPublished")
            {
                ViewBag.Books = (from b in db.books
                                 orderby b.YearPublished
                                 select b).ToList();
            }

            ViewBag.Statuses = db.Statuses.ToList();

            return View("BookList");
        }

        public ActionResult Add(int id)
        {
            LibraryEntities db = new LibraryEntities();

            //check if the Cart object already exists
            if (Session["Cart"] == null)
            {
                //if it doesn't, make a new list of books
                List<book> cart = new List<book>();
                //add this book to it
                cart.Add ((from b in db.books
                          where b.Ranking == id
                          select b).Single());
                //add the list to the session
                Session.Add("Cart", cart);
            }
            else
            {
                //if it does exist, get the list
                List<book> cart = (List<book>)(Session["Cart"]);
                //add this book to it
                cart.Add((from b in db.books
                          where b.Ranking == id
                          select b).Single());
            }
            return View();
        }
    }
}