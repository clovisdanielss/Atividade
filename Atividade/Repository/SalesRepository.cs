using Atividade.Models;
using Atividade.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Atividade.Repository
{
    public class SalesRepository : Repository<Sale>
    {
        private static List<Sale> salesDatabase = new List<Sale>();
        public SalesRepository()
        {

        }

        public override Sale? Create(Sale item)
        {
            if (item == null || item?.Salesman == null ||
                item?.Cart == null || item?.Cart?.Items.Count < 1)
            {
                return null;
            }
            item!.Id = new Random().Next(10000).ToString();
            salesDatabase.Add(item);
            return item;
        }

        public override Sale? DeleteById(string id)
        {
            var sale = salesDatabase.FirstOrDefault(sale => sale.Id == id);
            if (sale != null)
            {
                salesDatabase.Remove(sale);
            }
            else
            {
                return null;
            }
            return sale;
        }

        public override List<Sale> GetAll()
        {
            return salesDatabase;
        }

        public override Sale? GetById(string id)
        {
            var sale = salesDatabase.FirstOrDefault(sale => sale.Id == id);
            if (sale == null)
                return null;
            return sale;
        }

        public override bool Update(Sale newItem)
        {
            var index = salesDatabase.FindIndex(item => item.Id == newItem.Id);
            if (index < 0)
                return false;
            salesDatabase[index] = newItem;
            return true;
        }


    }
}