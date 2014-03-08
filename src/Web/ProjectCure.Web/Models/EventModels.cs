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

        public bool IsMine { get; private set; }

        public bool IsAssigned { get; private set; }

        public EventDetailsModel(int id, string title, string description, string startDateTime, string endDateTime, string manager, bool isMine, bool isAssigned)
        {
            Id = id;
            Title = title;
            Description = description;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            Manager = manager;
            IsMine = isMine;
            IsAssigned = isAssigned;
        }
    }

    public class EditEventModel
    {
        public string Title { get; set; }

        public string StartDateTime { get; set; }

        public string EndDateTime { get; set; }

        public string Description { get; set; }

        public EventEditAction Action { get; set; }

        public int ManagerId { get; set; }
    }

    public enum EventEditAction
    {
        Unknown, Assign, Unassign, Edit, Delete
    }
}