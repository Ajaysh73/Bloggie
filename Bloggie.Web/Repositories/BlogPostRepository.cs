using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        async Task<BlogPost> IBlogPostRepository.AddAsync(BlogPost blogPost)
        {
            await bloggieDbContext.BlogPosts.AddAsync(blogPost);
            await bloggieDbContext.SaveChangesAsync();
            return blogPost;
        }

        async Task<IEnumerable<BlogPost>> IBlogPostRepository.GetAllAsync()
        {
            return await bloggieDbContext.BlogPosts.ToListAsync();
        }

        async Task<BlogPost> IBlogPostRepository.GetAsync(Guid id)
        {
            var blogPost = await bloggieDbContext.BlogPosts.FindAsync(id);
            return blogPost;
        }

        async Task<BlogPost> IBlogPostRepository.UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await bloggieDbContext.BlogPosts.FindAsync(blogPost.Id);
            if (existingBlogPost != null)
            {
                existingBlogPost.Heading = blogPost.Heading;
                existingBlogPost.PageTitle = blogPost.PageTitle;
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.ShortDescription = blogPost.ShortDescription;
                existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlogPost.UrlHandle = blogPost.UrlHandle;
                existingBlogPost.PublishedDate = blogPost.PublishedDate;
                existingBlogPost.Author = blogPost.Author;
                existingBlogPost.Visible = blogPost.Visible;
            }
            await bloggieDbContext.SaveChangesAsync();
            return existingBlogPost;
        }

        async Task<bool> IBlogPostRepository.DeleteAsync(Guid id)
        {
            var blogPost = await bloggieDbContext.BlogPosts.FindAsync(id);
            if (blogPost != null)
            {
                bloggieDbContext.BlogPosts.Remove(blogPost);
                await bloggieDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
