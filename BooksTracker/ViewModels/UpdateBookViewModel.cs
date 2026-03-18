using BooksTracker.Models;
using BooksTracker.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BooksTracker.ViewModels;

[QueryProperty(nameof(Book), "BookObject")]
public partial class UpdateBookViewModel : ObservableObject
{
    private readonly IDataService _dataService;


    // 1) ObservableProperty automatically generates a public property (with getter & setter) based on a private field
    //     - someone can't access the private field, thus he access it from a "generated public property"
    // 2) The public property is used for data binding and property change notification
    //     -> Data binding = links UI controls to view model properties
    //     -> property change notification = In Shaa Allah when properties change, the UI updates
    [ObservableProperty]
    private Book _book;

    public UpdateBookViewModel(IDataService dataService)
    {
        _dataService = dataService;
    }

    [RelayCommand]
    private async Task UpdateBook()
    {
        if (!string.IsNullOrEmpty(Book.Title))
        {
            await _dataService.UpdateBook(Book);

            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "No title!", "OK");
        }
    }
}