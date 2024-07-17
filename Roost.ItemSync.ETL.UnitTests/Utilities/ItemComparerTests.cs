using Roost.ItemSync.ETL.Utilities;

namespace Roost.ItemSync.ETL.UnitTests.Utilities
{
    [TestClass]
    public class ItemComparerTests
    {
        [TestMethod]
        public void WhenItemsMatch_ShouldReturnTrue()
        {
            var source = new Models.Source.Item()
            {
                Category = new Models.Source.Category()
                {
                    CategoryName = "Unit Test Sub Category",
                    CategoryDescription = "Unit Test Sub Category Desc",
                    ParentCategory = new Models.Source.Category()
                    {
                        CategoryName = "Unit Test Category",
                        CategoryDescription = "Unit Test Category Desc",
                    }
                },
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Source.UnitOfMeasure()
                {
                    UnitOfMeasureDescription = "Unit Test UOM",
                    UnitOfMeasureName = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789"
            };

            var target = new Models.Target.Item()
            {
                Category = "Unit Test Sub Category",
                CategoryDescription = "Unit Test Sub Category Desc",
                ParentCategory = "Unit Test Category",
                ParentCategoryDescription = "Unit Test Category Desc",
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Target.UnitOfMeasure()
                {
                    Description = "Unit Test UOM",
                    Name = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789"
            };

            Assert.IsTrue(ItemComparer.DoItemsMatch(source, target));
        }

        [TestMethod]
        public void WhenItemsHaveDifferences_ShouldReturnFalse()
        {
            var source = new Models.Source.Item()
            {
                Category = new Models.Source.Category()
                {
                    CategoryName = "CHANGED",
                    CategoryDescription = "Unit Test Sub Category Desc",
                    ParentCategory = new Models.Source.Category()
                    {
                        CategoryName = "Unit Test Category",
                        CategoryDescription = "Unit Test Category Desc",
                    }
                },
                ItemDescription = "Unit Test Item Desc",
                ItemName = "CHANGED",
                UnitOfMeasure = new Models.Source.UnitOfMeasure()
                {
                    UnitOfMeasureDescription = "Unit Test UOM",
                    UnitOfMeasureName = "Pack"
                },
                UnitQuantity = -1,
                Upc = "6345789"
            };

            var target = new Models.Target.Item()
            {
                Category = "Unit Test Sub Category",
                CategoryDescription = "Unit Test Sub Category Desc",
                ParentCategory = "Unit Test Category",
                ParentCategoryDescription = "Unit Test Category Desc",
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Target.UnitOfMeasure()
                {
                    Description = "Unit Test UOM",
                    Name = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789"
            };

            Assert.IsFalse(ItemComparer.DoItemsMatch(source, target));
        }

        [TestMethod]
        public void WhenAttributeHasBeenAdded_ShouldReturnFalse()
        {
            var source = new Models.Source.Item()
            {
                Category = new Models.Source.Category()
                {
                    CategoryName = "Unit Test Sub Category",
                    CategoryDescription = "Unit Test Sub Category Desc",
                    ParentCategory = new Models.Source.Category()
                    {
                        CategoryName = "Unit Test Category",
                        CategoryDescription = "Unit Test Category Desc",
                    }
                },
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Source.UnitOfMeasure()
                {
                    UnitOfMeasureDescription = "Unit Test UOM",
                    UnitOfMeasureName = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789",
                Attributes = new List<Models.Source.ItemAttribute>
                {
                    new Models.Source.ItemAttribute()
                    {
                        AttributeDescription = "UT Attr DESC",
                        AttributeName = "UT Attr",
                        AttributeValue = new Models.Source.ItemAttributeValue()
                        {
                            AttributeValue = "45",
                            DisplayOrder = 1
                        }
                    }
                }

            };

            var target = new Models.Target.Item()
            {
                Category = "Unit Test Sub Category",
                CategoryDescription = "Unit Test Sub Category Desc",
                ParentCategory = "Unit Test Category",
                ParentCategoryDescription = "Unit Test Category Desc",
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Target.UnitOfMeasure()
                {
                    Description = "Unit Test UOM",
                    Name = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789"
            };

            Assert.IsFalse(ItemComparer.DoItemsMatch(source, target));
        }

        [TestMethod]
        public void WhenAttributeHasBeenChanged_ShouldReturnFalse()
        {
            var source = new Models.Source.Item()
            {
                Category = new Models.Source.Category()
                {
                    CategoryName = "Unit Test Sub Category",
                    CategoryDescription = "Unit Test Sub Category Desc",
                    ParentCategory = new Models.Source.Category()
                    {
                        CategoryName = "Unit Test Category",
                        CategoryDescription = "Unit Test Category Desc",
                    }
                },
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Source.UnitOfMeasure()
                {
                    UnitOfMeasureDescription = "Unit Test UOM",
                    UnitOfMeasureName = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789",
                Attributes = new List<Models.Source.ItemAttribute>
                {
                    new Models.Source.ItemAttribute()
                    {
                        AttributeDescription = "UT Attr DESC",
                        AttributeName = "UT Attr",
                        AttributeValue = new Models.Source.ItemAttributeValue()
                        {
                            AttributeValue = "45",
                            DisplayOrder = 1
                        }
                    }
                }

            };

            var target = new Models.Target.Item()
            {
                Category = "Unit Test Sub Category",
                CategoryDescription = "Unit Test Sub Category Desc",
                ParentCategory = "Unit Test Category",
                ParentCategoryDescription = "Unit Test Category Desc",
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Target.UnitOfMeasure()
                {
                    Description = "Unit Test UOM",
                    Name = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789",
                Attributes = new List<Models.Target.ItemAttribute>
                {
                    new Models.Target.ItemAttribute()
                    {
                        DisplayOrder = source.Attributes[0].AttributeValue.DisplayOrder,
                        Description = source.Attributes[0].AttributeDescription,
                        Name = source.Attributes[0].AttributeName,
                        Value = "VALUE CHANGED"
                    }
                }
            };

            Assert.IsFalse(ItemComparer.DoItemsMatch(source, target));
        }

        [TestMethod]
        public void WhenAttributeHasBeenRemoved_ShouldReturnFalse()
        {
            var source = new Models.Source.Item()
            {
                Category = new Models.Source.Category()
                {
                    CategoryName = "Unit Test Sub Category",
                    CategoryDescription = "Unit Test Sub Category Desc",
                    ParentCategory = new Models.Source.Category()
                    {
                        CategoryName = "Unit Test Category",
                        CategoryDescription = "Unit Test Category Desc",
                    }
                },
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Source.UnitOfMeasure()
                {
                    UnitOfMeasureDescription = "Unit Test UOM",
                    UnitOfMeasureName = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789",
                Attributes = new List<Models.Source.ItemAttribute>
                {
                    new Models.Source.ItemAttribute()
                    {
                        AttributeDescription = "UT Attr DESC",
                        AttributeName = "UT Attr",
                        AttributeValue = new Models.Source.ItemAttributeValue()
                        {
                            AttributeValue = "45",
                            DisplayOrder = 1
                        }
                    }
                }

            };

            var target = new Models.Target.Item()
            {
                Category = "Unit Test Sub Category",
                CategoryDescription = "Unit Test Sub Category Desc",
                ParentCategory = "Unit Test Category",
                ParentCategoryDescription = "Unit Test Category Desc",
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Target.UnitOfMeasure()
                {
                    Description = "Unit Test UOM",
                    Name = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789",
                Attributes = new List<Models.Target.ItemAttribute>
                {
                    new Models.Target.ItemAttribute()
                    {
                        DisplayOrder = 1,
                        Description = "UT ATTR TO REMOVE",
                        Name = "UT ATTR",
                        Value = "VALUE CHANGED"
                    }
                }
            };

            Assert.IsFalse(ItemComparer.DoItemsMatch(source, target));
        }

        [TestMethod]
        public void WhenAllAttributesHasBeenRemoved_ShouldReturnFalse()
        {
            var source = new Models.Source.Item()
            {
                Category = new Models.Source.Category()
                {
                    CategoryName = "Unit Test Sub Category",
                    CategoryDescription = "Unit Test Sub Category Desc",
                    ParentCategory = new Models.Source.Category()
                    {
                        CategoryName = "Unit Test Category",
                        CategoryDescription = "Unit Test Category Desc",
                    }
                },
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Source.UnitOfMeasure()
                {
                    UnitOfMeasureDescription = "Unit Test UOM",
                    UnitOfMeasureName = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789"

            };

            var target = new Models.Target.Item()
            {
                Category = "Unit Test Sub Category",
                CategoryDescription = "Unit Test Sub Category Desc",
                ParentCategory = "Unit Test Category",
                ParentCategoryDescription = "Unit Test Category Desc",
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Target.UnitOfMeasure()
                {
                    Description = "Unit Test UOM",
                    Name = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789",
                Attributes = new List<Models.Target.ItemAttribute>
                {
                    new Models.Target.ItemAttribute()
                    {
                        DisplayOrder = 1,
                        Description = "UT ATTR TO REMOVE",
                        Name = "UT ATTR",
                        Value = "VALUE CHANGED"
                    }
                }
            };

            Assert.IsFalse(ItemComparer.DoItemsMatch(source, target));
        }

        [TestMethod]
        public void WhenImageHasBeenAdded_ShouldReturnFalse()
        {
            var source = new Models.Source.Item()
            {
                Category = new Models.Source.Category()
                {
                    CategoryName = "Unit Test Sub Category",
                    CategoryDescription = "Unit Test Sub Category Desc",
                    ParentCategory = new Models.Source.Category()
                    {
                        CategoryName = "Unit Test Category",
                        CategoryDescription = "Unit Test Category Desc",
                    }
                },
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Source.UnitOfMeasure()
                {
                    UnitOfMeasureDescription = "Unit Test UOM",
                    UnitOfMeasureName = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789",
                Images = new List<Models.Source.ItemImage>
                {
                    new Models.Source.ItemImage()
                    {
                        AbsoluteUri = "SOME URI",
                        DisplayOrder = 0
                    }
                }

            };

            var target = new Models.Target.Item()
            {
                Category = "Unit Test Sub Category",
                CategoryDescription = "Unit Test Sub Category Desc",
                ParentCategory = "Unit Test Category",
                ParentCategoryDescription = "Unit Test Category Desc",
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Target.UnitOfMeasure()
                {
                    Description = "Unit Test UOM",
                    Name = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789"
            };

            Assert.IsFalse(ItemComparer.DoItemsMatch(source, target));
        }

        [TestMethod]
        public void WhenImageHasBeenUpdated_ShouldReturnFalse()
        {
            var source = new Models.Source.Item()
            {
                Category = new Models.Source.Category()
                {
                    CategoryName = "Unit Test Sub Category",
                    CategoryDescription = "Unit Test Sub Category Desc",
                    ParentCategory = new Models.Source.Category()
                    {
                        CategoryName = "Unit Test Category",
                        CategoryDescription = "Unit Test Category Desc",
                    }
                },
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Source.UnitOfMeasure()
                {
                    UnitOfMeasureDescription = "Unit Test UOM",
                    UnitOfMeasureName = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789",
                Images = new List<Models.Source.ItemImage>
                {
                    new Models.Source.ItemImage()
                    {
                        AbsoluteUri = "SOME URI",
                        DisplayOrder = 0
                    }
                }

            };

            var target = new Models.Target.Item()
            {
                Category = "Unit Test Sub Category",
                CategoryDescription = "Unit Test Sub Category Desc",
                ParentCategory = "Unit Test Category",
                ParentCategoryDescription = "Unit Test Category Desc",
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Target.UnitOfMeasure()
                {
                    Description = "Unit Test UOM",
                    Name = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789",
                Images = new List<Models.Target.ItemImage>
                {
                    new Models.Target.ItemImage()
                    {
                         AbsoluteUri = "SOME URI",
                         DisplayOrder = 2
                    }
                }
            };

            Assert.IsFalse(ItemComparer.DoItemsMatch(source, target));
        }

        [TestMethod]
        public void WhenImageHasBeenRemoved_ShouldReturnFalse()
        {
            var source = new Models.Source.Item()
            {
                Category = new Models.Source.Category()
                {
                    CategoryName = "Unit Test Sub Category",
                    CategoryDescription = "Unit Test Sub Category Desc",
                    ParentCategory = new Models.Source.Category()
                    {
                        CategoryName = "Unit Test Category",
                        CategoryDescription = "Unit Test Category Desc",
                    }
                },
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Source.UnitOfMeasure()
                {
                    UnitOfMeasureDescription = "Unit Test UOM",
                    UnitOfMeasureName = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789",
                Images = new List<Models.Source.ItemImage>
                {
                    new Models.Source.ItemImage()
                    {
                        AbsoluteUri = "SOME URI",
                        DisplayOrder = 0
                    }
                }

            };

            var target = new Models.Target.Item()
            {
                Category = "Unit Test Sub Category",
                CategoryDescription = "Unit Test Sub Category Desc",
                ParentCategory = "Unit Test Category",
                ParentCategoryDescription = "Unit Test Category Desc",
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Target.UnitOfMeasure()
                {
                    Description = "Unit Test UOM",
                    Name = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789",
                Images = new List<Models.Target.ItemImage>
                {
                    new Models.Target.ItemImage()
                    {
                         AbsoluteUri = "SOME OTHER URI",
                         DisplayOrder = 0
                    }
                }
            };

            Assert.IsFalse(ItemComparer.DoItemsMatch(source, target));
        }

        [TestMethod]
        public void WhenAllImagesHasBeenRemoved_ShouldReturnFalse()
        {
            var source = new Models.Source.Item()
            {
                Category = new Models.Source.Category()
                {
                    CategoryName = "Unit Test Sub Category",
                    CategoryDescription = "Unit Test Sub Category Desc",
                    ParentCategory = new Models.Source.Category()
                    {
                        CategoryName = "Unit Test Category",
                        CategoryDescription = "Unit Test Category Desc",
                    }
                },
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Source.UnitOfMeasure()
                {
                    UnitOfMeasureDescription = "Unit Test UOM",
                    UnitOfMeasureName = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789"
            };

            var target = new Models.Target.Item()
            {
                Category = "Unit Test Sub Category",
                CategoryDescription = "Unit Test Sub Category Desc",
                ParentCategory = "Unit Test Category",
                ParentCategoryDescription = "Unit Test Category Desc",
                ItemDescription = "Unit Test Item Desc",
                ItemName = "Unit Test Item",
                UnitOfMeasure = new Models.Target.UnitOfMeasure()
                {
                    Description = "Unit Test UOM",
                    Name = "Pack"
                },
                UnitQuantity = 1,
                Upc = "6345789",
                Images = new List<Models.Target.ItemImage>
                {
                    new Models.Target.ItemImage()
                    {
                         AbsoluteUri = "SOME OTHER URI",
                         DisplayOrder = 0
                    }
                }
            };

            Assert.IsFalse(ItemComparer.DoItemsMatch(source, target));
        }
    }
}
