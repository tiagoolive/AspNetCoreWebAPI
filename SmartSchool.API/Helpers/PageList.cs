﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Helpers
{
    public class PageList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public PageList(List<T>itens, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(itens);
        }

        public static async Task<PageList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var itens = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PageList<T>(itens, count, pageNumber, pageSize);
        }
    }
}
