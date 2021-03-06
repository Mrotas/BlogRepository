﻿using BlogRepository.DataAccess.Collection;
using System.Collections.Generic;

namespace BlogRepository.DataAccess.Dao.Interfaces
{
    public interface ICommentLikeDao
    {
        List<CommentLike> GetByCommentId(int commentId);
        void Insert(int commentId, int? userId);
        void Delete(int commentLikeId);
    }
}