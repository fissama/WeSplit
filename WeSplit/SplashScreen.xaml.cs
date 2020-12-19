using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WeSplit.Model;


namespace WeSplit
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public Random _rng = new Random();
        public bool isChecked = false;

        public SplashScreen()
        {
            InitializeComponent();
            setDestinationDataContext();
            loadProgressBar();
        }

        public void setDestinationDataContext()
        {

            var db = new WeSplitEntities();
            int maxRan = db.Destinations.ToList().Count();
            int index = _rng.Next(1,maxRan);
            var destination = db.Destinations.Find(index);
            var des = new DestinationModel
            {
                imgPath = destination.DImage,
                nameDes = destination.DName,
                infoDes = destination.DIntroduce
            };
            this.splashScreen.DataContext = des;
        }

        public void loadProgressBar()
        {
            Duration dur = new Duration(TimeSpan.FromSeconds(10));
            DoubleAnimation dbani = new DoubleAnimation(200.0, dur);
            loading_PB.BeginAnimation(ProgressBar.ValueProperty, dbani);
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
        }

        private void skipBtn_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var db = new WeSplitEntities();
            var flat = db.Status.Find(1);
            if (flat.value == 1)
            {
                isChecked = true;
                dontShowCB.IsChecked = isChecked;
                var main = new MainWindow();
                main.Show();
                this.Close();
            }
            else { isChecked = false;
                dontShowCB.IsChecked = isChecked;
            }
        }

        private void checked_Click(object sender, RoutedEventArgs e)
        {
            var db = new WeSplitEntities();
            var flat = db.Status.Find(1);
            flat.value = 1;
            db.SaveChanges();
        }

        private void uncheck_Click(object sender, RoutedEventArgs e)
        {
            var db = new WeSplitEntities();
            var flat = db.Status.Find(1);
            flat.value = 0;
            db.SaveChanges();
        }
    }
}