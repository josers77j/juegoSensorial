using HalcyonJuegoSensorial.dataLayer;
using HalcyonJuegoSensorial.modelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HalcyonJuegoSensorial.viewLayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewScore : ContentPage
    {
        private readonly DataBase _database;

        public ViewScore()
        {
            InitializeComponent();
            _database = new DataBase();
            LoadUsers();
        }
        private async Task LoadUsers()
        {
            List<ModelUser> users = await _database.GetUsuariosAsync();
            UsersListView.ItemsSource = users;
        }
    }
}