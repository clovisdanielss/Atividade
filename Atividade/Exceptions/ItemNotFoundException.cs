namespace Atividade.Exceptions
{
    public class ItemNotFoundException: Exception
    {
        public ItemNotFoundException(): base("The item searched was not found")
        {
        }
    }
}
