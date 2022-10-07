using Atividade.Models;

namespace Atividade.Services
{
    public class SalesService
    {
        public static Dictionary<SaleStatus, List<SaleStatus>> Transitions = new Dictionary<SaleStatus, List<SaleStatus>>{
            [SaleStatus.WAITING_PAYMENT] = new List<SaleStatus>{
                SaleStatus.CANCELLED,
                SaleStatus.PAYMENT_APPROVED,
            },
            [SaleStatus.PAYMENT_APPROVED] = new List<SaleStatus>{
                SaleStatus.CANCELLED,
                SaleStatus.SENT,
            },
            [SaleStatus.SENT] = new List<SaleStatus>{
                SaleStatus.DELIVERED
            }
        };

        public bool ChangeStatus(Sale sale, SaleStatus saleStatus)
        {
            if (sale == null) return false;
            if (Transitions.GetValueOrDefault(sale.Status)?.Contains(saleStatus)??false)
            {
                sale.Status = saleStatus;
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}