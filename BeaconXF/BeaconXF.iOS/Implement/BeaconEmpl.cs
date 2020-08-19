using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeaconXF;
using BeaconXF.iOS;
using CoreBluetooth;
using CoreLocation;
using Foundation;
using UIKit;
using TTGSnackBar;

[assembly: Xamarin.Forms.Dependency(typeof(BeaconEmpl))]
namespace BeaconXF.iOS
{


    public class BeaconEmpl : IBeacon
    {

        public BeaconEmpl() : base()
        {

        }

        public CLLocationManager manager { set; get; }

        public CLBeaconRegion beaconregin { set; get; }

        public IntPtr Handle
        {
            get
            {
                return new IntPtr(0);
            }
        }



        public void Dispose()
        {

        }

        public void Initial()
        {
            manager = new CLLocationManager();
            //manager.Delegate = new MyLocationDelegate();

            manager.AllowsBackgroundLocationUpdates = true;

            manager.RequestWhenInUseAuthorization();

            manager.RequestAlwaysAuthorization();


            this.beaconregin = new CLBeaconRegion(new NSUuid("FDA50693-A4E2-4FB1-AFCF-C6EB07647826"), "myregion");

            this.beaconregin.NotifyEntryStateOnDisplay = true;
            this.manager.DesiredAccuracy = 100;
            this.manager.DistanceFilter = 100;
            this.manager.AllowsBackgroundLocationUpdates = true;
           

            //this.manager.LocationsUpdated += Manager_LocationsUpdated;

            //this.manager.StartRangingBeacons(this.beaconregin);

            //this.manager.RegionEntered += Manager_RegionEntered;

            this.manager.DidRangeBeacons += Manager_DidRangeBeacons;
            //throw new NotImplementedException();
        }

        private void Manager_DidRangeBeacons(object sender, CLRegionBeaconsRangedEventArgs e)
        {
            Message.InvokeSnackBar($"DidRangeBeacons - {e.Beacons[0].Minor} - {DateTime.Now.ToString("yyyy/MM/dd-HH:mm")}");
        }

        private void Manager_RegionEntered(object sender, CLRegionEventArgs e)
        {
            Message.InvokeSnackBar("Manager_RegionEntered");
        }

        private void Manager_LocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
        {
            Message.InvokeSnackBar("Manager_LocationsUpdated");
            //throw new NotImplementedException();
        }

        public void Start()
        {
            this.manager.StartMonitoring(this.beaconregin);
            this.manager.StartRangingBeacons(this.beaconregin);
            this.manager.StartUpdatingLocation();
            //throw new NotImplementedException();
        }

        public void Stop()
        {
            //throw new NotImplementedException();
        }
    }


    public class Message
    {
        public static void InvokeSnackBar(string message)
        {
            var snackbar = new TTGSnackbar(message);

            snackbar.Duration = TimeSpan.FromSeconds(3);
            snackbar.MessageTextColor = UIColor.White;

            UIColor oragecolor = new UIColor(new nfloat(255 / 255f), new nfloat(140 / 255f), new nfloat(0), 1);

            snackbar.BackgroundColor = oragecolor;
            snackbar.MessageTextAlign = UITextAlignment.Center;
            UIFont font = UIFont.FromName("Menlo", 16);

            snackbar.Show();

        }
    }

    public class MyLocationDelegate : CLLocationManagerDelegate
    {
        public override void RegionEntered(CLLocationManager mgr, CLRegion rgn)
        {
            Message.InvokeSnackBar("Entered region " + rgn.Identifier);
            Console.WriteLine("Entered region " + rgn.Identifier);
        }
        public override void RegionLeft(CLLocationManager mgr, CLRegion rgn)
        {
            Message.InvokeSnackBar("Left region " + rgn.Identifier);
            Console.WriteLine("Left region " + rgn.Identifier);
        }


        
    }
}
