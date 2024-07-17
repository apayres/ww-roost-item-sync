using Roost.ItemSync.ETL.Models.Source;
using Roost.ItemSync.ETL.Utilities;

namespace Roost.ItemSync.ETL.UnitTests.Utilities
{
    [TestClass]
    public class ItemMapperTests
    {
        [TestMethod]
        public void WhenMappingItemsWithNoAttributesOrImages_ShouldReturnObjectWithCorrectValues()
        {
            var source = new Item()
            {
                Category = new Category()
                {
                    CategoryName = "Unit Test Sub Category",
                    CategoryDescription = "Unit Test Sub Category Desc",
                    ParentCategory = new Category()
                    {
                        CategoryName = "Unit Test Category",
                        CategoryDescription = "Unit Test Category Desc",
                    }
                },
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new UnitOfMeasure()
                {
                    UnitOfMeasureDescription = "Unit Test UOM",
                    UnitOfMeasureName = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789"
            };

            var target = ItemMapper.MapToTarget(source);
            Assert.AreEqual(source.ItemName, target.ItemName);
            Assert.AreEqual(source.ItemDescription, target.ItemDescription);
            Assert.AreEqual(source.Upc, target.Upc);
            Assert.AreEqual(source.UnitQuantity, target.UnitQuantity);
            Assert.AreEqual(source.Category.CategoryName, target.Category);
            Assert.AreEqual(source.Category.CategoryDescription, target.CategoryDescription);
            Assert.AreEqual(source.Category.ParentCategory.CategoryName, target.ParentCategory);
            Assert.AreEqual(source.Category.ParentCategory.CategoryDescription, target.ParentCategoryDescription);
            Assert.AreEqual(source.UnitOfMeasure.UnitOfMeasureName, target.UnitOfMeasure.Name);
            Assert.AreEqual(source.UnitOfMeasure.UnitOfMeasureDescription, target.UnitOfMeasure.Description);
            Assert.IsFalse(target.Attributes.Any());
            Assert.IsFalse(target.Images.Any());
        }

        [TestMethod]
        public void WhenMappingItemsWithAttributesAndImages_ShouldReturnObjectWithCorrectValues()
        {
            var source = new Item()
            {
                Category = new Category()
                {
                    CategoryName = "Unit Test Sub Category",
                    CategoryDescription = "Unit Test Sub Category Desc",
                    ParentCategory = new Category()
                    {
                        CategoryName = "Unit Test Category",
                        CategoryDescription = "Unit Test Category Desc",
                    }
                },
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new UnitOfMeasure()
                {
                    UnitOfMeasureDescription = "Unit Test UOM",
                    UnitOfMeasureName = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789",
                Attributes = new List<ItemAttribute>
                {
                    new ItemAttribute()
                    {
                        AttributeDescription = "UT Attribute Desc",
                        AttributeName = "UT ATTR",
                        AttributeValue = new ItemAttributeValue()
                        {
                            AttributeValue = "FourtyTwo",
                            DisplayOrder = 9
                        }
                    }
                },
                Images = new List<ItemImage>
                {
                    new ItemImage()
                    {
                        AbsoluteUri = "some-web-address",
                        DisplayOrder = 1
                    }
                }
            };

            var target = ItemMapper.MapToTarget(source);
            Assert.AreEqual(1, target.Attributes.Count);
            Assert.AreEqual(source.Attributes[0].AttributeName, target.Attributes[0].Name);
            Assert.AreEqual(source.Attributes[0].AttributeDescription, target.Attributes[0].Description);
            Assert.AreEqual(source.Attributes[0].AttributeValue.AttributeValue, target.Attributes[0].Value);
            Assert.AreEqual(source.Attributes[0].AttributeValue.DisplayOrder, target.Attributes[0].DisplayOrder);
            Assert.AreEqual(1, target.Images.Count);
            Assert.AreEqual(source.Images[0].AbsoluteUri, target.Images[0].AbsoluteUri);
            Assert.AreEqual(source.Images[0].DisplayOrder, target.Images[0].DisplayOrder);
        }
    }
}
