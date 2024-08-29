using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using ConsoleApiCall.Keys;
using ApiTest;

namespace ApiTest
{
   public class Program 
  {
    static void Main()
    {
      Task<string> apiCallTask = ApiHelper.ApiCall(EnvironmentVariables.ApiKey);
      string result = apiCallTask.Result;
      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
      
      List<Article> articleList = JsonConvert.DeserializeObject<List<Article>>(jsonResponse["results"].ToString());

      foreach (Article article in articleList)
      {
        Console.WriteLine($"Section: {article.Section}");
        Console.WriteLine($"Title: {article.Title}");
        Console.WriteLine($"Abstract: {article.Abstract}");
        Console.WriteLine($"Url: {article.Url}");
        Console.WriteLine($"Byline: {article.Byline}");
        Console.WriteLine($"Item_Type: {article.Item_Type}");
        if (article.Multimedia != null)
        {
          foreach (Multimedia media in article.Multimedia)
          {
              Console.WriteLine("---------");
              Console.WriteLine($"MULTIMEDIA");
              Console.WriteLine($"Type: {media.Type}");
              Console.WriteLine($"SubType: {media.SubType}");
              Console.WriteLine($"Caption: {media.Caption}");
          }
        }
        Console.WriteLine("__________________________________");
      }
    }
  }

      /*
        In the Main() method, we create a variable to store the returned Task<string> from our async function and then call the ApiHelper class' ApiCall method. It's here that we pass in our New York Times API key via our environment variable EnvironmentVariables.ApiKey.

        Then, we create a variable to store the Result of the Task, which in our case is a string representation of the API call's response content.
        JsonConvert.DeserializeObject()<JObject>(result) converts the JSON-formatted string result into a JObject.
        The type JObject comes from the Newtonsoft.Json.Linq library and is a .NET object we can treat as JSON.
        Now let's take a closer look at the code. We use the DeserializeObject() method to create a list of Articles. The method will automatically grab any JSON keys in our response that match the names of the properties in our class. In order for this to work, the property name has to match the JSON key. This means that the Section property for our message class needs to be named Section. We could not rename it to something like Category because the information is named "section" in the JSON data.
      */
  class ApiHelper
  {
    public static async Task<string> ApiCall(string apiKey)
    {
      RestClient client = new RestClient("https://api.nytimes.com/svc/topstories/v2");
      RestRequest request = new RestRequest($"home.json?api-key={apiKey}", Method.Get);
      RestResponse response = await client.ExecuteAsync(request);
      return response.Content;

    /*
        We'll first take a look at the ApiCall static method that we've created:

        We create a class called ApiHelper that contains a static method ApiCall which takes an apiKey parameter.

        We want our API calls to run asynchronously so that the application is responsive and free to run other tasks while the HTTP request/response loop executes. In order to achieve this, we add the async keyword to our method declaration.

        Whenever a method is declared as async, we need to return a Task type. We specify the return type of our Task object (a string) in the angle brackets, but a generic Task can also be returned.

        Note that we use the base URL https://api.nytimes.com/svc/topstories/v2 from the Top Stories API. We instantiate a RestSharp RestClient object and store the connection in a variable called client.

        Next, we create a RestRequest object. This is our actual request. We include the path to the endpoint we are looking for (home.json) along with our API key. We also specify that we will be using a GET Http method.

        Note that we utilize C#'s string interpolation to place the apiKey variable into the RestRequest by placing a $ before a string and then placing any interpolated values in curly braces.

        Then we use the await keyword to specify that we need to receive a result before we attempt to define response. We call the RestClient's ExecuteAsync method and pass in our request object.

        Finally, we return the Content property of the RestResponse response variable, which is a string representation of the response content.
    */
    }
  }
}
