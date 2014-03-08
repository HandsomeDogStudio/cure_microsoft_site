using ProjectCure.Web.Code;
using ProjectCure.Web.Models;
using ProjectCureData;
using ProjectCureData.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

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
            User currentUser = Repository.GetUserByUserName(HttpContext.User.Identity.Name);

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
                    className = e.User == null ? "available" : (e.User.UserId == currentUser.UserId ? "owner" : "assigned")
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

        [HttpPost]
        public void List(EditEventModel input)
        {
            if (HttpContext.User.IsInRole("Admin"))
            {
                var manager = Repository.GetUserById(input.ManagerId);
                var e = new Event
                {
                    EventDescription = input.Description,
                    EventStartDateTime = DateTime.Parse(input.StartDateTime),
                    EventEndDateTime = DateTime.Parse(input.EndDateTime),
                    EventTitle = input.Title,
                    User = manager
                };
                Repository.SaveEvent(e);
            }
        }

        [HttpPut]
        public void Item(int id, EditEventModel input)
        {
            switch (input.Action)
            {
                case "assign":
                    Repository.AssignManager(id, HttpContext.User.Identity.Name);
                    break;
                case "unassign":
                    Repository.AssignManager(id, null);
                    break;
                case "edit":
                    if (HttpContext.User.IsInRole("Admin"))
                    {
                        var manager = Repository.GetUserById(input.ManagerId);
                        var e = Repository.GetEventById(id);

                        e.EventDescription = input.Description;
                        e.EventStartDateTime = DateTime.Parse(input.StartDateTime);
                        e.EventEndDateTime = DateTime.Parse(input.EndDateTime);
                        e.EventTitle = input.Title;
                        e.User = manager;

                        Repository.SaveEvent(e);
                    }
                    break;
            }
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Repository.DeleteEventById(id);
        }
    }
}
