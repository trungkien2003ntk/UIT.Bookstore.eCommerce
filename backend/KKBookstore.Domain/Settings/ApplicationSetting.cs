using KKBookstore.Models;

namespace KKBookstore.Settings;

public class ApplicationSetting : BaseAuditedEntity
{
    public string Key { get; set; }
    public string Value { get; set; }

    public ApplicationSetting(int id, string key, string value)
    {
        Id = id;
        Key = key;
        Value = value;
    }
}
