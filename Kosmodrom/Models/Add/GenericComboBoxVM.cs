using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmodrom.Models.Add
{
    public class GenericComboBoxVM<T>
    {
        public int Id { set; get; }
        public T Item { set; get; }

        public GenericComboBoxVM(int id, T item)
        {
            Id = id;
            Item = item;
        }

        public GenericComboBoxVM()
        { }
    }
}
