namespace FavoriteBooks.Models;

public class Book
{
    public int Id {get; set;}
    public string Key {get; set;} = "";
    public string Title {get; set;} = "";
    public string Author {get; set;} = "";
    public int Year {get; set;}
    public int CoverId {get; set;}


    public string CoverUrl
    {
        get
        {
            if (CoverId == 0)
            {
                return "";
            }

            return "https://covers.openlibrary.org/b/id/" + CoverId + "-L.jpg";
        }
    }

    public string Era
    {
        get
        {
            if (Year == 0)
            {
                return "Unknown";
            }

            if (Year < 1950)
            {
                return "Classic";
            }

            return "Modern";
        }
    }
}