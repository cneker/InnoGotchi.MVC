namespace InnoGotchi.MVC.Middlewares
{
    public class AddingAuthenticationHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public AddingAuthenticationHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Cookies["jwt"];
            if (token != null)
                context.Request.Headers.Append("Authorization", $"Bearer {token}");

            await _next(context);
        }
    }
}
