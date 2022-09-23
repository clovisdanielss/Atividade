namespace Atividade.Exceptions
{
    public class CartMustNotBeNullException: NullReferenceException
    {
        public CartMustNotBeNullException(): base("Cart must not be null")
        {

        }
    }
}
