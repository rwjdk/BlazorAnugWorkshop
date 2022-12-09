﻿using SharedModels.Model;

namespace AnugBlazorWorkshop.Services;

public interface IBookmarkService
{
    Task<List<Bookmark>> GetBookmarks();
    Task AddBookmark(Bookmark bookmark);
}

public class BookmarkService : IBookmarkService
{
    public List<Bookmark> _dummyPersistance = new List<Bookmark>();

    public BookmarkService()
    {
        _dummyPersistance = new List<Bookmark>()
        {
            new Bookmark() { Guid = "3a1e18db-d585-41ec-ad7c-3ba0088575c6", Title = "Google", Url = "https://www.google.com", Description = "Search Engine" },
            new Bookmark() { Guid = "58ee60eb-e67a-4962-afd3-36d05121e607", Title = "Microsoft", Url = "https://www.microsoft.com", Description = "The people that made it possible not to write Javascript!" },
            new Bookmark() { Guid = "b5684299-4da1-46ae-8f29-d94fa1265cbc", Title = "ANUG", Url = "http://www.anug.dk", Description = "Cool .NET UserGroup" }
        };
    }

    public async Task<List<Bookmark>> GetBookmarks()
    {
        await Task.CompletedTask; //to get rid of warning for now
        return _dummyPersistance;
    }

    public async Task AddBookmark(Bookmark bookmark)
    {
        await Task.CompletedTask; //to get rid of warning for now
        _dummyPersistance.Add(bookmark);
    }
}