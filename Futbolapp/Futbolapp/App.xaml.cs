using Futbolapp.datos;
using SQLite.Net.Interop;
using Xamarin.Forms;


namespace Futbolapp
{
    public partial class App : Application
    {
        //conectar con la base de datos y es publico para poder operar en el dentro de toda la app
        public static BaseDatos BaseDatos { get; set; }
       
        public App(string rutaBD, ISQLitePlatform plataforma)
        {
            //iniciar y conectar la base de datos
            BaseDatos = new BaseDatos(plataforma, rutaBD);
            BaseDatos.Conectar();

            InitializeComponent();

            MainPage = new NavigationPage(new FutbolApp.Paginas.PaginaMenu());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
