using System.IO;
using System.Reflection;
using System.Text;

namespace jr.common.tests
{
    public static class TestUtils
    {
        public static Stream GetStreamFromResource(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceStream = assembly.GetManifestResourceStream($"jr.common.tests.testdata.{fileName}");
            return resourceStream;
        }

        public static string GetTextFromResource(string fileName)
        {
            var resourceStream = GetStreamFromResource(fileName);
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}