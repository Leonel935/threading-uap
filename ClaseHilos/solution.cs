using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClaseHilos
{
   internal class Producto
   {
      public string Nombre { get; set; }
      public double PrecioUnitarioDolares { get; set; }
      public int CantidadEnStock { get; set; }

      public Producto(string nombre, double precioUnitario, int cantidadEnStock)
      {
         Nombre = nombre;
         PrecioUnitarioDolares = precioUnitario;
         CantidadEnStock = cantidadEnStock;
      }
   }
   internal class Solution //reference: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock
   {

      static List<Producto> productos = new List<Producto>
        {
            new Producto("Camisa", 10, 50),
            new Producto("Pantalón", 8, 30),
            new Producto("Zapatilla/Champión", 7, 20),
            new Producto("Campera", 25, 100),
            new Producto("Gorra", 16, 10)
        };

      static int precio_dolar = 500;

       static Barrier barrera = new Barrier(4, (b) =>
        {
            Console.WriteLine($"Post-Phase action: {b.CurrentPhaseNumber}");
        });
        public static Barrier barrera_
        {
            get { return barrera; }
        }

        static void Tarea1()
      {
         static void HolaUAP()
            {
                Console.WriteLine($"Proceso {Thread.CurrentThread.Name}");
                Thread.Sleep(1500);
                for (int i = 0; i < productos.Count; i++)
                {
                    productos[i].CantidadEnStock += 10;
                    Console.WriteLine("Se actualizo la cantidad en stock de " + productos[i].Nombre + " a:" + productos[i].CantidadEnStock);
                }
                Console.WriteLine("----------------------------------------");
                barrera.SignalAndWait();
                Console.WriteLine("Hola desde " + Thread.CurrentThread.Name);

            }
            Thread task = new Thread(HolaUAP);
            task.Name = "Hilo 1";
            task.Start();
        }
        static void Tarea2()
        {

            static void HolaUAP()
            {
                Console.WriteLine($"Proceso {Thread.CurrentThread.Name}");
                Thread.Sleep(2000);
                //actualizoa el precio del dolar, día 7/6/24
                precio_dolar = 1250;
                Console.WriteLine("Se actualiza el precio del dolar");
                Console.WriteLine("----------------------------------------");
                barrera.SignalAndWait();
                Console.WriteLine("Hola desde " + Thread.CurrentThread.Name);
            }
            Thread task = new Thread(HolaUAP);
            task.Name = "Hilo 2";
            task.Start();
        }
      static void Tarea3()
      {
         static void HolaUAP()
            {
                Console.WriteLine($"Proceso {Thread.CurrentThread.Name}");
                Thread.Sleep(4000);
                double contador = 0;
                for (int i = 0; i < productos.Count; i++)
                {
                    Console.Write("-- >Producto: " + productos[i].Nombre + " - cantidad en stock:" + productos[i].CantidadEnStock);
                    contador = contador + (productos[i].PrecioUnitarioDolares * productos[i].CantidadEnStock);
                    Console.WriteLine($" - precio en dolar del producto: {productos[i].PrecioUnitarioDolares}");

                }
                Console.WriteLine($" Precio total del inventario en pesos: {contador * precio_dolar}");
                Console.WriteLine("----------------------------------------");
                barrera.SignalAndWait();
                Console.WriteLine("Hola desde " + Thread.CurrentThread.Name);
            }

            Thread task = new Thread(HolaUAP);
            task.Name = "Hilo 3";
            task.Start();
      }
 static void Tarea4()
        {

            static void HolaUAP()
            {
                Console.WriteLine($"Proceso {Thread.CurrentThread.Name}");
                Thread.Sleep(3000);
                for (int i = 0; i < productos.Count; i++)
                {
                    productos[i].PrecioUnitarioDolares *= 1.1;                    
                }
                Console.WriteLine("Se actualizo por inflación");
                Console.WriteLine("----------------------------------------");
                barrera.SignalAndWait();
                Console.WriteLine("Hola desde " + Thread.CurrentThread.Name);
      }
            Thread task = new Thread(HolaUAP);
            task.Name = "Hilo 4";
            task.Start();
        }
        internal static void Excecute()
      {
         Tarea3();

            Tarea2();

            Tarea4();
            Tarea1();
        }
   }
}