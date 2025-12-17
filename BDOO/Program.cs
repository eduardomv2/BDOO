using BDOO;
using Db4objects.Db4o; // Tu librería NuGet
using System;
using System.IO;

namespace PracticaBDOO_Agencia
{
    class Program
    {
        
        static string archivoBD = "AgenciaMotors.yap";

        static void Main(string[] args)
        {
          

            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== GESTIÓN DE AGENCIA (BDOO) ===");
                Console.WriteLine("1. Agregar AUTO");
                Console.WriteLine("2. Agregar MOTO");
                Console.WriteLine("3. Ver Inventario (Polimorfismo)");
                Console.WriteLine("4. Borrar Archivo de BD (Reiniciar)");
                Console.WriteLine("5. Salir");
                Console.Write("Elige una opción: ");

                string opcion = Console.ReadLine();

                
                using (IObjectContainer db = Db4oEmbedded.OpenFile(archivoBD))
                {
                    switch (opcion)
                    {
                        case "1":
                            AgregarAuto(db);
                            break;
                        case "2":
                            AgregarMoto(db);
                            break;
                        case "3":
                            VerInventario(db);
                            break;
                        case "4":
                            db.Close(); 
                            File.Delete(archivoBD);
                            Console.WriteLine("Base de datos borrada. Inicias desde cero.");
                            Console.ReadKey();
                            break;
                        case "5":
                            continuar = false;
                            break;
                    }
                }
            }
        }

        static void AgregarAuto(IObjectContainer db)
        {
            Console.WriteLine("\n--- NUEVO AUTO ---");
            Auto a = new Auto();
            Console.Write("Marca: "); a.Marca = Console.ReadLine();
            Console.Write("Modelo: "); a.Modelo = Console.ReadLine();
            Console.Write("Precio: "); a.Precio = Convert.ToDouble(Console.ReadLine());
            Console.Write("Num Puertas: "); a.Puertas = Convert.ToInt32(Console.ReadLine());

            db.Store(a); 
            Console.WriteLine("¡Auto guardado exitosamente!");
            System.Threading.Thread.Sleep(1000);
        }

        static void AgregarMoto(IObjectContainer db)
        {
            Console.WriteLine("\n--- NUEVA MOTO ---");
            Moto m = new Moto();
            Console.Write("Marca: "); m.Marca = Console.ReadLine();
            Console.Write("Modelo: "); m.Modelo = Console.ReadLine();
            Console.Write("Precio: "); m.Precio = Convert.ToDouble(Console.ReadLine());
            Console.Write("Cilindrada CC: "); m.Cilindrada = Convert.ToInt32(Console.ReadLine());

            db.Store(m);
            Console.WriteLine("¡Moto guardada exitosamente!");
            System.Threading.Thread.Sleep(1000);
        }

        static void VerInventario(IObjectContainer db)
        {
            Console.WriteLine("\n--- INVENTARIO COMPLETO ---");
            
            IObjectSet resultado = db.Query(typeof(Vehiculo));

            if (resultado.Count == 0) Console.WriteLine("El inventario está vacío.");

            foreach (Vehiculo v in resultado)
            {
                Console.WriteLine(v.ToString());
            }
            Console.WriteLine("\nPresiona una tecla para volver al menú...");
            Console.ReadKey();
        }
    }
}