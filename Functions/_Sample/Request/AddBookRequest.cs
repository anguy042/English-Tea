using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Sample.Function.Request
{
    public class AddBookRequest
    {
        public string Isbn { get; set; }
        public string Name { get; set; }
    }
}
