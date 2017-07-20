using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JDDemo.Data;
using JDDemo.Helper;
using JDDemo.WareService;
using Hank.BrowserParse;
using Newtonsoft.Json;
using System.Collections;

namespace JDDemo.WareService
{
    public class JDHelper
    {
        private static JDHelper _jdhelper;
        public static JDHelper GetInstance()
        {
            return _jdhelper ?? (_jdhelper = new JDHelper());
        }

        /// <summary>
        /// 获取关注列表数据
        /// </summary>
        /// <param name="watchhtml"></param>
        public List<Wares> GetWatchWareList(string watchhtml)
        {
            List<Wares> rtnSites = new List<Wares>();
            string pattern = "<(?<div>[\\w]+)[^>]*\\s[iI][dD]=(?<Quote>[\"']?)cart-item-list-\\d+(?(Quote)\\k<Quote>)[\"']?[^>]*>(((?<Nested><\\k<div>[^>]*>)|</\\k<div>>(?<-Nested>)|.*?)*)</\\k<div>>";
            MatchCollection m = Regex.Matches(watchhtml, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline);

            if (m.Count > 0)
            {
                //商店列表 一般第一个为京东自营
                foreach (Match cart_item in m)
                {
                    string ptnProduct = "<(?<div>[\\w]+)[^>]*\\s[iI][dD]=(?<Quote>[\"']?)product_\\d+(?(Quote)\\k<Quote>)[\"']?[^>]*>(((?<Nested><\\k<div>[^>]*>)|</\\k<div>>(?<-Nested>)|.*?)*)</\\k<div>>";
                    //此为商品列表
                    MatchCollection mProduct = Regex.Matches(cart_item.Value, ptnProduct, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline);

                    foreach (Match product_item in mProduct)
                    {

                    }



                }




                watchhtml = m[0].Value;
            }

            string cartInfo = Regex.Match(watchhtml, "<div.*id=\"shop-extra-r_8888\"(.*?)</div>").Value;

            MatchCollection matchs = Regex.Matches(watchhtml, @"<div class=""p-name"">(.*?)</div>", RegexOptions.Singleline);
            foreach (Match item in matchs)
            {
                if (!item.Value.Contains("item.jd.com/{{sku}}.html"))
                {
                    //ImportThreads.LastMsg = item.Value;
                    Match reg = Regex.Match(item.Value, "//item.jd.com/(\\d{1,14}).html", RegexOptions.Singleline);
                    if (reg.Success)
                    {
                        string site = "http:" + reg.Value;
                        rtnSites.Add(new Wares() { url = site });
                        //ShowGetMessage(string.Format("添加{0}到商品列表", site));
                        //ShowGetStep(iStep++);
                    }
                }
            }
            return rtnSites;
        }

        /// <summary>
        /// 获取购物车页面html
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public string GetCartHtml(string cookies)
        {
            JDHttpHelper jdhttp = new JDHttpHelper();
            string url = "https://cart.jd.com/cart.action";

            Dictionary<string, string> headerDic = new Dictionary<string, string>();
            headerDic["Accept-Encoding"] = "gzip, deflate, sdch, br";
            headerDic["Accept-Language"] = "zh-CN,zh;q=0.8";
            jdhttp.Referer = "https://www.jd.com/";
            jdhttp.Cookies = cookies;
            jdhttp.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";

            string result = jdhttp.GetHtml(url, "get", Encoding.UTF8, headerDic);

            return result;
        }

        /// <summary>
        /// 获取购物车列表数据
        /// </summary>
        /// <param name="watchhtml"></param>
        public List<Wares> GetCartWareList(string watchhtml)
        {
            List<Wares> rtnSites = new List<Wares>();
            string pattern = "<(?<div>[\\w]+)[^>]*\\s[iI][dD]=(?<Quote>[\"']?)cart-item-list-\\d+(?(Quote)\\k<Quote>)[\"']?[^>]*>(((?<Nested><\\k<div>[^>]*>)|</\\k<div>>(?<-Nested>)|.*?)*)</\\k<div>>";
            MatchCollection m = Regex.Matches(watchhtml, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline);

            if (m.Count > 0)
            {
                //商店列表 一般第一个为京东自营
                foreach (Match cart_item in m)
                {
                    string ptnProduct = "<(?<div>[\\w]+)[^>]*\\s[iI][dD]=(?<Quote>[\"']?)product_\\d+(?(Quote)\\k<Quote>)[\"']?[^>]*>(((?<Nested><\\k<div>[^>]*>)|</\\k<div>>(?<-Nested>)|.*?)*)</\\k<div>>";
                    //此为商品列表
                    MatchCollection mProduct = Regex.Matches(cart_item.Value, ptnProduct, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline);

                    foreach (Match product_item in mProduct)
                    {
                        //MatchCollection matchs = Regex.Matches(product_item.Value, @"<div class=""p-name"">(.*?)</div>", RegexOptions.Singleline);
                        //foreach (Match item in matchs)
                        // {
                        if (!product_item.Value.Contains("item.jd.com/{{sku}}.html"))
                        {
                            //ImportThreads.LastMsg = item.Value;
                            Match reg = Regex.Match(product_item.Value, @"<div class=""p-name"">.*(//item.jd.com/(\d{1,14}).html)""[\s\w='""]+>(.*?)</a>.*<strong\s>¥([\d.]+)</s", RegexOptions.Singleline);
                            if (reg.Success)
                            {
                                string site = "http:" + reg.Groups[1].Value;

                                rtnSites.Add(new Wares()
                                {
                                    url = site,
                                    id = reg.Groups[2].Value,
                                    title = reg.Groups[3].Value,
                                    price = reg.Groups[4].Value
                                });
                                //ShowGetMessage(string.Format("添加{0}到商品列表", site));
                                //ShowGetStep(iStep++);
                            }
                        }
                        // }
                    }

                }

            }

            string cartInfo = Regex.Match(watchhtml, "<div.*id=\"shop-extra-r_8888\"(.*?)</div>").Value;

            return rtnSites;


        }


        /// <summary>
        /// 获取购物车json
        /// </summary>
        /// <returns></returns>
        public string GetCartJson(string cookies)
        {
            //https://cart.jd.com/cart/miniCartServiceNew.action?callback=jQuery2645472&method=GetCart&_=1495720413767

            Random rdm = new Random();
            string param1 = "jQuery" + Math.Floor(1e7 * rdm.NextDouble()).ToString();
            string strTime = Tool.ConvertDateTimeToInt(DateTime.Now).ToString();
            HttpItem item = new HttpItem();
            SFHttpHelper helper = new SFHttpHelper();
            HttpResult result = new HttpResult();

            item.URL = string.Format("https://cart.jd.com/cart/miniCartServiceNew.action?callback={0}&method=GetCart&_={1}", param1, strTime);

            item.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
            item.Method = "get";
            item.Allowautoredirect = true;
            item.Header.Add("Accept-Encoding", "gzip, deflate, sdch, br");
            item.Header.Add("Accept-Language", "zh-CN,zh;q=0.8");
            item.Referer = "https://www.jd.com/";
            item.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            item.Encoding = Encoding.UTF8;
            item.Cookie = cookies;
            result = helper.GetHtml(item);
            //_jdLoginer.cookies = result.Cookie;
            return result.Html;
        }


        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="waresId"></param>
        /// <returns></returns>
        public Wares GetWaresInfo(string waresId)
        {
            JDHttpHelper jdHttp = new JDHttpHelper();
            jdHttp.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";

            Dictionary<string, string> headerDic = new Dictionary<string, string>();
            headerDic["Accept-Encoding"] = "gzip, deflate, sdch, br";
            headerDic["Accept-Language"] = "zh-CN,zh;q=0.8";

            string url = string.Format("https://item.jd.com/{0}.html", waresId);
            string result = jdHttp.GetHtml(url, "get", Encoding.UTF8, headerDic);

            Match reg = Regex.Match(result, @"<div class=""sku-name"">(.*?)</div>", RegexOptions.Singleline);

            WPrice wp = null;
            this.GetWaresPrice(waresId).TryGetValue(waresId, out wp);

            if (wp == null)
            {
                wp = new WPrice() { p = "error!" };
            }
            Wares wares = null;
            wares = new Wares()
            {
                url = url,
                id = waresId,
                title = reg.Groups[1].Value.Trim(),
                price = wp.p
            };

            //查询价格
            //https://p.3.cn/prices/mgets?callback=jQuery5638364&type=1&area=5_274_3207_0&pdtk=&pduid=938279766&pdpin=&pdbp=0&skuIds=J_4082633&source=item-pc
            //查询是否有货
            //https://c0.3.cn/stock?skuId=4082633&area=5_274_3207_0&venderId=1000079304&cat=1318,1466,12122&buyNum=1&choseSuitSkuIds=&extraParam={%22originid%22:%221%22}&ch=1&fqsp=0&pduid=1982668971&pdpin=&callback=jQuery680992
            return wares;
        }

        public Dictionary<string, WPrice> GetWaresPrice(string waresId)
        {
            /*
             	            function a(o, e) {
		            function n() {
			            var o = readCookie("__jda"),
				            e = "";
			            return o && o.indexOf(".") > -1 && (e = o.split(".")[1]), e
		            }
		            var t = o.skus || [],
			            i = o.$el || $("body"),
			            c = o.selector || ".J-p-",
			            a = o.text || "￥{NUM}";
		            $.ajax({
			            url: "//p.3.cn/prices/mgets",
			            data: {
				            skuids: "J_" + t.join(",J_"),
				            type: 2,
				            pdtk: readCookie("pdtk") || "",
				            pduid: n,
				            pdpin: readCookie("pin") || "",
				            pdbp: 0
			            },
			            dataType: "jsonp",
			            cache: !0,
			            success: function(o) {
				            for (var n in o) {
					            if (!o[n].id) return !1;
					            var t = o[n].id.replace("J_", "");
					            parseFloat(o[n].p) > 0 ? i.find(c + t).html(a.replace("{NUM}", o[n].p)) : i.find(c + t).html("免费")
				            }
				            "function" == typeof e && e(o)
			            }
		            })
	            }
             */
            JDHttpHelper jdHttp = new JDHttpHelper();
            Dictionary<string, string> headerDic = new Dictionary<string, string>();
            headerDic["Accept-Encoding"] = "gzip, deflate, sdch, br";
            headerDic["Accept-Language"] = "zh-CN,zh;q=0.8";

            jdHttp.Referer = string.Format("https://item.jd.com/{0}.html", waresId);
            jdHttp.Accept = "*/*";

            //Random rdm = new Random();
            //string param1 = "jQuery" + Math.Floor(1e7 * rdm.NextDouble()).ToString();
            string url = string.Format("https://p.3.cn/prices/mgets?skuIds=J_{0}&source=item-pc", waresId);
            string result = jdHttp.GetHtml(url, "get", Encoding.UTF8, headerDic);

            string pattern = @"{""id"":""J_(?<id>\d+)"",""p"":""(?<pvalue>[\d.]+)"",""m"":""(?<mvalue>[\d.]+)"",""op"":""(?<opvalue>[\d.]+)""";
            MatchCollection m = Regex.Matches(result, pattern);
            Dictionary<string, WPrice> priceDic = new Dictionary<string, WPrice>();

            foreach (Match item in m)
            {
                WPrice wp = new WPrice()
                {
                    p = item.Groups["pvalue"].Value,
                    m = item.Groups["mvalue"].Value,
                    op = item.Groups["opvalue"].Value
                };
                priceDic[item.Groups["id"].Value] = wp;
            }

            //
            return priceDic;
        }
        /// <summary>
        /// 获取购物车json
        /// </summary>
        /// <returns></returns>
        public string CartSelectAllItem(string cookies)
        {
            //https://cart.jd.com/cart/miniCartServiceNew.action?callback=jQuery2645472&method=GetCart&_=1495720413767

            Random rdm = new Random();
            string param1 = "jQuery" + Math.Floor(1e7 * rdm.NextDouble()).ToString();
            string strTime = Tool.ConvertDateTimeToInt(DateTime.Now).ToString();
            HttpItem item = new HttpItem();
            SFHttpHelper helper = new SFHttpHelper();
            HttpResult result = new HttpResult();

            item.URL = string.Format("https://cart.jd.com/cart/miniCartServiceNew.action?callback={0}&method=GetCart&_={1}", param1, strTime);

            item.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
            item.Method = "get";
            item.Allowautoredirect = true;
            item.Header.Add("Accept-Encoding", "gzip, deflate, sdch, br");
            item.Header.Add("Accept-Language", "zh-CN,zh;q=0.8");
            item.Referer = "https://www.jd.com/";
            item.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            item.Encoding = Encoding.UTF8;
            item.Cookie = cookies;
            result = helper.GetHtml(item);
            //_jdLoginer.cookies = result.Cookie;
            return result.Html;
        }

        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="cookies"></param>
        /// <param name="waresId"></param>
        /// <returns></returns>
        public bool AddCart(string cookies, string waresId)
        {
            JDHttpHelper jdHttp = new JDHttpHelper();
            jdHttp.Accept = "text/javascript, application/javascript, application/ecmascript, application/x-ecmascript, */*; q=0.01";
            jdHttp.ContentType = "application/x-www-form-urlencoded";
            jdHttp.Cookies = cookies;

            Dictionary<string, string> headerDic = new Dictionary<string, string>();
            headerDic["X-Requested-With"] = "XMLHttpRequest";
            headerDic["Accept-Encoding"] = "gzip, deflate, br";
            headerDic["Accept-Language"] = "zh-CN,zh;q=0.8";

            //string queryParam = string.Format("pid={0}&rid={1}",waresId,new Random().NextDouble());
            //string url = string.Format("https://cart.jd.com/tproduct?{0}",queryParam);
            string url = "https://cart.jd.com/gate.action";
            //https://cart.jd.com/gate.action
            //jdHttp.Postdata = queryParam;
            jdHttp.Postdata = string.Format("pid={0}&ptype=1&pcount=1&packId=0&targetId=0&f=3&fc=1&t=0&outSkus=&random={1}&locationId=5-274-3207-51527&callback=", waresId, new Random().NextDouble());
            //jdHttp.Referer = "https://cart.jd.com/addToCart.html?rcd=1&pid=2142731&pc=1&nr=1&rid=1496069725603&em=";
            string result = jdHttp.GetHtml(url, "post", Encoding.UTF8, headerDic).TrimStart('(').TrimEnd(')');
            //Hashtable 格式化json
            Hashtable hs = (Hashtable)MUJson.jsonDecode(result);
            if (hs == null || hs["flag"] != "true")
            {
                return false;
            }
            else
            {
                /*Wares wa = new Wares() { 
                id = waresId
                };*/
            }
            return true;
        }
        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public bool EmptyCart(string cookies)
        {
            JDHttpHelper jdHttp = new JDHttpHelper();
            jdHttp.Referer = "https://cart.jd.com/cart.action";
            jdHttp.ContentType = "application/x-www-form-urlencoded";
            jdHttp.Accept = "application/json, text/javascript, */*; q=0.01";
            jdHttp.Cookies = cookies;

            Dictionary<string, string> headerDic = new Dictionary<string, string>();
            headerDic["X-Requested-With"] = "XMLHttpRequest";
            headerDic["Accept-Encoding"] = "gzip, deflate, br";
            headerDic["Accept-Language"] = "zh-CN,zh;q=0.8";

            string url = "https://cart.jd.com/selectAllItem.action";
            jdHttp.Postdata = string.Format("t=0&outSkus&random={0}&locationId=5-274-3207-0", new Random().NextDouble());

            jdHttp.GetHtml(url, "post", Encoding.UTF8, headerDic);

            url = "https://cart.jd.com/batchRemoveSkusFromCart.action";
            jdHttp.Postdata = string.Format("t=0&outSkus&random={0}&locationId=5-274-3207-51527", new Random().NextDouble());
            jdHttp.Cookies = cookies;
            string resule = jdHttp.GetHtml(url, "post", Encoding.UTF8, headerDic);
            return true;
        }

        /// <summary>
        /// 取消全部商品选择
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public bool CancelAllCart(string cookies)
        {
            JDHttpHelper jdHttp = new JDHttpHelper();
            jdHttp.Referer = "https://cart.jd.com/cart.action";
            jdHttp.ContentType = "application/x-www-form-urlencoded";
            jdHttp.Accept = "application/json, text/javascript, */*; q=0.01";
            jdHttp.Cookies = cookies;

            Dictionary<string, string> headerDic = new Dictionary<string, string>();
            headerDic["X-Requested-With"] = "XMLHttpRequest";
            headerDic["Accept-Encoding"] = "gzip, deflate, br";
            headerDic["Accept-Language"] = "zh-CN,zh;q=0.8";

            string url = "https://cart.jd.com/cancelAllItem.action";
            jdHttp.Postdata = string.Format("t=0&outSkus&random={0}&locationId=5-274-3207-49859", new Random().NextDouble());

            jdHttp.GetHtml(url, "post", Encoding.UTF8, headerDic);
            return true;
        }
        /// <summary>
        /// 购物车里选择商品
        /// </summary>
        /// <param name="waresId"></param>
        /// <param name="cookies"></param>
        /// <param name="ptype"></param>
        /// <param name="targetId"></param>
        /// <returns></returns>
        public bool SelectCart(string waresId, string cookies, string ptype, string targetId)
        {
            JDHttpHelper jdHttp = new JDHttpHelper();
            jdHttp.Referer = "https://cart.jd.com/cart.action";
            jdHttp.ContentType = "application/x-www-form-urlencoded";
            jdHttp.Accept = "application/json, text/javascript, */*; q=0.01";
            jdHttp.Cookies = cookies;

            Dictionary<string, string> headerDic = new Dictionary<string, string>();
            headerDic["X-Requested-With"] = "XMLHttpRequest";
            headerDic["Accept-Encoding"] = "gzip, deflate, br";
            headerDic["Accept-Language"] = "zh-CN,zh;q=0.8";

            string url = string.Format("https://cart.jd.com/selectItem.action?rd{0}", new Random().NextDouble());
            jdHttp.Postdata = string.Format("outSkus=&pid={0}&ptype={2}&packId=0&targetId={1}&promoID={1}&locationId=5-274-3207-49859&t=0", waresId, targetId, ptype);

            string result = jdHttp.GetHtml(url, "post", Encoding.UTF8, headerDic);
            return true;
        }

        /// <summary>
        /// 获取结算页面
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public string GetOrderInfo(string cookies)
        {
            JDHttpHelper jdHttp = new JDHttpHelper();
            jdHttp.Referer = "https://cart.jd.com/cart.action";
            jdHttp.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            jdHttp.Cookies = cookies;

            Dictionary<string, string> headerDic = new Dictionary<string, string>();
            headerDic["Accept-Encoding"] = "gzip, deflate, sdch, br";
            headerDic["Accept-Language"] = "zh-CN,zh;q=0.8";

            string url = string.Format("https://trade.jd.com/shopping/order/getOrderInfo.action?rid={0}", Math.Floor(1e7 * new Random().NextDouble()).ToString());

            string result = jdHttp.GetHtml(url, "get", Encoding.UTF8, headerDic);
            return result;
        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public string SubmitOrder(string cookies, string eid, string fp, string riskControl)
        {
            JDHttpHelper jdHttp = new JDHttpHelper();

            jdHttp.Referer = "https://trade.jd.com/shopping/order/getOrderInfo.action?rid=" + Math.Floor(1e7 * new Random().NextDouble()).ToString();
            jdHttp.Accept = "application/json, text/javascript, */*; q=0.01";
            jdHttp.ContentType = "application/x-www-form-urlencoded";
            jdHttp.Cookies = cookies;
            jdHttp.Postdata = string.Format("overseaPurchaseCookies=&submitOrderParam.sopNotPutInvoice=true&submitOrderParam.ignorePriceChange=0&submitOrderParam.btSupport=0&submitOrderParam.eid={0}&submitOrderParam.fp={1}&riskControl={2}", eid, fp, riskControl);

            Dictionary<string, string> headerDic = new Dictionary<string, string>();
            headerDic["Accept-Encoding"] = "gzip, deflate, br";
            headerDic["Accept-Language"] = "zh-CN,zh;q=0.8";
            headerDic["X-Requested-With"] = "XMLHttpRequest";

            string url = "https://trade.jd.com/shopping/order/submitOrder.action";


            string result = jdHttp.GetHtml(url, "post", Encoding.UTF8, headerDic);

            return result;
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public List<Order> GetOrderList(string cookies)
        {
            JDHttpHelper jdHttp = new JDHttpHelper();
            jdHttp.Cookies = cookies;
            string url = "https://order.jd.com/center/list.action";

            string result = jdHttp.GetHtml(url, "get", Encoding.UTF8);

            string pattern = @"<span class=""number"">(.*?)<div class=""operate"">";
            MatchCollection m = Regex.Matches(result, pattern,RegexOptions.Singleline);
            foreach (Match item in m)
            {
                string orderid = Regex.Match(item.Value, @"id=""track(.*?)""", RegexOptions.Singleline).Groups[1].Value;
                orderid.Trim();
                string name = Regex.Match(item.Value, @"<strong>(.*?)</strong>", RegexOptions.Singleline).Groups[1].Value;
            }

            result.Trim();
            return null;
        }

        /// <summary>
        /// 网页隐藏input内容格式化
        /// </summary>
        /// <param name="html"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetValueFormHtml(string html, string key = "name")
        {
            Dictionary<string, string> valueDic = new Dictionary<string, string>();
            MatchCollection matchs = Regex.Matches(html, string.Format(@"<input.+?(?={0}=""(?<name>\S+)"").+?(?=value=""(?<value>\S+)"").+?>", key));

            if (matchs.Count > 0)
            {
                foreach (Match mitem in matchs)
                {
                    string name = mitem.Groups["name"].ToString();
                    string value = mitem.Groups["value"].ToString();
                    valueDic[name] = value;
                }
            }
            return valueDic;
        }
    }
}
