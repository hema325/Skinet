namespace WebApi.Dtos.Respons
{
    public class PaginatedListDto<TEntity>
    {
        public IReadOnlyCollection<TEntity> Data { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalPages { get; }

        public PaginatedListDto(IReadOnlyCollection<TEntity> data, int totalCount, int pageNumber, int pageSize)
        {
            Data = data;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public bool HasNextpage => PageNumber < TotalPages;
        public bool HasPrevPage => PageNumber > 1;
    }
}
