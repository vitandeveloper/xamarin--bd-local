using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Threading.Tasks;

namespace Futbolapp.clases
{
    public static class ServicioImagenes
    {
        //Seleciona una imagen de la galeria del movil 
        public static async Task<MediaFile> SeleccionarImagen()
        {
            await CrossMedia.Current.Initialize();
            var file = await CrossMedia.Current.PickPhotoAsync();
             return file;
        }
    }
}
