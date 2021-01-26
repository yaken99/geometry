using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Mkgeojson
{
    public class Rootobject
    {
        public string type {get; set;}
        public Feature[] features {get; set;}
    }
    public class Feature
    {
        public string type {get; set;}
        public Properties properties {get; set;}
        public Geometry geometry {get; set;}
    }
    public class Properties
    {
        public string _color {get; set;}
        public int _opacity {get; set;}
    }
    public class Geometry
    {
        public string type {get; set;}
        public double[][] coordinates {get; set;}
    }
}
class tgeojson
{
    static void Main()
    {
        Mkgeojson.Geometry geoinstance = new Mkgeojson.Geometry
        {
            type = "LineString",
            coordinates = new double[][]
            {
                new double[]{139.764786,35.677724},
                new double[]{135.498592,34.732517}
            }
        };
        Mkgeojson.Properties propinstance = new Mkgeojson.Properties
        {
            _color = "#FF4500",
            _opacity = 1
        };
        Mkgeojson.Feature featureinstance = new Mkgeojson.Feature
        {
            type = "Feature",
            properties = propinstance,
            geometry = geoinstance
        };
        Mkgeojson.Rootobject rootinstance = new Mkgeojson.Rootobject
        {
            type = "FeatureCollection",
            features = new Mkgeojson.Feature[]
            {featureinstance}
        };

        File.WriteAllText("./sample.geojson", JsonConvert.SerializeObject(rootinstance));
        // serialize JSON directly to a file
        using (StreamWriter file = File.CreateText("./sample.geojson"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, rootinstance);
        }

        string json = JsonConvert.SerializeObject(rootinstance, Formatting.Indented); 
        Console.WriteLine(json);
    }
}
