using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SRR_Devolopment.BaseLib;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Windows.Interactivity;
using System.Windows.Documents;

namespace SRR_Devolopment.BaseLib.Class
{

    //Testing For Dependency Injection Method
    class ExtendTextBox : Control
    {

        public static readonly DependencyProperty ValueProperty =
        DependencyProperty.RegisterAttached("Value",
        typeof(string), typeof(ExtendTextBox),
        new FrameworkPropertyMetadata("", ValuePropertyChanged)
        {
            BindsTwoWayByDefault = true,
            DefaultUpdateSourceTrigger =
                UpdateSourceTrigger.LostFocus
        });

        private static void ValuePropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (_inPasswordChange) return;
            var passwordBox = d as PasswordBox;
            if (passwordBox == null) return;
            passwordBox.PasswordChanged += (s, e2) =>
            {
                if (_inExternalPasswordChange) return;
                _inPasswordChange = true;
                SetValue(d, passwordBox.Password);
                _inPasswordChange = false;
            };

            _inExternalPasswordChange = true;
            if (e.NewValue != null)
                passwordBox.Password = e.NewValue.ToString();
            else
                passwordBox.Password = string.Empty;
            _inExternalPasswordChange = false;
        }

        private static bool _inPasswordChange;
        private static bool _inExternalPasswordChange;

        public static string GetValue(DependencyObject obj)
        {
            return (string)obj.GetValue(ValueProperty);
        }

        public static void SetValue(DependencyObject obj, string value)
        {
            obj.SetValue(ValueProperty, value);
        }
        
    }

    public class HighlightTextBehavior : Behavior<TextBlock>
    {
        public string HighlightedText
        {
            get { return (string)GetValue(HighlightedTextProperty); }
            set { SetValue(HighlightedTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HighlightedText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HighlightedTextProperty =
            DependencyProperty.Register("HighlightedText", typeof(string), typeof(HighlightTextBehavior), new UIPropertyMetadata(string.Empty, HandlePropertyChanged));

        private static void HandlePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as HighlightTextBehavior).HandlePropertyChanged();
        }

        private void HandlePropertyChanged()
        {
            if (AssociatedObject == null)
            {
                return;
            }

            var allText = GetCompleteText();

            AssociatedObject.Inlines.Clear();

            var indexOfHighlightString = allText.IndexOf(HighlightedText);

            if (indexOfHighlightString < 0)
            {
                AssociatedObject.Inlines.Add(allText);
            }
            else
            {
                AssociatedObject.Inlines.Add(allText.Substring(0, indexOfHighlightString));
                AssociatedObject.Inlines.Add(new Run()
                {
                    Text = allText.Substring(indexOfHighlightString, HighlightedText.Length),
                    Foreground = Brushes.Green,
                    FontWeight = FontWeights.Bold
                });
                AssociatedObject.Inlines.Add(allText.Substring(indexOfHighlightString + HighlightedText.Length));
            }
        }

        private string GetCompleteText()
        {
            var allText = AssociatedObject.Inlines.OfType<Run>().Aggregate(new StringBuilder(), (sb, run) => sb.Append(run.Text), sb => sb.ToString());
            return allText;
        }
    }

}
