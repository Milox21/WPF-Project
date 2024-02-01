using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmodrom.Models.Add
{
    public class ComboBoxVM
    {
        public ComboBoxVM(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public ComboBoxVM() { }
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
