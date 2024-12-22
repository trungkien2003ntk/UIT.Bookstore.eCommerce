namespace KKBookstore.Application.Common.Models.ResultDtos;

public class ListResult<T>
    where T : class
{
    public ListResult(IEnumerable<T> items, int totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }

    public ListResult(IList<T> items)
    {
        Items = items;
        TotalCount = items.Count;
    }

    public IEnumerable<object> Items { get; set; }
    public int TotalCount { get; set; }
}