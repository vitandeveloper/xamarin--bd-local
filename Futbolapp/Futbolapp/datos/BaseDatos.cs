using System.Collections.Generic;
using System.Linq;
using SQLite.Net;
using SQLite.Net.Interop;
using Futbolapp.modelos;
using SQLiteNetExtensions.Extensions;
using Futbolapp.clases;

namespace Futbolapp.datos
{
    public class BaseDatos
    {
        //pone un seguro para bloquear las demas operaciones que se quieran conectar a cierta tabla
        static object locker = new object();
        // maneja un objeto de sqlite espesifica para cada plataforma
        readonly ISQLitePlatform _plataforma;
        // ruta espesifica donde esta la informacion de acuerdo a cada plataforma
        string _rutaBD;
        // Para realizar la conexion con la base de datos
        public SQLiteConnection Conexion { get; set; }

        //contructor para inicializar la paltaforma y la ruta de la base de datos 
        public BaseDatos(ISQLitePlatform plataforma, string rutaBD)
        {
            _plataforma = plataforma;
            _rutaBD = rutaBD;
        }

        // conectar con la base de datos
        public void Conectar()
        {
            // permisos en la base de datos escribir, crear tablas, establecer la relaciones entre las tabalas, true = serelizar la tabla en caso de Json por ejemplo
            Conexion = new SQLiteConnection(_plataforma, _rutaBD, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create| SQLiteOpenFlags.FullMutex, true);
            // crea las tablas y si estas ya estan creada no dublica dichas tablas
            Conexion.CreateTable<Jugador>();
            Conexion.CreateTable<Equipo>();
            Conexion.CreateTable<JugadorEquipo>();
        }


        //TODOS LOS METODOS PARA LA TABLA DE JUGADOR

        public void AgregarJugador(Jugador jugador)
        {
            lock (locker)
            {
                Conexion.Insert(jugador);
            }
        }
        public void ActualizarJugador(Jugador jugador, Equipo equipo = null)
        {
            lock (locker)
            {
                Conexion.Update(jugador);
                if (equipo != null)
                {
                    if (jugador.Equipos == null)
                    {
                        jugador.Equipos = new List<Equipo>();
                    }
                    if (jugador.Equipos.Where(x => x.ID == equipo.ID).Count() == 0)
                        jugador.Equipos.Add(equipo);
                    Conexion.UpdateWithChildren(jugador);
                }
            }
        }
        public void EliminarJugador(Jugador jugador)
        {
            lock (locker)
            {
                Conexion.Delete(jugador);
            }
        }
        public List<Jugador> ObtenerJugadores()
        {
            lock (locker)
            {
                //GetAllwithchilden= da todal a informacion del jugador y del equipo  - Tab = solo da la informacion del jugador
                return Conexion.GetAllWithChildren<Jugador>().OrderBy(x => x.Nombre).ToList();
            }
        }
        public Jugador ObtenerJugador(int id)
        {
            lock (locker)
            {
                return Conexion.GetWithChildren<Jugador>(id);
            }
        }
        public List<Equipo> ObtenerEquiposJugador(int id)
        {
            lock (locker)
            {
                return Conexion.GetWithChildren<Jugador>(id).Equipos;
            }
        }

        /// TODOS LOS METODOS PARA LA TABLA EQUIPO
        public void AgregarEquipo(Equipo equipo)
        {
            lock (locker)
            {
                Conexion.Insert(equipo);
            }
        }
        public void ActualizarEquipo(Equipo equipo)
        {
            lock (locker)
            {
                Conexion.Update(equipo);
            }
        }
        public void EliminarEquipo(Equipo equipo)
        {
            lock (locker)
            {
                Conexion.Delete(equipo);
            }
        }
        public List<Equipo> ObtenerEquipos()
        {
            lock (locker)
            {
                return Conexion.GetAllWithChildren<Equipo>().OrderBy(x => x.Nombre).ToList();
            }
        }
        public Equipo ObtenerEquipo(int id)
        {
            lock (locker)
            {
                return Conexion.GetWithChildren<Equipo>(id);
            }
        }
        public List<Jugador> ObtenerJugadoresEquipo(int id)
        {
            lock (locker)
            {
                return Conexion.GetWithChildren<Equipo>(id).Jugadores;
            }
        }

        // TODOS LOS METODOS PARA LA TABLA JUGADOR EQUIPO
        public JugadorEquipo ObtenerJugadorEquipo(int idEquipo, int idJugador)
        {
            lock (locker)
            {
                var tabla = Conexion.Table<JugadorEquipo>().ToList();
                var num = tabla.Where(x => x.IDEquipo == idEquipo && x.IDJugador == idJugador).Count();
                if (num > 0)
                    return tabla.Where(x => x.IDEquipo == idEquipo && x.IDJugador == idJugador).First();
                else
                    return new JugadorEquipo() { IDEquipo = idEquipo, IDJugador = idJugador, Goles = 0, Numero = 0 };
            }
        }
        public DetalleJugadorEquipo ObtenerDetalleJugadorEquipo(Equipo equipo, Jugador jugador)
        {
            lock (locker)
            {
                var jugadorEquipo = ObtenerJugadorEquipo(equipo.ID, jugador.ID);
                var detalle = new DetalleJugadorEquipo()
                {
                    NombreJugador = jugador.Nombre,
                    FotoJugador = jugador.Foto,
                    NombreEquipo = equipo.Nombre,
                    EscudoEquipo = equipo.Escudo,
                    Goles = (jugadorEquipo != null) ? jugadorEquipo.Goles : 0,
                    Numero = (jugadorEquipo != null) ? jugadorEquipo.Numero : 0
                };
                return detalle;
            }
        }
        public void ActualizarJugadorEquipo(JugadorEquipo jugadorEquipo)
        {
            lock (locker)
            {
                Conexion.Update(jugadorEquipo);
            }
        }
    }
}
