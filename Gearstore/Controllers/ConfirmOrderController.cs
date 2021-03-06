﻿using Gearstore.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Gearstore.Controllers
{
    public class ConfirmOrderController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        OrderInfo orderinfo = new OrderInfo();
        OrderDetails details = new OrderDetails();
     

        // POST: api/ConfirmOrder
        [Authorize(Roles = "Client")]
        public IHttpActionResult Post([FromBody]Checkout cout)
        {

            if (ModelState.IsValid)
            {
                orderinfo.OrderDate = DateTime.Now;
                orderinfo.OrderStatus = "confirm";
                 orderinfo.TotalCost = cout.total.ToString();
                orderinfo.userid = User.Identity.GetUserId();
                db.Orders.Add(orderinfo);
                db.SaveChanges();
                foreach (var value in cout.cart)
                {
                    details.OrderId = orderinfo.OrderInfoId;
                    details.productId = value.product.productId;
                    details.Quantity = value.quantity;
                    details.currentprice = value.product.Price;
                    db.OrderDetails.Add(details);
                    db.SaveChanges();
                  }
                return Ok();
            }
            else return BadRequest();
        }

        // PUT: api/ConfirmOrder/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ConfirmOrder/5
        public void Delete(int id)
        {
        }
    }
}
