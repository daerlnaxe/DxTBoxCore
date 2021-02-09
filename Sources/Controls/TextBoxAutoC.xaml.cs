using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
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

namespace DxTBoxCore.Controls
{

    /*
     * Echec à cause de la liste, borderl pas possible
     */
    /// <summary>
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class TextBoxAutoC : UserControl
    {
        private PropertyInfo[] _aProperties;

        private Type _Type;

        #region Text
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(TextBoxAutoC),
                new FrameworkPropertyMetadata("Hello Dolly", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault ));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        #endregion


        // ---

        #region Selected Item
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register
            (nameof(SelectedItem), typeof(object), typeof(TextBoxAutoC));

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
        #endregion Selected Item

        // --- 

        #region Available Items

        public static readonly DependencyProperty AvailableItemsProperty = DependencyProperty.Register
            (nameof(AvailableItems), typeof(IEnumerable), typeof(TextBoxAutoC),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                new PropertyChangedCallback(OnAvailableItemsChanged)/*,
                new CoerceValueCallback(OnBleble)*/));

        private static object OnBleble(DependencyObject d, object baseValue)
        {
            if (baseValue != null)
            {
                string test = baseValue.ToString();
            }
            return null;
            //throw new NotImplementedException();
        }

        public IEnumerable AvailableItems
        {
            get => (IEnumerable)GetValue(AvailableItemsProperty);
            set => SetValue(AvailableItemsProperty, value);
        }

        private static void OnAvailableItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxAutoC me = (TextBoxAutoC)d;
            me.OnAvailableItemsChanged(e);

        }

        private static void AvailableItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnAvailableItemsChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null )
                return;

            FilteredItems.Clear();
            foreach (object o in (IEnumerable)e.NewValue)
                FilteredItems.Add(o);

            // todo trouver peut être quelque chose de plus propre
            if (FilteredItems.Count == 0)
                return;

            _Type = FilteredItems[0].GetType();
            _aProperties = _Type.GetProperties();
            /*

            lBox.Items.Clear();
            foreach (var elem in AvailableItems)
                lBox.Items.Add(elem);*/
        }

        //public IEnumerable<object> AvailableItems2 { get; set; }

        public ObservableCollection<object> FilteredItems { get; set; } = new ObservableCollection<object>();


        #endregion Available Items

        // ---

        #region Display Member Path / Désigne la valeur qu'il faut afficher
        public static readonly DependencyProperty DisplayMemberPathProperty = DependencyProperty.Register(
            nameof(DisplayMemberPath), typeof(string), typeof(TextBoxAutoC),
            new PropertyMetadata(null, new PropertyChangedCallback(OnDisplayMemberPathChanged)));

        public string DisplayMemberPath
        {
            get => (string)GetValue(DisplayMemberPathProperty);
            set => SetValue(DisplayMemberPathProperty, value);
        }

        private static void OnDisplayMemberPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxAutoC me = (TextBoxAutoC)d;
            me.OnDisplayMemberPathChanged(e);
        }

        private void OnDisplayMemberPathChanged(DependencyPropertyChangedEventArgs e)
        {
            lBox2.DisplayMemberPath = (string)e.NewValue;
        }
        #endregion Display Member Path

        // --- 

        #region QuickMode to have a quicker mode to select/ pour avoir un mode de sélection plus rapide
        public static readonly DependencyProperty QuickModeProperty = DependencyProperty.Register(
            nameof(QuickMode), typeof(bool), typeof(TextBoxAutoC));

        public bool QuickMode
        {
            get => (bool)GetValue(QuickModeProperty);
            set => SetValue(QuickModeProperty, value);
        }

        #endregion
        
        //--- Graphismes

        #region Graphismes

        #region BorderBrush

        /// <summary>
        /// OverWrite BorderThickness of UserControl
        /// </summary>
        public static new readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register(nameof(BorderThickness), typeof(int), typeof(TextBoxAutoC),
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
            DependencyProperty.Register(nameof(BorderBrush), typeof(Brush), typeof(TextBoxAutoC),
                new UIPropertyMetadata(Brushes.DarkGray));

        public new Brush BorderBrush
        {
            get => (Brush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
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

        #region Padding
        public static readonly new DependencyProperty PaddingProperty = DependencyProperty.Register(
            nameof(Padding), typeof(Thickness), typeof(TextBoxAutoC));

        public new Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
        #endregion

        #endregion Graphismes

        // ---

        #region Event ask to add
        public static readonly RoutedEvent AskToAdd_Event =
            EventManager.RegisterRoutedEvent("AskDectryption", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(TextBoxAutoC));

        /// <summary>
        /// Ask to Decrypt password
        /// </summary>
        public event RoutedEventHandler AskToAdd
        {
            add { AddHandler(AskToAdd_Event, value); }
            remove { RemoveHandler(AskToAdd_Event, value); }
        }
        #endregion

        public TextBoxAutoC()
        {
            //AvailableItems = new ObservableCollection<object>();
            InitializeComponent();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {/*
            foreach (var elem in AvailableItems)
                FilteredItems.Add(elem);*/
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                Keyboard.Focus(lBox2);
                lBox2.SelectedItem = 0;
            }
            else
            {
                TextBox tb = (TextBox)sender;
                FilterItems(tb.Text);
            }
        }

        /// <summary>
        /// Filtre les données
        /// </summary>
        /// <param name="text"></param>
        private void FilterItems(string text)
        {
            if (AvailableItems == null)
                return;

            FilteredItems.Clear();

            foreach (object site in AvailableItems)
            {
                if (!string.IsNullOrEmpty(DisplayMemberPath))
                {
                    foreach (var prop in _aProperties)
                        if (prop.Name == DisplayMemberPath && prop.GetValue(site).ToString().StartsWith(text))
                        {
                            FilteredItems.Add(site);
                        }
                }
                else if(site.ToString().StartsWith(text))
                {
                    FilteredItems.Add(site.ToString());
                }
                //var d = Properties[DisplayMemberPath];
            }


            if (FilteredItems.Count == 0)
                pUP.IsOpen = false;
            else
                pUP.IsOpen = true;
        }

        private void lBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Complete_TextBox();
            }
            else if (e.Key == Key.Left)
            {
                Keyboard.Focus(tBox);
            }
        }

        private void lBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (QuickMode)
                Complete_TextBox();
        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Complete_TextBox();             
        }

        private void Complete_TextBox()
        {
            if (SelectedItem != null)
            {
                tBox.Text = _Type.GetProperty(DisplayMemberPath).GetValue(SelectedItem).ToString();
                pUP.IsOpen = false;
            }
        }

        private void Add_Value(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(AskToAdd_Event));

        }
    }
}
