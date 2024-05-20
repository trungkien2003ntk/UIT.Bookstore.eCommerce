using KKBookstore.Domain.Common;
using KKBookstore.Domain.Common.Interfaces;

namespace KKBookstore.Domain.ProductAggregate
{
    public class ProductOptionValue : BaseAuditableEntity, ISoftDelete
    {
        public ProductOptionValue() : base()   
        {
            
        }

        private ProductOptionValue(
            int optionId,
            string value
        ) : base()
        {
            OptionId = optionId;
            Value = value;
            IsDeleted = false;
        }

        public int OptionId { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedWhen { get; set; }

        //navigation properties
        public ProductOption Option { get; set; }

        public static Result<ProductOptionValue> Create(int optionId, string name)
        {
            // Perform validation or any other necessary logic
            if (string.IsNullOrEmpty(name))
            {
                return Result.Failure<ProductOptionValue>(ProductErrors.NotFound);
            }

            // Return the created instance as a successful result
            return new ProductOptionValue(optionId, name);
        }
    }
}
