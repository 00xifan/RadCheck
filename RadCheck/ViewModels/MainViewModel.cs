using CommunityToolkit.Mvvm.Input;
using RadCheck.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Timers;
using System.Windows.Data;


namespace RadCheck.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region 私有字段
        private readonly System.Timers.Timer _dataUpdateTimer;
        private int _updateInterval = 5; // 默认5秒更新一次
        private int _selectedProbeId = 1; // 默认选中第一个探头
        private ObservableCollection<ProbeData> _probeDataList;
        private ObservableCollection<HistoricalData> _historicalDataList;
        private MeasurementRecord _currentMeasurementRecord;
        #endregion

        #region 公共属性
        /// <summary>
        /// 探头数据列表
        /// </summary>
        public ObservableCollection<ProbeData> ProbeDataList
        {
            get => _probeDataList;
            set
            {
                _probeDataList = value;
                OnPropertyChanged(nameof(ProbeDataList));
            }
        }

        /// <summary>
        /// 历史数据列表
        /// </summary>
        public ObservableCollection<HistoricalData> HistoricalDataList
        {
            get => _historicalDataList;
            set
            {
                _historicalDataList = value;
                OnPropertyChanged(nameof(HistoricalDataList));
            }
        }

        /// <summary>
        /// 当前选中的探头ID
        /// </summary>
        public int SelectedProbeId
        {
            get => _selectedProbeId;
            set
            {
                _selectedProbeId = value;
                OnPropertyChanged(nameof(SelectedProbeId));
                LoadHistoricalDataForSelectedProbe();
            }
        }

        /// <summary>
        /// 数据更新间隔(秒)
        /// </summary>
        public int UpdateInterval
        {
            get => _updateInterval;
            set
            {
                if (value < 1) value = 1;
                if (value > 60) value = 60;

                _updateInterval = value;
                _dataUpdateTimer.Interval = value * 1000;
                OnPropertyChanged(nameof(UpdateInterval));
            }
        }

        /// <summary>
        /// 当前测量记录
        /// </summary>
        public MeasurementRecord CurrentMeasurementRecord
        {
            get => _currentMeasurementRecord;
            set
            {
                _currentMeasurementRecord = value;
                OnPropertyChanged(nameof(CurrentMeasurementRecord));
            }
        }
        #endregion

        #region 命令
        /// <summary>
        /// 保存测量记录命令
        /// </summary>
        public RelayCommand SaveMeasurementRecordCommand { get; private set; }

        /// <summary>
        /// 查询数据命令
        /// </summary>
        public RelayCommand QueryDataCommand { get; private set; }

        /// <summary>
        /// 导出数据命令
        /// </summary>
        public RelayCommand ExportDataCommand { get; private set; }

        /// <summary>
        /// 打印数据命令
        /// </summary>
        public RelayCommand PrintDataCommand { get; private set; }

        /// <summary>
        /// 修改密码命令
        /// </summary>
        public RelayCommand ChangePasswordCommand { get; private set; }
        #endregion

        #region 构造函数
        public MainViewModel()
        {
            // 初始化集合
            ProbeDataList = new ObservableCollection<ProbeData>();
            HistoricalDataList = new ObservableCollection<HistoricalData>();
            CurrentMeasurementRecord = new MeasurementRecord { MeasureDate = DateTime.Now };

            // 初始化6个探头
            for (int i = 1; i <= 6; i++)
            {
                ProbeDataList.Add(new ProbeData
                {
                    ProbeId = i,
                    SerialNumber = $"PR{i:D3}202300{i}",
                    DoseRate = 0,
                    Status = ProbeStatus.Normal,
                    MeasureTime = DateTime.Now
                });
            }

            // 初始化命令
            SaveMeasurementRecordCommand = new RelayCommand(SaveMeasurementRecord);
            QueryDataCommand = new RelayCommand(QueryData);
            ExportDataCommand = new RelayCommand(ExportData);
            PrintDataCommand = new RelayCommand(PrintData);
            ChangePasswordCommand = new RelayCommand(ShowChangePasswordWindow);

            // 初始化定时器
            _dataUpdateTimer = new System.Timers.Timer(UpdateInterval * 1000);
            _dataUpdateTimer.Elapsed += DataUpdateTimer_Elapsed;
            _dataUpdateTimer.Start();

            // 加载初始历史数据
            LoadHistoricalDataForSelectedProbe();
        }
        #endregion

        #region 事件处理
        private void DataUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // 模拟数据更新
            App.Current.Dispatcher.Invoke(() =>
            {
                foreach (var probe in ProbeDataList)
                {
                    // 随机微小变化，模拟实时数据
                    var random = new Random();
                    double change = (random.NextDouble() - 0.5) * 0.02;
                    double newDoseRate = Math.Round(probe.DoseRate + change, 4);

                    // 确保剂量率不为负
                    if (newDoseRate < 0) newDoseRate = 0;

                    probe.DoseRate = newDoseRate;
                    probe.MeasureTime = DateTime.Now;

                    // 更新状态
                    if (probe.DoseRate > 0.3)
                        probe.Status = ProbeStatus.Alarm;
                    else if (probe.DoseRate > 0.2)
                        probe.Status = ProbeStatus.Warning;
                    else
                        probe.Status = ProbeStatus.Normal;

                    // 添加到历史数据
                    HistoricalDataList.Add(new HistoricalData
                    {
                        Id = HistoricalDataList.Count + 1,
                        ProbeId = probe.ProbeId,
                        DoseRate = probe.DoseRate,
                        RecordTime = DateTime.Now
                    });
                }

                // 只保留最近的100条历史数据
                while (HistoricalDataList.Count > 100)
                {
                    HistoricalDataList.RemoveAt(0);
                }

                // 如果当前选中的探头有更新，刷新图表
                LoadHistoricalDataForSelectedProbe();
            });
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 加载选中探头的历史数据
        /// </summary>
        private void LoadHistoricalDataForSelectedProbe()
        {
            // 这里可以根据选中的探头ID筛选历史数据
            // 实际应用中可能需要从数据库查询
            OnPropertyChanged(nameof(HistoricalDataList));
        }

        /// <summary>
        /// 保存测量记录
        /// </summary>
        private void SaveMeasurementRecord()
        {
            // 保存测量记录到数据库
            CurrentMeasurementRecord.CreateTime = DateTime.Now;

            // 这里添加保存逻辑

            // 显示保存成功消息
            System.Windows.MessageBox.Show("测量记录保存成功！", "提示",
                System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        private void QueryData()
        {
            // 显示数据查询窗口
            //var queryWindow = new Views.DataQueryWindow();
            //queryWindow.ShowDialog();
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        private void ExportData()
        {
            // 实现数据导出逻辑
            System.Windows.MessageBox.Show("数据导出功能已执行", "提示",
                System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        /// <summary>
        /// 打印数据
        /// </summary>
        private void PrintData()
        {
            // 实现打印功能
            System.Windows.MessageBox.Show("打印功能已执行", "提示",
                System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        /// <summary>
        /// 显示修改密码窗口
        /// </summary>
        private void ShowChangePasswordWindow()
        {
            //var changePwdWindow = new Views.ChangePasswordWindow();
            //changePwdWindow.ShowDialog();
        }
        #endregion

        #region INotifyPropertyChanged 实现
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
