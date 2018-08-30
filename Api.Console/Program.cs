using System.Threading.Tasks;

namespace Api.Console
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var client = new Client.Client("https://localhost:5001/");
            var result = await client.ApiFooGetAsync();
            foreach (var item in result)
            {
                System.Console.WriteLine($"{item.Bar}, {item.Baz}");
            }

            System.Console.ReadKey();
        }
    }
}
