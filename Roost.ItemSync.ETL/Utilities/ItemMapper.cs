using Roost.ItemSync.ETL.Models.Target;

namespace Roost.ItemSync.ETL.Utilities
{
    public static class ItemMapper
    {
        public static Item MapToTarget(Models.Source.Item source)
        {
            var item = new Item()
            {
                OriginalItemId = source.ItemId,
                Upc = source.Upc,
                ItemName = source.ItemName,
                ItemDescription = source.ItemDescription,
                UnitQuantity = source.UnitQuantity,
                Category = source.Category.CategoryName,
                CategoryDescription = source.Category.CategoryDescription,
                ParentCategory = source.Category.ParentCategory?.CategoryName,
                ParentCategoryDescription = source.Category.ParentCategory?.CategoryDescription,
                ItemAttributes = new ItemAttributes()
                {
                    Price = source.RetailPrice
                },
                UnitOfMeasure = new UnitOfMeasure()
                {
                    Description = source.UnitOfMeasure?.UnitOfMeasureDescription,
                    Name = source.UnitOfMeasure?.UnitOfMeasureName,
                }
            };

            if (source.Attributes != null && source.Attributes.Any())
            {
                foreach (var attribute in source.Attributes)
                {
                    if (attribute.AttributeName == AttributeKeys.Calories && int.TryParse(attribute.AttributeValue.AttributeValue.ToString(), out int calories))
                    {
                        item.ItemAttributes.Calories = calories;
                        continue;
                    }

                    if (attribute.AttributeName == AttributeKeys.CupSize)
                    {
                        item.ItemAttributes.CupSize = attribute.AttributeValue?.AttributeValue?.ToString().Trim() ?? string.Empty;
                        continue;
                    }
                }
            }

            if (source.Images != null && source.Images.Any())
            {
                item.ImageUrl = source.Images[0].AbsoluteUri;
            }

            return item;
        }
    }
}
