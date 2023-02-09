using System;

namespace TicketManagement.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PriorityEnum Priority { get; set; }
        public TicketStatusEnum TicketStatus { get; set; }
    }
}
