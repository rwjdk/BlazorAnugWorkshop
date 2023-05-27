using System.Text.Json.Serialization;
using SharedModels.Model;

namespace AnugBlazorWorkshop.Services;

public interface IBookmarkService
{
    Task<List<Bookmark>> GetBookmarks();
    Task AddBookmark(Bookmark bookmark);
    Task UpdateBookmark(Bookmark bookmark);
    Task<Bookmark> GetBookmark(string bookmarkGuid);
    Task DeleteBookmark(Bookmark bookmark);
}

public class BookmarkService : IBookmarkService
{
    private readonly List<Bookmark> _dummyPersistance;

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

    public async Task<Bookmark> GetBookmark(string bookmarkGuid)
    {
        var match = (await GetBookmarks()).SingleOrDefault(x => x.Guid == bookmarkGuid) ?? new Bookmark() { Guid = bookmarkGuid };
        return new Bookmark
        {
            Guid = match.Guid,
            Title = match.Title,
            Description = match.Description,
            Url = match.Url
        };
    }

    public async Task DeleteBookmark(Bookmark bookmark)
    {
        await Task.CompletedTask;
        if (_dummyPersistance.Exists(x => x.Guid == bookmark.Guid))
        {
            _dummyPersistance.Remove(bookmark);
        }
    }

    public async Task UpdateBookmark(Bookmark updatedBookmark)
    {
        await Task.CompletedTask; //to get rid of warning for now
        var existingBookmark = _dummyPersistance.FirstOrDefault(x => x.Guid == updatedBookmark.Guid);
        if (existingBookmark != null)
        {
            existingBookmark.Title = updatedBookmark.Title;
            existingBookmark.Description = updatedBookmark.Description;
            existingBookmark.Url = updatedBookmark.Url;
        }
        else
        {
            await AddBookmark(updatedBookmark); //Or if you rater like it ignore or throw error
        }
    }
}