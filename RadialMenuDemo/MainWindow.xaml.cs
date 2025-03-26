using RadialMenu.Controls;
using RadialMenuDemo.Utils;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RadialMenuDemo
{
    class Item
    {

        public Item()
        {

        }
    }
    class Ring
    {
        List<Item> items;
        int Count = 6;
        public Ring() {
            items = new List<Item>();
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool _isOpen1 = false;
        public bool IsOpen1
        {
            get
            {
                return _isOpen1;
            }
            set
            {
                _isOpen1 = value;
                RaisePropertyChanged();
            }
        }

        private bool _isOpen2 = false;
        public bool IsOpen2
        {
            get
            {
                return _isOpen2;
            }
            set
            {
                _isOpen2 = value;
                RaisePropertyChanged();
            }
        }

        public ICommand CloseRadialMenu1
        {
            get
            {
                return new RelayCommand(() => IsOpen1 = false);
            }
        }
        public ICommand OpenRadialMenu1
        {
            get
            {
                return new RelayCommand(() => { IsOpen1 = true; IsOpen2 = false; });
            }
        }

        public ICommand CloseRadialMenu2
        {
            get
            {
                return new RelayCommand(() => IsOpen2 = false);
            }
        }
        public ICommand OpenRadialMenu2
        {
            get
            {
                return new RelayCommand(() => { IsOpen2 = true; IsOpen1 = false; });
            }
        }

        public ICommand Test1
        {
            get
            {
                return new RelayCommand(() => System.Diagnostics.Debug.WriteLine("1"));
            }
        }

        public ICommand Test2
        {
            get
            {
                return new RelayCommand(() => System.Diagnostics.Debug.WriteLine("2"));
            }
        }

        public ICommand Test3
        {
            get
            {
                return new RelayCommand(() => System.Diagnostics.Debug.WriteLine("3"));
            }
        }

        public ICommand Test4
        {
            get
            {
                return new RelayCommand(() => System.Diagnostics.Debug.WriteLine("4"));
            }
        }

        public ICommand Test5
        {
            get
            {
                return new RelayCommand(() => System.Diagnostics.Debug.WriteLine("5"));
            }
        }

        public ICommand Test6
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        System.Diagnostics.Debug.WriteLine("6");
                    },
                    () =>
                    {
                        return false; // To disable the 6th item
                    }
                );
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenRadialMenu1.Execute(null);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            grdMenu2.Children.Clear();
            
            RadialMenu.Controls.RadialMenu mnu = new RadialMenu.Controls.RadialMenu();
            List<RadialMenuItem> lst = new List<RadialMenuItem>();
            for (int ii = 0; ii < 6; ii++)
            {
                RadialMenuItem item = new RadialMenuItem();
                TextBlock tb = new TextBlock();
                item.ToolTip = "Hello Mel";
                tb.FontSize = 20;
                tb.Text = ii.ToString();
                item.Content = tb;
                lst.Add(item);
            }
            for (int ii = 0; ii <4; ii++)
            {
                RadialMenuItem item = new RadialMenuItem();
                item.Ring = 1;
                item.Count = 10;
                item.Click += Item_Click;
                TextBlock tb = new TextBlock();
                tb.Text = ii.ToString() + "a";
                item.Content = tb;
                lst.Add(item);
            }
            for (int ii = 0; ii < 3; ii++)
            {
                RadialMenuItem item = new RadialMenuItem();
                item.Ring = 2;
                item.Count = 15;
                item.Click += Item_Click;
                TextBlock tb = new TextBlock();
                tb.Text = ii.ToString() + "a";
                item.Content = tb;
                lst.Add(item);
            }
            mnu.Items = lst;
            grdMenu2.Children.Add(mnu);
            mnu.IsOpen = true;
        }

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            return;
            throw new System.NotImplementedException();
        }
    }
}
