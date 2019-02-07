using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Spider.Application.Tests.Infrastructure
{
    public class TestData : ITestData
    {
        private string Path => "Data";

        public async Task<T> Read<T>(string name)
        {
            var path = System.IO.Path.GetFullPath($"{Path}/{name}.json");
            if (!File.Exists(path))
            {
                throw new Exception("File not found");
            }

            using (StreamReader r = new StreamReader($"{Path}/{name}.json"))
            {
                var json = await r.ReadToEndAsync();
                var items = JsonConvert.DeserializeObject<T>(json);
                return items;
            }
        }
    }
}