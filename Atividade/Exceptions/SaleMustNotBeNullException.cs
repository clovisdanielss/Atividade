namespace Atividade.Exceptions
{
    public class SaleMustNotBeNullException: NullReferenceException
    {
        public SaleMustNotBeNullException():base(("Sale must not be null"))
        {

        }
    }
}
