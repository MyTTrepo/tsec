using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class ClosingPriceInfo
    {
        public long InsCode { get; set; }

        public int DEven { get; set; }

        public Decimal PClosing { get; set; }

        public Decimal PDrCotVal { get; set; }

        public Decimal ZTotTran { get; set; }

        public Decimal QTotTran5J { get; set; }

        public Decimal QTotCap { get; set; }

        public Decimal PriceMin { get; set; }

        public Decimal PriceMax { get; set; }

        public Decimal PriceYesterday { get; set; }

        public Decimal PriceFirst { get; set; }
    }
}
