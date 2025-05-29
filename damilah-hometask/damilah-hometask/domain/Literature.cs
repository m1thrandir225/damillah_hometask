namespace damilah_hometask.domain;

public class Literature(string name, string url, string author, DateTime publishDate)
{
    public string Name { get; set; } = name;
    public string Url { get; set; } = url;
    public string Author { get; set; } = author;
    public DateTime PublishDate { get; set; } = publishDate;

    public override string ToString()
    {
        return $"Name: {Name}, Url: {Url}, Author: {Author}, PublishDate: {PublishDate.ToShortDateString()}";
    }
}