using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using EthanDiaz.Model;


namespace EthanDiaz
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnsalvado_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Lista());
        }

        private async void btnmapas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }

        private void btnSalvar_Clicked(object sender, EventArgs e)
        {
            if (txtLatitud.Text == "")
            {
                DisplayAlert("Alerta", "Debe ingresar Latitud", "Ok");
            }
            else
            {
                Int32 resultado = 0;
                var ubicacion = new Ubicacion()
                {
                    Latitud = txtLatitud.Text,
                    Longitud = txtLongitud.Text,
                    Descripcion = txtDescripcion.Text,
                    DescripcionCorta = txtDescripcionCorta.Text
                };
                using (SQLiteConnection conexion = new SQLiteConnection(App.UbicacionDB))
                {
                    conexion.CreateTable<Ubicacion>();
                    resultado = conexion.Insert(ubicacion);

                    if (resultado > 0)
                        DisplayAlert("Aviso", "Adicionado", "Ok");
                    else
                        DisplayAlert("Aviso", "Error", "Ok");
                }
            }
            
        }
    }
}
