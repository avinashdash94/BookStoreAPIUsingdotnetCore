using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            //Getting all books Data and converting into BookModel type by selecting each Book Data
            var records = await _context.Books.Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            })
            .ToListAsync();

            return records;
        }

        public async Task<BookModel> GetBookByIdAsync(int bookId)
        {
            //Getting all books Data and converting into BookModel type by selecting each Book Data
            var records = await _context.Books.Where(x => x.Id == bookId).Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            })
            .FirstOrDefaultAsync();

            return records;
        }

        public async Task<int> AddBookAsync(BookModel bookModel)
        {
            //Getting all books Data and converting into BookModel type by selecting each Book Data
            var book = new Books()
            {
                Title = bookModel.Title,
                Description = bookModel.Description
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book.Id;
        }

        public async Task UpdateBookAsync(int bookId, BookModel bookModel)
        {
            //**First approach to upodate
            //*****In this appraoch we hit Db Two time one to get the book data which we want to update and againg to save the changes
            //var book = await _context.Books.FindAsync(bookId);

            //if(book != null)
            //{
            //    book.Title = bookModel.Title;
            //    book.Description = bookModel.Description;

            //    await _context.SaveChangesAsync();
            //};
            ///**End of above approach


            //Second Approach in which directly we send the updated data to update with Id
            var book = new Books()
            {
                Id = bookId,
                Title = bookModel.Title,
                Description = bookModel.Description
            };

            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateBookPatchAsync(int bookId, JsonPatchDocument bookModel)
        {
            var book = await _context.Books.FindAsync(bookId);

            if (book != null)
            {
                bookModel.ApplyTo(book);
                await _context.SaveChangesAsync();
            }

        }
        public async Task DeleteBookAsync(int bookId)
        {
            //** if we have some other primary key or unique key then we first find the book then Remove it
            //e.g.  var book = awiat _context.Books.Where(x => x.Title = title).FirstDefault();
            //As book Id is primary key so we create an Books obj with same id and remove it 
            var book = new Books() { Id = bookId };

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
