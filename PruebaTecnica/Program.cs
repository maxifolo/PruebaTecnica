using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PruebaTecnica
{
    class Program
    {
        static void Main(string[] args)
        {
            //pedimos los datos al usuario
            Console.WriteLine("Ingresar la fraccion: ");
            var fraction = Console.ReadLine();
            string resultado = simplificar(fraction);
            Console.WriteLine(resultado);
            Console.ReadKey();
            //pedimos los datos al usuario
            Console.WriteLine("Ingresar el nombre : ");
            var nombre = Console.ReadLine();
            Console.WriteLine(validarNombre(nombre).ToString());
            Console.ReadKey();
        }
        private static bool validarNombre(string nombre)
        {
            string[] valores = nombre.Split();

            if (valores.Length == 2 && valores[1].isPalabra()) return valores[0].isInicial();

            else if (valores.Length == 3 && valores[2].isPalabra())
            {
                return (valores[0].isInicial()) ? valores[1].isInicial()
                       : (valores[0].isPalabra()) ? (valores[1].isInicial() || valores[1].isPalabra())
                       : false;
            }
            else return false;
        }

        private static string simplificar(string fraction)
        {
            
            const string expre = @"[0-9]{1,3}\/[0-9]{1,3}";
            int num, den, res;
            //comprobammos que nos esten enviando el fromato correcto de una fraccion
            if (Regex.IsMatch(fraction, expre))
            {
                string[] values = fraction.Split('/');
                //evaluo si se puede leer correctamente los valores
                if (int.TryParse(values[0], out num) && int.TryParse(values[1], out den))
                {
                    var num2 = num;
                    var den2 = den;
                    string result;

                    if (num == 0 || den == 0) return "Indefinido";

                    while (den2 != 0)
                    {
                        res = num2 % den2;
                        num2 = den2;
                        den2 = res;
                    }
                    result = $"{num / num2}";
                    return (den / num2 == 1) ? result : String.Concat(result, $"/{den / num2}");
                }
                else return "No se pudo convertir los valores";
            }
            else return "Formato no valido";
        }
    }
    public static class Extensiones
    {
        public static bool isPalabra(this string value)
        {
            const string paranP = @"^\b([A-Z]{1}[a-z]+)\r?$"; // formato Poe
            return Regex.IsMatch(value, paranP, RegexOptions.None);
        }
        public static bool isInicial(this string value)
        {
            const string paranIU = @"^\b([A-Z]{1})\.\r?$"; // formato E.
            return Regex.IsMatch(value, paranIU, RegexOptions.None);
        }
    }
}

