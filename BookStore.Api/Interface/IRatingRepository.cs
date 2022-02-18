﻿using BookStore.Api.Models;

namespace BookStore.Api.Interface
{
    public interface IRatingRepository
    {
        Task<bool> AddRating(Rating rating);
        Task Create(string isbn, int user_id, int stars, string comment);


    }
}
