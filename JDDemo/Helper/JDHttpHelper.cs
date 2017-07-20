using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hank.BrowserParse;

namespace JDDemo.Helper
{
    public class JDHttpHelper
    {
        /*
        private JDHttpHelper()
        {

        }

        private static JDHttpHelper _jdHttpHelper;

        public static JDHttpHelper GetInstance()
        {
            return _jdHttpHelper ?? (_jdHttpHelper = new JDHttpHelper());
        }
        */

        private string _PC_UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
        /// <summary>
        /// PC端UserAgent
        /// </summary>
        public string PC_UserAgent
        {
            get { return _PC_UserAgent; }
            set { _PC_UserAgent = value; }
        }

        private string _m_UserAgent = "";
        /// <summary>
        /// 手机端UserAgent
        /// </summary>
        public string M_UserAgent
        {
            get { return _m_UserAgent; }
            set { _m_UserAgent = value; }
        }

        private string _ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType
        {
            get { return _ContentType; }
            set { _ContentType = value; }
        }

        private string _Postdata = "";
        /// <summary>
        /// Post数据
        /// </summary>
        public string Postdata
        {
            get { return _Postdata; }
            set { _Postdata = value; }
        }

        private string _Cookies = "";
        /// <summary>
        /// Cookies
        /// </summary>
        public string Cookies
        {
            get { return _Cookies; }
            set { _Cookies = value; }
        }

        private string _Accept = "text/plain, */*; q=0.01";

        public string Accept
        {
            get { return _Accept; }
            set { _Accept = value; }
        }

        private HttpItem hitem = new HttpItem();

        public PostDataType PostType = PostDataType.String;

        private string _Referer = null;

        public string Referer
        {
            get { return _Referer; }
            set { _Referer = value; }
        }

        public string GetHtml(string url,string method, Encoding encoding ,Dictionary<string,string> header = null)
        {
            SFHttpHelper helper = new SFHttpHelper();
            HttpResult result = new HttpResult();
            hitem.URL = url;
            hitem.Method = method;
            hitem.Allowautoredirect = true;
            hitem.UserAgent = _PC_UserAgent;
            hitem.ContentType = _ContentType;
            hitem.Accept = _Accept;
            hitem.Cookie = _Cookies;

            if(_Referer != null)
            {
                hitem.Referer = _Referer;
            }

            if(header != null)
            {
                foreach (var item in header.Keys)
	            {
		            hitem.Header.Add(item,header[item]);
	            }
            }
            if(method == "post")
            {
                hitem.Postdata = _Postdata;
                hitem.PostDataType = PostType;
            }

            result = helper.GetHtml(hitem);

            _Cookies = result.Cookie;
            
            return result.Html;
        }

    }
}
