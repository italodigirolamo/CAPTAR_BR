using Microsoft.EntityFrameworkCore;

namespace CAPTAR.Utilities
{
    public class Pagination<T> : List<T>
    {
        public int StartPage { get; private set; }
        public int TotalPages { get; private set; }
        public Pagination(List<T> items, int counter, int startpage, int totalrecords)
        {
            StartPage = startpage;
            TotalPages = (int)Math.Ceiling(counter / (double)totalrecords);
            this.AddRange(items);
        }

        public bool PreviusPages => StartPage > 1;
        public bool LatesPages => StartPage < TotalPages;

        public static async Task<Pagination<T>> CreatePagination(IQueryable<T> values, int startpage, int totalrecords)
        {
            var counter = await values.CountAsync();
            var items = await values.Skip((startpage - 1) * totalrecords).Take(totalrecords).ToListAsync();
            return new Pagination<T>(items, counter, startpage, totalrecords);
        }
    }
}
