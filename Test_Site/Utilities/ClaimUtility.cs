using System.Security.Claims;

namespace Test_Site.Utilities
{
    public static class ClaimUtility
    {
        public static long? GetUserId(ClaimsPrincipal user) 
        {

            try
            {
                var claimsIdentity = user.Identity as ClaimsIdentity;
               
                long userId = long.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                return userId;
            }
            catch (Exception)
            {

               return null;
            }
            
        }
    }
}
