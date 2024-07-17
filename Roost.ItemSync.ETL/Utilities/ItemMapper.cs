using Roost.ItemSync.ETL.Models.Target;

namespace Roost.ItemSync.ETL.Utilities
{
    public static class ItemMapper
    {
        public static Item MapToTarget(Models.Source.Item source)
        {
            var item = new Item()
            {
                Upc = source.Upc,
                ItemName = source.ItemName,
                ItemDescription = source.ItemDescription,
                UnitQuantity = source.UnitQuantity,
                Category = source.Category.CategoryName,
                CategoryDescription = source.Category.CategoryDescription,
                ParentCategory = source.Category.ParentCategory?.CategoryName,
                ParentCategoryDescription = source.Category.ParentCategory?.CategoryDescription,
                Attributes = new List<ItemAttribute>(),
                Images = new List<ItemImage>(),
                UnitOfMeasure = new UnitOfMeasure()
                {
                    Description = source.UnitOfMeasure?.UnitOfMeasureDescription,
                    Name = source.UnitOfMeasure?.UnitOfMeasureName,
                }
            };

            if (source.Attributes != null && source.Attributes.Any())
            {
                item.Attributes = source.Attributes.Select(x => new ItemAttribute()
                {
                    Description = x.AttributeDescription,
                    Name = x.AttributeName,
                    Value = x.AttributeValue?.AttributeValue,
                    DisplayOrder = x.AttributeValue?.DisplayOrder ?? 0
                }).ToList();
            }

            if (source.Images != null && source.Images.Any())
            {
                item.Images = source.Images.Select(x => new ItemImage()
                {
                    DisplayOrder = x.DisplayOrder,
                    AbsoluteUri = x.AbsoluteUri
                }).ToList();
            }

            return item;
        }
    }
}
