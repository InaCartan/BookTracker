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

        // In Shaa Allah, ObservableCollection is used to update the ui if a change happens, thus Books updates the ui
        public ObservableCollection<Book> Books { get; set; } = new();

        public BookListingViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }


        // this method is doing the following:
        // "GetBooks" in the file, DataService, gets the books from the database
        // "GetBooks" (the func below) clear the book collection it currently have
        // then calls the function from DataService, and update it's book collection
        // then it updates the ui's collections of books
        [RelayCommand]
        public async Task GetBooks()
        {
            // In Shaa Allah, removes all elements from the collection
            Books.Clear();

            try
            {
                
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

        // In Shaa Allah, this func navigates to AddBookPage.xaml.cs (not the viewmodel)
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


        // In Shaa Allah, this func navigates to UpdateBookPage.xaml.cs (not the viewmodel)
        [RelayCommand]
        private async Task UpdateBook(Book book) => await Shell.Current.GoToAsync("UpdateBookPage", new Dictionary<string, object>
        {
            {"BookObject", book }
        });


    }
}
