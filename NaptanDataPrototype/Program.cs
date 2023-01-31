// See https://aka.ms/new-console-template for more information

using System.Xml.Linq;
using NaptanDataPrototype;
using NaptanDataPrototype.Services;

Console.WriteLine("Hello, World!");

var naptanData = new NaptonXmlFileService();

naptanData.GetLocation();
