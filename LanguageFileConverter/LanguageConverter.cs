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
        public static XDocument Load(string path)
        {
            StreamReader r = File.OpenText(path);

            var deserializer = new Deserializer();

            var yamlObject = (Dictionary<object, object>)deserializer.Deserialize(r);

            r.Close();

            var language = (Dictionary<object, object>) yamlObject["language"];

            XAttribute[] attArray = {
                new XAttribute("name", (string)language["name"]),
                new XAttribute("base", (string)language["base"]),
                new XAttribute("variant", (string)language["variant"])
            };

            var str = (Dictionary<object, object>) yamlObject["strings"];

            XElement xStrings = new XElement("Root");

            foreach (var x in str.Keys)
            {
                if (!(str[x] is IList<object>))
                {
                    throw new Exception(str[x].GetType().Name + " is not a list");
                }

                XElement xValues = new XElement("Root");
                foreach (var y in (List<object>)str[x])
                {
                    xValues.Add(new XElement("value", y));
                }

                xStrings.Add(new XElement("string", new[] { new XAttribute("key", x) }, xValues.Elements()));

            }

            XDocument document = new XDocument(
                new XElement("strings",
                        new XElement("language", attArray),
                        xStrings.Elements()
                    )
                );

            return document;

        }
    }
}