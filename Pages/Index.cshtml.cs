using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AdviceGeneratorWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public IActionResult OnGetRefresh()
        {
            return RedirectToPage("Index");
        }


        [BindProperty(SupportsGet = true)]
        public adviceResponseObj? deserialAdvice { get; set; }
        public async Task OnGetAsync()
        {
           var client = new HttpClient();
            var adviceUrl = "https://api.adviceslip.com/advice";
            var adviceResponse = await client.GetAsync(adviceUrl);
            adviceResponse.EnsureSuccessStatusCode();
            var responseContent = await adviceResponse.Content.ReadAsStringAsync();
             deserialAdvice = JsonConvert.DeserializeObject<adviceResponseObj>(responseContent);
        }
    }
}

public class adviceResponseObj
{
    public adviceInnerObj slip { get; set; }
}

public class adviceInnerObj
{
    public int id { get; set; }
    public string advice { get; set; }
}

