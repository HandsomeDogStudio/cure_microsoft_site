using System.Globalization;
using System.Web.Routing;
using System.Web.UI.WebControls.WebParts;
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

        public JsonResult List(long start, long end)
        {
            User currentUser = Repository.GetUserByUserName(HttpContext.User.Identity.Name);

            var results = new List<object>();

            var dt1970 = new DateTime(1970, 1, 1);
            DateTime startDate = dt1970.AddMilliseconds(start * 1000).Date;
            DateTime endDate = dt1970.AddMilliseconds(end * 1000).Date;

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

        public PartialViewResult Create(DateTime date)
        {
            return PartialView("Create", new CreateEventModel(date.ToString("d"), Repository.GetUserList()));
        }

        public PartialViewResult Item(int id)
        {
            Event @event = Repository.GetEventById(id);
            string managerName = string.Empty;
            string managerUser = null;
            if (@event.User != null)
            {
                managerName = @event.User.UserFirstName + " " + @event.User.UserLastName;
                managerUser = @event.User.UserEmail;
            }
            return PartialView("Details", new EventDetailsModel(@event.EventId, @event.EventTitle, @event.EventDescription, @event.EventStartDateTime.ToString("h:mm tt"), @event.EventEndDateTime.ToString("h:mm tt"), managerName, managerUser == HttpContext.User.Identity.Name, managerUser != null));
        }

        [HttpPost]
        public void List(EditEventModel input)
        {
            if (HttpContext.User.IsInRole("Admin"))
            {
                var e = new Event
                {
                    EventDescription = input.Description,
                    EventStartDateTime = DateTime.ParseExact(input.Date + " " + input.StartTime, "M/d/yyyy HH:mm", CultureInfo.InvariantCulture),
                    EventEndDateTime = DateTime.ParseExact(input.Date + " " + input.EndTime, "M/d/yyyy HH:mm", CultureInfo.InvariantCulture),
                    EventTitle = input.Title,
                    EventManagerId = input.ManagerId
                };
                Repository.SaveEvent(e);
            }
        }

        [HttpPut]
        public void Item(int id, EditEventModel input)
        {
            var notifier = new EmailNotifier();

            switch (input.Action)
            {
                case EventEditAction.Assign:
                    Repository.AssignManager(id, HttpContext.User.Identity.Name);
                    break;
                case EventEditAction.Unassign:
                    Repository.AssignManager(id, null);                    
                    notifier.LeadCancellationNotification(Repository, Repository.GetEventById(id));
                    break;
                case EventEditAction.Edit:
                    if (HttpContext.User.IsInRole("Admin"))
                    {
                        User manager = null;
                        if (input.ManagerId != null)                        
                            manager = Repository.GetUserById(input.ManagerId.Value);                        

                        Event e = Repository.GetEventById(id);

                        e.EventDescription = input.Description;
                        e.EventStartDateTime = DateTime.ParseExact(input.Date + " " + input.StartTime, "M/d/yyyy HH:mm", CultureInfo.InvariantCulture);
                        e.EventEndDateTime = DateTime.ParseExact(input.Date + " " + input.EndTime, "M/d/yyyy HH:mm", CultureInfo.InvariantCulture);
                        e.EventTitle = input.Title;
                        e.User = manager;
                        e.EventManagerId = input.ManagerId;

                        Repository.SaveEvent(e);
                    }
                    break;
                case EventEditAction.Delete:
                    if (HttpContext.User.IsInRole("Admin"))
                    {   
                        Event @event = Repository.GetEventById(id);
                        string email = (@event != null && @event.User != null) ? @event.User.UserEmail : string.Empty;
                        Repository.DeleteEventById(id);

                        if (!string.IsNullOrWhiteSpace(email))                        
                            notifier.EventCancellationNotification(Repository, @event, @event.User.UserEmail);
                        
                    }
                    break;
            }
        }
    }
}
