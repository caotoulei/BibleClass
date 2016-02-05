using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SS.Web.Models
{
    public class Class
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int RelatedChapter { get; set; }
        public string RelatedBook { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        //public HttpPostedFileBase Attachment { get; set; }

        public DateTime AddedOn { get; set; }
        public string AddedBy { get; set; }

        public bool IsArchived { get; set; }
    }
}