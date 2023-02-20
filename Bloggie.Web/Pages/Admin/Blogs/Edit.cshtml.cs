using Bloggie.Web.Models.Domain;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.Blogs
{
    public class EditModel : PageModel
    {
        private readonly IBlogPostRepository blogPostRepository;

        [BindProperty]
        public BlogPost BlogPost { get; set; }


        public EditModel(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        public async Task OnGet(Guid id)
        {
            BlogPost = await blogPostRepository.GetAsync(id);
        }
        public async Task<IActionResult> OnPostEdit()
        {
            var blogPost = await blogPostRepository.UpdateAsync(BlogPost);
            return RedirectToPage("/Admin/Blogs/List");
        }
        public async Task<IActionResult> OnPostDelete()
        {
            bool success = await blogPostRepository.DeleteAsync(BlogPost.Id);
            if (success)
            {
                return RedirectToPage("/Admin/Blogs/List");
            }
            return Page();

        }
    }
}
