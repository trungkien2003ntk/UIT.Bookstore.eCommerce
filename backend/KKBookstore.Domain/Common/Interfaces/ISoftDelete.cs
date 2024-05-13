using System.ComponentModel;

namespace KKBookstore.Domain.Common.Interfaces
{
    public interface ISoftDelete
    {
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public DateTimeOffset? DeletedWhen { get; set; }

        public void Undo()
        {
            IsDeleted = false;
            DeletedWhen = null;
        }
    }
}
