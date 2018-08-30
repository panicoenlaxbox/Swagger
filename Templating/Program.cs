using DotLiquid;
using DotLiquid.FileSystems;
using System;
using System.Collections.Generic;
using System.IO;

namespace Templating
{
    class Program
    {
        static void Main(string[] args)
        {
            // Parses and compiles the template
            Template.RegisterFilter(typeof(MyFilter));
            Template.RegisterFilter(typeof(YourFilter));
            Template.RegisterTag<MyTag>("myTag");
            var root = Directory.GetCurrentDirectory();
            Template.FileSystem = new LocalFileSystem(root);
            Template template = Template.Parse(@"
hi {{name | my}}, 
hi {{ name | your }}, 
{% include ""partial_template"" %}
{% myTag sergio %}");
            string result = template.Render(Hash.FromAnonymousObject(
                new { name = "tobi" })); // Renders the output => "hi my tobi, hi your tobi, myTag sergio"
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }

    public static class MyFilter
    {
        public static string My(string input)
        {
            return $"my {input}";
        }
    }

    public static class YourFilter
    {
        public static string Your(Context context, string input)
        {
            return $"your {input}";
        }
    }

    public class MyTag : DotLiquid.Tag
    {
        private string _markup;

        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            base.Initialize(tagName, markup, tokens);
            _markup = markup;
        }

        public override void Render(Context context, TextWriter result)
        {
            result.Write($"myTag {_markup}");
        }
    }

}
