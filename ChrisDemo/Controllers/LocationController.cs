using ChrisDemo.Models;
using ChrisDemo.ViewModel.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;

namespace ChrisDemo.Controllers
{
    public class LocationController : Controller
    {
        private ChrisDemoContext db;

        public LocationController()
        {
            db = new ChrisDemoContext();
        }

        // GET: Location/Index
        public ActionResult Index()
        {
           // get all Locations and display as a list
            try
            {             
                return View(new IndexViewModel
                {
                    Locations = db.Location.ToList()
                });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }


        // GET: Location/Create
        public ActionResult Create()
        {
            // Try to create a new Location
            try
            {
                return View(new CreateViewModel
                {
                    Location = new Location(),
                    Items = db.Item.ToList(),
                    SelectedItemIds = new List<int>()
                });
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }          
        }

        // Post: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel viewModel)
        {
            // Try to create a new Location
            try
            {
                if (ModelState.IsValid)
                {
                    // Loop through multi select list item id's to get the Location Items
                    if (viewModel.SelectedItemIds != null)
                    {
                        foreach (var sii in viewModel.SelectedItemIds)
                        {
                            var item = db.Item.Single(i => i.Id == sii);
                            viewModel.Location.Items.Add(item);
                        }
                    }
                   
                    // Add user's form values to db
                    db.Location.Add(viewModel.Location);

                    // Save db
                    db.SaveChanges();

                    // Message to handle Create's result
                    TempData["success"] = "Location was successfully created";

                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
            catch (Exception)
            {
                // Message to handle Create's result
                TempData["danger"] = "Location was not created";

                return RedirectToAction("Index");
            }            
        }

        // Get Location/Details/id
        public ActionResult Details(int id)
        {
            // Try to get the details of the selected record
            try
            {
                return View(new DetailsViewModel
                {
                    Location = db.Location.Single(l => l.Id == id)
                });
            }
            catch (InvalidOperationException)
            {
                // Message to handle record not found
                TempData["danger"] = "Location could not be found";

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Message to handle record not found
                TempData["danger"] = "Location could not be found";

                return RedirectToAction("Index");
            }
        }

        // Get: Location/Edit/id
        public ActionResult Edit(int id)
        {
           // Try to get the location to be Edited
            try
            {
                // Finds the user's seleced location
                var location = db.Location.Single(l => l.Id == id);

                return View(new EditViewModel
                {
                    Location = location,
                    Items = db.Item.ToList(),
                    SelectedItemIds = location.Items.Select(e => e.Id).ToList()
                });
            }
            catch (Exception)
            {
                // Message to handle record not found
                TempData["danger"] = "Location could not be found";

                return RedirectToAction("Index");
            }
        }

        // Post: Location/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Get the existing Location
                    var location = db.Location.Single(i => i.Id == viewModel.Location.Id);

                    // Set its properties to the same properties from the Location in the ViewModel
                    location.Name = viewModel.Location.Name;

                    // Remove all items for this Location
                    foreach(var l in location.Items.ToList())
                    {
                        location.Items.Remove(l);
                    }

                    // Set its properties for the new selected items
                    if(viewModel.SelectedItemIds != null)
                    {
                        foreach (var sii in viewModel.SelectedItemIds)
                        {
                            var item = db.Item.Single(i => i.Id == sii);
                            location.Items.Add(item);
                        }
                    }
                    
                    // Save changes
                    db.SaveChanges();

                    // Message to handle Edit result
                    TempData["success"] = "Location edited successfuly";

                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }            
            catch (Exception)
            {
                // Message to handle Edit result
                TempData["danger"] = "Location was not updated";

                return View(viewModel);
            }
        }

        // Get: Location/Delete/id
        public ActionResult Delete(int id)
        {
            try
            {
                return View(new DeleteViewModel
                {
                    Location = db.Location.Single(l => l.Id == id)
                });
            }
            catch (Exception)
            {
                // Message to handle record not found
                TempData["danger"] = "Location could not be found";

                return RedirectToAction("Index");
            }
        }

        // Post: Location/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            // Try to delete Location
            try
            {             
                // Find User's selected location   
                var location = db.Location.Single(i => i.Id == id);

                // Remove the selected location
                db.Location.Remove(location);

                // Save the db
                db.SaveChanges();

                // Message to handle Delete's result
                TempData["success"] = "Location deleted successfuly";

                return RedirectToAction("index");
            }
            catch (Exception)
            {
                // Message to handle Delete's result
                TempData["danger"] = "Location deleted successfuly";

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