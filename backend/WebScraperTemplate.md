# Web Scraper Template for KKBookstore

🧩 Step 1: Input Classes (for Python models or DB structure)

```python
class Product:
    Id: int
    Name: str
    Description: str
    ProductTypeId: int
    IsBook: bool
    IsActive: bool
    UnitMeasureId: int
    Sku: str
    CreatedAt: str
    UpdatedAt: str

class ProductVariant:
    Id: int
    ProductId: int
    SkuValue: str
    Barcode: str
    RecommendedRetailPrice: float
    UnitPrice: float
    TaxRate: float
    Comment: str
    ValidFrom: str
    ValidTo: Optional[str]
    DiscontinuedWhen: Optional[str]
    Weight: int
    Dimension: Dict[str, float]  # Height, Width, Length
    IsActive: bool
    Tags: str

class ProductOption:
    Id: int
    ProductId: int
    Name: str
    IsOptionWithImage: bool

class ProductOptionValue:
    Id: int
    OptionId: int
    Value: str
    ThumbnailImageUrl: Optional[str]
    LargeImageUrl: Optional[str]

class ProductVariantOptionValue:
    Id: int
    ProductVariantId: int
    OptionId: int
    OptionValueId: int

class ProductImage:
    Id: int
    ProductId: int
    ThumbnailImageUrl: str
    LargeImageUrl: str

class BookAuthor:
    Id: int
    ProductId: int
    AuthorName: str
```

🗂 Step 2: Desired Output JSON Structure

```json
products.json:
[
  {
    "Id": 1,
    "Name": "Book Title Example",
    "Description": "This is a sample book description",
    "ProductTypeId": 1,
    "IsBook": true,
    "IsActive": true,
    "UnitMeasureId": 1,
    "Sku": "BOOK-001"
  }
]

product_variants.json:
[
  {
    "Id": 1,
    "ProductId": 1,
    "SkuValue": "BOOK-001-VAR1",
    "Barcode": "978123456789",
    "RecommendedRetailPrice": 199000,
    "UnitPrice": 159000,
    "TaxRate": 10.0,
    "Comment": "Paperback version",
    "ValidFrom": "2025-04-14T00:00:00Z",
    "ValidTo": null,
    "DiscontinuedWhen": null,
    "Weight": 500,
    "Dimension": {
      "Height": 22.5,
      "Width": 15.0,
      "Length": 2.5
    },
    "IsActive": true,
    "Tags": "paperback,new release"
  }
]

product_options.json:
[
  {
    "Id": 1,
    "ProductId": 1,
    "Name": "Format",
    "IsOptionWithImage": false
  }
]

product_option_values.json:
[
  {
    "Id": 1,
    "OptionId": 1,
    "Value": "Paperback",
    "ThumbnailImageUrl": "https://example.com/thumbnail1.jpg",
    "LargeImageUrl": "https://example.com/large1.jpg"
  }
]

product_variant_option_values.json:
[
  {
    "Id": 1,
    "ProductVariantId": 1,
    "OptionId": 1,
    "OptionValueId": 1
  }
]

product_images.json:
[
  {
    "Id": 1,
    "ProductId": 1,
    "ThumbnailImageUrl": "https://example.com/thumbnail1.jpg",
    "LargeImageUrl": "https://example.com/large1.jpg"
  }
]
```

🔗 Step 3: Notes About Website DOM (Optional)
Please fill in the CSS selectors or XPath for the elements on the target website:

Product Identification:

//h1[@class='fhs_name_product_desktop']/text()[normalize-space()]: Extracts the product's title for desktop view.
Product Details:

//div[@class='product-view-sa_one']/div[@class='product-view-sa-supplier']/a/text(): Extracts the supplier's name.
//div[@class='product-view-sa_one']/div[@class='product-view-sa-author']/span[2]/text(): Extracts the author(s)' names.
//div[@class='product-view-sa_two']/div[@class='product-view-sa-supplier']/span[2]/text(): Extracts the publisher's name.
//div[@class='product-view-sa_two']/div[@class='product-view-sa-author']/span[2]/text(): Extracts the cover type.
Product Ratings and Sales:

//table[@class='ratings-desktop']//a/text(): Extracts the number of reviews for the product.
//div[@class='product-view-qty-num']/span/text(): Extracts the quantity of the product sold.
Pricing and Discounts:

//span[@id='product-price-613534']/text(): Extracts the special price of the product.
//span[@id='old-price-613534']/text(): Extracts the regular (old) price of the product.
//span[@class='discount-percent']/text(): Extracts the discount percentage.

# Product Code (Mã hàng)

//table[@class='data-table table-additional']/tbody/tr[th[contains(text(), 'Mã hàng')]]/td/div/text()

# Note: Dynamically added data.

# Supplier Name (Tên Nhà Cung Cấp)

//table[@class='data-table table-additional']/tbody/tr[th[contains(text(), 'Tên Nhà Cung Cấp')]]/td/div/a/text()

# Note: Dynamically added data.

# Author (Tác giả)

//table[@class='data-table table-additional']/tbody/tr[th[contains(text(), 'Tác giả')]]/td/div/text()

# Note: Dynamically added data.

# Publisher (NXB)

//table[@class='data-table table-additional']/tbody/tr[th[contains(text(), 'NXB')]]/td/div/text()

# Note: Dynamically added data.

# Year of Publication (Năm XB)

//table[@class='data-table table-additional']/tbody/tr[th[contains(text(), 'Năm XB')]]/td/div/text()

# Note: Dynamically added data.

# Language (Ngôn Ngữ)

//table[@class='data-table table-additional']/tbody/tr[th[contains(text(), 'Ngôn Ngữ')]]/td/div/text()

# Note: Dynamically added data.

# Weight (Trọng lượng (gr))

//table[@class='data-table table-additional']/tbody/tr[th[contains(text(), 'Trọng lượng (gr)')]]/td/div/text()

# Note: Dynamically added data.

# Package Dimensions (Kích Thước Bao Bì)

//table[@class='data-table table-additional']/tbody/tr[th[contains(text(), 'Kích Thước Bao Bì')]]/td/div/text()

# Note: Dynamically added data.

# Number of Pages (Số trang)

//table[@class='data-table table-additional']/tbody/tr[th[contains(text(), 'Số trang')]]/td/div/text()

# Note: Dynamically added data.

# Format (Hình thức)

//table[@class='data-table table-additional']/tbody/tr[th[contains(text(), 'Hình thức')]]/td/div/text()

# Note: Dynamically added data.

# Best Selling Product Link

//table[@class='data-table'][2]/tbody/tr/td/a/@href

# Note: Dynamically added data.

# Best Selling Product Text

//table[@class='data-table'][2]/tbody/tr/td/a/text()

# Note: Dynamically added data.

# Price Includes Tax Disclaimer

//div[contains(text(), 'Giá sản phẩm trên Fahasa.com đã bao gồm thuế theo luật hiện hành.')]/text()

# Note: Dynamically added data.

# Promotion Policy Disclaimer

//div[contains(text(), 'Chính sách khuyến mãi trên Fahasa.com không áp dụng cho Hệ thống Nhà sách Fahasa trên toàn quốc')]/text()

# Note: Dynamically added data.

Additional notes about site structure:

-   Does the site use AJAX to load product data? => nope, service side rendered
-   Are products listed in pagination or infinite scroll? => already extract variant by url
-   Is there a product detail page separate from listing page? => yes
-   Is there any embedded JSON in the page that contains product data? => none, we use html element and classes for scraping
-   Are there any anti-scraping measures on the site? => nope, free to enter and query, but no api provided

🧪 Step 4: Data Processing Rules

-   Price normalization: (e.g., convert "159.000&nbsp;₫" to 159000)
-   Date formatting: (e.g., convert displayed dates to datetimeoffset format)
-   Image URL processing:
-   Product variant handling: (how to associate variants with main product) - just scrape the full product data, loads to variant detail since the product in fahasa is a variant in my system.
-   Error handling: (what to do if a field is missing) => set blank
-   Deduplication strategy: (how to handle duplicate products) => already filtered from the urls.
-   Rate limiting: (delay between requests to avoid being blocked) => None rate limiting, since we're scraping thru elemnt html

Additional Requirements

-   Storage format: (JSON, CSV, database) => json
-   Store data in batch as processed to reload if notebook auto disconnects.
