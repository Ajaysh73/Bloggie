using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.Web.Pages.Admin.Blogs
{
    public class ListModel : PageModel
    {
        //private readonly BloggieDbContext bloggieDbContext;
        private readonly IBlogPostRepository blogPostRepository;

        public List<BlogPost> BlogPosts { get; set; }

        public ListModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        public async Task OnGet()
        {
            var notification = TempData["Notification"];
            if (notification != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notification.ToString());
            }
            //var messageDescription = (string)TempData["MessageDescription"];
            //if (!string.IsNullOrWhiteSpace(messageDescription))
            //{
            //    ViewData["MessageDescription"] = messageDescription;
            //}
            BlogPosts = (await blogPostRepository.GetAllAsync()).ToList();
        }
    }
}
