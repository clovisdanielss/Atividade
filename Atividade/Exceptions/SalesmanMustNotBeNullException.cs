namespace Atividade.Exceptions
{
    public class SalesmanMustNotBeNullException:NullReferenceException
    {
        public SalesmanMustNotBeNullException():base("Salesman must not be null")
        {
               
        }
    }
}
