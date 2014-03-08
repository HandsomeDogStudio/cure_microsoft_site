using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCure.Web.Models
{
    public class EventDetailsModel
    {
        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public string StartDateTime { get; private set; }

        public string EndDateTime { get; private set; }

        public string Manager { get; private set; }

        public EventDetailsModel(int id, string title, string description, string startDateTime, string endDateTime, string manager)
        {
            Id = id;
            Title = title;
            Description = description;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            Manager = manager;
        }
    }

    public class EditEventModel
    {
        public string Title { get; set; }

        public string StartDateTime { get; set; }

        public string EndDateTime { get; set; }

        public string Description { get; set; }

        public string Action { get; set; }

        public int ManagerId { get; set; }
    }
}