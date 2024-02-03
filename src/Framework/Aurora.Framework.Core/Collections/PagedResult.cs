namespace Aurora.Framework;

public class PagedResult<T> where T : class
{
    public IReadOnlyList<T> Items { get; }
    public int TotalItems { get; private set; }
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public string Description => ToString();
    public bool HasItems => Items.Any();
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;

    public PagedResult(List<T> items, int totalItems, int currentPage, int totalPages)
    {
        Items = items;
        TotalItems = totalItems;
        CurrentPage = currentPage;
        TotalPages = totalPages;
    }

    public override string ToString()
    {
        return $"Page {CurrentPage} of {TotalPages} (Total: {TotalItems} elements).";
    }
}