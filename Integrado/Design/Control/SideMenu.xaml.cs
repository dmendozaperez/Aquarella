using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Integrado.Design.Control
{
    /// <summary>
    /// Lógica de interacción para SideMenu.xaml
    /// </summary>
    public partial class SideMenu : UserControl
    {
        private bool _isShown;


        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(MenuState), typeof(SideMenu));
        public static readonly DependencyProperty ThemeProperty = DependencyProperty.Register("Theme", typeof(SideMenuTheme), typeof(SideMenu));
        public static readonly DependencyProperty MenuWidthProperty = DependencyProperty.Register("MenuWidth", typeof(double), typeof(SideMenu));
        public static readonly DependencyProperty MenuProperty = DependencyProperty.Register("Menu", typeof(ScrollViewer), typeof(SideMenu));
        public static readonly DependencyProperty ShadowBackgroundProperty = DependencyProperty.Register("ShadowBackground", typeof(Brush), typeof(SideMenu));
        public static readonly DependencyProperty ButtonBackgroundProperty = DependencyProperty.Register("ButtonBackground", typeof(Brush), typeof(SideMenu));
        public static readonly DependencyProperty ButtonHoverProperty = DependencyProperty.Register("ButtonHover", typeof(Brush), typeof(SideMenu));

        public ClosingType ClosingType { get; set; }

        public Brush ButtonBackground
        {
            get
            {
                return (Brush)this.GetValue(SideMenu.ButtonBackgroundProperty);
            }
            set
            {
                this.SetValue(SideMenu.ButtonBackgroundProperty, (object)value);
            }
        }

        public Brush ButtonHover
        {
            get
            {
                return (Brush)this.GetValue(SideMenu.ButtonHoverProperty);
            }
            set
            {
                this.SetValue(SideMenu.ButtonHoverProperty, (object)value);
            }
        }

        public Brush ShadowBackground
        {
            get
            {
                return (Brush)this.GetValue(SideMenu.ShadowBackgroundProperty);
            }
            set
            {
                this.SetValue(SideMenu.ShadowBackgroundProperty, (object)value);
                ResourceDictionary resources = this.Resources;
                string str = "Shadow";
                Brush brush = value;
                if (brush == null)
                {
                    SolidColorBrush solidColorBrush = new SolidColorBrush();
                    Color black = Colors.Black;
                    solidColorBrush.Color = black;
                    double num = 0.2;
                    solidColorBrush.Opacity = num;
                    brush = (Brush)solidColorBrush;
                }
                resources[(object)str] = (object)brush;
            }
        }

        public ScrollViewer Menu
        {
            get
            {
                return (ScrollViewer)this.GetValue(SideMenu.MenuProperty);
            }
            set
            {
                this.SetValue(SideMenu.MenuProperty, (object)value);
            }
        }

        public double MenuWidth
        {
            get
            {
                return (double)this.GetValue(SideMenu.MenuWidthProperty);
            }
            set
            {
                this.SetValue(SideMenu.MenuWidthProperty, (object)value);
            }
        }

        public MenuState State
        {
            get
            {
                return (MenuState)this.GetValue(SideMenu.StateProperty);
            }
            set
            {
                this.SetValue(SideMenu.StateProperty, (object)value);
                if (value == MenuState.Visible)
                    this.Show();
                else
                    this.Hide();
            }
        }

        public SideMenuTheme Theme
        {
            get
            {
                return (SideMenuTheme)this.GetValue(SideMenu.ThemeProperty);
            }
            set
            {
                if (value == SideMenuTheme.None)
                    return;
                this.SetValue(SideMenu.ThemeProperty, (object)value);
                SolidColorBrush solidColorBrush1;
                SolidColorBrush solidColorBrush2;
                SolidColorBrush solidColorBrush3;
                switch (value - 1)
                {
                    case SideMenuTheme.None:
                        SolidColorBrush solidColorBrush4 = new SolidColorBrush();
                        Color color1 = Color.FromArgb((byte)205, (byte)20, (byte)20, (byte)20);
                        solidColorBrush4.Color = color1;
                        solidColorBrush1 = solidColorBrush4;
                        SolidColorBrush solidColorBrush5 = new SolidColorBrush();
                        Color color2 = Color.FromArgb((byte)50, (byte)30, (byte)30, (byte)30);
                        solidColorBrush5.Color = color2;
                        solidColorBrush2 = solidColorBrush5;
                        SolidColorBrush solidColorBrush6 = new SolidColorBrush();
                        Color color3 = Color.FromArgb((byte)50, (byte)70, (byte)70, (byte)70);
                        solidColorBrush6.Color = color3;
                        solidColorBrush3 = solidColorBrush6;
                        break;
                    case SideMenuTheme.Default:
                        SolidColorBrush solidColorBrush7 = new SolidColorBrush();
                        Color color4 = Color.FromArgb((byte)205, (byte)24, (byte)57, (byte)85);
                        solidColorBrush7.Color = color4;
                        solidColorBrush1 = solidColorBrush7;
                        SolidColorBrush solidColorBrush8 = new SolidColorBrush();
                        Color color5 = Color.FromArgb((byte)50, (byte)35, (byte)85, (byte)126);
                        solidColorBrush8.Color = color5;
                        solidColorBrush2 = solidColorBrush8;
                        SolidColorBrush solidColorBrush9 = new SolidColorBrush();
                        Color color6 = Color.FromArgb((byte)50, (byte)45, (byte)110, (byte)163);
                        solidColorBrush9.Color = color6;
                        solidColorBrush3 = solidColorBrush9;
                        break;
                    case SideMenuTheme.Primary:
                        SolidColorBrush solidColorBrush10 = new SolidColorBrush();
                        Color color7 = Color.FromArgb((byte)205, (byte)55, (byte)109, (byte)55);
                        solidColorBrush10.Color = color7;
                        solidColorBrush1 = solidColorBrush10;
                        SolidColorBrush solidColorBrush11 = new SolidColorBrush();
                        Color color8 = Color.FromArgb((byte)50, (byte)65, (byte)129, (byte)65);
                        solidColorBrush11.Color = color8;
                        solidColorBrush2 = solidColorBrush11;
                        SolidColorBrush solidColorBrush12 = new SolidColorBrush();
                        Color color9 = Color.FromArgb((byte)50, (byte)87, (byte)172, (byte)87);
                        solidColorBrush12.Color = color9;
                        solidColorBrush3 = solidColorBrush12;
                        break;
                    case SideMenuTheme.Success:
                        SolidColorBrush solidColorBrush13 = new SolidColorBrush();
                        Color color10 = Color.FromArgb((byte)205, (byte)150, (byte)108, (byte)49);
                        solidColorBrush13.Color = color10;
                        solidColorBrush1 = solidColorBrush13;
                        SolidColorBrush solidColorBrush14 = new SolidColorBrush();
                        Color color11 = Color.FromArgb((byte)50, (byte)179, (byte)129, (byte)58);
                        solidColorBrush14.Color = color11;
                        solidColorBrush2 = solidColorBrush14;
                        SolidColorBrush solidColorBrush15 = new SolidColorBrush();
                        Color color12 = Color.FromArgb((byte)50, (byte)216, (byte)155, (byte)70);
                        solidColorBrush15.Color = color12;
                        solidColorBrush3 = solidColorBrush15;
                        break;
                    case SideMenuTheme.Warning:
                        SolidColorBrush solidColorBrush16 = new SolidColorBrush();
                        Color color13 = Color.FromArgb((byte)205, (byte)135, (byte)52, (byte)49);
                        solidColorBrush16.Color = color13;
                        solidColorBrush1 = solidColorBrush16;
                        SolidColorBrush solidColorBrush17 = new SolidColorBrush();
                        Color color14 = Color.FromArgb((byte)50, (byte)179, (byte)69, (byte)65);
                        solidColorBrush17.Color = color14;
                        solidColorBrush2 = solidColorBrush17;
                        SolidColorBrush solidColorBrush18 = new SolidColorBrush();
                        Color color15 = Color.FromArgb((byte)50, (byte)238, (byte)92, (byte)86);
                        solidColorBrush18.Color = color15;
                        solidColorBrush3 = solidColorBrush18;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("value", (object)value, (string)null);
                }
                this.Resources[(object)"ButtonHover"] = (object)solidColorBrush2;
                this.Resources[(object)"ButtonBackground"] = (object)solidColorBrush3;
                if (this.Menu == null)
                    return;
                this.Menu.Background = (Brush)solidColorBrush1;
            }
        }

        public SideMenu()
        {
            this.InitializeComponent();
            this.Theme = SideMenuTheme.Default;
            this.ClosingType = ClosingType.Auto;
        }

        private static void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public void Toggle()
        {
            if (this._isShown)
                this.Hide();
            else
                this.Show();
        }

        public void Show()
        {
            DoubleAnimation doubleAnimation1 = new DoubleAnimation();
            double? nullable1 = new double?(-this.MenuWidth * 0.85);
            doubleAnimation1.From = nullable1;
            double? nullable2 = new double?(0.0);
            doubleAnimation1.To = nullable2;
            Duration duration = (Duration)TimeSpan.FromMilliseconds(100.0);
            doubleAnimation1.Duration = duration;
            DoubleAnimation doubleAnimation2 = doubleAnimation1;
            this.RenderTransform.BeginAnimation(TranslateTransform.XProperty, (AnimationTimeline)doubleAnimation2);
            this._isShown = true;
            DependencyObject parent = this.Parent;
            (this.FindName("ShadowColumn") as ColumnDefinition).Width = new GridLength(10000.0);
        }

        public void Hide()
        {
            DoubleAnimation doubleAnimation1 = new DoubleAnimation();
            double? nullable = new double?(-this.MenuWidth);
            doubleAnimation1.To = nullable;
            Duration duration = (Duration)TimeSpan.FromMilliseconds(100.0);
            doubleAnimation1.Duration = duration;
            DoubleAnimation doubleAnimation2 = doubleAnimation1;
            this.RenderTransform.BeginAnimation(TranslateTransform.XProperty, (AnimationTimeline)doubleAnimation2);
            this._isShown = false;
            (this.FindName("ShadowColumn") as ColumnDefinition).Width = new GridLength(0.0);
        }

        public override void OnApplyTemplate()
        {
            Panel.SetZIndex((UIElement)this, int.MaxValue);
            this.RenderTransform = (Transform)new TranslateTransform(-this.MenuWidth, 0.0);
            (this.FindName("MenuColumn") as ColumnDefinition).Width = new GridLength(this.MenuWidth);
            this.State = this.State;
            this.Theme = this.Theme;
            this.ShadowBackground = this.ShadowBackground;
        }

        private void ShadowMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.ClosingType != ClosingType.Auto)
                return;
            this.Hide();
        }
    }
}
