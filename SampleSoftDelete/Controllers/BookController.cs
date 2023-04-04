using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleSoftDelete.Dtos;
using SampleSoftDelete.Entities;
using SampleSoftDelete.Repositories.Interfaces;

namespace SampleSoftDelete.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        return Ok(await _bookRepository.GetListAsync(new GetListRequest()
        {
            IsGettingDeletedItem = false
        }));
    }
    [HttpGet("get-deleted")]
    public async Task<IActionResult> GetListDeleted ()
    {
        return Ok(await _bookRepository.GetListAsync(new GetListRequest()
        {
            IsGettingDeletedItem = true
        }));
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookRequest request)
    {
        return Ok(await _bookRepository.CreateBookAsync(request));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBook(DeleteBookRequest request)
    {
        return Ok(await _bookRepository.DeleteBookAsync(request));
    }
}
