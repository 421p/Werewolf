using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using YamlDotNet.Serialization;

namespace LanguageFileConverter
{
    public static class LanguageConverter
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<XDocument> LoadAsync(string path)
        {
            return await Task.Run(()=>Load(path));
        }

        public static XDocument Load(string path)
        {

            var r = new StringReader(File.ReadAllText(path));

            var deserializer = new Deserializer();

            Dictionary<object, object> yamlObject = (Dictionary<object, object>)deserializer.Deserialize(r);

            var serializer = new SerializerBuilder().JsonCompatible().Build();

            var language = (Dictionary<object, object>) yamlObject["language"];

            // XAttribute object array content:
            XAttribute[] attArray = {
                new XAttribute("name", (string)language["name"]),
                new XAttribute("base", (string)language["base"]),
                new XAttribute("variant", (string)language["variant"])
            };

            var str = (Dictionary<object, object>) yamlObject["strings"];

            //Dictionary<string, List<string>> strings = new Dictionary<string, List<string>>();

            //List<string> s;

            XElement xStrings = new XElement("Root");

            str.Keys.ToList().ForEach(x =>
            {
                //s = new List<string>();
                XElement xValues = new XElement("Root");

                if (str[x].GetType().Name == "List`1") { 
                    ((List<object>)str[x]).ForEach(y =>
                    {
                        //s.Add((string)y);
                        xValues.Add(new XElement("value", y));
                    });
                }

                else if(str[x].GetType().Name == "Dictionary`2"){
                    //s.Add("");
                    xValues.Add(new XElement("value", ""));
                }
                else
                {
                    //s.Add("");
                    xValues.Add(new XElement("value", ""));
                }

                //strings.Add((string)x, s);

                xStrings.Add(new XElement("string", new[] {new XAttribute("key", x)}, xValues.Elements().Select(el=>el)));
            });


            XDocument document = new XDocument(
                new XComment("This is a comment"),
                new XElement("strings",
                        new XElement("language", attArray),
                        xStrings.Elements().Select(el => el)
                    )
                );

            return document;

        }
    }
}