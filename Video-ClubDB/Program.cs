using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Video_ClubDB
{
    class Program
    {
        public SqlConnection connection = new SqlConnection("Data Source=LAPTOP-93TLR9QC\\SQLEXPRESS; Initial Catalog = Video-Club; Integrated Security = True");
        //Crear un programa de consola basado en un videoclub el cual tendrá las clases correspondientes a las tablas de 
        //la BBDD. Los usuarios para acceder se tendrán que loguear utilizando la BBDD. 
        //Una vez logueados, 
        //se les desplegará un menú con las siguientes opciones:

        static void Main(string[] args)
        {
            Usuario user = new Usuario();
            Pelicula film = new Pelicula();
            Reserva op = new Reserva();
            List<Pelicula> listaShow = new List<Pelicula>();
            List<int> listaIdes = new List<int>();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*****************************************");
            Console.WriteLine("Bienvenido al Video Club");
            Console.WriteLine("*****************************************");
            Console.ForegroundColor = ConsoleColor.White;


            Console.WriteLine("Introduce el usuario y contraseña");
            string usuario00 = Console.ReadLine();
            string contrasenia00 = Console.ReadLine();
            bool t;
            //Zona Loggear
            do
            {
                t = user.UserIsLog(usuario00, contrasenia00);
                if (t == false)
                {
                    Console.WriteLine("Usuario incorrecto: Pulsa s para volver a introducer contraseña \n Pulsa n para crear nuevo usuario");
                    string conUs = Console.ReadLine();
                    if (conUs == "s")   //Si queremos volver a entrar
                    {
                        Console.WriteLine("Introduce el usuario y contraseña");
                        usuario00 = Console.ReadLine();
                        contrasenia00 = Console.ReadLine();
                        t = user.UserIsLog(usuario00, contrasenia00);
                        Console.WriteLine("Se vuelve a introducir");
                    }
                    else if(conUs == "n")   //Si queremos crear un nuevo usuario
                    {
                        user.CreateLogUser();
                        Console.WriteLine("Usuario creado");
                        t = true;
                    }
                    else
                    {
                        t = false;
                        Console.WriteLine("Login no Ok");
                    }
                }
                else
                {
                    Console.WriteLine("LogIn OK");
                }

            } while (t == false);
            //Zona BD
            int r;
            do
            {
                Console.WriteLine($"\n Pulsa 1 para ver tus películas disponibles \n Pulsa 2 para alquilar una película disponible \n Pulsa 3 para ver tus alquileres y/o devolver \n Pulsa 4 para el logout");
                r =Convert.ToInt32(Console.ReadLine());
                switch (r)
                {
                    case 1:
                        Console.WriteLine("El listado disponible es");
                        Console.WriteLine($"Titulo\n");
                        //Obtenemos edad del usuario
                        listaShow = op.ShowPeliculaFilt(usuario00);
                        listaIdes = op.FiltroID(listaShow); //Ambas tiene misma longitud
                        int cont = 0;
                        foreach (Pelicula peliculon in listaShow )
                        {
                            Console.WriteLine($"{listaIdes[cont]}\t {peliculon.Titulo}");
                            cont++;
                        }
                        try
                        {
                        Console.WriteLine("Selecciona la película");
                        int selecc = Convert.ToInt32(Console.ReadLine());
                        op.MostrarPel(listaShow,selecc);

                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Repetimos");
                        }
                        break;
                    case 2:
                        Console.WriteLine("El listado de peliculas a alquilar es");
                        Console.WriteLine($"   Titulo\n");
                        //Obtenemos edad del usuario
                        listaShow = op.ShowPeliculaAlq(usuario00);
                        listaIdes = op.FiltroID(listaShow); //Ambas tiene misma longitud
                        int contad = 0;
                        foreach (Pelicula peliculon in listaShow)
                        {
                            Console.WriteLine($"{listaIdes[contad]}\t {peliculon.Titulo}");
                            contad++;
                        }
                        try
                        {

                            Console.WriteLine("Selecciona la película disponible");
                            int selecAlquiler = Convert.ToInt32(Console.ReadLine());
                            op.GenerarAlquiler(listaShow, selecAlquiler,usuario00);
                            Console.WriteLine("Pelicula alquilada");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Repetimos");
                        }
                        break;
                    case 3:
                        Console.WriteLine("El listado de tus peliculas es");
                        listaShow = op.ShowTusPeliculas(usuario00);
                        listaIdes = op.FiltroID(listaShow);
                        int contador = 0;
                        foreach (Pelicula peliculon in listaShow)
                        {
                            Console.WriteLine($"{listaIdes[contador]}\t {peliculon.Titulo}");
                            contador++;
                        }
                        Console.WriteLine("Selecciona la película a devolver");
                        int selecDevolucion;
                        try
                        {
                            selecDevolucion = Convert.ToInt32(Console.ReadLine());
                            op.GenerarDevolucion(listaShow, selecDevolucion, usuario00);
                            Console.WriteLine("Pelicula devuelta");

                        }
                        catch (Exception )
                        {
                            Console.WriteLine("Repetimos");
                        }
                        //Damos el aviso que imprime en pantalla que se ha superado los tres días de devolución
                        break;
                    case 4:
                        Console.WriteLine("Logout");
                        break;
                    default:
                        Console.WriteLine("Repetimos");
                        break;
                }
            } while (r != 4);
        }
    }
}
