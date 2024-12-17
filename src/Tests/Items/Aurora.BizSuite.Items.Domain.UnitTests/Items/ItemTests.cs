using Aurora.BizSuite.Items.Domain.Brands;
using Aurora.BizSuite.Items.Domain.Categories;
using Aurora.BizSuite.Items.Domain.Items;

namespace Aurora.BizSuite.Items.Domain.UnitTests.Items;

public class ItemTests
{
    [Fact]
    public void Create_ShouldReturnItem_WhenCreate()
    {
        // Arrange
        var code = "code123";
        var name = "Item Name";
        var description = "Item Description";
        var itemType = ItemType.Product;
        var category = Category.Create("Category Name", null, 1);
        var brand = Brand.Create("Brand Name", null, null);

        // Act
        var item = Item.Create(
            code,
            name,
            description,
            category,
            brand,
            itemType,
            null, null, []);

        // Assert
        item.Should().NotBeNull();
        item.Code.Should().Be(code);
        item.Name.Should().Be(name);
        item.Description.Should().Be(description);
        item.ItemType.Should().Be(itemType);
        item.CategoryId.Should().Be(category.Id);
        item.BrandId.Should().Be(brand.Id);
        item.Status.Should().Be(ItemStatus.Draft);
    }

    [Fact]
    public void Update_ShouldReturnItem_WhenUpdate()
    {
        // Arrange
        var code = "code123";
        var name = "Item Name";
        var description = "Item Description";
        var itemType = ItemType.Product;
        var category = Category.Create("Category Name", null, 1);
        var brand = Brand.Create("Brand Name", null, null);

        var item = Item.Create(
            code,
            name,
            description,
            category,
            brand,
            itemType,
            null, null, []);

        var newName = "New Item Name";
        var newDescription = "New Item Description";
        var newBrand = Brand.Create("New Brand Name", null, null);

        // Act
        var result = item.Update(newName, newDescription, newBrand, null, null, []);

        // Assert
        result.IsSuccessful.Should().BeTrue();
        item.Should().NotBeNull();
        item.Name.Should().Be(newName);
        item.Description.Should().Be(newDescription);
        item.BrandId.Should().Be(newBrand.Id);
    }
}