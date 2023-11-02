using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketySystem.DAL;
using TicketySystem.Models;

namespace TicketySystem.Controllers
{
    public class TicketController : Controller
    {
        Ticket_DAL _TicketDAL = new Ticket_DAL();

        // GET: Ticket
        public ActionResult Index()
        {
            var ticketList = _TicketDAL.GetAllTickets();
            return View(ticketList);
        }

        // GET: Ticket/Details/5
        public ActionResult Details(Guid id)
        {
            var ticketList = _TicketDAL.GetTickets(id).FirstOrDefault();
            return View(ticketList);
        }

        // GET: Ticket/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ticket/Create
        [HttpPost]
        public ActionResult Create(Ticket ticket)
        {
            try
            {
                string guidValue = Session["userGuid"].ToString();
                // TODO: Add insert logic here
                ticket.Id = Guid.Parse(guidValue);
                ticket.IsResolved = false;
                bool IsInserted = false;
                if (ModelState.IsValid)
                {
                    IsInserted = _TicketDAL.insertTicket(ticket);
                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "New Ticket saved successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to saved Ticket successfully...!";
                    }

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ticket/Edit/5
        public ActionResult Edit(Guid id)
        {
            var ticketList = _TicketDAL.GetTickets(id).FirstOrDefault();
            return View(ticketList);
        }

        // POST: Ticket/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Ticket ticket)
        {
            try
            {
                bool IsUpdate = false;
                if (ModelState.IsValid)
                {
                    IsUpdate = _TicketDAL.updTicketDetail(ticket);
                    if (IsUpdate)
                    {
                        TempData["SuccessMessage"] = "New Ticket saved successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to saved Ticket successfully...!";
                    }

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ReslovedTicket(Guid id)
        {
            var ticketList = _TicketDAL.GetTickets(id).FirstOrDefault();
            return View(ticketList);
        }

        [HttpPost]
        public ActionResult ReslovedTicketConfirmation(Guid id, Ticket ticket)
        {
            try
            {
                Guid guidValue = (Guid)Session["userGuid"];
                ticket.RDUserId = guidValue;
                _TicketDAL.updTicketResloved(id,ticket);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ticket/Delete/5
        public ActionResult Delete(Guid id)
        {
            var ticketList = _TicketDAL.GetTickets(id).FirstOrDefault();
            return View(ticketList);
        }

        // POST: Ticket/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(Guid id)
        {
            try
            {
                _TicketDAL.delTicket(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
