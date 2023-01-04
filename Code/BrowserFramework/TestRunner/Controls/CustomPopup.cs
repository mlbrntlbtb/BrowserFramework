using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TestRunner.Controls
{
    [System.Windows.TemplatePart(Name = PART_PopupLabel, Type = typeof(Label))]
    
    public class CustomPopup : ContentControl
    {
        private const string PART_PopupLabel = "PART_PopupLabel";
        private Label _label = null;
        static CustomPopup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomPopup), new System.Windows.FrameworkPropertyMetadata(typeof(CustomPopup)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _label = GetTemplateChild(PART_PopupLabel) as Label;
        }

        public static readonly DependencyProperty PopupLabelProperty =
            DependencyProperty.Register("PopupLabel", typeof(string), typeof(CustomPopup), new UIPropertyMetadata(string.Empty));

        public string PopupLabel
        {
            get
            {
                return (string)GetValue(PopupLabelProperty);
            }
            set
            {
                SetValue(PopupLabelProperty, value);
            }
        }
    }

}