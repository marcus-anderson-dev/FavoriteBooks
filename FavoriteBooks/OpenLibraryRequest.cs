using System.Text.Json.Serialization;

namespace FavoriteBooks.Models;

public class SearchResult
{
    public List<BookFromApi> Docs {get; set;} = new();
}

public class BookFromApi
{
    public string Key {get; set;} = "";
    public string Title {get; set;} = "";

    [JsonPropertyName("author_name")]
    public List<string>? AuthorName {get; set;}

    [JsonPropertyName("first_publish_year")]
    public int? FirstPublishYear {get; set;}

    [JsonPropertyName("cover_i")]
    public int? CoverId {get; set;}
}