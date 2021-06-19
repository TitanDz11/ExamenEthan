﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using Plugin.Geolocator;

namespace EthanDiaz
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            /* Pin ubicacion = new Pin();
             ubicacion.Label = "SPS";
             ubicacion.Address = "Cerca de uth";
             ubicacion.Position = new Position(15.5510539,-88.0109923);
             mapas.Pins.Add(ubicacion);

             var localizacion = await Geolocation.GetLastKnownLocationAsync();

             if (localizacion == null)
             {
                 localizacion = await Geolocation.GetLocationAsync();                
             }
             mapas.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(localizacion.Latitude, localizacion.Longitude),
                     Distance.FromKilometers(1)));

             */

            var localizacion = CrossGeolocator.Current;

            if (localizacion != null)
            {
                localizacion.PositionChanged += Localizacion_PositionChanged;
                if (!localizacion.IsListening)
                {
                    await localizacion.StartListeningAsync(TimeSpan.FromSeconds(10), 100);
                }

                var posicion = await localizacion.GetPositionAsync();
                var centromapa = new Position(posicion.Latitude, posicion.Longitude);
                mapas.MoveToRegion(new MapSpan(centromapa, 1, 1));
            }
            else 
            {
                              
                var posicion = await localizacion.GetLastKnownLocationAsync();
                var centromapa = new Position(posicion.Latitude, posicion.Longitude);
                mapas.MoveToRegion(new MapSpan(centromapa, 1, 1));
                await DisplayAlert("Alerta", "Activar GPS", "OK");
            }
            
        }

        private void Localizacion_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var centromapa = new Position(e.Position.Latitude, e.Position.Longitude);
            mapas.MoveToRegion(new MapSpan(centromapa, 1, 1));
        }
    }
}