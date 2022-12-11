using System.Text.Json;
using SharedModels.Model;

namespace AnugBlazorWorkshop.Services;

public class BookmarkServiceReal : IBookmarkService
{
    private const string GetUrl = "http://localhost:7084/api/GetBookmarks";
    private const string UpdateUrl = "http://localhost:7084/api/AddUpdateBookmark";
    private const string DeleteUrl = "http://localhost:7084/api/DeleteBookmark";
    //private const string GetUrl = "https://anugbookmark.azurewebsites.net/api/GetBookmarks";
    //private const string UpdateUrl = "https://anugbookmark.azurewebsites.net/api/AddUpdateBookmark";
    //private const string DeleteUrl = "https://anugbookmark.azurewebsites.net/api/DeleteBookmark";
    private const string MyUserId = "RWJ";

    public HttpClient HttpClient { get; }

    public BookmarkServiceReal(HttpClient httpClient)
    {
        HttpClient = httpClient;
        HttpClient.DefaultRequestHeaders.Add("UserId", MyUserId);
    }

    public async Task<List<Bookmark>> GetBookmarks()
    {
        try
        {
            var response = await HttpClient.GetAsync(new Uri(GetUrl));
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Bookmark>>(json);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task AddBookmark(Bookmark bookmark)
    {
        await AddUpdateBookmark(bookmark);
    }

    public async Task UpdateBookmark(Bookmark updatedBookmark)
    {
        await AddUpdateBookmark(updatedBookmark);
    }

    private async Task AddUpdateBookmark(Bookmark bookmark)
    {
        var bookmarkAsJson = JsonSerializer.Serialize(bookmark);
        HttpContent content = new StringContent(bookmarkAsJson);
        await HttpClient.PostAsync(new Uri(UpdateUrl), content);
    }

    public async Task<Bookmark> GetBookmark(string bookmarkGuid)
    {
        return (await GetBookmarks()).FirstOrDefault(x => x.Guid == bookmarkGuid) ?? new Bookmark() { Guid = bookmarkGuid }; //treat error scenario as you like
    }

    public async Task DeleteBookmark(Bookmark bookmark)
    {
        var bookmarkAsJson = JsonSerializer.Serialize(bookmark);
        HttpContent content = new StringContent(bookmarkAsJson);
        await HttpClient.PostAsync(new Uri(DeleteUrl), content);
    }
}