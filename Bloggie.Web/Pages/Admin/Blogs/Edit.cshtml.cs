using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.Blogs
{
    public class EditModel : PageModel
    {
        public BloggieDbContext bloggieDbContext { get; set; }
        [BindProperty]
        public BlogPost BlogPost { get; set; }
        public EditModel(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }



        public void OnGet(Guid id)
        {
            BlogPost = bloggieDbContext.BlogPosts.Find(id);
        }
        public async Task<IActionResult> OnPostEdit()
        {
            var blogPost = bloggieDbContext.BlogPosts.Find(BlogPost.Id);
            if (blogPost != null)
            {
                blogPost.Heading = BlogPost.Heading;
                blogPost.PageTitle = BlogPost.PageTitle;
                blogPost.Content = BlogPost.Content;
                blogPost.ShortDescription = BlogPost.ShortDescription;
                blogPost.FeaturedImageUrl = BlogPost.FeaturedImageUrl;
                blogPost.UrlHandle = BlogPost.UrlHandle;
                blogPost.PublishedDate = BlogPost.PublishedDate;
                blogPost.Author = BlogPost.Author;
                blogPost.Visible = BlogPost.Visible;
            }
            await bloggieDbContext.SaveChangesAsync();
            return RedirectToPage("/Admin/Blogs/List");
        }
        public async Task<IActionResult> OnPostDelete()
        {
            var blogPost = await bloggieDbContext.BlogPosts.FindAsync(BlogPost.Id);
            if (blogPost != null)
            {
                bloggieDbContext.BlogPosts.Remove(blogPost);
            }
            await bloggieDbContext.SaveChangesAsync();
            return RedirectToPage("/Admin/Blogs/List");
        }
    }
}
