using BDOO;
using Db4objects.Db4o;
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
                Console.WriteLine("=== BATERÍA DE OPERACIONES BDOO ===");
                Console.WriteLine("1. [INSERT] Agregar Auto");
                Console.WriteLine("2. [INSERT] Agregar Moto");
                Console.WriteLine("3. [SELECT] Ver Inventario (Polimorfismo)");
                Console.WriteLine("4. [UPDATE] Actualizar Precio (Por Modelo)");
                Console.WriteLine("5. [DELETE] Vender/Eliminar Vehículo");
                Console.WriteLine("6. Salir");
                Console.Write("\nElige una operación: ");

                string opcion = Console.ReadLine();

                using (IObjectContainer db = Db4oEmbedded.OpenFile(archivoBD))
                {
                    switch (opcion)
                    {
                        case "1": AgregarAuto(db); break;
                        case "2": AgregarMoto(db); break;
                        case "3": VerInventario(db); break;
                        case "4": ActualizarPrecio(db); break;
                        case "5": EliminarVehiculo(db); break;
                        case "6": continuar = false; break;
                    }
                }
            }
        }

       

        static void AgregarAuto(IObjectContainer db)
        {
            Console.WriteLine("\n--- INSERTAR AUTO ---");
            Auto a = new Auto();
            Console.Write("Marca: "); a.Marca = Console.ReadLine();
            Console.Write("Modelo: "); a.Modelo = Console.ReadLine();
            Console.Write("Precio: "); a.Precio = Convert.ToDouble(Console.ReadLine());
            Console.Write("Puertas: "); a.Puertas = Convert.ToInt32(Console.ReadLine());
            db.Store(a);
            Console.WriteLine("-> Auto guardado.");
            System.Threading.Thread.Sleep(800);
        }

        static void AgregarMoto(IObjectContainer db)
        {
            Console.WriteLine("\n--- INSERTAR MOTO ---");
            Moto m = new Moto();
            Console.Write("Marca: "); m.Marca = Console.ReadLine();
            Console.Write("Modelo: "); m.Modelo = Console.ReadLine();
            Console.Write("Precio: "); m.Precio = Convert.ToDouble(Console.ReadLine());
            Console.Write("Cilindrada: "); m.Cilindrada = Convert.ToInt32(Console.ReadLine());
            db.Store(m);
            Console.WriteLine("-> Moto guardada.");
            System.Threading.Thread.Sleep(800);
        }

        static void VerInventario(IObjectContainer db)
        {
            Console.WriteLine("\n--- CONSULTA GLOBAL (Polimorfismo) ---");
            
            IObjectSet resultado = db.Query(typeof(Vehiculo));

            if (resultado.Count == 0) Console.WriteLine("Inventario vacío.");

            foreach (Vehiculo v in resultado)
            {
                Console.WriteLine(v.ToString());
            }
            Console.WriteLine("\nPresiona una tecla...");
            Console.ReadKey();
        }

        static void ActualizarPrecio(IObjectContainer db)
        {
            Console.WriteLine("\n--- ACTUALIZAR PRECIO (UPDATE) ---");
            Console.Write("Escribe el MODELO exacto a modificar: ");
            string modeloBusqueda = Console.ReadLine();

            
            Vehiculo proto = new Vehiculo { Modelo = modeloBusqueda };
            IObjectSet result = db.QueryByExample(proto);

            if (result.HasNext())
            {
                
                Vehiculo v = (Vehiculo)result.Next();
                Console.WriteLine("Precio actual: " + v.Precio);

               
                Console.Write("Nuevo Precio: ");
                v.Precio = Convert.ToDouble(Console.ReadLine());

               
                db.Store(v);
                Console.WriteLine("-> ¡Precio actualizado exitosamente!");
            }
            else
            {
                Console.WriteLine("-> No se encontró ese modelo.");
            }
            System.Threading.Thread.Sleep(1500);
        }

        static void EliminarVehiculo(IObjectContainer db)
        {
            Console.WriteLine("\n--- ELIMINAR VEHÍCULO (DELETE) ---");
            Console.Write("Escribe el MODELO exacto a eliminar: ");
            string modeloBusqueda = Console.ReadLine();

            Vehiculo proto = new Vehiculo { Modelo = modeloBusqueda };
            IObjectSet result = db.QueryByExample(proto);

            if (result.HasNext())
            {
                Vehiculo v = (Vehiculo)result.Next();
                
                db.Delete(v);
                Console.WriteLine("-> Vehículo eliminado de la base de datos.");
            }
            else
            {
                Console.WriteLine("-> No se encontró ese modelo.");
            }
            System.Threading.Thread.Sleep(1500);
        }
    }
}