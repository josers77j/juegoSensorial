using HalcyonJuegoSensorial.dataLayer;
using HalcyonJuegoSensorial.modelLayer;
using HalcyonJuegoSensorial.viewLayer;
using HalcyonJuegoSensorial.viewLayer.primerDesafio;
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

        private async void OnIngresoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewLogin());
        }
    }
}
