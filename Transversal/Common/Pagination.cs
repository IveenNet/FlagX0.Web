namespace FlagX0.Web.Transversal.Common
{
    public record Pagination<T> (List<T> items, int totalItems, int pageSize, int currentPage, string? search) where T : class
    {
    }
}
