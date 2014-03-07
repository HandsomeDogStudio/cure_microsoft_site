using System;
using System.Collections.Generic;

namespace ProjectCureData.Models
{
    public partial class Template
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateText { get; set; }
        public string TemplateSubject { get; set; }
    }
}
