using System;
using System.IO;
using ExifLib;

namespace PropImagemGPS
{
    class Program
    {

        static void Main(string[] args)
        {
            args = new string[5];
            args[0] = @"C:\Users\User\Documents\Downloads\img.jpg";
            DateTime datetime;
            double[] lt, lg;
            double degrees, minutes, seconds, lat_dd, long_dd;
            string data, lat, lon, lat_ref, lon_ref;

            if (args.Length != 0)
            {
                string path = @args[0];
                string pathTxt = @System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\propgps.txt";
                data = "";
                lat = "";
                lon = "";
                lat_ref = "";
                lon_ref = "";

                /*var dll = new DLLPropImagem.DLLPropImagem();
                string data = dll.RetornaData(path).ToString();
                string lat = dll.RetornaLat(path).ToString();
                string lon = dll.RetornaLong(path).ToString();*/
                
                try
                {
                    ExifReader reader = new ExifReader(path);

                    //inicio
                    if (reader.GetTagValue<DateTime>(ExifTags.DateTimeDigitized, out datetime))
                    {
                        data = datetime.ToString();
                    }
                    if (reader.GetTagValue<double[]>(ExifTags.GPSLatitude, out lt))//latitude
                    {
                        reader.GetTagValue<string>(ExifTags.GPSLatitudeRef, out lat_ref);
                        degrees = lt[0];
                        minutes = lt[1];
                        seconds = lt[2];
                        lat_ref = lat_ref.ToString();
                        if (lat_ref == "S")
                        {
                            lat_dd = (degrees + (minutes / 60) + (seconds / 3600)) * -1;
                            lat = lat_dd.ToString();
                        }
                        else
                        {
                            lat_dd = degrees + (minutes / 60) + (seconds / 3600);
                            lat = lat_dd.ToString();
                        }
                    }
                    if (reader.GetTagValue<double[]>(ExifTags.GPSLongitude, out lg))//longitude
                    {
                        reader.GetTagValue<string>(ExifTags.GPSLongitudeRef, out lon_ref);
                        degrees = lg[0];
                        minutes = lg[1];
                        seconds = lg[2];
                        lon_ref = lon_ref.ToString();
                        if (lon_ref == "W")
                        {
                            long_dd = (degrees + (minutes / 60) + (seconds / 3600)) * -1;
                            lon = long_dd.ToString();
                        }
                        else
                        {
                            long_dd = degrees + (minutes / 60) + (seconds / 3600);
                            lon = long_dd.ToString();
                        }

                    }
                    try
                    {
                        string[] lines =
                            {
                        "PATH="+path, "LATITUDE="+lat, "LONGITUDE="+lon, "TIRADA EM="+data, "LAT_REF=" +lat_ref, "LON_REF="+lon_ref
                    };

                        File.WriteAllLines(pathTxt, lines);
                        Environment.Exit(0);
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Erro inesperado:\n" + e.Message);
                        Environment.Exit(0);
                    }
                    //fim
                }
                catch (Exception)
                {
                    try
                    {
                        string[] lines =
                            {
                        "Não foi possivel ler a latitude e longitude da imagem\n" +
                        "Formato da imagem invalido!"
                    };

                        File.WriteAllLines(pathTxt, lines);
                        Environment.Exit(0);
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Erro inesperado:\n" + e.Message);
                        Environment.Exit(0);
                    }
                    //throw;
                }
                
            }

        }
    }
}
