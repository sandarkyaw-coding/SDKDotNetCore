using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDKDotNetCore.MvcApp.Models;

[Table("Tbl_Blog")]

public class BlogModel
{
    [Key]
    public int BlogId { get; set; }
    public string? BlogTitle { get; set; }

    public string? BlogAuthor { get; set; }

    public string? BlogContent { get; set; }
}

//lasted way
//public record BlogEntity(int BlogId, string BlogTitle, string BlogAuthor, string BlogContent);
