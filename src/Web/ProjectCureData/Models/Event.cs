using System;
using System.Collections.Generic;

namespace ProjectCureData.Models
{
    public partial class Event
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public System.DateTime EventStartDateTime { get; set; }
        public System.DateTime EventEndDateTime { get; set; }
        public bool EventStatus { get; set; }
        public Nullable<int> EventManagerId { get; set; }
        public string EventDescription { get; set; }
        public virtual User User { get; set; }
    }
}
