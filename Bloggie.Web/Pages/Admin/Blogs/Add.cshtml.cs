using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.Web.Pages.Admin.Blogs
{
    public class AddModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;

        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }
        [BindProperty]
        public IFormFile FeaturedImage { get; set; }
        [BindProperty]
        public string Tags { get; set; }
        public AddModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(object jsonSerializer)
        {
            var blogPost = new BlogPost()
            {
                Heading = AddBlogPostRequest.Heading,
                PageTitle = AddBlogPostRequest.PageTitle,
                Content = AddBlogPostRequest.Content,
                ShortDescription = AddBlogPostRequest.ShortDescription,
                FeaturedImageUrl = AddBlogPostRequest.FeaturedImageUrl,
                UrlHandle = AddBlogPostRequest.UrlHandle,
                PublishedDate = AddBlogPostRequest.PublishedDate,
                Author = AddBlogPostRequest.Author,
                Visible = AddBlogPostRequest.Visible,
                Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag() { Name = x.Trim() }))
            };

            await blogPostRepository.AddAsync(blogPost);
            var notification = new Notification
            {
                Message = "New blog added successfully!",
                Type = Enums.NotificationType.Success
            };
            TempData["Notification"] = JsonSerializer.Serialize(notification);

            //TempData["MessageDescription"] = "New blog post created!";
            return RedirectToPage("/Admin/Blogs/List");
        }
    }
}
