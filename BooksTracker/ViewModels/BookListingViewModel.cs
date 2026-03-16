using BooksTracker.Models;
using BooksTracker.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BooksTracker.ViewModels
{
    // what is partial?
    public partial class BookListingViewModel
    {
        private readonly IDataService _dataService;

        // In Shaa Allah, we get notified when an item is added/removed & if the whole list is refreshed
        public ObservableCollection<Book> Books { get; set; } = new();

        public BookListingViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        [RelayCommand]
        public async Task GetBooks()
        {
            // In Shaa Allah, removes all elements from the collection
            Books.Clear();

            try
            {
                //should "books" not be returned so it's value can be used?
                var books = await _dataService.GetBooks();

                if (books.Any()) //In Shaa Allah, "Any" checks if there contains something in books
                {
                    foreach(var book in books)
                    {
                        Books.Add(book);
                    }
                }
            }

            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
            
        }

        [RelayCommand]
        private async Task AddBook() => await Shell.Current.GoToAsync("AddBookPage");

        [RelayCommand]
        private async Task DeleteBook(Book book)
        {
            var result = await Shell.Current.DisplayAlert("Delete", $"Are you sure you want to delete \"{book.Title}\"?", "Yes", "No");

            if (result is true)
            {
                try
                {
                    await _dataService.DeleteBook(book.Id);
                    await GetBooks();
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
                }
            }

        }

        [RelayCommand]
        private async Task UpdateBook(Book book) => await Shell.Current.GoToAsync("UpdateBookPage", new Dictionary<string, object>
        {
            {"BookObject", book }
        });


    }
}
