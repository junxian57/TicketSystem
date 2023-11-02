using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace TicketySystem.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        [Required]
        public TicketPriority Priority { get; set; }
        [Display(Name = "Ticket Type")]
        [Required]
        public TicketType ticketType { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsResolved { get; set; }
        public Guid? CreatorUserId { get; set; }
        public Guid? RDUserId { get; set; }

        public enum TicketPriority
        {
            [Display(Name = "Low")]
            Low = 1,
            [Display(Name = "Medium")]
            Medium = 2,
            [Display(Name = "Severity")]
            Severity = 3,
            [Display(Name = "Priority")]
            Priority = 4
        }

        public enum TicketType
        {
            [Display(Name = "Bug")]
            Bug = 1,
            [Display(Name = "FeatureRequest")]
            FeatureRequest = 2,
            [Display(Name = "TestCase")]
            TestCase = 3
        }
    }
}