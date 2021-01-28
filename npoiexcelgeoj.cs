using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;


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

namespace NPOIreadtest
{
    class Program
    {
        static void Main(string[] args)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheetA1 = workbook.CreateSheet("Sheet A1");

//1地点目            
            IRow row1 = sheetA1.CreateRow(0);
            row1.CreateCell(2).SetCellValue("'035'40'39'8");
            row1.CreateCell(3).SetCellValue("'139'45'53'2");

            ICell cell1 = sheetA1.GetRow(0).GetCell(2);
            ICell cell2 = sheetA1.GetRow(0).GetCell(3);    

            //excelシート上から取得した座標を数値に変換
            string cell1_2 = Convert.ToString(cell1); 
            double outnum1;            
            cell1_2 = cell1_2.Replace("'","");
            outnum1 = Convert.ToDouble(cell1_2);
            outnum1 = outnum1 / 10;
            Console.WriteLine(sixty2ten(outnum1));            
            IRow row2 = sheetA1.CreateRow(2);
            row2.CreateCell(0).SetCellValue(sixty2ten(outnum1));
            
            string cell2_2 = Convert.ToString(cell2); 
            double outnum2;
            cell2_2 = cell2_2.Replace("'","");
            outnum2 = Convert.ToDouble(cell2_2);
            outnum2 = outnum2 / 10;
            Console.WriteLine(sixty2ten(outnum2)); 
            row2.CreateCell(1).SetCellValue(sixty2ten(outnum2));

//2地点目
            row1.CreateCell(4).SetCellValue("'35'41'05'0");
            row1.CreateCell(5).SetCellValue("'139'45'10'0");

            ICell cell3 = sheetA1.GetRow(0).GetCell(4);
            ICell cell4 = sheetA1.GetRow(0).GetCell(5);    

            //excelシート上から取得した座標を数値に変換
            string cell3_2 = Convert.ToString(cell3); 
            double outnum3;            
            cell3_2 = cell3_2.Replace("'","");
            outnum3 = Convert.ToDouble(cell3_2);
            outnum3 = outnum3 / 10;
            Console.WriteLine(sixty2ten(outnum3));            
            row2.CreateCell(2).SetCellValue(sixty2ten(outnum3));
            
            string cell4_2 = Convert.ToString(cell4); 
            double outnum4;
            cell4_2 = cell4_2.Replace("'","");
            outnum4 = Convert.ToDouble(cell4_2);
            outnum4 = outnum4 / 10;
            Console.WriteLine(sixty2ten(outnum4)); 
            row2.CreateCell(3).SetCellValue(sixty2ten(outnum4));
            

            double lat1,lng1,lat2,lng2,dist;
            lat1 = sixty2ten(outnum1);
            lng1 = sixty2ten(outnum2);
            lat2 = sixty2ten(outnum3);
            lng2 = sixty2ten(outnum4);

            double WGS84_A = 6378137.000;
            double WGS84_E2 = 0.0066943799901975;
            double WGS84_MNUM = 6335439.327292462;
            double P,W,W3,M,N,dx,dy;

            P = ((lat1+lat2)/2) * Math.PI / 180;
            W = Math.Sqrt(1-WGS84_E2*Math.Pow(Math.Sin(P),2));
            W3 = W * W * W;
            M = WGS84_MNUM/W3;
            N = WGS84_A/W;
            dx = (lng1-lng2) * Math.PI / 180;
            dy = (lat1-lat2) * Math.PI / 180;

            dist = Math.Sqrt(Math.Pow(dy*M,2) + Math.Pow(dx * N * Math.Cos(P),2)); 

            row2.CreateCell(5).SetCellValue(Math.Round(dist,3));


            Mkgeojson.Geometry geoinstance = new Mkgeojson.Geometry
            {
                type = "LineString",
                coordinates = new double[][]
                {
                    new double[]{lng1,lat1},
                    new double[]{lng2,lat2}
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



            FileStream sw = File.Create("sample.xlsx");
            workbook.Write(sw);
            sw.Close();
        }

        static double sixty2ten(double x)
        {
            double d,m,s;
            
            //度
            d = x / 10000;
            d = Math.Truncate(d);
    
            //分のところを10進数に変換
            m = x / 100; 
            m = Math.Truncate(m);
            m = m - (d * 100);
            
            //秒算出のための準備
            s = x * 10;
            s = Math.Truncate(s);
            s = s - (d * 100000);
            s = s - (m * 1000);
            s = s / 10;        

            //分及び秒を算出→全体の算出
            if((d<360) && (m<60) && (s<60))
            {
                m = Math.Round(m / 60,7);
                s = Math.Round(s / (60*60), 8);
                d = d + m + s;
                return Math.Round(d,6);
            }else
            {
                return 1;
            }
            
        }
    }
}