﻿using ChatServerApp.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Iniciando servidor en el puerto {0}", puerto);
            ServerSocket servidor = new ServerSocket(puerto);
            if (servidor.Iniciar())
            {
                while (true)
                {
                    //Esperar a que se conecte un cliente
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Esperando conexion de cliente ...");
                    if (servidor.ObtenerCliente())
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Cliente conectado!");
                        //Implementar protocolo
                        //1. El cliente inicia la comunicacion
                        //2, el servidor responde
                        //3. Si alguno de los extremos dice chao, la comunicacion se finaliza
                        string mensaje = "";
                        while (mensaje.ToLower() != "chao")
                        {
                            mensaje = servidor.Leer();
                            Console.WriteLine("C: {0}", mensaje);
                            if (mensaje.ToLower() != "chao")
                            {
                                Console.WriteLine("Digame lo que quiere decirle guruguru");
                                mensaje = Console.ReadLine().Trim();
                                Console.WriteLine("s:{0}", mensaje);
                                servidor.Escribir(mensaje);
                            }
                        }
                        servidor.CerrarConexion();

                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error al iniciar el servidor");
                Console.ReadKey();
            }
        }
    }
}

 

