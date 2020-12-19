using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeSplit.Model;
using LiveCharts;
using LiveCharts.Wpf;

namespace WeSplit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<JourneyModel> journeys = new List<JourneyModel>();
        List<String> listImgPath = new List<String>();
        List<String> fileName = new List<String>();
        List<ExpenditureModel> listExpend = new List<ExpenditureModel>();
        List<MemberModel> listMember = new List<MemberModel>();
        public SeriesCollection expendSeri { get; set; }
        public SeriesCollection donaSeri { get; set; }

        public MainWindow()
        {
            InitializeComponent();

        }

        private void expendPieChartLoad(int id)
        {
            var expend = journeys.ElementAt(id - 1).JExpenditure;
            try
            {
                expendSeri = new SeriesCollection();
                foreach(var el in expend)
                {
                    var seri = new PieSeries();
                    seri.Title = el.EName;
                    seri.Values = new ChartValues<int> { Convert.ToInt32(el.EPrice) };
                    seri.DataLabels = true;
                    expendSeri.Add(seri);
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show("Oh no! No! No! No! No!");
            }
        }
        private void donaPieChartLoad(int id)
        {
            var dona = journeys.ElementAt(id - 1).JMembers;
            donaSeri = new SeriesCollection();
            foreach (var el in dona)
            {
                var seri = new PieSeries();
                seri.Title = el.MName;
                seri.Values = new ChartValues<int> { Convert.ToInt32(el.MDonation) };
                seri.DataLabels = true;
                donaSeri.Add(seri);
            }
        }

        private void Power_Click(object sender, RoutedEventArgs e)
        {
            PowerWindow powerWindow = new PowerWindow();
            powerWindow.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadJourneyToScreen();
            var db = new WeSplitEntities();
            var stat = db.Status.Find(1);
            if (stat.value == 1)
            {
                this.splashOnOffBtn.IsChecked = true;
            }
            else
            {
                this.splashOnOffBtn.IsChecked = false;
            }
            hideAllWindow();
            this.homeWindow.Visibility = Visibility.Visible;
        }

        private void splashOnOff_Checked(object sender, RoutedEventArgs e)
        {
            var db = new WeSplitEntities();
            var flat = db.Status.Find(1);
            flat.value = 1;
            db.SaveChanges();
        }

        private void splashOnOff_UnChecked(object sender, RoutedEventArgs e)
        {
            var db = new WeSplitEntities();
            var flat = db.Status.Find(1);
            flat.value = 0;
            db.SaveChanges();
        }

        private void hideAllWindow()
        {
            this.homeWindow.Visibility = Visibility.Hidden;
            this.settingWindow.Visibility = Visibility.Hidden;
            this.infoWindow.Visibility = Visibility.Hidden;
            this.createWindow.Visibility = Visibility.Hidden;
            this.searchWindow.Visibility = Visibility.Hidden;
            this.journeyWindow.Visibility = Visibility.Hidden;
        }

        private void home_Clicked(object sender, RoutedEventArgs e)
        {
            hideAllWindow();
            this.homeWindow.Visibility = Visibility.Visible;
        }

        private void search_Clicked(object sender, RoutedEventArgs e)
        {
            hideAllWindow();
            this.searchWindow.Visibility = Visibility.Visible;
        }

        private void create_Clicked(object sender, RoutedEventArgs e)
        {
            hideAllWindow();
            this.destinationCB.Items.Clear();
            changeEditOrCreateMode("Create Journey");
            var db = new WeSplitEntities();
            foreach (var el in db.Destinations.ToList())
            {
                destinationCB.Items.Add(el.DName);
            }
            this.createWindow.Visibility = Visibility.Visible;
        }

        private void setting_Clicked(object sender, RoutedEventArgs e)
        {
            hideAllWindow();
            this.settingWindow.Visibility = Visibility.Visible;
        }

        private void info_Clicked(object sender, RoutedEventArgs e)
        {
            hideAllWindow();
            this.infoWindow.Visibility = Visibility.Visible;
        }

        private void pushImage(int id)
        {
            JImageSP.Children.Clear();
            foreach(var el in journeys.ElementAt(id - 1).JImage)
            {
                var image = new Image();
                var currentFolder = AppDomain.CurrentDomain.BaseDirectory;
                var absolute = $"{currentFolder}{el}";
                var uriSource = new Uri(absolute);
                image.Source = new BitmapImage(uriSource);
                image.Stretch = Stretch.UniformToFill;
                image.Width = 200;
                image.Height = 200;
                JImageSP.Children.Add(image);
            }
        }

        private void pushInfoTrip(int id)
        {
            pushExpenditure(id);
            pushMember(id);
            pushSplit(id);
            pushImage(id);

            expendPieChartLoad(id);
            expendPieStack.Children.Clear();
            donaPieStack.Children.Clear();
            var pie = new PieChart();
            pie.LegendLocation = LegendLocation.Left;
            pie.Width = 400;
            pie.Height = 400;
            pie.Series = expendSeri;
            pie.Margin = new Thickness(20, 20, 20, 20);
            ((DefaultLegend)pie.ChartLegend).BulletSize = 10;
            ((DefaultTooltip)pie.DataTooltip).BulletSize = 5;
            expendPieStack.Children.Add(pie);
            expendPieStack.Children.Add(new TextBlock { Text = "Expenditure", FontSize = 20, TextAlignment = TextAlignment.Center });
            donaPieChartLoad(id);
            var pie2 = new PieChart();
            pie2.LegendLocation = LegendLocation.Right;
            pie2.Width = 400;
            pie2.Height = 400;
            pie2.Series = donaSeri;
            pie2.Margin = new Thickness(20, 20, 20, 20);
            ((DefaultLegend)pie2.ChartLegend).BulletSize = 10;
            ((DefaultTooltip)pie2.DataTooltip).BulletSize = 5;
            donaPieStack.Children.Add(pie2);
            donaPieStack.Children.Add(new TextBlock { Text = "Donation", FontSize = 20, TextAlignment = TextAlignment.Center });
        }

        private void pushSplit(int id)
        {
            JSplitSP.Children.Clear();
            var jour = journeys.ElementAt(id - 1);
            var moneys = new List<String>();
            int sumPrice = 0;
            foreach (var el in jour.JExpenditure)
            {
                sumPrice += Convert.ToInt32(el.EPrice);
            }
            double average = sumPrice / jour.JMembers.Count();
            int memPrice = (int)Math.Floor(average);
            foreach (var el in jour.JMembers)
            {
                if (memPrice - Convert.ToInt32(el.MDonation) > 0)
                {
                    moneys.Add((memPrice - Convert.ToInt32(el.MDonation)).ToString() + "(Pay)");

                }
                else
                {
                    moneys.Add((Convert.ToInt32(el.MDonation) - memPrice).ToString() + "(Receive)");
                }
            }
            var stackHeader = new StackPanel();
            stackHeader.Margin = new Thickness(10, 0, 10, 5);
            stackHeader.Orientation = Orientation.Horizontal;
            stackHeader.Background = Brushes.RoyalBlue;
            var txt1 = new TextBlock();
            txt1.Width = 480;
            txt1.TextAlignment = TextAlignment.Center;
            txt1.FontWeight = FontWeights.Bold;
            txt1.FontSize = 20;
            txt1.Text = "Member";
            txt1.Foreground = Brushes.White;
            var txt2 = new TextBlock();
            txt2.Width = 300;
            txt2.FontSize = 20;
            txt2.TextAlignment = TextAlignment.Center;
            txt2.FontWeight = FontWeights.Bold;
            txt2.Text = "Split";
            txt2.Foreground = Brushes.White;
            stackHeader.Children.Add(txt1);
            stackHeader.Children.Add(txt2);
            JSplitSP.Children.Add(stackHeader);
            for (int i = 0; i < jour.JMembers.Count(); i++)
            {
                var stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                var text1 = new TextBlock();
                text1.Width = 380;
                text1.TextAlignment = TextAlignment.Center;
                text1.FontSize = 20;
                var text2 = new TextBlock();
                text2.Width = 400;
                text2.FontSize = 20;
                text2.TextAlignment = TextAlignment.Center;
                text1.Foreground = Brushes.Black;
                text2.Foreground = Brushes.Black;
                text1.FontWeight = FontWeights.Normal;
                text2.FontWeight = FontWeights.Normal;
                text1.Text = jour.JMembers.ElementAt(i).MName;
                text2.Text = moneys.ElementAt(i);
                stack.Children.Add(text1);
                stack.Children.Add(text2);
                stack.Margin = new Thickness(10, 0, 10, 5);
                JSplitSP.Children.Add(stack);
            }
        }

        private void pushMember(int id)
        {
            JMemberSP.Children.Clear();
            var jour = journeys.ElementAt(id - 1);
            var stackHeader = new StackPanel();
            stackHeader.Margin = new Thickness(10, 0, 10, 5);
            stackHeader.Orientation = Orientation.Horizontal;
            stackHeader.Background = Brushes.RoyalBlue;
            var txt1 = new TextBlock();
            txt1.Width = 480;
            txt1.TextAlignment = TextAlignment.Center;
            txt1.FontWeight = FontWeights.Bold;
            txt1.FontSize = 20;
            txt1.Text = "Member";
            txt1.Foreground = Brushes.White;
            var txt2 = new TextBlock();
            txt2.Width = 300;
            txt2.FontSize = 20;
            txt2.TextAlignment = TextAlignment.Center;
            txt2.FontWeight = FontWeights.Bold;
            txt2.Text = "Donation";
            txt2.Foreground = Brushes.White;
            stackHeader.Children.Add(txt1);
            stackHeader.Children.Add(txt2);
            JMemberSP.Children.Add(stackHeader);
            foreach (var el in jour.JMembers)
            {
                var stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                var text1 = new TextBlock();
                text1.Width = 480;
                text1.TextAlignment = TextAlignment.Center;
                text1.FontSize = 20;
                var text2 = new TextBlock();
                text2.Width = 300;
                text2.FontSize = 20;
                text2.TextAlignment = TextAlignment.Center;
                text1.Foreground = Brushes.Black;
                text2.Foreground = Brushes.Black;
                text1.FontWeight = FontWeights.Normal;
                text2.FontWeight = FontWeights.Normal;
                text1.Text = el.MName;
                text2.Text = el.MDonation;
                stack.Children.Add(text1);
                stack.Children.Add(text2);
                stack.Margin = new Thickness(10, 0, 10, 5);
                JMemberSP.Children.Add(stack);
            }
        }

        private void pushExpenditure(int id)
        {
            JExpenditureSP.Children.Clear();
            var jour = journeys.ElementAt(id - 1);
            var stackHeader = new StackPanel();
            stackHeader.Margin = new Thickness(10, 0, 10, 5);
            stackHeader.Orientation = Orientation.Horizontal;
            stackHeader.Background = Brushes.RoyalBlue;
            var txt1 = new TextBlock();
            txt1.Width = 480;
            txt1.TextAlignment = TextAlignment.Center;
            txt1.FontWeight = FontWeights.Bold;
            txt1.FontSize = 20;
            txt1.Text = "Expenditure";
            txt1.Foreground = Brushes.White;
            var txt2 = new TextBlock();
            txt2.Width = 300;
            txt2.FontSize = 20;
            txt2.TextAlignment = TextAlignment.Center;
            txt2.FontWeight = FontWeights.Bold;
            txt2.Text = "Price";
            txt2.Foreground = Brushes.White;
            stackHeader.Children.Add(txt1);
            stackHeader.Children.Add(txt2);
            JExpenditureSP.Children.Add(stackHeader);
            foreach (var el in jour.JExpenditure)
            {
                var stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                var text1 = new TextBlock();
                text1.Width = 480;
                text1.TextAlignment = TextAlignment.Center;
                text1.FontSize = 20;
                var text2 = new TextBlock();
                text2.Width = 300;
                text2.FontSize = 20;
                text2.TextAlignment = TextAlignment.Center;
                text1.Foreground = Brushes.Black;
                text2.Foreground = Brushes.Black;
                text1.FontWeight = FontWeights.Normal;
                text2.FontWeight = FontWeights.Normal;
                text1.Text = el.EName;
                text2.Text = el.EPrice;
                stack.Children.Add(text1);
                stack.Children.Add(text2);
                stack.Margin = new Thickness(10, 0, 10, 5);
                JExpenditureSP.Children.Add(stack);
            }
        }

        private void loadJourneyToScreen()
        {
            loadDataJourney();
            this.ongoingStackPanel.Children.Clear();
            this.completedStackPanel.Children.Clear();
            foreach (var jour in journeys)
            {
                var border = new Border();
                var dock = new StackPanel();
                var image = new Image();
                var currentFolder = AppDomain.CurrentDomain.BaseDirectory;
                var absolute = $"{currentFolder}{jour.JImage.First()}";
                var uriSource = new Uri(absolute);
                image.Source = new BitmapImage(uriSource);
                image.Stretch = Stretch.UniformToFill;
                image.Width = 200;
                image.Height = 200;
                dock.Width = 200;
                dock.Height = 250;
                dock.Margin = new Thickness(10, 0, 10, 0);
                image.Margin = new Thickness(0, 5, 0, 5);
                var desName = new TextBlock();
                desName.Text = jour.nameDes;
                desName.FontSize = 25;
                desName.Foreground = Brushes.Black;
                desName.FontWeight = FontWeights.Bold;
                desName.Width = 200;
                desName.Background = Brushes.Transparent;
                desName.HorizontalAlignment = HorizontalAlignment.Center;
                desName.TextAlignment = TextAlignment.Center;
                dock.Children.Add(image);
                dock.Children.Add(desName);
                dock.Background = Brushes.Wheat; ;
                int id_copy = jour.JId;
                dock.MouseLeftButtonUp += (sender, e) =>
                {
                    journey_Clicked(sender, e, id_copy);
                };
                if (jour.JStatus == false)
                {
                    this.ongoingStackPanel.Children.Add(dock);
                }
                else
                {
                    this.completedStackPanel.Children.Add(dock);
                }
            }

        }

        private void journey_Clicked(object sender, RoutedEventArgs e, int id)
        {
            this.DataContext = journeys.ElementAt(id - 1);
            pushInfoTrip(id);
            hideAllWindow();
            this.editBtn.Click += (sender3, e3) => edit_Clicked(sender, e, id);
            this.journeyWindow.Visibility = Visibility.Visible;
        }

        private void loadDataJourney()
        {
            this.journeys.Clear();
            var db = new WeSplitEntities();
            var journeys = db.Journeys.ToList();
            foreach (var jour in journeys)
            {
                JourneyModel temp = new JourneyModel();
                temp.JName = jour.JName;
                temp.JId = jour.JourneyID;
                if (jour.JStatus == true)
                {
                    temp.JStatus = true;
                    temp.Jstatus = "Completed";
                }
                else
                {
                    temp.JStatus = false;
                    temp.Jstatus = "Ongoing";
                }
                temp.JIntroduce = jour.JIntroduce;
                temp.JDayStart = (DateTime)jour.JDayStart;
                temp.JDayEnd = (DateTime)jour.JDayEnd;
                var des = db.Destinations.Where(s => s.DestinationID == jour.DestinationID);
                temp.nameDes = des.First().DName;
                temp.infoDes = des.First().DIntroduce;
                temp.imgPath = des.First().DImage;
                var expends = db.Expenditures.Where(s => s.JourneyID == jour.JourneyID);
                foreach (var ex in expends)
                {
                    ExpenditureModel tempEx = new ExpenditureModel();
                    tempEx.EName = ex.EName;
                    tempEx.EPrice = ex.EPrice;
                    temp.JExpenditure.Add(tempEx);
                }
                var members = db.Members.Where(s => s.JourneyID == jour.JourneyID);
                foreach (var mem in members)
                {
                    MemberModel tempMem = new MemberModel();
                    tempMem.MName = mem.MName;
                    tempMem.MDonation = mem.MDonation;
                    temp.JMembers.Add(tempMem);
                }
                var images = db.JImages.Where(s => s.JourneyID == jour.JourneyID);
                foreach(var el in images)
                {
                    temp.JImage.Add(el.JImageLink);
                }
                this.journeys.Add(temp);
            }
        }

        private void searchJourney_Clicked(object sender, RoutedEventArgs e)
        {
            ongoingSearch.Children.Clear();
            completedSearch.Children.Clear();
            if (journeyCB.IsSelected)
            {
                var searchJourney = journeys.Where(s => convertToUnSign(s.JName).Contains(convertToUnSign(searchTB.Text))).ToList();
                foreach (var jour in searchJourney)
                {

                    var stackSearch = new StackPanel();
                    var image2 = new Image();
                    var currentFolder = AppDomain.CurrentDomain.BaseDirectory;
                    var absolute = $"{currentFolder}{jour.JImage.First()}";
                    var uriSource = new Uri(absolute);
                    image2.Source = new BitmapImage(uriSource);
                    image2.Stretch = Stretch.UniformToFill;
                    image2.Width = 200;
                    image2.Height = 200;
                    stackSearch.Width = 200;
                    stackSearch.Height = 250;
                    stackSearch.Margin = new Thickness(10, 0, 10, 0);
                    image2.Margin = new Thickness(0, 5, 0, 5);
                    var desName2 = new TextBlock();
                    desName2.Text = jour.nameDes;
                    desName2.FontSize = 25;
                    desName2.Foreground = Brushes.Black;
                    desName2.FontWeight = FontWeights.Bold;
                    desName2.Width = 200;
                    desName2.Background = Brushes.Transparent;
                    desName2.HorizontalAlignment = HorizontalAlignment.Center;
                    desName2.TextAlignment = TextAlignment.Center;
                    stackSearch.Children.Add(image2);
                    stackSearch.Children.Add(desName2);
                    stackSearch.Background = Brushes.Wheat; ;
                    int id_copy = jour.JId;
                    stackSearch.MouseLeftButtonUp += (sender2, e2) => journey_Clicked(sender, e, id_copy);
                    if (jour.JStatus == false)
                    {
                        this.ongoingSearch.Children.Add(stackSearch);
                    }
                    else
                    {
                        this.completedSearch.Children.Add(stackSearch);
                    }


                }
            }
            else
            {
                var searchJourney = new List<JourneyModel>();
                foreach (var el in journeys)
                {
                    var searchMemberName = el.JMembers.Where(s => convertToUnSign(s.MName).Contains(convertToUnSign(searchTB.Text))).ToList();
                    if (searchMemberName.Count() > 0)
                    {
                        searchJourney.Add(el);
                    }
                }
                foreach (var jour in searchJourney)
                {

                    var stackSearch = new StackPanel();
                    var image2 = new Image();
                    var currentFolder = AppDomain.CurrentDomain.BaseDirectory;
                    var absolute = $"{currentFolder}{jour.JImage.First()}";
                    var uriSource = new Uri(absolute);
                    image2.Source = new BitmapImage(uriSource);
                    image2.Stretch = Stretch.UniformToFill;
                    image2.Width = 200;
                    image2.Height = 200;
                    stackSearch.Width = 200;
                    stackSearch.Height = 250;
                    stackSearch.Margin = new Thickness(10, 0, 10, 0);
                    image2.Margin = new Thickness(0, 5, 0, 5);
                    var desName2 = new TextBlock();
                    desName2.Text = jour.nameDes;
                    desName2.FontSize = 25;
                    desName2.Foreground = Brushes.Black;
                    desName2.FontWeight = FontWeights.Bold;
                    desName2.Width = 200;
                    desName2.Background = Brushes.Transparent;
                    desName2.HorizontalAlignment = HorizontalAlignment.Center;
                    desName2.TextAlignment = TextAlignment.Center;
                    stackSearch.Children.Add(image2);
                    stackSearch.Children.Add(desName2);
                    stackSearch.Background = Brushes.Wheat; ;
                    int id_copy = jour.JId;
                    stackSearch.MouseLeftButtonUp += (sender2, e2) => journey_Clicked(sender, e, id_copy);
                    if (jour.JStatus == false)
                    {
                        this.ongoingSearch.Children.Add(stackSearch);
                    }
                    else
                    {
                        this.completedSearch.Children.Add(stackSearch);
                    }
                }
            }
        }

        public static string convertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToLower();
        }

        private void importImg_Clicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "Image Files(*.jpg; *.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";
            bool? result = open.ShowDialog();
            if (result == true)
            {
                var img = open.FileNames;
                ImageSource imgsource = new BitmapImage(new Uri(img[0].ToString()));
                var importImg = new Image();
                importImg.Source = new BitmapImage(new Uri(img[0].ToString()));
                importImg.Stretch = Stretch.UniformToFill;
                importImg.Width = 150;
                importImg.Height = 150;
                createImageJourney.Children.Add(importImg);
                fileName.Add(System.IO.Path.GetFileName(open.FileName));
                JImage newImg = new JImage();
                listImgPath.Add(img[0].ToString());
            }
        }

        private void createCancel_Clicked(object sender, RoutedEventArgs e)
        {
            changeEditOrCreateMode("Create Journey");
            hideAllWindow();
            this.homeWindow.Visibility = Visibility.Visible;
        }

        private void createSave_Clicked(object sender, RoutedEventArgs e)
        {
            if(createNameJourney.Text=="")
            {
                MessageBox.Show("Journey's name isn't allowed null");
            }
            else
            {
                if (createJourneyTBk.Text == "Create Journey")
                {
                    var db = new WeSplitEntities();
                    int? Jid = db.Journeys.Max(u => (int?)u.JourneyID);
                    int? Eid = db.Expenditures.Max(u => (int?)u.ExpenditureID);
                    int? Mid = db.Members.Max(u => (int?)u.MemberID);
                    int? JIid = db.JImages.Max(u => (int?)u.JImageID);
                    Destination des = db.Destinations.Where(s => s.DName == destinationCB.Text).ToList().First();
                    var jour = new Journey();
                    jour.JourneyID = (int)(Jid + 1);
                    jour.DestinationID = des.DestinationID; ;
                    jour.Destination = des;
                    jour.JName = createNameJourney.Text;
                    jour.JIntroduce = createDetailJourney.Text;
                    jour.JStatus = createStatusCB.IsChecked;
                    jour.JDayStart = createDayStart.SelectedDate;
                    jour.JDayEnd = createDayEnd.SelectedDate;
                    db.Journeys.Add(jour);
                    db.SaveChanges();
                    foreach (var el in listExpend)
                    {
                        Eid++;
                        var expend = new Expenditure();
                        expend.ExpenditureID = (int)Eid;
                        expend.EName = el.EName;
                        expend.EPrice = el.EPrice;
                        expend.JourneyID = jour.JourneyID;
                        db.Expenditures.Add(expend);
                        db.SaveChanges();
                    }
                    foreach (var el in listMember)
                    {

                        Mid++;
                        var mem = new Member();
                        mem.MemberID = (int)Mid;
                        mem.MName = el.MName;
                        mem.MDonation = el.MDonation;
                        mem.JourneyID = jour.JourneyID;
                        db.Members.Add(mem);
                        db.SaveChanges();

                    }
                    for (int i = 0; i < listImgPath.Count(); i++)
                    {
                        JIid++;
                        var img = new JImage();
                        img.JourneyID = jour.JourneyID;
                        img.JImageID = (int)JIid;
                        String paths = AppDomain.CurrentDomain.BaseDirectory;
                        paths = paths + "Images\\" + fileName.ElementAt(i);
                        System.IO.File.Copy(listImgPath.ElementAt(i), paths);
                        paths = "Images\\" + fileName.ElementAt(i);
                        img.JImageLink = paths;
                        db.JImages.Add(img);
                        db.SaveChanges();
                    }
                }
                else
                {
                    var db = new WeSplitEntities();
                    int? Jid = db.Journeys.Max(u => (int?)u.JourneyID);
                    int? Eid = db.Expenditures.Max(u => (int?)u.ExpenditureID);
                    int? Mid = db.Members.Max(u => (int?)u.MemberID);
                    int? JIid = db.JImages.Max(u => (int?)u.JImageID);
                    Destination des = db.Destinations.Where(s => s.DName == destinationCB.Text).ToList().First();
                    var oldJour = db.Journeys.Where(s => s.JName == createNameJourney.Text).ToList().First();
                    oldJour.DestinationID = des.DestinationID;
                    oldJour.Destination = des;
                    oldJour.JName = createNameJourney.Text;
                    oldJour.JIntroduce = createDetailJourney.Text;
                    oldJour.JStatus = createStatusCB.IsChecked;
                    oldJour.JDayStart = createDayStart.SelectedDate;
                    oldJour.JDayEnd = createDayEnd.SelectedDate;
                    db.SaveChanges();
                    foreach (var el in listExpend)
                    {
                        if (el.EPrice != "old")
                        {
                            Eid++;
                            var expend = new Expenditure();
                            expend.ExpenditureID = (int)Eid;
                            expend.EName = el.EName;
                            expend.EPrice = el.EPrice;
                            expend.JourneyID = oldJour.JourneyID;
                            db.Expenditures.Add(expend);
                            db.SaveChanges();
                        }
                    }
                    foreach (var el in listMember)
                    {
                        if (el.MDonation != "old")
                        {
                            Mid++;
                            var mem = new Member();
                            mem.MemberID = (int)Mid;
                            mem.MName = el.MName;
                            mem.MDonation = el.MDonation;
                            mem.JourneyID = oldJour.JourneyID;
                            db.Members.Add(mem);
                            db.SaveChanges();
                        }
                    }
                    for (int i = 0; i < listImgPath.Count(); i++)
                    {
                        JIid++;
                        var img = new JImage();
                        img.JourneyID = oldJour.JourneyID;
                        img.JImageID = (int)JIid;
                        String paths = AppDomain.CurrentDomain.BaseDirectory;
                        paths = paths + "Images\\" + fileName.ElementAt(i);
                        System.IO.File.Copy(listImgPath.ElementAt(i), paths);
                        paths = "Images\\" + fileName.ElementAt(i);
                        img.JImageLink = paths;
                        db.JImages.Add(img);
                        db.SaveChanges();
                    }
                    hideAllWindow();
                    loadJourneyToScreen();
                    this.homeWindow.Visibility = Visibility.Visible;
                }
                hideAllWindow();
                loadJourneyToScreen();
                this.homeWindow.Visibility = Visibility.Visible;
            }
        }

        private void addPrice_Clicked(object sender, RoutedEventArgs e)
        {
            if (createNameExpenditure.Text == "")
            {
                MessageBox.Show("Expenditure isn't allowed null");
            }
            else
            {
                var addExpend = new ExpenditureModel();
                addExpend.EName = createNameExpenditure.Text;
                if (createPrice.Text == "")
                {
                    addExpend.EPrice = "0";
                }
                else addExpend.EPrice = createPrice.Text;
                listExpend.Add(addExpend);
                var stackExpend = new StackPanel();
                stackExpend.Orientation = Orientation.Horizontal;
                var txtE1 = new TextBlock();
                txtE1.Width = 300;
                txtE1.TextAlignment = TextAlignment.Center;
                txtE1.FontSize = 20;
                var txtE2 = new TextBlock();
                txtE2.Width = 250;
                txtE2.FontSize = 20;
                txtE2.TextAlignment = TextAlignment.Center;
                txtE1.Foreground = Brushes.Black;
                txtE2.Foreground = Brushes.Black;
                txtE1.FontWeight = FontWeights.Normal;
                txtE2.FontWeight = FontWeights.Normal;
                txtE1.Text = addExpend.EName;
                txtE2.Text = addExpend.EPrice;
                var btnExDel = new Button();
                btnExDel.Content = "Delete";
                int index = listExpend.Count;
                btnExDel.Click += (sender2, e2) => deleteExpend_Clicked(sender, e, index);
                stackExpend.Children.Add(txtE1);
                stackExpend.Children.Add(txtE2);
                stackExpend.Children.Add(btnExDel);
                stackExpend.Margin = new Thickness(10, 0, 10, 5);
                createExpenditureSB.Children.Add(stackExpend);
            }
            createNameExpenditure.Text = "";
            createPrice.Text = "";
        }

        private void deleteExpend_Clicked(object sender, RoutedEventArgs e, int index)
        {
            createExpenditureSB.Children.RemoveAt(index);
            listExpend.RemoveAt(index - 1);
        }

        private void addMember_Clicked(object sender, RoutedEventArgs e)
        {
            if (createNameMember.Text == "")
            {
                MessageBox.Show("Member's name isn't allowed null");
            }
            else
            {
                var addMember = new MemberModel();
                addMember.MName = createNameMember.Text;
                if (createDonation.Text == "")
                {
                    addMember.MDonation = "0";
                }
                else addMember.MDonation = createDonation.Text;
                listMember.Add(addMember);
                var stackAddMember = new StackPanel();
                stackAddMember.Orientation = Orientation.Horizontal;
                var txtM1 = new TextBlock();
                txtM1.Width = 300;
                txtM1.TextAlignment = TextAlignment.Center;
                txtM1.FontSize = 20;
                var txtM2 = new TextBlock();
                txtM2.Width = 250;
                txtM2.FontSize = 20;
                txtM2.TextAlignment = TextAlignment.Center;
                txtM1.Foreground = Brushes.Black;
                txtM2.Foreground = Brushes.Black;
                txtM1.FontWeight = FontWeights.Normal;
                txtM2.FontWeight = FontWeights.Normal;
                txtM1.Text = addMember.MName;
                txtM2.Text = addMember.MDonation;
                var btnMemDel = new Button();
                btnMemDel.Content = "Delete";
                int index = listMember.Count();
                btnMemDel.Click += (sender2, e2) => deleteMember_Clicked(sender, e, index);
                stackAddMember.Children.Add(txtM1);
                stackAddMember.Children.Add(txtM2);
                stackAddMember.Children.Add(btnMemDel);
                stackAddMember.Margin = new Thickness(10, 0, 10, 5);
                createMemberSB.Children.Add(stackAddMember);
            }
            createNameMember.Text = "";
            createDonation.Text = "";
        }

        private void deleteMember_Clicked(object sender, RoutedEventArgs e, int index)
        {
            createMemberSB.Children.RemoveAt(index);
            listMember.RemoveAt(index - 1);
        }

        private void edit_Clicked(object sender, RoutedEventArgs e, int id)
        {
            hideAllWindow();
            var jour = journeys.ElementAt(id-1);
            changeEditOrCreateMode("Edit Journey");
            createNameJourney.Text = jour.JName;
            createNameJourney.IsReadOnly = true;
            createDetailJourney.Text = jour.JIntroduce;
            foreach(var el in jour.JImage)
            {
                var image = new Image();
                var currentFolder = AppDomain.CurrentDomain.BaseDirectory;
                var absolute = $"{currentFolder}{el}";
                var uriSource = new Uri(absolute);
                image.Source = new BitmapImage(uriSource);
                image.Stretch = Stretch.UniformToFill;
                image.Width = 150;
                image.Height = 150;
                createImageJourney.Children.Add(image);
            }
            this.createDayStart.SelectedDate = jour.JDayStart;
            this.createDayEnd.SelectedDate = jour.JDayEnd;
            this.createStatusCB.IsChecked = jour.JStatus;
            foreach(var el in jour.JExpenditure)
            {
                var tempStack = new StackPanel();
                tempStack.Orientation = Orientation.Horizontal;
                var txtE1 = new TextBlock();
                txtE1.Width = 300;
                txtE1.TextAlignment = TextAlignment.Center;
                txtE1.FontSize = 20;
                var txtE2 = new TextBlock();
                txtE2.Width = 250;
                txtE2.FontSize = 20;
                txtE2.TextAlignment = TextAlignment.Center;
                txtE1.Foreground = Brushes.Black;
                txtE2.Foreground = Brushes.Black;
                txtE1.FontWeight = FontWeights.Normal;
                txtE2.FontWeight = FontWeights.Normal;
                txtE1.Text = el.EName;
                txtE2.Text = el.EPrice;
                var btnExDel = new Button();
                btnExDel.Content = "Delete";
                int index = listExpend.Count();
                btnExDel.Click += (sender2, e2) => deleteExpend_Clicked(sender, e, index);
                tempStack.Children.Add(txtE1);
                tempStack.Children.Add(txtE2);
                tempStack.Children.Add(btnExDel);
                tempStack.Margin = new Thickness(10, 0, 10, 5);
                createExpenditureSB.Children.Add(tempStack);
                ExpenditureModel temp = new ExpenditureModel();
                temp.EPrice = "old";
                temp.EName = el.EName;
                listExpend.Add(temp);
            }
            foreach(var el in jour.JMembers)
            {
                var tempStack2 = new StackPanel();
                tempStack2.Orientation = Orientation.Horizontal;
                var txtM1 = new TextBlock();
                txtM1.Width = 300;
                txtM1.TextAlignment = TextAlignment.Center;
                txtM1.FontSize = 20;
                var txtM2 = new TextBlock();
                txtM2.Width = 250;
                txtM2.FontSize = 20;
                txtM2.TextAlignment = TextAlignment.Center;
                txtM1.Foreground = Brushes.Black;
                txtM2.Foreground = Brushes.Black;
                txtM1.FontWeight = FontWeights.Normal;
                txtM2.FontWeight = FontWeights.Normal;
                txtM1.Text = el.MName;
                txtM2.Text = el.MDonation;
                var btnMemDel = new Button();
                btnMemDel.Content = "Delete";
                int index = listMember.Count();
                btnMemDel.Click += (sender2, e2) => deleteMember_Clicked(sender, e, index);
                tempStack2.Children.Add(txtM1);
                tempStack2.Children.Add(txtM2);
                tempStack2.Children.Add(btnMemDel);
                tempStack2.Margin = new Thickness(10, 0, 10, 5);
                createMemberSB.Children.Add(tempStack2);
                MemberModel temp = new MemberModel();
                temp.MDonation = "old";
                temp.MName = el.MName;
                listMember.Add(temp);
            }
            var db = new WeSplitEntities();
            int selectIndex=0;
            foreach (var el in db.Destinations.ToList())
            {
                destinationCB.Items.Add(el.DName);
                if(el.DName==jour.nameDes)
                {
                    selectIndex = el.DestinationID;
                }
            }
            destinationCB.SelectedIndex = selectIndex-1;
            this.createWindow.Visibility = Visibility.Visible;
        }

        private void changeEditOrCreateMode(String mode)
        {
            if(mode =="Edit Journey")
            {
                createJourneyTBk.Text = "Edit Journey";
            }
            else
            {
                createJourneyTBk.Text = "Create Journey";
            }
            createNameJourney.Text = "";
            createNameJourney.IsReadOnly = false;
            createDetailJourney.Text = "";
            createImageJourney.Children.Clear();
            this.fileName.Clear();
            this.createExpenditureSB.Children.RemoveRange(1, createExpenditureSB.Children.Count-1);
            this.createMemberSB.Children.RemoveRange(1, createMemberSB.Children.Count - 1);
            this.listExpend.Clear();
            this.listImgPath.Clear();
            this.listMember.Clear();
            this.createDayStart.SelectedDate = DateTime.Now;
            this.createDayEnd.SelectedDate = DateTime.Now;
            this.createStatusCB.IsChecked = false;
            this.createNameExpenditure.Text = "";
            this.createNameMember.Text = "";
            this.createPrice.Text = "";
            this.createDonation.Text = "";
        }
    }
}
