using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace Futbolapp.modelos
{
    public class Equipo
    {
        //Se declara la clave primara y es autoincrimental
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Nombre { get; set; }
        public string Escudo { get; set; }

        [ManyToMany(typeof(JugadorEquipo))]
        public List <Jugador> Jugadores { get; set; }
    }
}
