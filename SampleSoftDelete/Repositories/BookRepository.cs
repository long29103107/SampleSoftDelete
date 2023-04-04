using Microsoft.EntityFrameworkCore;
using SampleSoftDelete.Database;
using SampleSoftDelete.Dtos;
using SampleSoftDelete.Entities;
using SampleSoftDelete.Repositories.Interfaces;

namespace SampleSoftDelete.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Book> CreateBookAsync(CreateBookRequest request)
    {
        var book = new Book()
        {
            Title = request.Title,
            Author = request.Author
        };

        _context.Book.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<bool> DeleteBookAsync(DeleteBookRequest request)
    {
        var book = await GetDetailAsync(request.Id);
        if(book == null)
        {
            throw new Exception("Book is not found !");
        }
        _context.Book.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Book?> GetDetailAsync(int id)
    {
        return await _context.Book.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Book>> GetListAsync(GetListRequest request)
    {
        var query = _context.Book;
        if (request.IsGettingDeletedItem)
           return await query.IgnoreQueryFilters().ToListAsync();

        return await query.ToListAsync();
    }
}
