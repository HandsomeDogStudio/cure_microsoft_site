using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectCure.Web.Code;
using ProjectCureData;

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

        public JsonResult Index(DateTime startDate, DateTime endDate)
        {
            var results = new List<object>();
            foreach (var e in Repository.GetEventsBetweenDates(startDate, endDate))
            {
                results.Add(new
                {
                    id = e.EventId,
                    url = Url.RouteUrl(new { id = e.EventId }),
                    title = e.EventTitle,
                    start = e.EventStartDateTime.ToString("O"),
                    end = e.EventEndDateTime.ToString("O"),
                    className = e.User == null ? "available" : "assigned" //TODO: owner
                });
            }

            return Json(results, JsonRequestBehavior.AllowGet);
        }

//        public PartialViewResult Index(int id)
//        {
//            return PartialView("", Repository.GetEventById(id));
//        }

//        [HttpPost]
//        public JsonResult Index(EventDTO input)
//        {
//            return Json(new EventWithIdDTO("4", new EventDTO(DateTime.Now, DateTime.Now, "HCA Hackathon 1", "A fun event for nerds", "Unassigned", null)));
//        }
//
//        [HttpPut]
//        public void Index(string id, EventDTO input)
//        {
//        }
//
//        [HttpDelete]
//        public void Index(string id)
//        {
//        }
    }
}
