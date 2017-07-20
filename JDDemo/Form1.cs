using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hank.BrowserParse;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using MaYongze.Helper;
using JDDemo.WareService;
using JDDemo.Helper;
using JDDemo.Data;

namespace JDDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //初始化界面
            InitLogin();

            if (_isAuthcode)
            {
                pieAuthcode.Image = _imageAuthCode;
                pieAuthcode.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //PassWordHelper.GetInstance().DesStr("密码", "isarahan", "wolfstud")

            if (!_isAuthcode || !string.IsNullOrEmpty(txtAuthcode.Text))
            {
                Login4JD("账号", "密码", txtAuthcode.Text.Trim());            
            }
        }


        private JDLoginer _jdLoginer;
        Dictionary<string, string> _loginParams = new Dictionary<string, string>();
        private bool _isAuthcode = false;
        private Image _imageAuthCode;


        /// <summary>
        /// 登录京东
        /// </summary>
        /// <param name="myAuthCode"></param>
        /// <returns></returns>
        public bool Login4JD(string uName,string uPasswd,string myAuthCode)
        {
            try
            {

                _jdLoginer.loginname = uName;
                _jdLoginer.loginpwd = uPasswd;

                return Login(myAuthCode);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                throw;
            }
        }
        public class LoginMsg
        {
            public string name { get; set; }
            public string value { get; set; }
        }

        private void btnGetCart_Click(object sender, EventArgs e)
        {

           //string cartJson =  JDHelper.GetInstance().GetCartJson(_jdLoginer.cookies);
            
            HttpItem item = new HttpItem();
            SFHttpHelper helper = new SFHttpHelper();
            HttpResult result = new HttpResult();

            item.URL = "https://cart.jd.com/cart.action";

            item.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
            item.Method = "get";
            item.Allowautoredirect = true;
            item.Header.Add("Accept-Encoding", "gzip, deflate, sdch, br");
            item.Header.Add("Accept-Language", "zh-CN,zh;q=0.8");
            item.Referer = "https://www.jd.com/";
            item.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            item.Encoding = Encoding.UTF8;
            item.Cookie = _jdLoginer.cookies;
            result = helper.GetHtml(item);
            //_jdLoginer.cookies = result.Cookie;
            List<Data.Wares> waresUrls = JDHelper.GetInstance().GetCartWareList(result.Html);
            foreach(Data.Wares ware in waresUrls)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = ware.id.ToString();
                lvItem.SubItems.Add(ware.title);
                lvItem.SubItems.Add(ware.price);
                lsvCart.Items.Add(lvItem);
            }
            

        }

        /// <summary>
        /// 登录京东商城
        /// </summary>
        private bool Login(string authcode)
        {

            //如果需要验证码就需要jda，jdb，jdc，jdv这些，如果没有出验证码，可以直接post登录成功的
            //string cookies = "__jda=95931165.290243407.1371634814.1371634814.1371634814.1; __jdb=95931165.1.290243407|1.1371634814; __jdc=95931165; __jdv=95931165|direct|-|none|-;" + _jdLoginer.cookies;
            //string cookies = "sc_t=2; login_c=3; login_m=1; mp=15613205624; _pst=1014057907_m; user-key=f72ffe8f-9b91-4070-832f-b0b0b629c29b; qr_p=b88154dc50d904488011bb01cb4a3f376a47996514992a933ad08306629f1ea14c2e9832c74f4352; _ntpMlTi=vXJxir6m6Vd9BsZJWl24b+fK9qCM5B5gsw/PRlsIJh0=; _nthIgzC=zFOqBg4bg68erd1CFscm7bm2dQ89oDYnD0GHe15/dRc=; _ntKLEyC=rfLTlB6ItHHXACrMnuQam+OAJdQ0E7iRifv0ItmpmL8=; _ntJoupV=usJaM8yLGXJOeSSSYV7dkFL2wGyPRjY9xXURlnsKnnE=; _ntzDLDp=XKm1bDUulyglxGyVTW6UoXfvhPfIuQY0en4ky3lQXZA=; unick=jd101405win; cn=9; ipLoc-djd=5-199-47213-47701.137613558; unpl=V2_ZzNtbRIEFBMmWhRSLh9cBmIKR1wSUBFFclpHA34aXA1lAEVaclRCFXMUR1BnGlQUZwsZX0BcRxNFCEdkeylVDGEGElhyVkJXN11DB34dWQAwVxFUQ15FFXY4RVRLEWwFYQMUW0VXShFxOHZdcils0OyNy9nEg%2f21RQ9DU34cXQRkMxNtQ2cIe3RFRlJ7H1oCZwoWWXJWcxY%3d; __jdv=122270672|p.yiqifa.com|t_1_887414|uniongoubiao|abf7bcc7d71248d0a6ca6c0f421932f6|1496057643904; qr_t=f; alc=3XgYzzkGzZ/czw9E0mmNxA==; _ntPdEQO=L4zHOOT+hWqjQjdFcmJaJqgV4nfDjeavJJnio6Jxx+U=; __jda=122270672.1717761855.1496050986.1496057630.1496057644.5; __jdb=122270672.2.1717761855|5.1496057644; __jdc=122270672; ext_pgvwcount=2; hibext_instdsigdip=1; __jdu=1717761855"
               // + _jdLoginer.cookies;


            string cookies = _jdLoginer.cookies;
            cookies = cookies.Replace("HttpOnly,", null);
            _jdLoginer.cookies = cookies;

            JDHttpHelper jdHttp = new JDHttpHelper();
            jdHttp.Cookies = _jdLoginer.cookies;

            Dictionary<string, string> header = new Dictionary<string, string>();
            header["x-requested-with"] = "XMLHttpRequest";
            header["Accept-Encoding"] = "gzip, deflate, br";
            header["Accept-Language"] = "zh-CN,zh;q=0.8";
            string url = string.Format("https://passport.jd.com/uc/loginService?uuid={0}&r={1}&version=2015", _jdLoginer.uuid, _jdLoginer.r);

            jdHttp.Postdata = string.Format("uuid={0}&eid={1}&fp={2}&_t={3}&loginType:c"
               + "&loginname={4}&nloginpwd={5}&chkRememberMe=on&authcode={6}&pubKey={7}&sa_token={8}&seqSid:9206473278124856000",
               _jdLoginer.uuid, _jdLoginer.eid, _jdLoginer.fp, _jdLoginer._t,
               _jdLoginer.loginname, _jdLoginer.loginpwd, authcode, _jdLoginer.pubKey, _jdLoginer.sa_token);

            string html = jdHttp.GetHtml(url, "post", Encoding.UTF8, header);
            _jdLoginer.cookies = jdHttp.Cookies;

            //PassWordHelper.GetInstance().SetRsaKey(null,_jdLoginer.pubKey);
            //密码rsa处理
            //_jdLoginer.loginpwd = PassWordHelper.GetInstance().SSLRsaEncrypt(_jdLoginer.loginpwd);

            //_jdLoginer.loginpwd = "RnD/dVjzyvCDXK9sKgEOh2iSeXF7T2egfvNQwOUZDW3YLtsB1WVvrJJvfSb1N6f561qgGEjI52Vzlt4i3TTB0OZBR/iS3PaRXZ36w3tPX3jd9H7nBc3eJQ9BG4ImfwcYyXeJq9hvo1EDWwdzcr9eoCwNG1XkkipYkUx4iR7nFCg=";

            //https://passport.jd.com/uc/loginService?uuid=c4ee6786-a737-43f2-9110-0aea68500665&ReturnUrl=http%3A%2F%2Fwww.jd.com%2F&r=0.12566684937051964&version=2015
            //https://passport.jd.com/uc/loginService?uuid=c8422a2d-e011-4783-8bca-38625607a086&ltype=logout&r=0.04991701628602441&version=2015


            //item.Referer = "http://passport.jd.com/new/login.aspx?ReturnUrl=http%3a%2f%2fjd2008.jd.com%2fJdHome%2fOrderList.aspx";

           

            if (!html.Contains("success"))
            {
                string rtnMsg = html.Remove(0, 1).TrimEnd(')');
                LoginMsg jdMsg = JsonConvert.DeserializeObject<LoginMsg>(rtnMsg);
                //用户名错误({"username":"\u8d26\u6237\u540d\u4e0d\u5b58\u5728\uff0c\u8bf7\u91cd\u65b0\u8f93\u5165"})
                if (html.ToLower().Contains("username"))
                {
                   // ShowGetMessage("失败：用户名错误!" + jdMsg.value);
                    MessageBox.Show(jdMsg.value);
                    //ImportThreads.LastMsg = "失败：用户名错误!" + jdMsg.value;
                }
                else if (html.ToLower().Contains("pwd"))
                {
                    //ShowGetMessage("失败：密码验证不通过!" + jdMsg.value);
                    //密码错误 ({"pwd":"\u8d26\u6237\u540d\u4e0e\u5bc6\u7801\u4e0d\u5339\u914d\uff0c\u8bf7\u91cd\u65b0\u8f93\u5165"})
                    MessageBox.Show(jdMsg.value);
                   // ImportThreads.LastMsg = "失败：密码验证不通过!" + jdMsg.value;
                }
                else if (html.ToLower().Contains("emptyauthcode"))
                {
                   // ShowGetMessage("失败：请输入登录验证码!" + jdMsg.value);
                    //验证码错误 ({"emptyAuthcode":"\u8bf7\u8f93\u5165\u9a8c\u8bc1\u7801"})
                    //({"_t":"_ntcKIiJ","emptyAuthcode":"\u9a8c\u8bc1\u7801\u4e0d\u6b63\u786e\u6216\u9a8c\u8bc1\u7801\u5df2\u8fc7\u671f"})
                    MessageBox.Show(jdMsg.value);
                   // ImportThreads.LastMsg = "失败：请输入登录验证码!" + jdMsg.value;
                }
                else
                {
                    MessageBox.Show(jdMsg.value);
                    //ImportThreads.LastMsg = jdMsg.value;
                }
               // ImportThreads.WareEnd = true;
                return false;
            }
            MessageBox.Show("登录成功！");
            //ShowGetMessage("登录成功！");
           // ImportThreads.LastMsg = "登录成功！";
            return true;

        }

        /// <summary>
        /// 初始化登录接口
        /// </summary>
        /// <param name="uName"></param>
        /// <param name="uPass"></param>
        public void InitLogin()
        {
            try
            {
                if (_jdLoginer == null)
                {
                    _jdLoginer = new JDLoginer()
                    {
                        r = new Random().NextDouble().ToString(),
                        //loginname = uName,
                        //loginpwd = uPass,
                        cookies = "",
                        chkRememberMe = ""
                    };
                }
                _loginParams.Clear();
                GetLoginUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 获取登录界面数据
        /// </summary>
        private void GetLoginUI()
        {
            string loginHtml = GetJDWebHtml("https://passport.jd.com/new/login.aspx");

            //<input[^(>[\\s\\S]*?<)]*?hidden[^(>[\\s\\S]*?<)]*?/>
            MatchCollection matchs = Regex.Matches(loginHtml, @"<input.+?(?=name=""(?<name>\S+)"").+?(?=value=""(?<value>\S+)"").+?>");
            if (matchs.Count > 0)
            {
                foreach (Match mitem in matchs)
                {
                    _loginParams.Add(mitem.Groups["name"].ToString(), mitem.Groups["value"].ToString());

                    string name = mitem.Groups["name"].ToString();
                    string value = mitem.Groups["value"].ToString();
                    if (name == "uuid")
                    {
                        _jdLoginer.uuid = value;
                    }
                    else if (name == "_t")
                    {
                        _jdLoginer._t = value;
                    }
                    else if (name == "pubKey")
                    {
                        _jdLoginer.pubKey = value;
                    }
                    else if (name == "sa_token")
                    {
                        _jdLoginer.sa_token = value;
                    }
                    else if(name == "eid")
                    {
                        _jdLoginer.eid = value;
                    }
                    else if(name == "fp")
                    {
                        _jdLoginer.fp = value;
                    }
                    else
                    {
                        _jdLoginer.tname = name;
                        _jdLoginer.tvalue = value;
                    }
                }
                //判断是否需要验证码 TO-DO
                if (CheckAuthcode())
                {
                    _isAuthcode = true;
                    //获取验证码图片
                    GetCodeImg();
                }
            }
            else
            {
                //ImportThreads.LastMsg = "获取网页参数错误";
            }
        }

        /// <summary>
        /// 获取验证码图片
        /// </summary>
        private void GetCodeImg()
        {
            //<img id="JD_Verification1" class="verify-code" src2="https://authcode.jd.com/verify/image?a=1&amp;acid=6bb6fa1d-a929-404c-a0cc-1d10e13aa639&amp;uid=6bb6fa1d-a929-404c-a0cc-1d10e13aa639"
            //"https://authcode.jd.com/verify/image?a=1&acid=1c55bd67-241f-4b29-a56b-5c5bc6c717f7&uid=1c55bd67-241f-4b29-a56b-5c5bc6c717f7&yys=1454509280755";
            try
            {
                //Match rtnRgx = Regex.Match(loginHtml, @"src2=""(.*?)""", RegexOptions.Singleline);
                {
                    int sjs = new Random().Next();
                    string imgUrl = string.Format("https://authcode.jd.com/verify/image?a=1&acid={0}&uid={0}&yys={1}", _jdLoginer.uuid, sjs);

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imgUrl);
                    request.Timeout = 20000;
                    request.ServicePoint.ConnectionLimit = 100;
                    request.ReadWriteTimeout = 30000;
                    request.Method = "GET";
                    //request.CookieContainer
                    //CookieContainer co = new CookieContainer();
                    //co.SetCookies(new Uri("https://passport.jd.com/uc/loginService"), _jdLoginer.cookies);
                    //request.CookieContainer = co;

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode != HttpStatusCode.OK)
                        return;
                    Stream resStream = response.GetResponseStream();
                    //this.pictureBox1.Image = Image.FromStream(resStream);
                    _imageAuthCode = new Bitmap(resStream);
                }
            }
            catch (Exception ex)
            {
                _imageAuthCode = null;
                //Debug.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// 是否要验证码
        /// </summary>
        private bool CheckAuthcode()
        {
            //判断是否需要验证 返回Json({"verifycode":false})
            //https://passport.jd.com/uc/showAuthCode?r=0.7007493122946471&version=2015
            //https://authcode.jd.com/verify/image?a=1&acid=1c55bd67-241f-4b29-a56b-5c5bc6c717f7&uid=1c55bd67-241f-4b29-a56b-5c5bc6c717f7&yys=1454509280755
            JDHttpHelper jdHttp = new JDHttpHelper();
            string r = new Random().NextDouble().ToString();
            string url = string.Format("https://passport.jd.com/uc/showAuthCode?r={0}&version=2015", r);
            jdHttp.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            jdHttp.Postdata = string.Format("loginName={0}", _jdLoginer.loginname);
            jdHttp.Cookies = _jdLoginer.cookies;
            string result = jdHttp.GetHtml(url,"post",Encoding.UTF8);
            if (result.ToLower().Contains("false"))
            {
                _isAuthcode = false;
                return false;
            }
            else
            {
                _isAuthcode = true;
                return true;
            }
        }

        /// <summary>
        /// 获取京东页面数据
        /// </summary>
        /// <param name="website"></param>
        /// <returns></returns>
        private string GetJDWebHtml(string website)
        {
            try
            {
                if (string.IsNullOrEmpty(website))
                {
                    return null;
                }
                JDHttpHelper jdHttp = new JDHttpHelper();
                jdHttp.ContentType = "text/html";
                Dictionary<string, string> headerDic = new Dictionary<string, string>();
                headerDic["Accept-Encoding"] = "gzip, deflate";
                jdHttp.Cookies = _jdLoginer.cookies;

                string result = jdHttp.GetHtml(website,"get",Encoding.UTF8,headerDic);

                if (jdHttp.Cookies != null)
                {
                    _jdLoginer.cookies = jdHttp.Cookies;
                }

                return result;
            }
            catch (Exception ex)
            {
                //ImportThreads.LastMsg = "失败：未获取到网页数据";
                //Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public class JDLoginer
        {
            /// <summary>
            /// 当前帐号临时编号
            /// </summary>
            public string uuid { get; set; }
            public string machineNet { get; set; }
            public string machineCpu { get; set; }
            public string machineDisk { get; set; }
            /// <summary>
            /// 随机数
            /// </summary>
            public string r { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string eid { get; set; }
            /// <summary>
            /// sessionId
            /// </summary>
            public string fp { get; set; }
            /// <summary>
            /// token
            /// </summary>
            public string _t { get; set; }
            /// <summary>
            /// key键
            /// </summary>
            public string tname { get; set; }
            /// <summary>
            /// key值
            /// </summary>
            public string tvalue { get; set; }
            /// <summary>
            /// 登录名
            /// </summary>
            public string loginname { get; set; }
            /// <summary>
            /// 登录密码
            /// </summary>
            public string nloginpwd { get; set; }
            /// <summary>
            /// 登录密码
            /// </summary>
            public string loginpwd { get; set; }
            /// <summary>
            /// 记住账号信息
            /// </summary>
            public string chkRememberMe { get; set; }
            /// <summary>
            /// 验证码
            /// </summary>
            public string authcode { get; set; }
            /// <summary>
            /// 网站本地缓存数据
            /// </summary>
            public string cookies { get; set; }
            
            /// <summary>
            /// RSA公共密钥 
            /// </summary>
            public string pubKey { get; set; }

            /// <summary>
            /// sa_token 
            /// </summary>
            public string sa_token { get; set; }
        }

        /// <summary>
        /// 清空购物车按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEmptyCart_Click(object sender, EventArgs e)
        {
            JDHelper.GetInstance().EmptyCart(_jdLoginer.cookies);
            lsvCart.Items.Clear();
        }

        /// <summary>
        /// 获取商品信息按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetWaresInfo_Click(object sender, EventArgs e)
        {
            Wares wares = JDHelper.GetInstance().GetWaresInfo(txtWaresId.Text);

            if(wares != null)
            {
                lsvFindWInfo.Items.Clear();
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = wares.id;
                lvItem.SubItems.Add(wares.title);
                lvItem.SubItems.Add(wares.price);
                lsvFindWInfo.Items.Add(lvItem);
            }
        }

        /// <summary>
        /// 添加购物车按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCart_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtWaresId.Text))
            {
                JDHelper.GetInstance().AddCart(_jdLoginer.cookies, txtWaresId.Text);

            }
        }

        /// <summary>
        /// 结算按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJieSuan_Click(object sender, EventArgs e)
        {
            if(lsvCart.SelectedItems.Count > 0)
            {
                string cartHtml = JDHelper.GetInstance().GetCartHtml(_jdLoginer.cookies);
                string strRegex = string.Format(@"<input p-type=""{0}_\d+"".*?value=""([\d_]+)""", lsvCart.SelectedItems[0].Text);

                //把选中商品需要的优惠ID匹配出来
                Match reg = Regex.Match(cartHtml, strRegex, RegexOptions.Singleline);
                string[] pat = reg.Groups[1].Value.Split('_');
                string targetId = "0";
                string ptype = pat[1];
                if(pat.Length >= 3)
                {
                    targetId = pat[2];
                }
                //先取消全部商品选中状态
                JDHelper.GetInstance().CancelAllCart(_jdLoginer.cookies);
                //勾选需要结算的物品
                JDHelper.GetInstance().SelectCart(lsvCart.SelectedItems[0].Text, _jdLoginer.cookies, ptype, targetId);
                //获取结算页面
                string orderHtml = JDHelper.GetInstance().GetOrderInfo(_jdLoginer.cookies);
                //提交订单
                Dictionary<string,string> valueDic = JDHelper.GetInstance().GetValueFormHtml(orderHtml,"id");
                string eid = "", fp = "", riskControl = "";
                valueDic.TryGetValue("eid", out eid);
                valueDic.TryGetValue("fp", out fp);
                valueDic.TryGetValue("riskControl", out riskControl);
                string submitHtml = JDHelper.GetInstance().SubmitOrder(_jdLoginer.cookies, eid, fp, riskControl);
                return;
            }
        }

        private void btnOrderList_Click(object sender, EventArgs e)
        {
            JDHelper.GetInstance().GetOrderList(_jdLoginer.cookies);
            txtMessage.AppendText("");
        }




    }
}
