using ChrisDemo.Models;
using ChrisDemo.ViewModel.Item;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ChrisDemo.Controllers
{
    public class ItemController : Controller
    {
        private ChrisDemoContext db;

        public ItemController()
        {
            db = new ChrisDemoContext();
        }

        // GET: Item/Index
        public ActionResult Index()
        {
            // Try to get a list of Items
            try
            {
                return View(new IndexViewModel
                {
                    Items = db.Item.ToList()
                });               
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
           
        }

        // GET: Item/Create
        public ActionResult Create()
        {
           // Try to retrieve properties needed to create a new Item        
            try
            {
                return View(new CreateViewModel
                {
                    Item = new Item(),
                    Categories = db.Category.ToList(),
                    Locations = db.Location.ToList(),
                    SelectedLocationIds = new List<int>()                    
                });
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel viewModel)
        {
            // Try to save a new Item
            try
            {
                if (ModelState.IsValid)
                {
                    // Loop through Location Id's to save an Item's Locations from the multi select list
                    if(viewModel.SelectedLocationIds != null)
                    {
                        foreach (var sli in viewModel.SelectedLocationIds)
                        {
                            Location location = db.Location.Single(l => l.Id == sli);
                            viewModel.Item.Locations.Add(location);
                        }
                    }
                    
                    // Add Item to db
                    db.Item.Add(viewModel.Item);

                    // Save to the db
                    db.SaveChanges();

                    // Message to handle Create's result
                    TempData["success"] = "Item was successfully created";

                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                // Message to handle Create's result
                TempData["danger"] = "Item was not created";

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Item/Details/id
        public ActionResult Details(int id)
        {
            // Try to get details of User's selected Item
            try
            {               
                return View(new DetailsViewModel
                {
                    Item = db.Item.Single(i => i.Id == id)
                });
            }
            catch (InvalidOperationException)
            {
                // Message to handle Details's result
                TempData["danger"] = "Item does not exist";

                return RedirectToAction("Index");
            }  
            catch (Exception)
            {
                // Message to handle Detail's result
                TempData["danger"] = "Item does not exist";

                return RedirectToAction("Index");
            }
        }

        // GET: Item/Edit/id
        public ActionResult Edit(int id)
        {
            // Try to Edit the user's selected Item
            try
            {
                // Get the item by finding the id
                var item = db.Item.Single(l => l.Id == id);

                return View(new EditViewModel
                {
                    Item = item,
                    Categories = db.Category.ToList(),
                    Locations = db.Location.ToList(),
                    SelectedLocationIds = item.Locations.Select(e => e.Id).ToList()
                });
            }
            catch (Exception)
            {
                // Message to handle Edit's result
                TempData["danger"] = "Item does not exist";

                return RedirectToAction("Index");
            }
        }

        // POST: Item/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Finds the item by id
                    var item = db.Item.Single(i => i.Id == viewModel.Item.Id);

                    // Set the item's properties to the user's values
                    item.Description = viewModel.Item.Description;
                    item.ItemNumber = viewModel.Item.ItemNumber;
                    
                    // Related Tables
                    item.CategoryId = viewModel.Item.CategoryId;

                    // Remove all Items from the Location
                    foreach (var l in item.Locations.ToList())
                    {
                        item.Locations.Remove(l);
                    }

                    // Set Item's new location to from the user's choices on the multi select list
                    if(viewModel.SelectedLocationIds != null)
                    {
                        foreach (var sii in viewModel.SelectedLocationIds)
                        {
                            var location = db.Location.Single(i => i.Id == sii);
                            item.Locations.Add(location);
                        }
                    }
                    
                    // Save changes to db
                    db.SaveChanges();

                    // Message to handle Edit's result
                    TempData["success"] = "Item updated successfully";

                    return RedirectToAction("Index");
                }

                return View(viewModel);
            }
            catch (Exception)
            {
                // Message to handle Edit's result
                TempData["danger"] = "Item was not updated";

                return View(viewModel);
            }
        }


        // GET: Item/Delete/id
        public ActionResult Delete(int id)
        {
            // Try to Delete the user's selected record
            try
            {
                return View(new DeleteViewModel
                {
                    Item = db.Item.Single(i => i.Id == id)
                });
            }
            catch (Exception)
            {
                // Message to handle Delete's result
                TempData["danger"] = "Item does not exist";

                return RedirectToAction("Index");
            }
        }

        // POST: Item/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            // Try to delete user's selected record on button click
            try
            {                
                var item = db.Item.Single(i => i.Id == id);
                db.Item.Remove(item);
                db.SaveChanges();

                // Message to handle Delete's result
                TempData["success"] = "Item was deleted successfully";

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Message to handle Delete's result
                TempData["danger"] = "Item was not deleted";

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