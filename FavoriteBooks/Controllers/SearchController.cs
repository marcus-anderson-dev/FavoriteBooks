using Microsoft.AspNetCore.Mvc;
using FavoriteBooks.Models;

namespace FavoriteBooks.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    private readonly HttpClient _http;

    public SearchController(IHttpClientFactory factory)
    {
        _http = factory.CreateClient();
        _http.DefaultRequestHeaders.UserAgent.ParseAdd("FavoriteBooks-StudentProject");
    }

    [HttpGet]
    public async Task<ActionResult<List<Book>>> Search(string q)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return BadRequest("Please type something to search for.");
        }

        SearchResult? result;
        try
        {
            string address = "https://openlibrary.org/search.json?q=" + Uri.EscapeDataString(q)
                + "&limit=10&fields=key,title,author_name,first_publish_year,cover_i";

            result = await _http.GetFromJsonAsync<SearchResult>(address);
        }
        catch (HttpRequestException)
        {
            return StatusCode(502, "Unable to reach Open Library, check Internet Connection.");
        }

        var books = new List<Book>();

        if (result != null)
        {
            foreach (BookFromApi item in result.Docs)
            {
                var book = new Book
                {
                    Key = item.Key,
                    Title = item.Title,
                    Author = item.AuthorName?.FirstOrDefault() ?? "Unknown author",
                    Year = item.FirstPublishYear ?? 0,
                    CoverId = item.CoverId ?? 0
                };

                books.Add(book);
            }
        }

        return Ok(books);
    }
}
