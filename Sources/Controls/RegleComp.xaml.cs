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

namespace DxTBoxCore.Controls
{
    /// <summary>
    /// Logique d'interaction pour Reglette.xaml
    /// </summary>
    public partial class RegleComp : UserControl
    {
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(nameof(Description), typeof(string), typeof(RegleComp));
        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        // ---

        public static readonly DependencyProperty MinLabelProperty = DependencyProperty.Register(nameof(MinLabel), typeof(string), typeof(RegleComp));
        public string MinLabel
        {
            get => (string)GetValue(MinLabelProperty);
            set => SetValue(MinLabelProperty, value);
        }

        public static readonly DependencyProperty MediumLabelProperty = DependencyProperty.Register(nameof(MediumLabel), typeof(string), typeof(RegleComp));
        public string MediumLabel
        {
            get => (string)GetValue(MediumLabelProperty);
            set => SetValue(MediumLabelProperty, value);
        }

        public static readonly DependencyProperty MaxLabelProperty = DependencyProperty.Register(nameof(MaxLabel), typeof(string), typeof(RegleComp));
        public string MaxLabel
        {
            get => (string)GetValue(MaxLabelProperty);
            set => SetValue(MaxLabelProperty, value);
        }

        // ---

        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(nameof(Max), typeof(int), typeof(RegleComp),
                                                                    new PropertyMetadata(10));

        public string Max
        {
            get => (string)GetValue(MaxProperty);
            set => SetValue(MaxProperty, value);
        }

        // ---
        public static readonly DependencyProperty MinAdjustProperty = DependencyProperty.Register(nameof(MinAdjust), typeof(Thickness), typeof(RegleComp));
        public Thickness MinAdjust
        {
            get => (Thickness)GetValue(MinAdjustProperty);
            set => SetValue(MinAdjustProperty, value);
        }

        public static readonly DependencyProperty MaxAdjustProperty = DependencyProperty.Register(nameof(MaxAdjust), typeof(Thickness), typeof(RegleComp));
        public Thickness MaxAdjust
        {
            get => (Thickness)GetValue(MaxAdjustProperty);
            set => SetValue(MaxAdjustProperty, value);
        }

        // --- 

        public static readonly DependencyProperty LargeStepProperty = DependencyProperty.Register(nameof(LargeStep), typeof(int), typeof(RegleComp),
                                                                        new PropertyMetadata(5));
        public int LargeStep
        {
            get => (int)GetValue(LargeStepProperty);
            set => SetValue(LargeStepProperty, value);
        }

        // ---

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(int), typeof(RegleComp),
                                                                    new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public RegleComp()
        {
            InitializeComponent();
        }
    }
}
