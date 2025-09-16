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
            Assert.IsNull(target.ItemAttributes.Price);
            Assert.IsNull(target.ItemAttributes.Calories);
            Assert.IsNull(target.ItemAttributes.CupSize);
            Assert.IsNull(target.ImageUrl);
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
                        AttributeName = "Calories",
                        AttributeValue = new ItemAttributeValue()
                        {
                            AttributeValue = "42"
                        }
                    },
                    new ItemAttribute()
                    {
                        AttributeName = "Price",
                        AttributeValue = new ItemAttributeValue()
                        {
                            AttributeValue = "3.99"
                        }
                    },
                    new ItemAttribute()
                    {
                        AttributeName = "Cup Size",
                        AttributeValue = new ItemAttributeValue()
                        {
                            AttributeValue = "Regular"
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
            Assert.AreEqual(3.99m, target.ItemAttributes.Price);
            Assert.AreEqual(42, target.ItemAttributes.Calories);
            Assert.AreEqual("Regular", target.ItemAttributes.CupSize);
            Assert.AreEqual("some-web-address", target.ImageUrl);

        }
    }
}
