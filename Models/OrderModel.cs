using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using K_amazon.Utils;

namespace K_amazon.Models
{
    public class OrderModel
    {
        private AppDbContext _db;
        public OrderModel(AppDbContext ctx)
        {
            _db = ctx;
        }
        public int AddOrder(Dictionary<string, object> products, ApplicationUser user, Dictionary<string, string> msg)
        {
            int orderId = -1;
            using (_db)
            {
                // we need a transaction as multiple entities involved
                using (var _trans = _db.Database.BeginTransaction())
                {
                    try
                    {
                        Order order = new Order();
                        order.UserId = user.Id;
                        order.OrderDate = System.DateTime.Now;
                        order.OrderAmount = 0;

                        // calculate the totals and then add the order row to the table
                        foreach (var key in products.Keys)
                        {
                            ProductViewModel item = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(products[key]));
                            if (item.Qty > 0)
                            {
                                order.OrderAmount += item.MSRP * item.Qty;

                            }
                        }
                        _db.Orders.Add(order);
                        _db.SaveChanges();
                        // then add each item to the orderproducts table
                        foreach (var key in products.Keys)
                        {
                            ProductViewModel product = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(products[key]));
                            if (product.Qty > 0)
                            {
                                OrderLineItem oItem = new OrderLineItem();
                                oItem.QtyOrdered = product.Qty;
                                oItem.Product = _db.Products.FirstOrDefault(p => p.Id == product.Id);
                                // Enough stock
                                if (product.Qty <= oItem.Product.QtyOnHand)
                                {
                                    oItem.Product.QtyOnHand -= product.Qty;
                                    oItem.QtySold = product.Qty;
                                    oItem.QtyBackOrdered = 0;
                                }
                                else
                                {
                                    oItem.QtyBackOrdered = oItem.Product.QtyOnBackOrder += product.Qty - oItem.Product.QtyOnHand;
                                    oItem.QtySold = oItem.Product.QtyOnHand;
                                    oItem.Product.QtyOnHand = 0;
                                    msg[SessionVariables.OrderMessage] = " Some goods were backordered";
                                }

                                oItem.OrderId = order.Id;
                                oItem.SellingPrice = product.MSRP;
                                oItem.ProductId = product.Id;

                                _db.OrderLineItems.Add(oItem);
                                _db.Update(oItem.Product);
                                _db.SaveChanges();
                            }
                        }
                        // test trans by uncommenting out these 3 lines
                        //int x = 1;
                        //int y = 0;
                        //x = x / y;
                        _trans.Commit();
                        orderId = order.Id;
                    }
                    catch (Exception ex)
                    {
                        orderId = -1;
                        Console.WriteLine(ex.Message);
                        _trans.Rollback();
                    }
                }
            }
            return orderId;
        }

        public List<Order> GetAll(string user)
        {
            return _db.Orders.Where(order => order.UserId == user).ToList<Order>();
        }

        public List<OrderViewModel> GetOrderDetails(int tid, string uid)
        {
            List<OrderViewModel> allDetails = new List<OrderViewModel>();
            // LINQ way of doing INNER JOINS
            var results = from o in _db.Set<Order>()
                          join oi in _db.Set<OrderLineItem>() on o.Id equals oi.OrderId
                          join p in _db.Set<Product>() on oi.ProductId equals p.Id
                          where (o.UserId == uid && o.Id == tid)
                          select new OrderViewModel
                          {
                              OrderId = o.Id,
                              OrderAmount = oi.QtySold * oi.SellingPrice,
                              UserId = uid,
                              ProductName = oi.Product.ProductName,
                              MSRP = oi.SellingPrice,
                              QtyOrdered = oi.QtyOrdered,
                              QtySold = oi.QtySold,
                              QtyBackOrdered = oi.QtyBackOrdered,
                              OrderDate = o.OrderDate.ToString("yyyy/MM/dd - hh:mm tt")
                          };
            allDetails = results.ToList<OrderViewModel>();
            return allDetails;
        }
    }
}
