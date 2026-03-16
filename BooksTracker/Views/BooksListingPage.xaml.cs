using BooksTracker.ViewModels;
namespace BooksTracker.Views;

public partial class BooksListingPage : ContentPage
{
	public BooksListingPage(BookListingViewModel booksListingViewModel)
	{
		InitializeComponent();
        BindingContext = booksListingViewModel;
    }
}