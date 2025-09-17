using Roost.ItemSync.ETL.Models.Target;

namespace Roost.ItemSync.ETL.Utilities
{
    public class IngredientMapper
    {
        public static Ingredient MapToTarget(Models.Source.Ingredient source)
        {
            return new Ingredient()
            {
                ItemDescription = source.Item.ItemDescription,
                ItemName = source.Item.ItemName,
                Ratio = source.Ratio,
                UnitOfMeasure = new UnitOfMeasure()
                {
                    Description = source.Item.UnitOfMeasure.UnitOfMeasureDescription,
                    Name = source.Item.UnitOfMeasure.UnitOfMeasureName
                },
                Upc = source.Item.Upc,
            };
        }
    }
}
