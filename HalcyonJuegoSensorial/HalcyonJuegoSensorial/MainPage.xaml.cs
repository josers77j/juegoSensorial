using HalcyonJuegoSensorial.dataLayer;
using HalcyonJuegoSensorial.modelLayer;
using HalcyonJuegoSensorial.viewLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HalcyonJuegoSensorial
{
    public partial class MainPage : ContentPage
    {
        private readonly DataBase _database;

        public MainPage()
        {
            InitializeComponent();
            _database = new DataBase();
        }
        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            string nombreUsuario = UsernameEntry.Text;
            if (!string.IsNullOrWhiteSpace(nombreUsuario))
            {
                // Verificar si el nombre de usuario ya existe
                var existingUser = await _database.GetUsuarioByNameAsync(nombreUsuario);
                if (existingUser != null)
                {
                    await DisplayAlert("Error", "El nombre de usuario ya existe. Por favor, elige otro nombre.", "OK");
                    return;
                }

                var usuario = new ModelUser { NombreUsuario = nombreUsuario, Puntuacion = 0 };
                await _database.SaveUsuarioAsync(usuario);
                Preferences.Set("NombreUsuario", nombreUsuario);

                await Navigation.PushAsync(new ViewMenu());
            }
            else
            {
                await DisplayAlert("Error", "El nombre de usuario no puede estar vacío.", "OK");
            }
        }
        private async void OnVerUsuariosClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewScore());
        }
    }
}
