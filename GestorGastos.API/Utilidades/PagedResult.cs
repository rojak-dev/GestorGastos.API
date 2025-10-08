namespace GestorGastos.API.Utilidades
{
    public sealed class PagedResponse<T>
    {
        public int Total { get; init; }
        public IReadOnlyList<T> Items { get; init; }

        public PagedResponse(int total, IReadOnlyList<T> items)
        {
            Total = total;
            Items = items;
        }
    }
}
