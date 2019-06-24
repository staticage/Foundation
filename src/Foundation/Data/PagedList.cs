using System;
using System.Collections;
using System.Collections.Generic;

namespace Foundation.Data
{
    public interface IPagedList
    {
        int StartPage { get; }
        int EndPage { get; }
        int Page { get; }
        int PageSize { get; }
        int Total { get; }
        int StartNumber { get; }
        int EndNumber { get; }
        int TotalPage { get; set; }
    }

    public interface IPagedQuery
    {
        int Page { get; }
        int PageSize { get; }
    }

    public class PagedList<TData> : IEnumerable<TData>, IPagedList
    {
        public PagedList(int page, int pageSize, int total, IEnumerable<TData> data)
        {
            Page = page;
            PageSize = pageSize;
            Total = total;
            Data = data;
            TotalPage = (int)Math.Ceiling((double)Total / pageSize);
            StartPage = (Page - 3 < 1) ? 1 : Page - 3;
            EndPage = (StartPage + 6 > TotalPage) ? TotalPage : StartPage + 6;
            StartNumber = (Page - 1) * PageSize + 1;
            EndNumber = (Page - 1) * PageSize + PageSize;
        }

        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; private set; }
        public int StartNumber { get; }
        public int EndNumber { get; }
        public int TotalPage { get; set; }
        public IEnumerable<TData> Data { get; set; }
        public IEnumerator<TData> GetEnumerator() => Data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Data.GetEnumerator();
    }
}
