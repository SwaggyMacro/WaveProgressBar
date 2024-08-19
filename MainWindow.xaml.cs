using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace WaveProgressDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private float _progress = 50f;
        public float Progress
        {
            get => _progress;
            set => SetField(ref _progress, value);
        }

        public BallColor NormalColor = new()
        {
            WaveFillColorBrush = new SolidColorBrush(Colors.Green),
            BorderColorBrush = new SolidColorBrush(Color.FromRgb(15, 127, 70)),
            FontBrush = new SolidColorBrush(Color.FromRgb(110, 177, 42)),
            WaveColorBrush = new SolidColorBrush(Colors.DarkGreen)
        };
        public BallColor WarnColor = new()
        {
            WaveFillColorBrush = new SolidColorBrush(Color.FromRgb(255, 170, 51)),
            BorderColorBrush = new SolidColorBrush(Color.FromRgb(201, 157, 28)),
            FontBrush = new SolidColorBrush(Color.FromRgb(126, 84, 24)),
            WaveColorBrush = new SolidColorBrush(Color.FromRgb(178, 119, 35))
        };
        public BallColor CriticColor = new()
        {
            WaveFillColorBrush = new SolidColorBrush(Colors.Red),
            BorderColorBrush = new SolidColorBrush(Colors.DarkRed),
            FontBrush = new SolidColorBrush(Colors.Yellow),
            WaveColorBrush = new SolidColorBrush(Color.FromRgb(201, 157, 28))
        };

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            // foreach warp item
            foreach (var item in Wrap.Children)
            {
                if (item is WaveProgress waveProgress)
                {
                    // set progress
                    waveProgress.Progress = (int)(new Random().NextDouble() * 100);
                    // set color
                    if (waveProgress.Progress < 20)
                    {
                        waveProgress.WaveColorFillBrush = NormalColor.WaveFillColorBrush;
                        waveProgress.WaveColorBrush = NormalColor.WaveColorBrush;
                        waveProgress.FontBrush = NormalColor.FontBrush;
                        waveProgress.WaveBorderBrush = NormalColor.BorderColorBrush;
                    }
                    else if (waveProgress.Progress < 80)
                    {
                        waveProgress.WaveColorFillBrush = WarnColor.WaveFillColorBrush;
                        waveProgress.WaveColorBrush = WarnColor.WaveColorBrush;
                        waveProgress.FontBrush = WarnColor.FontBrush;
                        waveProgress.WaveBorderBrush = WarnColor.BorderColorBrush;
                    }
                    else
                    {
                        waveProgress.WaveColorFillBrush = CriticColor.WaveFillColorBrush;
                        waveProgress.WaveColorBrush = CriticColor.WaveColorBrush;
                        waveProgress.FontBrush = CriticColor.FontBrush;
                        waveProgress.WaveBorderBrush = CriticColor.BorderColorBrush;
                    }
                }
            }
        }
    }
    public class BallColor
    {
        public Brush FontBrush { get; set; } = null!;
        public Brush BorderColorBrush { get; set; } = null!;
        public Brush WaveColorBrush { get; set; } = null!;
        public Brush WaveFillColorBrush { get; set; } = null!;
    }
}