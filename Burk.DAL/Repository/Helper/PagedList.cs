using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Burk.DAL.Repository.Helper
{
    internal class PagedList<T>
    {
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            Data = items.ToList();
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
    }

    //public static class PagedListExtensionMethods
    //{


    //    public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageNumber = 0, int pageSize = 0)
    //    {
    //        int totalCount = source.CountAsync().Result;
    //        var items = pageNumber == 0 || pageSize == 0 ? source.ToList() : source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    //        return new PagedList<T>(items, totalCount, pageNumber, pageSize);
    //    }

    //    public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageNumber = 0, int pageSize = 0)
    //    {
    //        int totalCount = source.Count();
    //        var items = pageNumber == 0 || pageSize == 0 ? source.ToList() : source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    //        return new PagedList<T>(items, totalCount, pageNumber, pageSize);
    //    }
    //}
}

