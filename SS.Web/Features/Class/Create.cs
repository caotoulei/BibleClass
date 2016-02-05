using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using MediatR;
using SS.Web.Content.bible;
using SS.Web.DAL;

namespace SS.Web.Features.Class
{
    public class Create
    {
        public class Command : IRequest
        {
            public string Title { get; set; }
            public int RelatedChapter { get; set; }
            public string RelatedBook { get; set; }
            public string Summary { get; set; }
            [AllowHtml] 
            public string Content { get; set; }
            public HttpPostedFileBase Attachment { get; set; }

            public DateTime AddedOn { get; set; }
            public string AddedBy { get; set; }

            public bool IsArchived { get; set; }
            public List<SelectListItem> BookNameList
            {
                get
                {
                    IEnumerable<Books.BookNames> values = Enum.GetValues(typeof(Books.BookNames)).Cast<Books.BookNames>();
                    var items = new List<SelectListItem>();
                    foreach (var i in values)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = Enum.GetName(typeof(Books.BookNames), i),
                            Value = ((int)i).ToString()
                        });
                    }
                    return items;
                }
            } 
        }

        public class Handler :RequestHandler<Command>
        {
            //todo: datacontaxt go here
            private readonly ClassContext _db;
            public Handler(ClassContext db)
            {
                _db = db; 

            }

            protected override void HandleCore(Command message)
            {
                var c = Mapper.Map<Command, Models.Class>(message);
                c.Id = Guid.NewGuid();
                c.AddedOn = DateTime.Now;
                c.AddedBy = "user1";
                _db.Classes.Add(c);
                //_db.SaveChanges(); //note: why doesn't work in ClassContext CloseTransaction
            }
        }
    }
}