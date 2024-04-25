using System.ComponentModel;

namespace KKBookstore.Data.Entities
{
    public interface ISoftDelete
    {
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public DateTimeOffset? DeletedAt { get; set; }
        
        public void Undo()
        {
            IsDeleted = false;
            DeletedAt = null;
        }
    }
}
