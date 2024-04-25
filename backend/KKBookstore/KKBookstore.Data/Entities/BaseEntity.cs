using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using KKBookstore.Model.Constants;

namespace KKBookstore.Data.Entities
{
    public class BaseEntity
    {
        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public long CreateBy { get; set; }

        public long UpdateBy { get; set; }

        [DefaultValue((int)UserStatus.Active)]
        public int? Status { get; set; }
    }
}

