using Atividade.Models;

namespace Atividade.Messages
{
    public class SalesControllerMessages
    {
        public static string ErrorWhenUpdating(SaleStatus from, SaleStatus to) => $"Error when updating status from {from} to {to}";
        public static string ItemNotFound(string id) => $"Item with id {id} was not found";
        public static string UnexpectedPayloadWhenCreatingItem => "Unexpected payload when creating item";
    }
}