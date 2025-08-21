using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RadCheck
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            InitializePlotModel();
            DataContext = this;
        }

        public PlotModel PlotModel { get; set; }


        /// <summary>
        /// 初始化图表数据
        /// </summary>
        private void InitializePlotModel()
        {
            PlotModel = new PlotModel { Title = "" };

            // 创建6个探头的数据线
            var series1 = new LineSeries { Title = "探头 1", Color = OxyColor.FromRgb(52, 152, 219), MarkerType = MarkerType.None };
            var series2 = new LineSeries { Title = "探头 2", Color = OxyColor.FromRgb(155, 89, 182), MarkerType = MarkerType.None };
            var series3 = new LineSeries { Title = "探头 3", Color = OxyColor.FromRgb(26, 188, 156), MarkerType = MarkerType.None };
            var series4 = new LineSeries { Title = "探头 4", Color = OxyColor.FromRgb(241, 196, 15), MarkerType = MarkerType.None };
            var series5 = new LineSeries { Title = "探头 5", Color = OxyColor.FromRgb(230, 126, 34), MarkerType = MarkerType.None };
            var series6 = new LineSeries { Title = "探头 6", Color = OxyColor.FromRgb(231, 76, 60), MarkerType = MarkerType.None };

            // 添加模拟数据（过去1小时的数据点）
            var now = DateTime.Now;
            for (int i = 60; i >= 0; i--)
            {
                var time = now.AddMinutes(-i);
                double x = DateTimeAxis.ToDouble(time);

                // 为每个探头生成模拟数据
                series1.Points.Add(new DataPoint(x, 0.1 + 0.05 * Math.Sin(i * 0.1)));
                series2.Points.Add(new DataPoint(x, 0.08 + 0.03 * Math.Sin(i * 0.15)));
                series3.Points.Add(new DataPoint(x, 0.15 + 0.04 * Math.Sin(i * 0.2)));
                series4.Points.Add(new DataPoint(x, 0.35 + 0.06 * Math.Sin(i * 0.12)));
                series5.Points.Add(new DataPoint(x, 0.09 + 0.02 * Math.Sin(i * 0.18)));
                series6.Points.Add(new DataPoint(x, 0.65 + 0.1 * Math.Sin(i * 0.25)));
            }

            // 添加系列到图表
            PlotModel.Series.Add(series1);
            PlotModel.Series.Add(series2);
            PlotModel.Series.Add(series3);
            PlotModel.Series.Add(series4);
            PlotModel.Series.Add(series5);
            PlotModel.Series.Add(series6);

            // 设置坐标轴
            PlotModel.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, Title = "时间" });
            PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "剂量率 (μSv/h)", Minimum = 0 });
        }

        /// <summary>
        /// 修改密码按钮点击事件
        /// </summary>
        //private void BtnChangePassword_Click(object sender, RoutedEventArgs e)
        //{
        //    var passwordWindow = new PasswordChangeWindow();
        //    passwordWindow.ShowDialog();
        //}

        ///// <summary>
        ///// 退出登录按钮点击事件
        ///// </summary>
        //private void BtnLogout_Click(object sender, RoutedEventArgs e)
        //{
        //    var result = MessageBox.Show("确定要退出登录吗？", "确认", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //    if (result == MessageBoxResult.Yes)
        //    {
        //        // 关闭当前窗口，打开登录窗口
        //        this.Close();
        //        var loginWindow = new LoginWindow();
        //        loginWindow.Show();
        //    }
        //}
    }
}
