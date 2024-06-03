using HalcyonJuegoSensorial.dataLayer;
using HalcyonJuegoSensorial.viewLayer.primerDesafio;
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
        public ViewMenu()
        {
            _database = new DataBase();
            InitializeComponent();
            WelcomeLabel.Text = $"Bienvenido, {Preferences.Get("NombreUsuario", "Usuario")}";
        }       
        private async void OnVerUsuariosClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewScore());
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
    }
}