namespace Atividade.Models
{
    public class Cart
    {
        public List<Item> Items { get; set; }
        public double Total
        {
            get
            {
                return Items?.Count > 0 ? Items.Sum(item => item.Price) : 0;
            }
        }
    }
}
