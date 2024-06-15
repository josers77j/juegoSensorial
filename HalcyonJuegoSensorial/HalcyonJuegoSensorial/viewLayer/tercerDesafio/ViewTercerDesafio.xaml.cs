using HalcyonJuegoSensorial.dataLayer;
using HalcyonJuegoSensorial.modelLayer;
using HalcyonJuegoSensorial.viewLayer.tercerDesafio.NivelesDesafio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HalcyonJuegoSensorial.viewLayer.tercerDesafio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewTercerDesafio : ContentPage
	{
        private readonly DataBase _database;
        private ModelUser _usuario;
        public ViewTercerDesafio()
		{
			InitializeComponent ();
            _database = new DataBase();
            LoadUsuario();
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
                }
            }
        }
        private async void OnNivel1Clicked(object sender, EventArgs e)
        {
            if (_usuario != null && _usuario.Puntuacion >= 570)
            {
                await Navigation.PushAsync(new ViewPrimerNivel());
            }
            else
            {
                await DisplayAlert("Acceso Restringido", "No tienes el puntaje suficiente para acceder al Nivel 1.", "OK");
            }
        }
        private async void OnNivel2Clicked(object sender, EventArgs e)
        {
            if (_usuario != null && _usuario.Puntuacion >= 670)
            {
                await Navigation.PushAsync(new ViewSegundoNivel());
            }
            else
            {
                await DisplayAlert("Acceso Restringido", "No tienes el puntaje suficiente para acceder al Nivel 2.", "OK");
            }
        }
        private async void OnNivel3Clicked(object sender, EventArgs e)
        {
            if (_usuario != null && _usuario.Puntuacion > 770)
            {
                await Navigation.PushAsync(new ViewTercerNivel());
            }
            else
            {
                await DisplayAlert("Acceso Restringido", "No tienes el puntaje suficiente para acceder al Nivel 3.", "OK");
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadUsuario();
        }
    }
}