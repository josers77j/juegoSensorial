using HalcyonJuegoSensorial.dataLayer;
using HalcyonJuegoSensorial.modelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HalcyonJuegoSensorial.viewLayer.SegundoDesafio.NivelesDesafio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewTercerNivel : ContentPage
    {
        private readonly DataBase _database;
        private ModelUser _usuario;
        private int _intentos;
        public ViewTercerNivel()
        {
            InitializeComponent();
            _database = new DataBase();
            LoadUsuario();
            _intentos = 0;
        }
        private async Task LoadUsuario()
        {
            string nombreUsuario = Preferences.Get("NombreUsuario", string.Empty);
            if (!string.IsNullOrWhiteSpace(nombreUsuario))
            {
                _usuario = await _database.GetUsuarioByNameAsync(nombreUsuario);
            }
        }
        private async void OnIncorrectAnswerClicked(object sender, EventArgs e)
        {
            if (_usuario != null)
            {
                _intentos++;
                int puntosPerdidos = 25 * _intentos;
                int puntosRestantes = 100 - puntosPerdidos;

                if (puntosRestantes <= 0)
                {
                    await DisplayAlert("Inténtalo de nuevo", "Has perdido todos los puntos. Inténtalo nuevamente.", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Respuesta incorrecta", $"Has perdido {puntosPerdidos} puntos. Puntos restantes: {puntosRestantes}.", "OK");
                }
            }
        }
        private async void OnCorrectAnswerClicked(object sender, EventArgs e)
        {
            if (_usuario != null)
            {
                int puntosGanados = 100 - (25 * _intentos);

                if (_usuario.Puntuacion >= 500)
                {
                    await DisplayAlert("Nivel Completado", "Ya has alcanzado el máximo de puntos para este nivel.", "OK");
                }
                else
                {
                    _usuario.Puntuacion += puntosGanados;
                    if (_usuario.Puntuacion > 500) _usuario.Puntuacion = 500; // Máximo de puntos para el nivel 3
                    await _database.SaveUsuarioAsync(_usuario);
                    await DisplayAlert("Respuesta correcta", $"Has ganado {puntosGanados} puntos.", "OK");
                }

                await Navigation.PopAsync();
            }
        }
    }
}