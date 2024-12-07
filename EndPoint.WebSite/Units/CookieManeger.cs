namespace EndPoint.WebSite.Units
{
    public class CookieManeger
    {
        public void Add(HttpContext context, string name, string value)
        {
            context.Response.Cookies.Append(name, value, getCookieOptions(context));
        }

        public void Remove(HttpContext context, string name)
        {
            if (Any(context, name))
                context.Response.Cookies.Delete(name);

        }
        public bool Any(HttpContext context, string name)
        {
            return context.Request.Cookies.ContainsKey(name);
        }
        public string Get(HttpContext context, string name)
        {
            string value;
            _ = context.Request.Cookies.TryGetValue(name, out value);

            return value ?? "";
        }
        public Guid GetBrowserId(HttpContext context)
        {
            string name = "Browser";
            string val = Get(context, name);
            if (string.IsNullOrEmpty(val))
            {
                Guid guid = Guid.NewGuid();
                val = guid.ToString();
                Add(context, name, val);
            }
            return Guid.Parse(val);
        }
        private CookieOptions getCookieOptions(HttpContext context)
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Path = context.Request.PathBase.HasValue ? context.Request.PathBase.ToString() : "/",
                Secure = context.Request.IsHttps,
                Expires = DateTime.Now.AddDays(100),
            };
        }
    }
}
