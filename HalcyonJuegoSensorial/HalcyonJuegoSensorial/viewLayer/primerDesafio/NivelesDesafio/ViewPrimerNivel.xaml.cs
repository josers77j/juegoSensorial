using HalcyonJuegoSensorial.dataLayer;
using HalcyonJuegoSensorial.modelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HalcyonJuegoSensorial.viewLayer.primerDesafio.NivelesDesafio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPrimerNivel : ContentPage
    {
        private readonly DataBase _database;
        private ModelUser _usuario;
        private int _intentos;

        public ViewPrimerNivel()
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

                if (_usuario.Puntuacion >= 100)
                {
                    await DisplayAlert("Nivel Completado", "Ya has alcanzado el máximo de puntos para este nivel.", "OK");
                    await Navigation.PopAsync();
                }
                else if (puntosRestantes <= 0)
                {
                    Button button = (Button)sender;
                    button.BackgroundColor = Color.Red; //se pone de color rojo el botón

                    await DisplayAlert("Inténtalo de nuevo", "Has perdido todos los puntos. Inténtalo nuevamente.", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    Button button = (Button)sender;
                    button.BackgroundColor = Color.Red; //se pone de color rojo el botón

                    await DisplayAlert("Respuesta incorrecta", $"Has perdido {puntosPerdidos} puntos. Puntos restantes: {puntosRestantes}.", "OK");
                }
            }
        }

        private async void OnCorrectAnswerClicked(object sender, EventArgs e)
        {
            if (_usuario != null)
            {
                int puntosGanados = 100 - (25 * _intentos);

                if (_usuario.Puntuacion >= 100)
                {
                    await DisplayAlert("Nivel Completado", "Ya has alcanzado el máximo de puntos para este nivel.", "OK");
                }
                else
                {
                    Vibration.Vibrate(TimeSpan.FromMilliseconds(500)); // Vibra al seleccionar respuesta correcta

                    _usuario.Puntuacion += puntosGanados;
                    if (_usuario.Puntuacion > 100) _usuario.Puntuacion = 100; // Máximo de puntos para el nivel 1
                    await _database.SaveUsuarioAsync(_usuario);

                    Button button = (Button)sender;
                    button.BackgroundColor = Color.Green; //se pone de color verde el botón

                    await DisplayAlert("Respuesta correcta", $"Has ganado {puntosGanados} puntos.", "OK");
                }

                await Navigation.PopAsync();
            }
        }

        private async void OnImageClicked(object sender, EventArgs e) // Al tocar la imagen suena audio
        {
            var text = "Le gusta jugar, también ladrar y cuidar la casa cuando los amos no están.";

            var locales = await TextToSpeech.GetLocalesAsync();
            var spanishLocale = locales.FirstOrDefault(locale => locale.Language == "es" && locale.Country == "ES");

            var settings = new SpeechOptions()
            {
                Locale = spanishLocale
            };

            await TextToSpeech.SpeakAsync(text, settings);
        }
    }
}
