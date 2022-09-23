using Atividade.Exceptions;
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

        public override Sale Create(Sale item)
        {
            if (item == null) throw new SaleMustNotBeNullException();
            if (item.Salesman == null) throw new SalesmanMustNotBeNullException();
            if (item.Cart == null) throw new CartMustNotBeNullException();
            if (item.Cart.Items.Count < 1) throw new AtLeastOneItemRequiredException();
            item.Id = new Random().Next(10000).ToString();
            salesDatabase.Add(item);
            return item;
        }

        public override Sale DeleteById(string id)
        {
            var sale = salesDatabase.FirstOrDefault(sale => sale.Id == id);
            if (sale != null)
            {
                salesDatabase.Remove(sale);
            }
            else
            {
                throw new ItemNotFoundException();
            }
            return sale;
        }

        public override List<Sale> GetAll()
        {
            return salesDatabase;
        }

        public override Sale GetById(string id)
        {
            var sale = salesDatabase.FirstOrDefault(sale => sale.Id == id);
            if (sale == null)
                throw new ItemNotFoundException();
            return sale;
        }

        public override void Update(Sale newItem)
        {
            var index = salesDatabase.FindIndex(item => item.Id == newItem.Id);
            if (index < 0)
                throw new ItemNotFoundException();
            salesDatabase[index] = newItem;
        }

        public void ChangeStatus(Sale sale, SaleStatus saleStatus)
        {
            var allowedWhenWaitingPayment = sale.Status == SaleStatus.WAITING_PAYMENT && (saleStatus == SaleStatus.CANCELLED || saleStatus == SaleStatus.PAYMENT_APPROVED);
            var allowedWhenPaymentApproved = sale.Status == SaleStatus.PAYMENT_APPROVED && (saleStatus == SaleStatus.CANCELLED || saleStatus == SaleStatus.SENT);
            var allowedWhenSent = sale.Status == SaleStatus.SENT && saleStatus == SaleStatus.DELIVERED;
            if (allowedWhenWaitingPayment || allowedWhenPaymentApproved || allowedWhenSent)
            {
                sale.Status = saleStatus;
            }
            else
            {
                throw new ImpossibleToChangeStatusException(sale.Status, saleStatus);
            }
            Update(sale);
        }
    }
}