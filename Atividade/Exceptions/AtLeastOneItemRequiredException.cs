namespace Atividade.Exceptions
{
    public class AtLeastOneItemRequiredException: Exception
    {
        public AtLeastOneItemRequiredException(): base("At least one item in cart required")
        {

        }
    }
}
