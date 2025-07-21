using System;
using System.Collections.Generic;
using ConsoleTables;

internal class Program
{
    static void Main(string[] args)
    {
        var contadorCarga = 0;
        string respuesta;
        for (int dia = 1; dia <= 7; dia++)
        {
            var totalLitrosSuper = 0;
            int totalLitrosPremium = 0;
            int cargasBajas = 0;
            int cargasMedias = 0;
            int cargasAltas = 0;
            int totalLitrosDia = 0;
            int cantidadCargasDia = 0;

            Console.WriteLine($"Día {NombreDia(dia)}");

            do
            {
                contadorCarga++;

                MenuCombustible();
                Console.Write("Ingrese que va a despachar: ");
                if (int.TryParse(Console.ReadLine(), out int opcionCombustible) && ValidaMenu(opcionCombustible))
                {
                    Console.Write("Ingrese la cantidad de litros: ");
                    if (int.TryParse(Console.ReadLine(), out int litrosCombustible) && ValidarLitros(litrosCombustible))
                    {
                        cantidadCargasDia++;
                        totalLitrosDia += litrosCombustible;
                        if (opcionCombustible == 1)
                            totalLitrosSuper += litrosCombustible;
                        else
                            totalLitrosPremium += litrosCombustible;

                        string clasif = ClasificacionCarga(litrosCombustible);
                        switch (clasif)
                        {
                            //si clasificacion es baja entoces lo cuenta y suma el contador carga baja
                            case "Baja": cargasBajas++;
                                break;
                            case "Media": cargasMedias++;
                                break;
                            case "Alta": cargasAltas++;
                                break;
                        }

                        Console.WriteLine($"Esta es la carga {contadorCarga}");
                        Console.WriteLine($"Se han cargado {litrosCombustible} litros");
                        Console.WriteLine($"{litrosCombustible} litros equivalen a {EquivalenteLitros(litrosCombustible, 0.26417)} US gal");
                        Console.WriteLine($"{litrosCombustible} litros equivalen a {EquivalenteLitros(litrosCombustible, 0.21997)} UK gal");
                        Console.WriteLine($"Tipo de combustible: {TipoCombustible(opcionCombustible)}");
                        Console.WriteLine($"Clasificación de carga: {clasif}");
                    }
                    else
                    {
                        Console.WriteLine("Error: Fuera de rango 5-50");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Fuera de rango 1-2");
                }

                do
                {
                    respuesta = IngresarRespuesta("¿Desea hacer otro ingreso? (s/n): ");
                    if (respuesta == "N") break;
                } while (respuesta != "S" && respuesta != "N");

            } while (respuesta == "S");

           
            Console.WriteLine($"Resumen del día {NombreDia(dia)}");
            var tablaCombustible = new ConsoleTable("Tipo", "Litros");
            tablaCombustible.AddRow("Súper", totalLitrosSuper).AddRow("Premium", totalLitrosPremium);
            Console.WriteLine(tablaCombustible.ToString()); 
            

            Console.WriteLine($"Promedio de litros cargados: {PromedioCarga(totalLitrosDia, cantidadCargasDia)} L");

            var tablaClasificacion = new ConsoleTable("Clasificación", "Cantidad");
            tablaClasificacion.AddRow("Baja", cargasBajas).AddRow("Media", cargasMedias).AddRow("Alta", cargasAltas);
            Console.WriteLine(tablaClasificacion.ToString());

           
        }
    }

    static string IngresarRespuesta(string mensaje)
    {
        string? respuesta;
        do
        {
            Console.Write(mensaje);
            respuesta = Console.ReadLine()?.ToUpper();
            if (respuesta == "S" || respuesta == "N")
                return respuesta;
            Console.WriteLine("Ingreso Erroneo. Debe ingresar S o N.");
        } while (true);
    }
    static double PromedioCarga(double TotalLitrosDia , int cantidadCargasDia)
    {
        return TotalLitrosDia / cantidadCargasDia;
    }

    static void MenuCombustible()
    {
        Console.WriteLine("Menu Combustible:");
        Console.WriteLine("1 - Súper");
        Console.WriteLine("2 - Premium");
    }

    static string NombreDia(int dia)
    {
        switch (dia)
        {
            case 1:
                return "Lunes";
                case 2:
                return "Martes";
                case 3:
                return "Miercoles";
                case 4:
                return "Jueves";
                case 5:
                return "Viernes";
                case 6:
                return "Sabado";
            default:
                return "Domingo";
        }
    }

    static bool ValidaMenu(int opcion)
    {
        return opcion >= 1 && opcion <= 2;
    }

    static bool ValidarLitros(int litros)
    {
        return litros >= 5 && litros <= 50;
    }

    static double EquivalenteLitros(int litros, double equivalente)
    {
        return litros / equivalente;
    }

    static string TipoCombustible(int opcion)
    {
        if (opcion == 1)
        {
            return "Super";
        }
        else
        {
            return "Premium";
        }
        
    }

    static string ClasificacionCarga(int litros)
    {
        if (litros > 5 && litros <= 10)
            return "Baja";
        else if (litros >= 11 && litros <= 30)
            return "Media";
        else
            return "Alta";
    }
}
