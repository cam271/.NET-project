using ChrisDemo.Models;
using ChrisDemo.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChrisDemo.Controllers
{
    public class CategoryController : Controller
    {
        private ChrisDemoContext db;

        public CategoryController()
        {
            db = new ChrisDemoContext();
        }

        // GET: Category
        public ActionResult Index()
        {
            //var viewModel = new IndexViewModel();
            //viewModel.Categories = db.Category.ToList();

            //var viewModel = new IndexViewModel
            //{
            //    Categories = db.Category.ToList()
            //};

            // New way uses object initialization refactored from commented section above
            try
            {
                return View(new IndexViewModel
                {
                    Categories = db.Category.ToList()
                });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // Get: Create
        public ActionResult Create()
        {
            //var viewModel = new CreateViewModel();
            //viewModel.Category = new Category();

            //return View(viewModel);

            // New way uses object initialization refactored from commented section above
            // TODO: Remove the try catch block around the return statement (not needed for this mehtod)    
            try
            {
                return View(new CreateViewModel
                {
                    Category = new Category(),
                    Items = db.Item.ToList(),
                    SelectedItemIds = new List<int>()
                });
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        // Post: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(viewModel.SelectedItemIds != null)
                    {
                        foreach(var sii in viewModel.SelectedItemIds)
                        {
                            var item = db.Item.Single(i => i.Id == sii);
                            viewModel.Category.Items.Add(item);
                        }
                    }

                    db.Category.Add(viewModel.Category);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(viewModel);
        }

        // Details method (read)
        // HttpGet Category/Details/#
        public ActionResult Details(int id)
        {
            try
            {
                // var viewModel = new DetailsViewModel();
                //var category = db.Category.Single(e => e.Id == id);
                //viewModel.Category = category;

                // how to reference another tables id
                //viewModel.Category.Items = db.Item.Where(i => i.CategoryId == id).ToList();

                // New way uses object initialization refactored from commented section above
                return View(new DetailsViewModel
                {
                    Category = db.Category.Single(e => e.Id == id)
                });
            }            
            catch (InvalidOperationException)
            {
                return RedirectToAction("Index");
            }
            
        }

        // Get: Edit
        public ActionResult Edit(int id)
        {
            //var viewModel = new EditViewModel();
            //viewModel.Category = db.Category.Find(id);

            // New way uses object initialization refactored from commented section above
            try
            {
                var category = db.Category.Single(i => i.Id == id);

                return View(new EditViewModel
                {
                    Category = category,
                    Items = db.Item.ToList(),
                    SelectedItemIds = category.Items.Select(e => e.Id).ToList()
                });
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        // Edit method (update)
        // HttpPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Get the existing Category
                    var category = db.Category.Single(c => c.Id == viewModel.Category.Id);

                    // Set its properties to the same properties from the Category in the ViewModel
                    category.Name = viewModel.Category.Name;
                    category.Description = viewModel.Category.Description;
                                                            
                    // Remove all items from the category
                    // NOTE: You cannot remove Item's from a Category on this end
                    //       you must re-assign the Category from the ItemEdit page
                    //foreach (var c in category.Items.ToList())
                    //{
                    //    category.Items.Remove(c);
                    //}

                    // Set its properties for the new selected items
                    if(viewModel.SelectedItemIds != null)
                    {
                        foreach (var sii in viewModel.SelectedItemIds)
                        {
                            var item = db.Item.Single(i => i.Id == sii);
                            category.Items.Add(item);
                        }
                    }

                    // Save changes
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(viewModel);
            }
            catch (Exception)
            {
                return View(viewModel);
            }
        }

        // Delete method
        // HttpGet
        public ActionResult Delete(int id)
        {
            //var viewModel = new DeleteViewModel();
            //viewModel.Category = db.Category.Find(id);

            try
            {
                return View(new DeleteViewModel
                {
                    Category = db.Category.Single(c => c.Id == id)
                });
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        // Delete method
        // HttpPost
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            try
            {
                var Category = db.Category.Single(c => c.Id == id);
                db.Category.Remove(Category);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Delete");
            }
        }

        // Dispose method
        // closes the db connection
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