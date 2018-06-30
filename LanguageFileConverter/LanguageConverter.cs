using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using YamlDotNet.Serialization;

namespace LanguageFileConverter
{
    public static class LanguageConverter
    {
        private static readonly Deserializer Deserializer = new Deserializer();

        public static XDocument Load(string path)
        {
            var file = File.OpenText(path);

            var yamlObject = Deserializer.Deserialize<Dictionary<object, object>>(file);

            file.Close();

            var language = (Dictionary<object, object>) yamlObject["language"];

            var attArray = new[]
            {
                new XAttribute("name", (string) language["name"]),
                new XAttribute("base", (string) language["base"]),
                new XAttribute("variant", (string) language["variant"])
            };

            var str = (Dictionary<object, object>) yamlObject["strings"];

            var xStrings = new XElement("Root");

            foreach (var x in str.Keys)
            {
                if (!(str[x] is IList<object>))
                {
                    throw new Exception(str[x].GetType().Name + " is not a list");
                }

                var xValues = new XElement("Root");
                foreach (var y in (List<object>) str[x])
                {
                    xValues.Add(new XElement("value", y));
                }

                xStrings.Add(new XElement("string", new[] {new XAttribute("key", x)}, xValues.Elements()));
            }

            var document = new XDocument(
                new XElement("strings",
                    new XElement("language", attArray),
                    xStrings.Elements()
                )
            );

            return document;
        }
    }
}