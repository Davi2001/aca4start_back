using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Blogic.Authentication
{
    public class BasicAutorizationAttributes:AuthorizeAttribute
    {
        public BasicAutorizationAttributes() {
            Policy = "BasicAuthentication";
        }
    }
}
