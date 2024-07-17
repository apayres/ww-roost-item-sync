namespace Roost.ItemSync.ETL.Utilities
{
    public static class ItemComparer
    {
        public static bool DoItemsMatch(Models.Source.Item source, Models.Target.Item target)
        {
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
                if (target.Attributes == null || !target.Attributes.Any())
                {
                    return false;
                }

                foreach (var attribute in source.Attributes)
                {
                    var matchingAttribute = target.Attributes.FirstOrDefault(x => x.Name == attribute.AttributeName);
                    if (matchingAttribute == null)
                    {
                        return false;
                    }

                    if (matchingAttribute.Name != attribute.AttributeName
                        || matchingAttribute.Description != attribute.AttributeDescription
                        || matchingAttribute.Value?.ToString() != attribute.AttributeValue?.AttributeValue
                        || matchingAttribute.DisplayOrder != attribute.AttributeValue?.DisplayOrder)
                    {
                        return false;
                    }
                }

                var deletedAttributes = target.Attributes.Where(x => source.Attributes.Any(y => y.AttributeName == x.Name));
                if (deletedAttributes.Any())
                {
                    return false;
                }
            }
            else if (target.Attributes != null && target.Attributes.Any())
            {
                return false;
            }

            return true;
        }

        private static bool DoImagesMatch(Models.Source.Item source, Models.Target.Item target)
        {
            if (source.Images != null && source.Images.Any())
            {
                if (target.Images == null || !target.Images.Any())
                {
                    return false;
                }

                foreach (var img in source.Images)
                {
                    var matchingImage = target.Images.FirstOrDefault(x => x.AbsoluteUri == img.AbsoluteUri);
                    if (matchingImage == null)
                    {
                        return false;
                    }

                    if (matchingImage.DisplayOrder != img.DisplayOrder)
                    {
                        return false;
                    }
                }

                var deletedImages = target.Images.Where(x => source.Images.Any(y => y.AbsoluteUri == x.AbsoluteUri));
                if (deletedImages.Any())
                {
                    return false;
                }
            }
            else if (target.Images != null && target.Images.Any())
            {
                return false;
            }

            return true;
        }

        private static bool DoUnitsOfMeasureMatch(Models.Source.Item source, Models.Target.Item target)
        {
            return source.UnitOfMeasure?.UnitOfMeasureName == target.UnitOfMeasure?.Name && source.UnitOfMeasure?.UnitOfMeasureDescription == target.UnitOfMeasure?.Description;
        }
    }
}
