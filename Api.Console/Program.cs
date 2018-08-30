using System.Threading.Tasks;
using MyNamespace;

namespace Api.Console
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var client = new ValuesClient("https://localhost:5001/");
            var result = await client.GetAllAsync();
            foreach (var item in result)
            {
                System.Console.WriteLine(item);
            }

            System.Console.ReadKey();
        }
    }
}
