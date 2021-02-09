using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace DxTBoxCore.Box_Password
{
    /// <summary>
    /// Logique d'interaction pour PasswordDec.xaml
    /// </summary>
    /// <remarks>
    /// Delete Textbox content when toggle button is uncheked
    /// </remarks>
    public partial class PasswordDec : UserControl
    {
        public string EncryptedPass
        {
            get => pwdEncPassword.Password;
            set => pwdEncPassword.Password = value;
        }

        public string ClearPassword
        {
            set { tbDecPassword.Text = value; }
        }

        private bool _Reveal;
        /*public Visibility Reveal
        {/*
            get
            {
               // if(_Reveal)

            }*/
        //}

        #region Events

        public static readonly RoutedEvent AskDectryption_Event =
            EventManager.RegisterRoutedEvent("AskDectryption", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(PasswordDec));

        /// <summary>
        /// Ask to Decrypt password
        /// </summary>
        public event RoutedEventHandler AskDectryption
        {
            add { AddHandler(AskDectryption_Event, value); }
            remove { RemoveHandler(AskDectryption_Event, value); }
        }


        public static readonly RoutedEvent AskToMask_Event =
            EventManager.RegisterRoutedEvent(nameof(AskToMask), RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(PasswordDec));

        public event RoutedEventHandler AskToMask
        {
            add { AddHandler(AskToMask_Event, value); }
            remove { RemoveHandler(AskToMask_Event, value); }
        }
        #endregion

        public PasswordDec()
        {
            InitializeComponent();
        }



        private void Button_Checked(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(AskDectryption_Event));
        }

        private void Button_UnChecked(object sender, RoutedEventArgs e)
        {
            tbDecPassword.Text = null;
            GC.Collect();
            RaiseEvent(new RoutedEventArgs(AskToMask_Event));
        }

        #region BorderBrush

        /// <summary>
        /// OverWrite BorderThickness of UserControl
        /// </summary>
        public static new readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register(nameof(BorderThickness), typeof(int), typeof(PasswordDec),
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
            DependencyProperty.Register(nameof(BorderBrush), typeof(Brush), typeof(PasswordDec),
                new UIPropertyMetadata(Brushes.DarkGray));

        public new Brush BorderBrush
        {
            get => (Brush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }

        private static void OnPwdBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordDec passExt = d as PasswordDec;
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

        #region Button
        public static readonly DependencyProperty RevealModeProperty =
            DependencyProperty.Register(nameof(RevealMode), typeof(bool), typeof(PasswordDec),
                new PropertyMetadata(false));

        /// <summary>
        /// Force le button
        /// </summary>
        /// <remarks>
        /// Thread Safe
        /// </remarks>
        public bool RevealMode
        {
            get => (bool)GetValue(RevealModeProperty);
            set
            {
                SetValue(RevealModeProperty, value);
               // this.Dispatcher.Invoke(() => SetValue(RevealModeProperty, value));
            }
        }
        #endregion
    }
}
