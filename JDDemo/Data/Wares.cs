using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDDemo.Data
{
    //商品类
    public class Wares
    {
        public string id { set; get; }

        public string url { set; get; }

        public string title { get; set; }

        public string price { get; set; }

        public string targetId { get; set; }
    }

    //价格类
    public class WPrice
    {
        public string p { set; get; }

        public string m { set; get; }

        public string op { set; get; }
    }

    public class Order
    {
        public string orderID { set; get; }

        public string name { set; get; }

        public string address { set; get; }

        public string phone { set; get; }

        public string item { set; get; }

        public string sku { set; get; }
    }
}
