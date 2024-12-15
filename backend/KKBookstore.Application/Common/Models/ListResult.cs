namespace KKBookstore.Application.Common.Models;

public class ListResult<T>
    where T : class
{
    public ListResult(IEnumerable<T> items, int totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }

    public IEnumerable<object> Items { get; set; }
    public int TotalCount { get; set; }
}