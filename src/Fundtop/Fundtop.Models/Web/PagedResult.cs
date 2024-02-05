namespace Fundtop.Models.Web
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int TotalCount { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalPages
        {
            get 
            { 
                return (int)Math.Ceiling(TotalCount / (double)PageSize); 
            }
        }
        
    }
}
