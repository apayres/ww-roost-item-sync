namespace Roost.ItemSync.ETL.Models.Source
{
    public class Ingredient
    {
        public int? IngredientID { get; set; }

        public int ItemID { get; set; }

        public Item? Item { set; get; }

        public double Ratio { get; set; }
    }
}
