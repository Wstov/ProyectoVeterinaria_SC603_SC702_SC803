using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB.Pages.Inventario
{
    [Authorize(Roles = "1")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
