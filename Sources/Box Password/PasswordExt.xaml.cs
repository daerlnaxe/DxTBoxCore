using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections;

namespace DxTBoxCore.Box_Password
{
    /// <summary>
    /// Logique d'interaction pour PasswordExt.xaml
    /// </summary>
    public partial class PasswordExt : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public string Password
        {
            get => pwdEncPassword.Password;
            set => pwdEncPassword.Password = value;
        }

        /// <summary>
        /// Show or hide eye button
        /// </summary>
        private Visibility _ShowButtonVisibility;
        public Visibility ShowButtonVisibility
        {
            get => _ShowButtonVisibility;
            set
            {
                _ShowButtonVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowButtonVisibility)));

            }
        }

        //public event RoutedEventHandler ShowPassClick;

        #region Events
        /*
        public static readonly RoutedEvent ShowPass_Event =
            EventManager.RegisterRoutedEvent("ShowPassClick", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(PasswordExt));

        /// <summary>
        /// Exposed in wpf using it
        /// </summary>
        public event RoutedEventHandler ShowPassClick
        {
            add { AddHandler(ShowPass_Event, value); }
            remove { RemoveHandler(ShowPass_Event, value); }
        }*/
        #endregion

        #region Error
        /// <summary>
        /// Permit to bind to something, just to unlock validationproperty
        /// </summary>
        public static readonly DependencyProperty ErrorProperty =
            DependencyProperty.Register("Error", typeof(bool), typeof(PasswordExt), new
                PropertyMetadata(false, new PropertyChangedCallback(OnErrorChanged)));

        public bool Error
        {
            get => (bool)GetValue(ErrorProperty);
            set => SetValue(ErrorProperty, value);
        }

        private static void OnErrorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordExt passExt = d as PasswordExt;
            passExt.OnErrorChanged(e);
        }

        private void OnErrorChanged(DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }



        #endregion

        #region Size of eye icon
        /// <summary>
        /// Define a static handler which will handle any changes that occur globally
        /// </summary>
        public static readonly DependencyProperty EyeSizeProperty =
            DependencyProperty.Register("EyeSize", typeof(int), typeof(PasswordExt), new
                PropertyMetadata(25, new PropertyChangedCallback(OnEyeSizeChanged)));


        /// <summary>
        /// Property showed in wpf
        /// </summary>
        public int EyeSize
        {
            get { return (int)GetValue(EyeSizeProperty); }
            set { SetValue(EyeSizeProperty, value); }
        }


        private static void OnEyeSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordExt passExt = d as PasswordExt;
            passExt.OnEyeSizeChanged(e);
        }

        private void OnEyeSizeChanged(DependencyPropertyChangedEventArgs e)
        {
         //   EyeButton.Width = (int)e.NewValue;
           // EyeButton.Height = (int)e.NewValue;
        }
        #endregion

        // --- Graphismes

        #region BorderBrush
        
        /// <summary>
        /// OverWrite BorderThickness of UserControl
        /// </summary>
        public static new readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register(nameof(BorderThickness), typeof(int), typeof(PasswordExt), 
                new PropertyMetadata(1));

        public new int BorderThickness
        {
            get => (int)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }
        
        /// <summary>
        /// 
        /// Permit to bind to something, just to unlock validationproperty
        /// </summary>
        public static readonly new DependencyProperty BorderBrushProperty =
            DependencyProperty.Register(nameof(BorderBrush), typeof(Brush), typeof(PasswordExt),
                new UIPropertyMetadata(Brushes.DarkGray));

        public new Brush BorderBrush
        {
            get => (Brush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }
        
        private static void OnPwdBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordExt passExt = d as PasswordExt;
            passExt.OnPwdBorderBrushChanged(e);
        }

        private void OnPwdBorderBrushChanged(DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }



        /*
        private static void OnBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordExt passExt = d as PasswordExt;
            passExt.OnBorderBrushChanged(e);
        }
        
        private void OnBorderBrushChanged(DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }*/
        #endregion

        #region
        /*
        /// <summary>
        /// Define a static handler which will handle any changes that occur globally
        /// </summary>
        public static readonly DependencyProperty DecPasswordProperty =
            DependencyProperty.Register("DecPassword", typeof(string), typeof(PasswordExt), new
                PropertyMetadata("Decoded Password", new PropertyChangedCallback(OnDecPasswordchanged)));


        /// <summary>
        /// Property showed in wpf
        /// </summary>
        public string DecPassword
        {
            get { return (string)GetValue(DecPasswordProperty); }
            set { SetValue(DecPasswordProperty, value); }
        }

        private static void OnDecPasswordchanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordExt passExt = d as PasswordExt;
            passExt.OnDecPasswordChanged(e);
        }

        private void OnDecPasswordChanged(DependencyPropertyChangedEventArgs e)
        {
            tbDecPassword.Text = e.NewValue.ToString();
        }*/
        #endregion


        public PasswordExt()
        {
            InitializeComponent();

            Manage_EyeBtn();
        }


        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Show_Password();
            Debug.WriteLine("MouseDown");
        }

        private void Button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Hide_Password();
            Debug.WriteLine("MouseUp");
        }

        private void Show_Password()
        {
            tbDecPassword.Visibility = Visibility.Visible;
            tbDecPassword.Text = pwdEncPassword.Password;
            pwdEncPassword.Visibility = Visibility.Collapsed;
        }

        private void Hide_Password()
        {
            tbDecPassword.Text = null;
            tbDecPassword.Visibility = Visibility.Collapsed;
            pwdEncPassword.Visibility = Visibility.Visible;

        }

        #region Affichage du bouton oeil
        private void pwdEncPassword_KeyUp(object sender, KeyEventArgs e)
        {
            Manage_EyeBtn();
        }

        private void Manage_EyeBtn()
        {
            if (pwdEncPassword.Password.Length == 0)
                ShowButtonVisibility = Visibility.Hidden;
            else
                ShowButtonVisibility = Visibility.Visible;

        }


        #endregion



        /*
        protected virtual void RaiseClickEvent()
        {
            RoutedEventArgs args = new RoutedEventArgs(PasswordExt.ShowPass_Event);
            RaiseEvent(args);
        }*/
    }
}
