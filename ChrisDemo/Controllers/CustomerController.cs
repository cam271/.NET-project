using ChrisDemo.Models;
using ChrisDemo.ViewModel.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChrisDemo.Controllers
{
    public class CustomerController : Controller
    {
        // Private field varibale db
        private ChrisDemoContext db;

        // CustomerController Constructor
        public CustomerController()
        {
            db = new ChrisDemoContext();
        }

        // GET: Customer/Index
        public ActionResult Index()
        {
            return View(new IndexViewModel
            {
                // Stores Customers to Customer property
                Customers = db.Customer.ToList()
            });
        }

        // Get: Customer/Create
        public ActionResult Create()
        {
            //var states = GetStates();
            return View(new CreateViewModel {
                Customer = new Customer(),
                Locations = db.Location.ToList(),
                States = GetStates()           
            });
        }

        // Get: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel viewModel)
        {
            try
            {
                // Add users form to db and save db
                db.Customer.Add(viewModel.Customer);
                db.SaveChanges();

                // Message to handle Create's status result
                TempData["success"] = "Customer created successfully";

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Message to handle Create status result
                TempData["danger"] = "Customer was not created";

                return RedirectToAction("Index");
            }
        }

        // Get Customer/Edit/id
        public ActionResult Edit(int id)
        {
            try
            {
                return View(new EditViewModel
                {
                    Customer = db.Customer.Single(c => c.Id == id),
                    Locations = db.Location.ToList(),
                    States = GetStates()
                });
            }
            catch (Exception)
            {
                // Message to handle record not existing
                TempData["danger"] = "Customer does not exist";

                return RedirectToAction("Index");
            }
        }

        // Post Customer/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // get object to Edit
                    var customer = db.Customer.Single(c => c.Id == viewModel.Customer.Id);

                    // Get user's values
                    customer.Name = viewModel.Customer.Name;
                    customer.Address = viewModel.Customer.Address;
                    customer.City = viewModel.Customer.City;
                    customer.State = viewModel.Customer.State;
                    customer.Zip = viewModel.Customer.Zip;
                    customer.LocationId = viewModel.Customer.LocationId;                                    
                    
                    // Save changes
                    db.SaveChanges();

                    // Message to handle Edit status result
                    TempData["success"] = "Customer updated successfully";

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Message to handle Edit status result
                TempData["danger"] = "Customer was not updated";

                return RedirectToAction("Index");
            }            
        }

        // Get: Customer/Details/id
        public ActionResult Details(int id)
        {
            try
            {
                return View(new DetailsViewModel
                {
                    Customer = db.Customer.Single(c => c.Id == id)
                });
            }
            catch (Exception)
            {
                // Message to handle record not existing
                TempData["danger"] = "Customer does not exist";

                return RedirectToAction("Index");
            }
        }

        // Get Delete
        public ActionResult Delete(int id)
        {
            try
            {
                return View(new DeleteViewModel
                {
                    Customer = db.Customer.Single(c => c.Id == id)
                });
            }
            catch (Exception)
            {
                // Message to handle record not existing
                TempData["danger"] = "Customer does not exist";

                return RedirectToAction("Index");
            }
        }

        // Post Delete
        // Get Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomer(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Get the passed in customer id
                    var customer = db.Customer.Single(c => c.Id == id);
                    
                    // Remove the customer form the db
                    db.Customer.Remove(customer);

                    // Svae the db
                    db.SaveChanges();

                    // Message to handle Delete status results
                    TempData["success"] = "Customer deleted successfully";

                    return RedirectToAction("Index");
                }            
            }
            catch (Exception)
            {
                // Message to handle Delete status results
                TempData["danger"] = "Customer was not deleted";

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GetStates populates the states for the drop down list returns a list of type State
        public List<State> GetStates()
        {
            var states = new List<State>();

            states.Add(new State { Name = "Alabama", Abbreviation = "AL" });
            states.Add(new State { Name = "Alaska", Abbreviation = "AK" });
            states.Add(new State { Name = "Arizona", Abbreviation = "AZ" });
            states.Add(new State { Name = "Arkansas", Abbreviation = "AR" });
            states.Add(new State { Name = "California", Abbreviation = "CA" });         
            states.Add(new State { Name = "Colorado", Abbreviation = "CO" });            
            states.Add(new State { Name = "Connecticut", Abbreviation = "CT" });
            states.Add(new State { Name = "Delaware", Abbreviation = "DE" });
            states.Add(new State { Name = "Florida", Abbreviation = "FL" });
            states.Add(new State { Name = "Georgia", Abbreviation = "GA" });
            states.Add(new State { Name = "Hawaii", Abbreviation = "HI" });
            states.Add(new State { Name = "Idaho", Abbreviation = "ID" });
            states.Add(new State { Name = "Illinois", Abbreviation = "IL" });
            states.Add(new State { Name = "Indiana", Abbreviation = "IN" });
            states.Add(new State { Name = "Iowa", Abbreviation = "IA" });
            states.Add(new State { Name = "Kansas", Abbreviation = "KS" });
            states.Add(new State { Name = "Kentucky", Abbreviation = "KY" });
            states.Add(new State { Name = "Louisiana", Abbreviation = "LA" });
            states.Add(new State { Name = "Maine", Abbreviation = "ME" });
            states.Add(new State { Name = "Maryland", Abbreviation = "MD" });
            states.Add(new State { Name = "Massachusetts", Abbreviation = "MA" });
            states.Add(new State { Name = "Michigan", Abbreviation = "MI" });
            states.Add(new State { Name = "Minnesota", Abbreviation = "MN" });
            states.Add(new State { Name = "Mississippi", Abbreviation = "MS" });
            states.Add(new State { Name = "Missouri", Abbreviation = "MO" });
            states.Add(new State { Name = "Montana", Abbreviation = "MT" });
            states.Add(new State { Name = "Nebraska", Abbreviation = "NE" });
            states.Add(new State { Name = "Nevada", Abbreviation = "NV" });
            states.Add(new State { Name = "New Hampshire", Abbreviation = "NH" });
            states.Add(new State { Name = "New Jersey", Abbreviation = "NJ" });
            states.Add(new State { Name = "New Mexico", Abbreviation = "NM" });
            states.Add(new State { Name = "New York", Abbreviation = "NY" });
            states.Add(new State { Name = "North Carolina", Abbreviation = "NC" });
            states.Add(new State { Name = "North Dakota", Abbreviation = "ND" });
            states.Add(new State { Name = "Ohio", Abbreviation = "OH" });
            states.Add(new State { Name = "Oklahoma", Abbreviation = "OK" });
            states.Add(new State { Name = "Oregon", Abbreviation = "OR" });
            states.Add(new State { Name = "Pennsylvania", Abbreviation = "PA" });
            states.Add(new State { Name = "Rhode Island", Abbreviation = "RI" });
            states.Add(new State { Name = "South Carolina", Abbreviation = "SC" });
            states.Add(new State { Name = "South Dakota", Abbreviation = "SD" });
            states.Add(new State { Name = "Tennessee", Abbreviation = "TN" });
            states.Add(new State { Name = "Texas", Abbreviation = "TX" });
            states.Add(new State { Name = "Utah", Abbreviation = "UT" });
            states.Add(new State { Name = "Vermont", Abbreviation = "VT" });
            states.Add(new State { Name = "Virginia", Abbreviation = "VA" });
            states.Add(new State { Name = "Washington", Abbreviation = "WA" });
            states.Add(new State { Name = "West Virginia", Abbreviation = "WV" });
            states.Add(new State { Name = "Wisconsin", Abbreviation = "WI" });
            states.Add(new State { Name = "Wyoming", Abbreviation = "WY" });

            return states;
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