using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Model
{
    public class Item
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public List<string> Tags { get; set; }
    }
}
