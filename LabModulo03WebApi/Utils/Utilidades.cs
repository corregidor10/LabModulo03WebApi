using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LabModulo03WebApi.Utils
{
    public class Utilidades
    {
        public static void EscribirLog(String contenido)
        {
            using (var destino=File.AppendText(@"c:\ZZlog\logWebApi.txt"))
            {
               destino.WriteLine(contenido);
            }
        }
    }
}