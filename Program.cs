using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using shpTest.Test;


static class MyClass
{
    public static void Main()
    {
        var i = TestASync().Result;
        TestSync();
        
        const string filename = "test";

        Shp.Write(filename, point:i);
        Shp.Read(filename);
    }



    public static async Task<Point> TestASync()
    {
        var address = "3 chemin des vignes 33112".ParseToUrlFormat();
        var httpClient = new HttpClient{BaseAddress = new Uri("http://nominatim.openstreetmap.org")};
        httpClient.DefaultRequestHeaders.Add("User-Agent", "C# App");
        var httpResult = await httpClient.GetAsync($"search?q={address}&format=json&polygon=1&addressdetails=1");
        var result = await httpResult.Content.ReadAsStringAsync();
        var r = JsonConvert.DeserializeObject(result) as JArray;
        var latString = ((JValue)r![0]["lat"]!).Value as string;
        var longString = ((JValue)r[0]["lon"]!).Value as string;

        var lat = float.Parse(latString!, System.Globalization.CultureInfo.InvariantCulture);
        var lon = float.Parse(longString!,System.Globalization.CultureInfo.InvariantCulture);
        
        return new Point{ X=lon, Y=lat};
    }
    
    public static void TestSync()
    {
        
    }


    public static string ParseToUrlFormat(this string str) => str.Replace(" ", "+");
}





