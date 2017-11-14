using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace Futbolapp.modelos
{
    public class JugadorEquipo
    {
        //Llave primaria
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        //Llaves foraneas de la tabla jugador y equipo
        [ForeignKey(typeof(Equipo))]
        public int IDEquipo { get; set; }

        [ForeignKey(typeof(Jugador))]
        public int IDJugador { get; set; }

        //Informacion a guardar en la tabla JugadorEquipo
        public int Numero { get; set; }
        public int Goles { get; set; }
    }
}
