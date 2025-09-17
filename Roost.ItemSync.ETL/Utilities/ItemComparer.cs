using Roost.ItemSync.ETL.Models.Target;

namespace Roost.ItemSync.ETL.Utilities
{
    public static class ItemComparer
    {
        public static bool DoItemsMatch(Models.Source.Item source, Models.Target.Item target)
        {
            if (source.ItemId != target.OriginalItemId)
            {
                return false;                
            }

            if (source.ItemName != target.ItemName)
            {
                return false;
            }

            if (source.ItemDescription != target.ItemDescription)
            {
                return false;
            }

            if (source.Category.CategoryName != target.Category)
            {
                return false;
            }

            if (source.Category.CategoryDescription != target.CategoryDescription)
            {
                return false;
            }

            if (source.Category.ParentCategory?.CategoryName != target.ParentCategory)
            {
                return false;
            }

            if (source.Category.ParentCategory?.CategoryDescription != target.ParentCategoryDescription)
            {
                return false;
            }

            if (source.RetailPrice != target.ItemAttributes?.Price)
            {
                return false;
            }

            if (!DoAttributesMatch(source, target))
            {
                return false;
            }

            if (!DoImagesMatch(source, target))
            {
                return false;
            }

            if (!DoUnitsOfMeasureMatch(source, target))
            {
                return false;
            }

            return true;
        }

        private static bool DoAttributesMatch(Models.Source.Item source, Models.Target.Item target)
        {
            if (source.Attributes != null && source.Attributes.Any())
            {
                if (target.ItemAttributes == null || target.ItemAttributes.Price == null || target.ItemAttributes.Calories == null || string.IsNullOrEmpty(target.ItemAttributes.CupSize))
                {
                    return false;
                }

                foreach (var attribute in source.Attributes)
                {
                    if (attribute.AttributeName == AttributeKeys.Calories)
                    {
                        if (int.TryParse(attribute.AttributeValue.ToString(), out int calories))
                        {
                            if (calories != target.ItemAttributes.Calories)
                            {
                                return false;
                            }
                        }

                        continue;
                    }

                    if (attribute.AttributeName == AttributeKeys.CupSize)
                    {
                        if (!string.IsNullOrWhiteSpace(attribute.AttributeValue?.ToString()))
                        {
                            var cupSize = attribute.AttributeValue.ToString().Trim();
                            if (cupSize != target.ItemAttributes.CupSize)
                            {
                                return false;
                            }
                        }

                        continue;
                    }
                }

                if (!source.Attributes.Any(x => x.AttributeName == AttributeKeys.Calories))
                {
                    return false;
                }

                if (!source.Attributes.Any(x => x.AttributeName == AttributeKeys.CupSize))
                {
                    return false;
                }
            }
            else if (target.ItemAttributes != null && target.ItemAttributes.Calories != null && !string.IsNullOrEmpty(target.ItemAttributes.CupSize))
            {
                return false;
            }

            return true;
        }

        private static bool DoImagesMatch(Models.Source.Item source, Models.Target.Item target)
        {
            if (source.Images != null && source.Images.Any())
            {
                if (string.IsNullOrEmpty(target.ImageUrl))
                {
                    return false;
                }

                if (source.Images[0].AbsoluteUri != target.ImageUrl)
                {
                    return false;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(target.ImageUrl))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool DoUnitsOfMeasureMatch(Models.Source.Item source, Models.Target.Item target)
        {
            return source.UnitOfMeasure?.UnitOfMeasureName == target.UnitOfMeasure?.Name && source.UnitOfMeasure?.UnitOfMeasureDescription == target.UnitOfMeasure?.Description;
        }
    }
}
