using AnugBlazorWorkshop.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using SharedModels.Model;

namespace AnugBlazorWorkshop.ViewModel;

public class AddEditViewModel
{
    private readonly IBookmarkService _bookmarkService;
    private readonly NavigationManager _navigationManager;
    public Bookmark Bookmark;
    public bool IsAddMode;
    public string SubmitButtonCaption;
    public string PageTitle;

    public AddEditViewModel(IBookmarkService bookmarkService, NavigationManager navigationManager)
    {
        _bookmarkService = bookmarkService;
        _navigationManager = navigationManager;
    }

    internal async Task Initialize(string bookmarkGuid)
    {
        IsAddMode = string.IsNullOrWhiteSpace(bookmarkGuid); //If no GUID we are in Add Mode
        if (IsAddMode)
        {
            Bookmark = new Bookmark();
            SubmitButtonCaption = "Add";
            PageTitle = "Add Bookmark";
        }
        else
        {
            Bookmark = await _bookmarkService.GetBookmark(bookmarkGuid);
            SubmitButtonCaption = "Update";
            PageTitle = $"Update Bookmark '{Bookmark.Title}'";
        }
    }

    internal void FormSubmitted(EditContext editContext)
    {
        bool formIsValid = editContext.Validate();
        if (formIsValid)
        {
            if (IsAddMode)
            {
                Bookmark.Guid = Guid.NewGuid().ToString();
                _bookmarkService.AddBookmark(Bookmark);
            }
            else
            {
                _bookmarkService.UpdateBookmark(Bookmark);
            }
            _navigationManager.NavigateTo("bookmarksMud");
            
        }
    }
}