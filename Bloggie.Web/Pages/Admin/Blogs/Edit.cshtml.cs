using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.Web.Pages.Admin.Blogs
{
    public class EditModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;

        [BindProperty]
        public BlogPost BlogPost { get; set; }
        [BindProperty]
        public string Tags { get; set; }
        public IFormFile FeaturedImage { get; set; }


        public EditModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        public async Task OnGet(Guid id)
        {
            BlogPost = await blogPostRepository.GetAsync(id);
            if (BlogPost != null && BlogPost.Tags != null)
            {
                Tags = string.Join(",", BlogPost.Tags.Select(x => x.Name));
            }
        }
        public async Task<IActionResult> OnPostEdit()
        {
            BlogPost.Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag() { Name = x.Trim() }));
            var blogPost = await blogPostRepository.UpdateAsync(BlogPost);
            try
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "Record updated successfully!",
                    Type = Enums.NotificationType.Success
                };
            }
            catch (Exception ex)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "Record Update failed!",
                    Type = Enums.NotificationType.Error
                };
            }
            return Page();
        }
        public async Task<IActionResult> OnPostDelete()
        {
            bool success = await blogPostRepository.DeleteAsync(BlogPost.Id);
            if (success)
            {
                var notification = new Notification
                {
                    Message = "Blog deleted successfully!",
                    Type = Enums.NotificationType.Success
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/Admin/Blogs/List");
            }
            return Page();

        }
    }
}
