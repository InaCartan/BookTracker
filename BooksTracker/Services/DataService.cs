using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksTracker.Models;
using Supabase;

namespace BooksTracker.Services
{

    public class DataService : IDataService
    {
        private readonly Client _supabaseClient;

        public DataService(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public async Task AddNewBook(Book book)
        {
            await _supabaseClient.From<Book>().Insert(book);
        }

        public async Task DeleteBook(int id)
        {
            await _supabaseClient.From<Book>()
                .Where(b => b.Id == id).Delete(); // Why Id with capitalise 'I'
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            var response = await _supabaseClient.From<Book>().Get();
            return response.Models.OrderByDescending(b => b.MadeAt);

        }

        public async Task UpdateBook(Book book)
        {
            await _supabaseClient.From<Book>().Where(b => b.Id == book.Id)
                .Set(b => b.Title, book.Title)
                .Set(b => b.Author, book.Author)
                .Set(b => b.ImageUrl, book.ImageUrl)
                .Set(b => b.IsFinished, book.IsFinished)
                .Update();
        }
    }
}
