// See https://aka.ms/new-console-template for more information

using System.Xml.Linq;
using NaptanDataPrototype;
using NaptanDataPrototype.Services;

Console.WriteLine("Hello, World!");

var naptanData = new NaptonXmlFileService();

naptanData.GetLocation();

using var client = new HttpClient();
var url = "https://api.getthedata.com/bng2latlong/529090/179645/xml";
// var content = await client.GetStringAsync(url);
//
// Console.WriteLine(content);
var conversionRequest = client.GetStringAsync(url).Result;
XDocument doc = XDocument.Parse(conversionRequest);
Console.WriteLine(doc);

var longitude = doc.Root.Element("longitude").Value;
Console.WriteLine(longitude);
