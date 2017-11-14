using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;


namespace Futbolapp.modelos
{
    public class Jugador
    {
        //Se declara la clave primara y es autoincrimental
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Nombre { get; set; }
        public string FechaNacimiento { get; set; } = DateTime.MinValue.ToString();
        public string Foto { get; set; }

        [ManyToMany(typeof(JugadorEquipo))]

        public List<Equipo> Equipos { get; set; }
    }
}
