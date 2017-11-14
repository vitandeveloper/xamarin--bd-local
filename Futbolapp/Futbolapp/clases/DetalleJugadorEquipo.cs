using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futbolapp.clases
{
    public class DetalleJugadorEquipo
    {
        //consulata a las tablas

        //conslta a la tabla jugador
        public string FotoJugador { get; set; }
        public string NombreJugador { get; set; }
        //consulta a la tabla equipo
        public string EscudoEquipo { get; set; }
        public string NombreEquipo { get; set; }
        //consulta a la tabla jugadorequipo
        public int Numero { get; set; }
        public int Goles { get; set; }
    }
}
