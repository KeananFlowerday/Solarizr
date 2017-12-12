﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Solarizr
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class Dashboard : Page
	{
<<<<<<< HEAD
        ObservableCollection<Appointment> Appointments;
        //List<ProjectSite> SiteList = new List<ProjectSite>();
        //initialised below
        public Dashboard()
=======
		AppointmentData apptData = new AppointmentData();
		ObservableCollection<Appointment> appointments;
		
		//initialised below
		public Dashboard()
>>>>>>> e8a434f979499d4b360e9e13bae929c4412890e2
		{
			this.InitializeComponent();

			appointments = apptData.GetTodaysAppointments();

            getMapObjects();
											 
			StartTimers();

			SmallMap.Loaded += Mapsample_Loaded;
            
            getMapObjects();
            
            //foreach( appt a in list) create marker on calander
            //foreach(appt a in list) if a.date == today create marker on map
            //sitelist read from db - make list;

            //initialize webview for weather - link from dian
            WebView_Weather.Navigate(new Uri("http://forecast.io/embed/#lat=42.3583&lon=-71.0603&name=the Job Site&color=#00aaff&font=Segoe UI&units=uk"));

		}
<<<<<<< HEAD

        private async void getMapObjects()
        {
            foreach (Appointment a in Appointments)
=======
		
private async void getMapObjects()
        {
            foreach (Appointment a in appointments)
>>>>>>> e8a434f979499d4b360e9e13bae929c4412890e2
            {
                // The address or business to geocode.
                string addressToGeocode = a.Address.ToString();

                // The nearby location to use as a query hint.
                BasicGeoposition queryHint = new BasicGeoposition();
                queryHint.Latitude = -28;
                queryHint.Longitude = 23;
                Geopoint hintPoint = new Geopoint(queryHint);

                // Geocode the specified address, using the specified reference point
                // as a query hint. Return no more than 3 results.
                MapLocationFinderResult result =
<<<<<<< HEAD
                      await MapLocationFinder.FindLocationsAsync(
                                        addressToGeocode,
                                        hintPoint,
                                        3);
=======
                      await MapLocationFinder.FindLocationsAsync(addressToGeocode, hintPoint);
>>>>>>> e8a434f979499d4b360e9e13bae929c4412890e2

                // If the query returns results, display the coordinates
                // of the first result.
                if (result.Status == MapLocationFinderStatus.Success)
                {
<<<<<<< HEAD
                    tbOutputText.Text = "result = (" +
                          result.Locations[0].Point.Position.Latitude.ToString() + "," +
                          result.Locations[0].Point.Position.Longitude.ToString() + ")";
=======
                    Debug.WriteLine("result = (" +
                          result.Locations[0].Point.Position.Latitude.ToString() + "," +
                          result.Locations[0].Point.Position.Longitude.ToString() + ")");

                    var center =
                    new Geopoint(new BasicGeoposition()
                    {
                        Latitude = result.Locations[0].Point.Position.Latitude,
                        Longitude = result.Locations[0].Point.Position.Longitude

                    });
                    await SmallMap.TrySetSceneAsync(MapScene.CreateFromLocationAndRadius(center, 3000));

                    //Define MapIcon
                    MapIcon myPOI = new MapIcon { Location = center, NormalizedAnchorPoint = new Point(0.5, 1.0), Title = a.Customer.Name, ZIndex = 0 };
                    // add to map and center it
                    SmallMap.MapElements.Add(myPOI);

>>>>>>> e8a434f979499d4b360e9e13bae929c4412890e2
                }
            }

        }

        private DispatcherTimer t_DateTime;

		public void StartTimers()
		{
			t_DateTime = new DispatcherTimer();
			t_DateTime.Tick += UpdateDateTime;
			t_DateTime.Interval = TimeSpan.FromSeconds(1);
			t_DateTime.Start();

		}

		public void UpdateDateTime(Object sender, Object e)
		{
			DateTime datetime = DateTime.Now;

			txtCurrTime.Text = datetime.ToString("hh:mm");
			txtCurrDate.Text = datetime.ToString("dddd, d MMMM yyyy");
		}

		private void BtnApptCreate_Click(object sender, RoutedEventArgs e)
		{
			//int cmbxItem = cmbxApptSitePicker.SelectedIndex;
			//ProjectSite pSite = SiteList[cmbxItem];


			DateTimeOffset _date = dateApptDatePicker.Date;
			TimeSpan _time = timeApptTimePicker.Time;

			DateTime apptDateTime;

			apptDateTime = _date.DateTime;
			apptDateTime.Add(_time);

			#region Notes
			//To convert back to offset and bind to datetimepicker   
			//DateTime newBookingDate;
			//newBookingDate = DateTime.SpecifyKind(apptDateTime, DateTimeKind.Utc);
			//DateTimeOffset bindTime = newBookingDate;
			//dateApptDatePicker.Date = bindTime;
			#endregion

			//Appointment newAppointment = new Appointment(apptDateTime, pSite);
		}

		private async void Mapsample_Loaded(object sender, RoutedEventArgs e)
		{
			var geoLocator = new Geolocator();
			geoLocator.DesiredAccuracy = PositionAccuracy.High;
			Geoposition pos = await geoLocator.GetGeopositionAsync();


			var center =
				new Geopoint(new BasicGeoposition()
				{
					Latitude = pos.Coordinate.Point.Position.Latitude,
					Longitude = pos.Coordinate.Point.Position.Longitude

				});
			await SmallMap.TrySetSceneAsync(MapScene.CreateFromLocationAndRadius(center, 3000));

			//Define MapIcon
			//MapIcon myPOI = new MapIcon { Location = center, NormalizedAnchorPoint = new Point(0.5, 1.0), Title = "Qaanita", ZIndex = 0 };
			// add to map and center it
			//SmallMap.MapElements.Add(myPOI);


		}

		private void AppBarHome_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(Dashboard), e);
		}

		private void AppBarProjSite_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(SiteManager), e);
		}

		private void AppBarAppointment_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(AppointmentManager), e);
		}

		private void AppBarMap_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MapView), e);
		}

		private void SetAlarm(object sender, RoutedEventArgs e)
		{
			//toastnotifications
		}
	}
}
