// ** BismiIllah Ar-Rahmaan Ar-Raheem **
using BooksTracker.Models;
using BooksTracker.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BooksTracker.ViewModels
{
    // what is partiel?
    public partial class AddBookViewModel : ObservableObject
    {
        private readonly IDataService _dataService;
        private readonly BookListingViewModel _bookListingViewModel;

        // 1) ObservableProperty automatically generates a public property (with getter & setter) based on a private field
        //     - someone can't access the private field, thus he access it from a "generated public property"
        // 2) The public property is used for data binding and property change notification
        //     -> Data binding = links UI controls to view model properties
        //     -> property change notification = In Shaa Allah when properties change, the UI updates
        [ObservableProperty]
        private string _bookTitle;

        [ObservableProperty]
        private string _bookAuthor;

        [ObservableProperty]
        private string _bookImageUrl;

        [ObservableProperty]
        private bool _bookIsFinished;


        public AddBookViewModel(IDataService dataService, BookListingViewModel bookListingViewModel)
        {
            _dataService = dataService;
            _bookListingViewModel = bookListingViewModel;
        }

        [RelayCommand]
        private async Task AddBook()
        {

            try
            {
                if (!string.IsNullOrEmpty(BookTitle))
                {
                    Book book = new()
                    {
                        Title = BookTitle, //Title (database) <-> BookTitle (UI)
                        Author = BookAuthor,
                        ImageUrl = BookImageUrl,
                        IsFinished = BookIsFinished
                    };
                    await _dataService.AddNewBook(book);
                    await _bookListingViewModel.GetBooks(); // Refresh the list
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No title", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}



