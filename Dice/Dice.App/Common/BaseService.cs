using Dice.App;
using Dice.App.Abstract;
using Dice.Domain;
using Dice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice.App.Common
{
    public class Program<T> : IService<T> where T : BaseEntity
    {
        public List<T> Items { get ; set ; }

        public Program() 
        {
            Items = new List<T>();        
        }

        public int AddItem(T item)
        {
            Items.Add(item);
            return item.Id;
        }

        public List<T> GetAllItems()
        {
            return Items;
        }

        public void RemoveItem(T item)
        {
            Items.Remove(item);
        }

        public int UpdateItem(T item)
        {
            var entity = Items.FirstOrDefault(p => p.Id == item.Id);
            if (entity != null)
            {
                entity = item;
            }
            return entity.Id;
        }
    }
}
