using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.Data;
using TicketManagement.Helpers;
using TicketManagement.Models;

namespace TicketManagement.Controllers
{
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public TicketController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            if (!_context.Tickets.Any())
            {
                _context.Tickets.Add(new Ticket
                { 
                    Id = Guid.NewGuid(),
                    Title = "Event Management", 
                    Description = "Some Test description",
                    Priority = PriorityEnum.High,
                    TicketStatus = TicketStatusEnum.Done
                });
                _context.SaveChanges();
            }
        }

        [Authorize]
        public async Task<IActionResult> ListTickets()
        {
            ViewBag.UserObj = await UserHelper.getUser(HttpContext, _userManager);
            var  result = _context.Tickets.ToList();
            return View(result);
        }

        [Authorize]
        public async Task<IActionResult> CreateTicket()
        {
            ViewBag.UserObj = await UserHelper.getUser(HttpContext, _userManager);
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateTicket(Ticket model)
        {
            model.Id= Guid.NewGuid();
            model.TicketStatus = TicketStatusEnum.New;
        
            _context.Tickets.Add(model);
            _context.SaveChanges();

            return RedirectToAction("ListTickets");
        }

        [HttpPost]
        [Authorize]
        public IActionResult SetTicketStatus(Guid Id, TicketStatusEnum ticketStatus) 
        {
            var ticket = _context.Tickets.Find(Id);
            ticket.TicketStatus = ticketStatus;
            
            _context.Tickets.Update(ticket);
            _context.SaveChanges();

            return Ok();
        }
    }
}
