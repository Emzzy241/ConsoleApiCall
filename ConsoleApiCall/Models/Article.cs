using System.Collections.Generic;

namespace ApiTest
{
  public class Article
  {
    public string Section { get; set; }
    public string Title { get; set; }
    public string Abstract { get; set; }
    public string Url { get; set; }
    public string Byline { get; set; }
    public string Item_Type { get; set; }
    public List<Multimedia> Multimedia { get; set; }
  }
}