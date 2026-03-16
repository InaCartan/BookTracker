using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksTracker.Models;

namespace BooksTracker.Services
{
    public interface IDataService
    {
        Task<IEnumerable<Book>> GetBooks();
        Task AddNewBook(Book book);
        Task DeleteBook(int id);
        Task UpdateBook(Book book);
    }
}
