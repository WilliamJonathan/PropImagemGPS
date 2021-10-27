using System;
using System.IO;
using ExifLib;

namespace PropImagemGPS
{
    class Program
    {

        static void Main(string[] args)
        {
            DateTime datetime;
            double[] lt, lg;
            double degrees, minutes, seconds, lat_dd, long_dd;
            string data, lat, lon;

            if (args.Length != 0)
            {
                string path = @args[0];
                string pathTxt = @System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\propgps.txt";
                data = "";
                lat = "";
                lon = "";

                /*var dll = new DLLPropImagem.DLLPropImagem();
                string data = dll.RetornaData(path).ToString();
                string lat = dll.RetornaLat(path).ToString();
                string lon = dll.RetornaLong(path).ToString();*/
                ExifReader reader = new ExifReader(path);
                if (reader.GetTagValue<DateTime>(ExifTags.DateTimeDigitized, out datetime))
                {
                    data = datetime.ToString();
                }
                if (reader.GetTagValue<double[]>(ExifTags.GPSLatitude, out lt))//latitude
                {
                    degrees = lt[0];
                    minutes = lt[1];
                    seconds = lt[2];
                    lat_dd = degrees + (minutes / 60) + (seconds / 3600);
                    lat = lat_dd.ToString();
                }
                if (reader.GetTagValue<double[]>(ExifTags.GPSLongitude, out lg))//longitude
                {
                    degrees = lg[0];
                    minutes = lg[1];
                    seconds = lg[2];
                    long_dd = degrees + (minutes / 60) + (seconds / 3600);
                    lon = long_dd.ToString();
                }
                try
                {
                    string[] lines =
                        {
                            "PATH="+path, "LATITUDE="+lat, "LONGITUDE="+lon, "TIRADA EM="+data
                        };

                    File.WriteAllLines(pathTxt, lines);
                    Environment.Exit(0);
                }
                catch (IOException e)
                {
                    Console.WriteLine("Erro inesperado:\n" + e.Message);
                    Environment.Exit(0);
                }

            }

        }
    }
}
