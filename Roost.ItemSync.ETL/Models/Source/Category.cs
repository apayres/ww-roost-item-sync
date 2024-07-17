namespace Roost.ItemSync.ETL.Models.Source
{
    public class Category
    {
        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        public Category ParentCategory { get; set; }

        public List<Category> SubCategories { get; set; }
    }
}
