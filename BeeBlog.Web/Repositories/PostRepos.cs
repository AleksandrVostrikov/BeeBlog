﻿using BeeBlog.Web.Data;
using BeeBlog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BeeBlog.Web.Repositories
{
    public class PostRepos : IPostRepos
    {
        private readonly BeeBlogDbContext _beeBlogDbContext;

        public PostRepos(BeeBlogDbContext beeBlogDbContext)
        {
            _beeBlogDbContext = beeBlogDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await _beeBlogDbContext.BlogPosts.AddAsync(blogPost);
            await _beeBlogDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingBlogPost = await _beeBlogDbContext.BlogPosts.FindAsync(id);
            if (existingBlogPost != null)
            {
                _beeBlogDbContext.Remove(existingBlogPost);
                await _beeBlogDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<BlogPost>> GetAllPostsAsync()
        {
            return await _beeBlogDbContext.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost> GetPostAsync(Guid id)
        {
           return await _beeBlogDbContext.BlogPosts.FindAsync(id);
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await _beeBlogDbContext.BlogPosts.FindAsync(blogPost.Id);
            if (existingBlogPost != null)
            {
                existingBlogPost.Heading = blogPost.Heading;
                existingBlogPost.PageTitle = blogPost.PageTitle;
                existingBlogPost.PageContent = blogPost.PageContent;
                existingBlogPost.ShortDescription = blogPost.ShortDescription;
                existingBlogPost.ImageURL = blogPost.ImageURL;
                existingBlogPost.PageTitle = blogPost.PageTitle;
                existingBlogPost.URLhandle = blogPost.URLhandle;
                existingBlogPost.DateOfPublication = blogPost.DateOfPublication;
                existingBlogPost.Author = blogPost.Author;
                existingBlogPost.IsVisible = blogPost.IsVisible;
            }
            await _beeBlogDbContext.SaveChangesAsync();
            return existingBlogPost;
        }
    }
}
