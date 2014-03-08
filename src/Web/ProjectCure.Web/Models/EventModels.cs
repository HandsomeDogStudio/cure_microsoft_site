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
}