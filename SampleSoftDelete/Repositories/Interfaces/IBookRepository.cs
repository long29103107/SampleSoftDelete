using SampleSoftDelete.Dtos;
using SampleSoftDelete.Entities;

namespace SampleSoftDelete.Repositories.Interfaces;

public interface IBookRepository
{ 
    Task<List<Book>> GetListAsync(GetListRequest request);
    Task<Book?> GetDetailAsync(int id);
    Task<Book> CreateBookAsync(CreateBookRequest request);
    Task<bool> DeleteBookAsync(DeleteBookRequest request);
}
