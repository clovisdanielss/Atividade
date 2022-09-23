using Atividade.Models;

namespace Atividade.Exceptions
{
    public class ImpossibleToChangeStatusException: Exception
    {
        public ImpossibleToChangeStatusException(SaleStatus from, SaleStatus to): base($"Impossible to update status from {from} to {to}")
        {

        }
    }
}
