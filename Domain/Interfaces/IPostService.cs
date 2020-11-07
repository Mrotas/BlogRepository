﻿using BlogRepository.Models;
using System.Collections.Generic;

namespace BlogRepository.Domain.Interfaces
{
    public interface IPostService
    {
        List<PostViewModel> GetPostViewModelsByBlogId(int blogId);
        PostViewModel GetPostViewModelByPostId(int postId);
    }
}