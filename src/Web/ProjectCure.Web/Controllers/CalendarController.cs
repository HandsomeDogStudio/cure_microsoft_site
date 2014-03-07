using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectCure.Web.Controllers
{
    public class CalendarController : Controller
    {
        //
        // GET: /Calendar/

        public JsonResult Index(DateTime startDate, DateTime endDate)
        {
            return Json(new
            {
                Events = new []
                {
                    new { Id = "1", Data = new EventDTO(startDate.AddMilliseconds(3), DateTime.Now, "HCA Hackathon 1", "A fun event for nerds", "Unassigned", null) },
                    new { Id = "2", Data = new EventDTO(DateTime.Now, DateTime.Now, "HCA Hackathon 2", "A fun event for nerds", "Assigned", new PersonDTO("123", "John Smith")) }
                }
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Index(EventDTO input)
        {
            return Json(new
            {
                Id = "4",
                Data = new EventDTO(DateTime.Now, DateTime.Now, "HCA Hackathon 1", "A fun event for nerds", "Unassigned", null)
            });
        }

        [HttpPut]
        public void Index(string id, EventDTO input)
        {

        }

        [HttpDelete]
        public void Index(string id)
        {
        }
    }

    public class EventDTO
    {
        public string StartDateTime { get; private set; }
        public string EndDateTime { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Status { get; private set; }
        public PersonDTO Manager { get; private set; }

        public EventDTO(DateTime startDateTime, DateTime endDateTime, string title, string description, string status, PersonDTO manager)
        {
            StartDateTime = startDateTime.ToString("O");
            EndDateTime = endDateTime.ToString("O");
            Title = title;
            Description = description;
            Status = status;
            Manager = manager;
        }
    }

    public class PersonDTO
    {
        public string Id { get; private set; }
        public string Name { get; private set; }

        public PersonDTO(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
