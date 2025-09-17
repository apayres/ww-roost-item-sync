namespace Roost.ItemSync.ETL.Utilities
{
    public class IngredientComparer
    {
        public static bool DoIngredientsMatch(Models.Source.Ingredient source, Models.Target.Ingredient target)
        {
            if(source.Item.ItemName != target.ItemName)
            {
                return false;
            }

            if(source.Item.ItemDescription != target.ItemDescription) 
            { 
                return false; 
            }

            if (source.Item.Upc != target.Upc)
            {
                return false;
            }

            if (source.Ratio != target.Ratio)
            {
                return false;
            }

            if (source.Item.UnitOfMeasure.UnitOfMeasureName != target.UnitOfMeasure.Name)
            {
                return false;
            }

            if (source.Item.UnitOfMeasure.UnitOfMeasureDescription != target.UnitOfMeasure.Description)
            {
                return false;
            }

            return true;
        }
    }
}
