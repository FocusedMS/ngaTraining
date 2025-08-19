using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RajorTestDemo.Pages.Employ
{
    public class EmployIndexModel : PageModel
    {
        private readonly ILogger<EmployIndexModel> _logger;

        public EmployIndexModel(ILogger<EmployIndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}