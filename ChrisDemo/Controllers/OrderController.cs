using ChrisDemo.Models;
using ChrisDemo.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChrisDemo.Controllers
{
    public class OrderController : Controller
    {
        // Field variable db of type ChrisDemoContext
        private ChrisDemoContext db;

        // Constructor for db instance variable
        public OrderController()
        {
            db = new ChrisDemoContext();
        }

        // GET: Order/Index
        public ActionResult Index()
        {
            return View(new IndexViewModel {
                
                // Stores orders to Orders property
                Orders = db.Order.ToList(),         
            });
        }

        // Get: Order/Create (Order)
        public ActionResult Create()
        {
            // Return a new Order object to the view
            return View(new CreateViewModel
            {
                Order = new Order
                {
                    // Sets the Date's default display value 
                    Date = DateTime.Now
                },
                // Stores the Location's to a list of Locations
                Locations = db.Location.ToList(),
                Customers = db.Customer.ToList()
            });
        }

        // Post: Order/Create (Order)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel viewModel)
        {
            // Try to save the new object to the db
            try
            {
                if (ModelState.IsValid)
                {
                    // Add the whole viewModel
                    db.Order.Add(viewModel.Order);

                    // Save the whole viewModel
                    db.SaveChanges();

                    // Message to handle Create's status result
                    TempData["success"] = "Order created successfully";

                    return RedirectToAction("Index");
                }

                return View(viewModel);
            }
            catch (Exception)
            {
                // Message to handle Create's status result
                TempData["danger"] = "Order was not created";

                return RedirectToAction("Index");
            }            
        }

        // Get: Order/CreateItem/id (OrderItem)
        [HttpGet]
        public ActionResult CreateItem(int id)
        {
            try
            {
                // Find order's id fir and if it fails then check for Item Id
                var order = db.Order.Single(o => o.Id == id);

                // another way to do the return view below
                //var viewModel = new CreateItemViewModel();
                //viewModel.OrderItem = new OrderItem();
                //viewModel.OrderItem.OrderId = order.Id;
                //return View(viewModel);

                // Sets the OrderItem order Id property so that the id can be used in the post method to redirect back to the Details page
                return View(new CreateItemViewModel
                {
                    OrderItem = new OrderItem
                    {
                        OrderId = order.Id
                    }
                });
            }
            catch (Exception)
            {
                // Message to handle Create Item status result
                TempData["danger"] = "Order Item's Order record does not exist";

                return HttpNotFound();           
            }
        }

        // Post: Order/CreateItem (OrderItem)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateItem(CreateItemViewModel viewModel)
        {
            // Tries to save the new OrderItem record
            try
            {
                if (ModelState.IsValid)
                {                   
                    // Add new user's record to db
                    db.OrderItem.Add(viewModel.OrderItem);

                    // Save user's object
                    db.SaveChanges();

                    // Message to handle Create's status result
                    TempData["success"] = "Order Item created successfully";

                    return RedirectToAction("Details", new { id = viewModel.OrderItem.OrderId });
                }
                return View(viewModel);
            }
            catch (Exception)
            {
                // Message to handle Create's status result
                TempData["danger"] = "Order Item was not created";

                return RedirectToAction("Details", new { id = viewModel.OrderItem.OrderId });
            }           
        }

        // Get: Order/Detail/id
        public ActionResult Details(int id)
        {
            // Try to get details for the requested object
            try
            {              
                // Gets user's requested object
                var order = db.Order.Single(o => o.Id == id);

                // Return to the view the details from the requested object
                return View(new DetailsViewModel
                {
                    Order = order,                    
                });
            }
            catch (Exception)
            {
                // Message to handle record not existing
                TempData["danger"] = "Order Item does not exist";

                return RedirectToAction("Index");
            }
        }


        // Get: Order/Edit/id
        public ActionResult Edit(int id)
        {
            // Try to Edit the user's selected object
            try
            {
                // Return to the view the selected Order for editing and it's locations in a select list
                return View(new EditViewModel
                {
                    Order = db.Order.Single(o => o.Id == id),
                    Locations = db.Location.ToList(),
                    Customers = db.Customer.ToList()
                });
            }
            catch (Exception)
            {
                // Message to handle record not existing
                TempData["danger"] = "Order does not exist";

                return RedirectToAction("Index");
            }
        }

        // Post: Order/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel viewModel)
        {
            // Try to save the User's object modifications
            try
            {
                // Get user object
                var order = db.Order.Single(o => o.Id == viewModel.Order.Id);
                               
                if (ModelState.IsValid)
                {
                    // Updates user object
                    order.LocationId = viewModel.Order.LocationId;
                    order.CustomerId = viewModel.Order.CustomerId;
                    order.Date = viewModel.Order.Date;                                     

                    // Save user's object
                    db.SaveChanges();

                    // Message to handle Edit's status result
                    TempData["success"] = "Order was updated successfully";

                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }            
            catch (Exception)
            {
                // Message to handle Edit's status result
                TempData["danger"] = "Order was not updated";

                return View(viewModel);
            }
        }
        
        // Get: Order/EditItem/id
        public ActionResult EditItem(int id)
        {
            // Try to get the object of the user's requested id for editing
            try
            {
                return View(new EditItemViewModel
                {
                    OrderItem = db.OrderItem.Single(oi => oi.Id == id)
                });
            }
            catch (Exception)
            {
                // Message to handle Edit's status result
                TempData["danger"] = "Order Item does not exist";

                return HttpNotFound();
            }
        }

        // Post: Object/EditItem        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditItem(EditItemViewModel viewModel)
        {
            try
            {
                // Saving viewModel's values (User) to orderItem's properties 
                if (ModelState.IsValid)
                {
                    // Gets user object
                    var orderItem = db.OrderItem.Single(oi => oi.Id == viewModel.OrderItem.Id);

                    // Explicitly verify that the Item exists
                    var item = db.Item.Single(i => i.Id == viewModel.OrderItem.ItemId);

                    // Updates user object
                    orderItem.ItemId = viewModel.OrderItem.ItemId;
                    orderItem.Price = viewModel.OrderItem.Price;

                    // Save changes
                    db.SaveChanges();

                    // Message to handle Edit's status result
                    TempData["success"] = "Order Item was updated successfully";

                    return RedirectToAction("Details", new { id = orderItem.OrderId });
                }
                return View(viewModel);
            }
            catch (Exception)
            {
                // Message to handle Edit's status result
                TempData["danger"] = "Order was not updated";

                return RedirectToAction("Index");
            }
        }

        // Get: Order/Delete/id
        [HttpGet]
        public ActionResult DeleteOrderItem(int id)
        {        
            try
            {
                // Get the object that the user requested by id            
                var orderItem = db.OrderItem.Single(oi => oi.Id == id);

                return View(new DeleteViewModel
                {
                    OrderItem = orderItem
                });
            }
            catch
            {
                // Message to handle Details status result
                TempData["danger"] = "Order Item does not exist";

                return RedirectToAction("Index");
            }                        
        }

        // Post: Order/Delete/id
        // ActionName is needed, because DeleteOrderItem signature I had to append Post to differentiate from the Get version
        [HttpPost, ActionName("DeleteOrderItem")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOrderItemPost(int id)
        {                      
            // Try to remove the OrderItem
            try
            {
                // Get object of requested id            
                var orderItem = db.OrderItem.Single(oi => oi.Id == id);

                // Remove OrderItem
                db.OrderItem.Remove(orderItem);

                // Save the changes
                db.SaveChanges();

                // Message to handle Delete's status result
                TempData["success"] = "Order Item was deleted successfully";

                // Redirect to the Order Details page
                return RedirectToAction("Details", new { id = orderItem.OrderId });
            }
            catch (Exception)
            {
                // Message to handle Delete's status result
                TempData["danger"] = "Order Item was not deleted";

                // Redirect to the Order Details page
                return RedirectToAction("Index");
            }
        }

        // Get Order/Delete/id (Delete's Order)
        public ActionResult Delete(int id)
        {
            // Get object of requested id
            var order = db.Order.Single(o => o.Id == id);

            try
            {
                return View(new DeleteViewModel
                {
                    Order = order,
                });
            }
            catch (Exception)
            {
                // Message to handle Delete's status result
                TempData["danger"] = "Order does not exist";

                return RedirectToAction("Details", new { id = order.Id });
            }
        }

        // Post: Order/Delet/id (Delete's Order)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            try
            {
                // Gets the object
                var order = db.Order.Single(o => o.Id == id);

                // Removes FK references to OrderItems
                foreach (var oi in order.OrderItems.ToList())
                {
                    db.OrderItem.Remove(oi);
                    db.SaveChanges();
                }

                // Delete's order
                db.Order.Remove(order);

                // Saves with removed records
                db.SaveChanges();

                // Message to handle Delete's status result
                TempData["success"] = "Order was deleted successfully";

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Message to handle Delete's status result
                TempData["danger"] = "Order was not deleted";

                return RedirectToAction("Index");
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