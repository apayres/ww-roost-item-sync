namespace Roost.ItemSync.ETL.Models.Target
{
    public class ItemAttribute
    {
        public string Name { get; set; }

        public string Description { set; get; }

        public object Value { get; set; }

        public int DisplayOrder { get; set; }

    }
}
