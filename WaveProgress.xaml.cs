using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WaveProgressDemo
{
    /// <summary>
    /// Interaction logic for WaveProgress.xaml
    /// </summary>
    public partial class WaveProgress : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.Register(
                nameof(Progress), 
                typeof(float), 
                typeof(WaveProgress), 
                new PropertyMetadata(1f, OnProgressChanged)
                );
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private static void OnProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WaveProgress waveProgress)
            {
                waveProgress.Filling = (50 - (float)e.NewValue) / 100.0;
                waveProgress.OnPropertyChanged(nameof(Filling));
                waveProgress.OnPropertyChanged(nameof(WaveDraw));
            }
        }

        public Grid WaveDraw => Progress is < 20.0f or > 80f ? waveGrid1 : waveGrid2;
        // public Grid WaveDraw
        // {
        //     get
        //     {
        //         if (Progress is < 20.0f or > 80f)
        //         {
        //             return waveGrid1;
        //         }
        //         else
        //         {
        //             return waveGrid2;
        //         }
        //     }
        //     set => SetValue(WaveDrawProperty, value);
        // }

        // public static readonly DependencyProperty WaveDrawProperty =
        //     DependencyProperty.Register(
        //         nameof(WaveDraw), 
        //         typeof(Grid), 
        //         typeof(WaveProgress), 
        //         new PropertyMetadata(null)
        //         );
        public static readonly DependencyProperty WaveBorderBrushProperty =
            DependencyProperty.Register(
                nameof(WaveBorderBrush), 
                typeof(Brush), 
                typeof(WaveProgress), 
                new PropertyMetadata(
                    new SolidColorBrush(Color.FromRgb(15, 127, 70)), 
                    null)
                );
        public static readonly DependencyProperty FontBrushProperty = 
            DependencyProperty.Register(
                nameof(FontBrush), 
                typeof(Brush), 
                typeof(UserControl), 
                new PropertyMetadata(
                    new SolidColorBrush(Color.FromRgb(110, 177, 42)), 
                    null)
                );
        public static readonly DependencyProperty WaveColorBrushProperty = 
            DependencyProperty.Register(
                nameof(WaveColorBrush), 
                typeof(Brush), 
                typeof(UserControl), 
                new PropertyMetadata(
                    new SolidColorBrush(
                        Color.FromRgb(0, 100, 0)
                        ),null)
                );
        public static readonly DependencyProperty WaveColorFillBrushProperty =
            DependencyProperty.Register(
                nameof(WaveColorFillBrush), 
                typeof(Brush), 
                typeof(UserControl), 
                new PropertyMetadata(
                    new SolidColorBrush(
                        Color.FromRgb(0, 128, 0)
                        ), 
                    null)
                );
        public float Progress
        {
            get => (float)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }
        public Brush WaveBorderBrush
        {
            get => (Brush)GetValue(WaveBorderBrushProperty);
            set => SetValue(WaveBorderBrushProperty, value);
        }
        public Brush FontBrush
        {
            get => (Brush)GetValue(FontBrushProperty);
            set => SetValue(FontBrushProperty, value);
        }

        public Brush WaveColorBrush
        {
            get => (Brush)GetValue(WaveColorBrushProperty);
            set => SetValue(WaveColorBrushProperty, value);
        }

        public Brush WaveColorFillBrush
        {
            get => (Brush)GetValue(WaveColorFillBrushProperty);
            set => SetValue(WaveColorFillBrushProperty, value);
        }

        // ReSharper disable once InconsistentNaming
        private readonly Grid waveGrid1 = WaveImage(10);
        // ReSharper disable once InconsistentNaming
        private readonly Grid waveGrid2 = WaveImage(20);
        public double Filling { get; set; } = 0.5;
        public WaveProgress()
        {
            InitializeComponent();
            Loaded += WaveProgress_Loaded;
        }
        private void WaveProgress_Loaded(object sender, RoutedEventArgs e)
        {
            var sb = new Storyboard();
            var an = new DoubleAnimation()
            {
                RepeatBehavior = RepeatBehavior.Forever,
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromMilliseconds(3000))
            };
            Storyboard.SetTargetProperty(an, new PropertyPath("(UIElement.OpacityMask).(Brush.RelativeTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));
            Storyboard.SetTarget(an, ellipse);
            sb.Children.Add(an);
            sb.Begin();

            var sb2 = new Storyboard();
            var an2 = new DoubleAnimation()
            {
                RepeatBehavior = RepeatBehavior.Forever,
                From = 1,
                To = 0,
                Duration = new Duration(TimeSpan.FromMilliseconds(3000))
            };
            Storyboard.SetTargetProperty(an2, new PropertyPath("(UIElement.OpacityMask).(Brush.RelativeTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));
            Storyboard.SetTarget(an2, ellipse2);
            sb2.Children.Add(an2);
            sb2.Begin();
        }
        private static Grid WaveImage(double h)
        {
            // Set the background
            var grid = new Grid
            {
                Width = 160,
                Height = 377,
                Background = new SolidColorBrush(Colors.Transparent)
            };
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(h) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) });
            var col = Color.FromArgb(0xFF, 0x82, 0xC6, 0xFF);
            var geo = "M 12.5 1.6926 C 31.25 1.6926 31.25 18.6157 50 18.6157 C 68.75 18.6157 68.75 1.6926 87.5 1.6926 C 87.4999 50 87.5 50 87.5 50 C 63.2813 50 12.5 50 12.5 50 C 12.5 50 12.5001 50 12.5 50 z";
            var pah = new Path()
            {
                // Fill = new SolidBrush(col),
                Fill = new SolidColorBrush(col),
                Data = Geometry.Parse(geo),
                Stretch = Stretch.Fill,
            };
            grid.Children.Add(pah);
            var rec = new Rectangle()
            {
                Fill = new SolidColorBrush(col),
            };
            grid.Children.Add(rec);
            Grid.SetRow(pah, 1);
            Grid.SetRow(rec, 2);
            return grid;
        }
    }

}
