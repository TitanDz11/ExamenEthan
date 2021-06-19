using EthanDiaz.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EthanDiaz
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lista : ContentPage
    {
        public Lista()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conexion = new SQLiteConnection(App.UbicacionDB))
            {
                conexion.CreateTable<Ubicacion>();
                var listubicacion = conexion.Table<Ubicacion>().ToList();
                ListaUbicacion.ItemsSource = listubicacion;
            }
        }

        private void ListaUbicacion_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Ubicacion selectedItem = e.SelectedItem as Ubicacion;
            int iduser = selectedItem.id;
            string nombre = selectedItem.DescripcionCorta;
            this.Navigation.PushAsync(new Page1(iduser,nombre));

        }

        private void ListaUbicacion_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}