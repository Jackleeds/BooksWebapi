using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BooksWebapi.Models
{
    public class Books
    {
        public string ID { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
      
        public DateTime Publication_date { get; set; }
        public string Remark { get; set; }
    }
}