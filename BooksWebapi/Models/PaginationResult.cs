using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksWebapi.Models
{
    public class PaginationResult
    {
        private long total;

        private List<Books> rows;

        public PaginationResult(long total, List<Books> rows)
        {
            this.total = total;
            this.rows = rows;
        }

        public long getTotal()
        {
            return total;
        }

        public void setTotal(long total)
        {
            this.total = total;
        }

        public List<Books> getRows()
        {
            return rows;
        }

        public void setRows(List<Books> rows)
        {
            this.rows = rows;
        }


        public String toString()
        {
            return "PaginationResult{" +
                    "total=" + total +
                    ", rows=" + rows +
                    '}';
        }
    }
}