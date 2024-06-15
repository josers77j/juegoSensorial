using HalcyonJuegoSensorial.dataLayer;
using HalcyonJuegoSensorial.modelLayer;
using HalcyonJuegoSensorial.viewLayer.primerDesafio;
using HalcyonJuegoSensorial.viewLayer.SegundoDesafio;
using HalcyonJuegoSensorial.viewLayer.tercerDesafio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HalcyonJuegoSensorial.viewLayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMenu : ContentPage
    {
        private readonly DataBase _database;
        private ModelUser _usuario;

        public ViewMenu()
        {
            _database = new DataBase();
            InitializeComponent();
            LoadUsuario();
         
        }
        
        private async void OnVerUsuariosClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewScore());
        }

        private async Task LoadUsuario()
        {
            string nombreUsuario = Preferences.Get("NombreUsuario", string.Empty);
            if (!string.IsNullOrWhiteSpace(nombreUsuario))
            {
                _usuario = await _database.GetUsuarioByNameAsync(nombreUsuario);
                if (_usuario != null)
                {
                    PuntajeLabel.Text = $"Puntaje: {_usuario.Puntuacion}";
                    WelcomeLabel.Text = $"¡Bienvenido, {Preferences.Get("NombreUsuario", "Usuario")}!";
                }
            }
        }

        private async void OnSalirClicked(object sender, EventArgs e)
        {
            Preferences.Remove("NombreUsuario");
            await Navigation.PopToRootAsync();
        }

        private async void OnDesafio1Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewPrimerDesafio());
        }

        private async void OnDesafio2Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewSegundoDesafio());
        }

        private async void OnDesafio3Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewTercerDesafio());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadUsuario();
        }
    }
}