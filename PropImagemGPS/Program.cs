using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLLPropImagem;
using System.IO;

namespace PropImagemGPS
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length != 0)
            {
                string path = @args[0];
                string pathTxt = @System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\propgps.txt";

                var dll = new DLLPropImagem.DLLPropImagem();
                string data = dll.RetornaData(path).ToString();
                string lat = dll.RetornaLat(path).ToString();
                string lon = dll.RetornaLong(path).ToString();
                try
                {
                    string[] lines =
                        {
                            "PATH="+path, "LATITUDE="+lat, "LONGITUDE="+lon, "TIRADA EM="+data
                        };

                    File.WriteAllLines(pathTxt, lines);
                }
                catch (IOException e)
                {
                    Console.WriteLine("Erro inesperado:\n" + e.Message);
                }

            }

        }
    }
}
