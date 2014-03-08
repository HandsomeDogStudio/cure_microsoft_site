using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectCure.Web.Code;
using ProjectCure.Web.Models;
using ProjectCureData;
using ProjectCureData.Models;

namespace ProjectCure.Web.Controllers
{
    public class EventsController : ProjectCureControllerBase
    {
        public EventsController(IRepository repository)
            : base(repository)
        {
        }

        //
        // GET: /Calendar/

        public JsonResult List(DateTime startDate, DateTime endDate)
        {
            var results = new List<object>();
            foreach (var e in Repository.GetEventsBetweenDates(startDate, endDate))
            {
                results.Add(new
                {
                    id = e.EventId,
                    url = Url.RouteUrl("CalendarEvent", new { id = e.EventId }),
                    title = e.EventTitle,
                    start = e.EventStartDateTime.ToString("O"),
                    end = e.EventEndDateTime.ToString("O"),
                    className = e.User == null ? "available" : "assigned" //TODO: owner
                });
            }

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Item(int id)
        {
            Event @event = Repository.GetEventById(id);
            string managerName = string.Empty;
            if (@event.User != null)
            {
                managerName = @event.User.UserFirstName + " " + @event.User.UserLastName;
            }
            return PartialView("Details", new EventDetailsModel(@event.EventId, @event.EventTitle, @event.EventDescription, @event.EventStartDateTime.ToString("g"), @event.EventEndDateTime.ToString("g"), managerName));
        }

//        [HttpPost]
//        public JsonResult Index(EventDTO input)
//        {
//            return Json(new EventWithIdDTO("4", new EventDTO(DateTime.Now, DateTime.Now, "HCA Hackathon 1", "A fun event for nerds", "Unassigned", null)));
//        }
//
//        [HttpPut]
//        public void Item(string id, EventDTO input)
//        {
//        }
//
        [HttpDelete]
        public void Delete(int id)
        {
            Repository.DeleteEventById(id);
        }
    }
}
