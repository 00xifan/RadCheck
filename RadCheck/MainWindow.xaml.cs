using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RadCheck
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DetectorLowData = new ObservableCollection<DetectorDataRow>
            {
                new DetectorDataRow { Angle0 = 0.00f, Angle60 = 0.00f, Angle120 = 0.00f, Angle180 = 0.00f, Angle240 = 0.00f, Angle300 = 0.00f }
            };
            DetectorMediumData = new ObservableCollection<DetectorDataRow>
            {
                new DetectorDataRow { Angle0 = 10.10f, Angle60 = 20.00f, Angle120 = 30.00f, Angle180 = 40.00f, Angle240 = 50.00f, Angle300 = 60.00f }
            };
            DetectorHighData = new ObservableCollection<DetectorDataRow>
            {
                new DetectorDataRow { Angle0 = 100.00f, Angle60 = 200.00f, Angle120 = 300.56f, Angle180 = 400.05f, Angle240 = 550.90f, Angle300 = 660.80f }
            };
            DetectorGammaData = new ObservableCollection<DetectorDataRow>
            {
                new DetectorDataRow { Angle0 = 1000.00f, Angle60 = 2050.00f, Angle120 = 3060.56f, Angle180 = 4070.05f, Angle240 = 5530.90f, Angle300 = 6670.80f }
            };

            this.DataContext = this;
        }


        public ObservableCollection<DetectorDataRow> DetectorLowData { get; set; }
        public ObservableCollection<DetectorDataRow> DetectorMediumData { get; set; }
        public ObservableCollection<DetectorDataRow> DetectorHighData { get; set; }
        public ObservableCollection<DetectorDataRow> DetectorGammaData { get; set; }

        private void UserAvatarButton_Click(object sender, RoutedEventArgs e)
        {
            UserMenuPopup.IsOpen = true;
            var storyboard = (Storyboard)UserMenuPopup.Resources["OpenMenuAnimation"];
            storyboard.Begin((FrameworkElement)UserMenuPopup.Child);
        }
    }

    public class DetectorDataRow
    {
        public float Angle0 { get; set; }
        public float Angle60 { get; set; }
        public float Angle120 { get; set; }
        public float Angle180 { get; set; }
        public float Angle240 { get; set; }
        public float Angle300 { get; set; }
    }
}