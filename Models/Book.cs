using System;
using System.Collections.Generic;

#nullable disable

namespace ReadingRoomStore.Models
{
    public partial class Book
    {
        public int BooksId { get; set; }
        public string BooksName { get; set; }
        public string BooksAouthers { get; set; }
        public string BooksPublishers { get; set; }
        public string BooKsMaterial { get; set; }
    }
}
