using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HW_CSharp_3._9
{
    internal class JSON2XML
    {
        public static void Convert(string path)
        {
            string json;
            XDocument doc = new XDocument();
            using (var sw = new StreamReader(path))
                json = sw.ReadToEnd();
            using (JsonDocument document = JsonDocument.Parse(json))
            {
                Console.WriteLine("<? xml version = \"1.0\" encoding = \"UTF-8\" ?>");
                GoInJson(document.RootElement);

            }           
        }

        private static void GoInJson(JsonElement element, string property = "")
        {
            switch(element.ValueKind)
            {
                case JsonValueKind.Object:
                    foreach (var prop in element.EnumerateObject())
                        GoInJson(prop.Value, prop.Name); 
                    break;
                case JsonValueKind.Array:
                    int arrN = 0;
                    foreach (var item in element.EnumerateArray())
                    {
                        if (property.Length != 0)
                            Console.WriteLine("<" + property + ">");
                        else
                            Console.WriteLine("<array" + arrN + ">");
                        GoInJson(item);
                        if (property.Length != 0)
                            Console.WriteLine("</" + property + ">");
                        else
                        {
                            Console.WriteLine("</array" + arrN + ">");
                            arrN++;
                        }
                    }
                    
                    break;
                case JsonValueKind.String:
                case JsonValueKind.Number:
                case JsonValueKind.False:
                case JsonValueKind.True:
                    if(property.Length != 0)
                        Console.WriteLine("<" + property + ">" + element.ToString() + "</" + property + ">");
                    else
                        Console.WriteLine(element.ToString());
                    break;
            }
        }
    }
}
