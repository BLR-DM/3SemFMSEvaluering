﻿using FMSEvaluering.Domain.Entities;

namespace FMSEvaluering.Application.Repositories;

public interface IPostRepository
{
    Task AddPost(Post post);
    Task<Post> GetPost(int id);
    Task AddVote();
    Task DeleteVote();
    Task UpdateVote();
}