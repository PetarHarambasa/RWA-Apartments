using System;

namespace WebApplication
{
    public class DefaultPage : System.Web.UI.Page
    {

        public int DDLThemeIndex
        {
            get
            {
                if (Request.Cookies["theme"] != null)
                {
                    if (Request.Cookies["theme"]["index"] != null)
                    {
                        return int.Parse(Request.Cookies["theme"]["index"]);
                    }
                }

                return 0;
            }
            set
            {
                Request.Cookies["theme"]["index"] = value.ToString();
            }
        }
        public DefaultPage()
        {

        }

        protected override void OnPreInit(EventArgs e)
        {
            if (Request.Cookies["theme"] != null)
            {
                if (Request.Cookies["theme"]["theme"] != null)
                {
                    Theme = Request.Cookies["theme"]["theme"];
                }
            }
            base.OnPreInit(e);
        }
    }
}