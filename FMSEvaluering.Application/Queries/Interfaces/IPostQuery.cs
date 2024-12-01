﻿using FMSEvaluering.Application.Queries.QueryDto;

namespace FMSEvaluering.Application.Queries.Interfaces;

public interface IPostQuery
{
    Task<PostDto> GetPostAsync(int postId, string appUserId, string role);
    Task<IEnumerable<PostDto>> GetPostsAsync(int forumId, string appUserId, string role);
}