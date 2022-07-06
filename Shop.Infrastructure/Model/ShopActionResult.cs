using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Model
{
    public class ShopActionResult<T>
    {

        public int Total { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public int PageCount
        {
            get
            {
                if (Total == 0) return 0;
                return Convert.ToInt32(Math.Ceiling(Total / (double)Size));
            }
        }
        public T Data { get; set; }
    }
}
