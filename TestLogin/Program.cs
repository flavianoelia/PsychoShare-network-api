using System.Net.Http.Json;

var client = new HttpClient { BaseAddress = new Uri("http://localhost:5174") };

try
{
    var loginData = new { email = "flavia@psychoshare.com", password = "Fla1234!" };
    var response = await client.PostAsJsonAsync("/api/user/login", loginData);
    
    Console.WriteLine($"Status: {response.StatusCode}");
    
    if (response.IsSuccessStatusCode)
    {
        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response: {result}");
    }
    else
    {
        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error: {error}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Exception: {ex.Message}");
}
