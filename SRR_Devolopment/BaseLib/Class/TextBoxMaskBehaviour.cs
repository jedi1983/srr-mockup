using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using System.Windows.Data;
using System.Diagnostics;
using System.Windows.Input;
using SRR_Devolopment.BaseLib;

namespace SRR_Devolopment.BaseLib.Class
{
    #region Documentation Tags
    /// <summary>
    ///     WPF Maskable TextBox class. Just specify the TextBoxMaskBehavior.Mask and ValueType 
    ///     attached property to a TextBox. It protect your TextBox from unwanted non numeric 
    ///     symbols and make it easy to modify your numbers.
    /// </summary>
    /// <remarks>
    /// <para>
    ///     Class Information:
    ///	    <list type="bullet">
    ///         <item name="authors">Authors: SmarterDB</item>
    ///         <item name="date">February 2012</item>
    ///         <item name="originalURL">http://www.smarterdb.com</item>
    ///         <item name="authors">Authors: Ruben Hakopian</item>
    ///         <item name="date">February 2009</item>
    ///         <item name="originalURL">http://www.rubenhak.com/?p=8</item>
    ///     </list>
    /// </para>
    /// </remarks>
    #endregion

    public class TextBoxMaskBehavior
    {
        #region MinimumValue Property

        public static double GetMinimumValue(DependencyObject obj)
        {
            return (double)obj.GetValue(MinimumValueProperty);
        }

        public static void SetMinimumValue(DependencyObject obj, double value)
        {
            obj.SetValue(MinimumValueProperty, value);
        }

        public static readonly DependencyProperty MinimumValueProperty =
            DependencyProperty.RegisterAttached(
                "MinimumValue",
                typeof(double),
                typeof(TextBoxMaskBehavior),
                new FrameworkPropertyMetadata(double.NaN, MinimumValueChangedCallback)
                );

        private static void MinimumValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox _this = (d as TextBox);

            ValueTypes vt = GetValueType(_this);
            double min = GetMinimumValue(_this);
            if (!min.Equals(double.NaN))
            {
                switch (vt)
                {
                    case ValueTypes.Integer:
                        if (min < Convert.ToDouble(Int32.MinValue))
                            throw new ArgumentOutOfRangeException("Overflow, minimum: " + Int32.MinValue.ToString());
                        //SetMinimumValue(_this, Convert.ToDouble(Int32.MinValue));
                        break;

                    case ValueTypes.Double:
                        //Egy karakterrel előbb megállunk, hogy ne okozzon exception-t.
                        if (min < (Double.MinValue / 100))
                            throw new ArgumentOutOfRangeException("Overflow, minimum: " + (Double.MinValue / 100).ToString());
                        //SetMinimumValue(_this, (Double.MinValue / 100));
                        break;
                }
            }

            ValidateTextBox(_this);
        }
        #endregion

        #region MaximumValue Property

        public static double GetMaximumValue(DependencyObject obj)
        {
            return (double)obj.GetValue(MaximumValueProperty);
        }

        public static void SetMaximumValue(DependencyObject obj, double value)
        {
            obj.SetValue(MaximumValueProperty, value);
        }

        public static readonly DependencyProperty MaximumValueProperty =
            DependencyProperty.RegisterAttached(
                "MaximumValue",
                typeof(double),
                typeof(TextBoxMaskBehavior),
                new FrameworkPropertyMetadata(double.NaN, MaximumValueChangedCallback)
                );

        private static void MaximumValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox _this = (d as TextBox);

            ValueTypes vt = GetValueType(_this);
            double max = GetMaximumValue(_this);
            if (!max.Equals(double.NaN))
            {
                switch (vt)
                {
                    case ValueTypes.Integer:
                        if (max > Convert.ToDouble(Int32.MaxValue))
                            throw new ArgumentOutOfRangeException("Overflow, maximum: " + Int32.MaxValue.ToString());
                        //SetMinimumValue(_this, Convert.ToDouble(Int32.MinValue));
                        break;

                    case ValueTypes.Double:
                        //We stop two characters ahead, so as not to cause an exception.
                        if (max > (Double.MinValue / 100))
                            throw new ArgumentOutOfRangeException("Overflow, maximum: " + (Double.MaxValue / 100).ToString());
                        //SetMinimumValue(_this, (Double.MinValue / 100));
                        break;
                }
            }

            ValidateTextBox(_this);
        }
        #endregion

        #region Mask Property

        public static string GetMask(DependencyObject obj)
        {
            return (string)obj.GetValue(MaskProperty);
        }

        public static void SetMask(DependencyObject obj, string value)
        {
            obj.SetValue(MaskProperty, value);
        }

        public static readonly DependencyProperty MaskProperty =
            DependencyProperty.RegisterAttached(
                "Mask",
                typeof(string),
                typeof(TextBoxMaskBehavior),
                new FrameworkPropertyMetadata(MaskChangedCallback)
                );

        private static void MaskChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is TextBox)
            {
                (e.OldValue as TextBox).PreviewTextInput -= TextBox_PreviewTextInput;
                (e.OldValue as TextBox).TextChanged -= TextBox_TextChanged;
                (e.OldValue as TextBox).PreviewKeyDown -= TextBox_PreviewKeyDown;
                (e.OldValue as TextBox).GotKeyboardFocus -= _TextBox_GotKeyboardFocus;
                DataObject.RemovePastingHandler((e.OldValue as TextBox), (DataObjectPastingEventHandler)TextBoxPastingEventHandler);
            }

            TextBox _this = (d as TextBox);
            if (_this == null)
                return;

            if (string.Empty != (string)e.NewValue)
            {
                _this.PreviewTextInput += TextBox_PreviewTextInput;
                _this.TextChanged += TextBox_TextChanged;
                _this.PreviewKeyDown += TextBox_PreviewKeyDown;
                _this.GotKeyboardFocus += _TextBox_GotKeyboardFocus;
                DataObject.AddPastingHandler(_this, (DataObjectPastingEventHandler)TextBoxPastingEventHandler);
            }

            ValidateTextBox(_this);
        }

        #endregion

        #region ValueType Property

        public static ValueTypes GetValueType(DependencyObject obj)
        {
            return (ValueTypes)obj.GetValue(ValueTypeProperty);
        }

        public static void SetValueType(DependencyObject obj, ValueTypes value)
        {
            obj.SetValue(ValueTypeProperty, value);
        }

        public static readonly DependencyProperty ValueTypeProperty =
            DependencyProperty.RegisterAttached(
                "ValueType",
                typeof(ValueTypes),
                typeof(TextBoxMaskBehavior),
                new FrameworkPropertyMetadata(ValueTypeChangedCallback)
                );

        private static void ValueTypeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox _this = (d as TextBox);
            ValidateTextBox(_this);
        }
        #endregion

        #region Static Methods

        static void _TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox _this = sender as TextBox;
            //A tizedesvessző baloldalára áll kezdéskor
            if (_this.Text.Contains(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
                _this.CaretIndex = _this.Text.IndexOf(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
            else
                _this.CaretIndex = _this.Text.Length;
        }

        static void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((ValueTypes.NoNumeric != GetValueType(sender as TextBox)) && (Key.Space == e.Key))
            {
                //Space is not allowed at number type entry.
                e.Handled = true;
                return;
            }

            if (Key.Back == e.Key)
            {
                //Backspace
                TextBox _this = sender as TextBox;
                if ((0 == _this.SelectionLength) &&
                    (0 < _this.CaretIndex))
                {
                    //If nothing is selected, the cursor is not at the very beginning.
                    if (NumberFormatInfo.CurrentInfo.NumberDecimalSeparator == _this.Text.Substring(_this.CaretIndex - 1, 1))
                    {
                        //This does not have to be carried out if we want to delete the separator
                        _this.CaretIndex -= 1;
                        e.Handled = true;
                        return;
                    }

                    if ((true == _this.Text.Contains(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) &&
                        (_this.CaretIndex > _this.Text.IndexOf(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator) + 1))
                    {
                        //If the cursor is at the decimal value and we delete backwards.
                        int caret = _this.CaretIndex;
                        _this.Text = _this.Text.Substring(0, _this.CaretIndex - 1) + _this.Text.Substring(_this.CaretIndex) + "0";
                        _this.CaretIndex = caret - 1;
                        e.Handled = true;
                        return;
                    }
                }

                if (0 < _this.CaretIndex)
                {
                    if (0 < _this.SelectionLength)
                    {
                        //If we delete the highlighted part of text.
                        int caret = _this.SelectionStart;
                        int rcaret = _this.Text.Length - caret - _this.SelectionLength;

                        string txtWS = _this.Text.Substring(0, caret);
                        string txtWOS = txtWS.Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, String.Empty);

                        string txtSWS = _this.Text.Substring(caret, _this.SelectionLength);
                        string txtSWOS = txtSWS.Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, String.Empty);

                        string text = _this.Text.Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, String.Empty);

                        text = text.Substring(0, caret - (txtWS.Length - txtWOS.Length)) +
                            //If the highlighted part contains the decimal separator, we put it back after deleting.
                               (txtSWOS.Contains(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator) ? NumberFormatInfo.CurrentInfo.NumberDecimalSeparator : String.Empty) +
                               text.Substring(caret - (txtWS.Length - txtWOS.Length) + _this.SelectionLength - (txtSWS.Length - txtSWOS.Length));

                        _this.Text = text;

                        if (txtSWOS.Contains(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
                        {
                            //If the decimal separator was also selected, then the cursor is put in front of the decimal separator.
                            caret = _this.Text.IndexOf(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
                        }
                        else
                        {
                            caret = _this.Text.Length - rcaret;
                        }

                        if (caret < 0)
                            caret = 0;

                        _this.CaretIndex = caret;
                        e.Handled = true;
                        return;
                    }
                    else
                    {
                        //One item is deleted from the left.
                        int caret = _this.CaretIndex;
                        int rcaret = _this.Text.Length - caret;

                        string txtWS = _this.Text.Substring(0, caret);
                        string txtWOS = txtWS.Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, String.Empty);

                        string text = _this.Text.Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, String.Empty);

                        text = text.Substring(0, caret - (txtWS.Length - txtWOS.Length) - 1) +
                               text.Substring(caret - (txtWS.Length - txtWOS.Length));

                        _this.Text = text;

                        caret = _this.Text.Length - rcaret;

                        if (caret < 0)
                            caret = 0;

                        _this.CaretIndex = caret;
                        e.Handled = true;
                        return;

                    }
                }

            }

            if (Key.Delete == e.Key)
            {
                //Del
                TextBox _this = sender as TextBox;
                if ((0 == _this.SelectionLength) && (_this.CaretIndex < _this.Text.Length))
                {
                    //If nothing is selected, the cursor is not at the very end.
                    if (NumberFormatInfo.CurrentInfo.NumberDecimalSeparator == _this.Text.Substring(_this.CaretIndex, 1))
                    {
                        //This does not have to be carried out if we want to delete the separator
                        _this.CaretIndex += 1;
                        e.Handled = true;
                        return;
                    }

                    if ((true == _this.Text.Contains(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) &&
                        (_this.CaretIndex > _this.Text.IndexOf(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)))
                    {
                        //If the cursor is at the decimal value and we delete.
                        int caret = _this.CaretIndex;
                        int ind = _this.Text.IndexOf(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);

                        _this.Text = _this.Text.Substring(0, caret) + _this.Text.Substring(caret + 1) + "0";
                        _this.CaretIndex = caret;
                        e.Handled = true;
                        return;
                    }
                }

                if (0 < _this.SelectionLength)
                {
                    //If we delete the highlighted part of text.
                    int caret = _this.SelectionStart;
                    int rcaret = _this.Text.Length - caret - _this.SelectionLength;

                    string txtWS = _this.Text.Substring(0, caret);
                    string txtWOS = txtWS.Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, String.Empty);

                    string txtSWS = _this.Text.Substring(caret, _this.SelectionLength);
                    string txtSWOS = txtSWS.Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, String.Empty);

                    string text = _this.Text.Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, String.Empty);

                    text = text.Substring(0, caret - (txtWS.Length - txtWOS.Length)) +
                        //If the highlighted part contains the decimal separator, we put it back after deleting.
                           (txtSWOS.Contains(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator) ? NumberFormatInfo.CurrentInfo.NumberDecimalSeparator : String.Empty) +
                           text.Substring(caret - (txtWS.Length - txtWOS.Length) + _this.SelectionLength - (txtSWS.Length - txtSWOS.Length));

                    //If there is only one decimal separator, it will be deleted.
                    text = (NumberFormatInfo.CurrentInfo.NumberDecimalSeparator == text ? String.Empty : text);

                    _this.Text = text;

                    if (txtSWOS.Contains(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
                    {
                        caret = _this.Text.IndexOf(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
                    }
                    else
                    {
                        caret = _this.Text.Length - rcaret;
                    }

                    if (caret < 0)
                        caret = 0;

                    _this.CaretIndex = caret;
                    e.Handled = true;
                    return;
                }
                else
                {
                    if (_this.CaretIndex < _this.Text.Length)
                    {

                        //One item is deleted from the right.
                        int caret = _this.CaretIndex;
                        int rcaret = _this.Text.Length - caret - 1;

                        if (NumberFormatInfo.CurrentInfo.NumberGroupSeparator == _this.Text.Substring(caret, 1))
                            rcaret -= 1;

                        string txtWS = _this.Text.Substring(0, caret);
                        string txtWOS = txtWS.Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, String.Empty);

                        string text = _this.Text.Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, String.Empty);

                        text = text.Substring(0, caret - (txtWS.Length - txtWOS.Length)) +
                               text.Substring(caret - (txtWS.Length - txtWOS.Length) + 1);

                        _this.Text = text;
                        caret = _this.Text.Length - rcaret;

                        if (caret < 0)
                            caret = 0;

                        _this.CaretIndex = caret;
                        e.Handled = true;
                        return;
                    }

                }


            }

            e.Handled = false;
        }

        #endregion

        #region Private Static Methods

        private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox _this = sender as TextBox;

            string text = _this.Text.Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, String.Empty);

            string mask = GetMask(_this);
            ValueTypes vt = GetValueType(_this);

            if (0 != mask.Length)
            {
                if (0 < _this.Text.Length)
                {
                    if (vt.Equals(ValueTypes.Integer))
                    {
                        //todo: TryParse/try-catch
                        _this.Text = String.Format("{" + mask + "}", Int32.Parse(text));
                        e.Handled = true;
                    }
                    else
                    {
                        _this.Text = String.Format("{" + mask + "}", Double.Parse(text));
                        e.Handled = true;
                    }
                }
                else
                {
                    _this.Text = "0";
                    e.Handled = true;
                }

            }

        }

        private static void ValidateTextBox(TextBox _this)
        {
            if (string.Empty != GetMask(_this))
            {
                _this.Text = ValidateValue(GetMask(_this),
                                           GetValueType(_this),
                                           _this.Text,
                                           GetMinimumValue(_this),
                                           GetMaximumValue(_this));
            }
        }

        private static void TextBoxPastingEventHandler(object sender, DataObjectPastingEventArgs e)
        {
            TextBox _this = (sender as TextBox);
            string clipboard = e.DataObject.GetData(typeof(string)) as string;
            clipboard = ValidateValue(GetMask(_this), GetValueType(_this), clipboard, GetMinimumValue(_this), GetMaximumValue(_this));
            if (!string.IsNullOrEmpty(clipboard))
            {
                _this.Text = clipboard;
            }
            e.CancelCommand();
            e.Handled = true;
        }

        private static void TextBox_PreviewTextInput(object sender,
                                                     System.Windows.Input.TextCompositionEventArgs e)
        {
            TextBox _this = (sender as TextBox);
            bool isValid = IsSymbolValid(GetMask(_this), e.Text, GetValueType(_this));
            bool textInserted = false;
            bool toNDS = false;

            if (isValid)
            {
                //Current content
                string txtOld = _this.Text.Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, String.Empty);
                //New content
                string txtNew = String.Empty;
                bool handled = false;
                int caret = _this.CaretIndex;
                int rcaret = 0;

                if (e.Text == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)
                {
                    //If we entered a decimal separator.
                    int ind = _this.Text.IndexOf(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator) + 1;
                    rcaret = _this.Text.Length - ind;
                    //The text doesn't change.
                    txtNew = txtOld;
                    handled = true;
                }

                if ((!handled) && (e.Text == NumberFormatInfo.CurrentInfo.NegativeSign))
                {
                    //We entered a negative symbol.
                    if (_this.Text.Contains(NumberFormatInfo.CurrentInfo.NegativeSign))
                    {
                        //A negative symbol is already in the text.
                        //As overriding the text initializes the cursor, the present position is remembered.
                        rcaret = _this.Text.Length - caret;
                        txtNew = txtOld.Replace(NumberFormatInfo.CurrentInfo.NegativeSign, string.Empty);
                    }
                    else
                    {
                        //There is no negative symbol in the text.
                        //As overriding the text initializes the cursor, the present position is remembered.
                        rcaret = _this.Text.Length - caret;
                        txtNew = NumberFormatInfo.CurrentInfo.NegativeSign + txtOld;
                    }
                    handled = true;
                }

                if (!handled)
                {
                    textInserted = true;
                    if (0 < _this.SelectionLength)
                    {
                        //We delete the highlighted text and insert what we have just written.
                        int ind = _this.SelectionStart;
                        rcaret = _this.Text.Length - ind - _this.SelectionLength;

                        string txtWS = _this.Text.Substring(0, ind);
                        string txtWOS = txtWS.Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, String.Empty);

                        string txtSWS = _this.Text.Substring(ind, _this.SelectionLength);
                        string txtSWOS = txtSWS.Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, String.Empty);

                        string txtNWS = e.Text;
                        string txtNWOS = txtNWS.Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, String.Empty);

                        txtNew = txtOld.Substring(0, ind - (txtWS.Length - txtWOS.Length)) + txtNWOS +
                                (txtSWOS.Contains(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator) ? NumberFormatInfo.CurrentInfo.NumberDecimalSeparator : String.Empty) +
                                txtOld.Substring(ind - (txtWS.Length - txtWOS.Length) + _this.SelectionLength - (txtSWS.Length - txtSWOS.Length));

                        if (txtSWOS.Contains(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
                        {
                            //If the decimal separator was also highlighted, then the cursor is put in front of the decimal separator.
                            toNDS = true;
                        }

                    }
                    else
                    {
                        //We insert the character to the right of the cursor.
                        int ind = _this.CaretIndex;
                        rcaret = _this.Text.Length - ind;

                        if ((0 < rcaret) &&
                            (NumberFormatInfo.CurrentInfo.NumberGroupSeparator == _this.Text.Substring(ind, 1)))
                            rcaret -= 1;

                        string txtWS = _this.Text.Substring(0, ind);
                        string txtWOS = txtWS.Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, String.Empty);

                        string txtNWS = e.Text;
                        string txtNWOS = txtNWS.Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, String.Empty);

                        txtNew = txtOld.Substring(0, ind - (txtWS.Length - txtWOS.Length)) + txtNWOS +
                                txtOld.Substring(ind - (txtWS.Length - txtWOS.Length));
                    }
                }

                try
                {
                    double val = Double.Parse(txtNew);
                    double newVal = ValidateLimits(GetMinimumValue(_this), GetMaximumValue(_this), val, GetValueType(_this));
                    if (val != newVal)
                    {
                        txtNew = newVal.ToString();
                    }
                    else if (val == 0)
                    {
                        if (!txtNew.Contains(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
                        {
                            txtNew = "0";
                        }
                    }
                }
                catch
                {
                    txtNew = "0";
                }

                _this.Text = txtNew;

                if ((true == _this.Text.Contains(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) &&
                    (caret > _this.Text.IndexOf(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)))
                {
                    //If the cursor is at the decimal value, then it moves to the right of the decimal separator, if possible.
                    if (caret < _this.Text.Length)
                    {
                        if (textInserted)
                        {
                            caret += 1;
                            rcaret = _this.Text.Length - caret;
                        }
                    }
                    else
                    {
                        //We are at the very end; it's not possible to enter more characters.
                        if (textInserted)
                            _this.Text = txtOld;
                    }
                }

                caret = _this.Text.Length - rcaret;

                if (caret < 0)
                    caret = 0;

                if (toNDS)
                {
                    _this.CaretIndex = _this.Text.IndexOf(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
                }
                else
                {
                    _this.CaretIndex = caret;
                }

            }

            e.Handled = true;
        }

        private static string ValidateValue(string mask, ValueTypes vt, string value, double min, double max)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            value = value.Trim();

            switch (vt)
            {
                case ValueTypes.Integer:
                    try
                    {
                        value = ValidateLimits(min, max, Int32.Parse(value), vt).ToString();
                        return value;
                    }
                    catch { }
                    return string.Empty;

                case ValueTypes.Double:
                    try
                    {
                        value = ValidateLimits(min, max, Double.Parse(value), vt).ToString();
                        return value;
                    }
                    catch { }
                    return string.Empty;
            }
            return string.Empty;

        }

        private static double ValidateLimits(double min, double max, double value, ValueTypes vt)
        {
            if (!min.Equals(double.NaN))
            {
                if (value < min)
                    return min;
            }
            else
            {
                switch (vt)
                {
                    case ValueTypes.Integer:
                        if (value < Convert.ToDouble(Int32.MinValue))
                            return Convert.ToDouble(Int32.MinValue);
                        break;

                    case ValueTypes.Double:
                        //Két karakterrel előbb megállunk, hogy ne okozzon exception-t.
                        if (value < (Double.MinValue / 100))
                            return (Double.MinValue / 100);
                        break;
                }
            }

            if (!max.Equals(double.NaN))
            {
                if (value > max)
                    return max;
            }
            else
            {
                switch (vt)
                {
                    case ValueTypes.Integer:
                        if (value > Convert.ToDouble(Int32.MaxValue))
                            return Convert.ToDouble(Int32.MaxValue);
                        break;

                    case ValueTypes.Double:
                        //We stop two characters ahead, so as not to cause an exception.
                        if (value > (Double.MaxValue / 100))
                            return (Double.MaxValue / 100);
                        break;
                }
            }
            return value;
        }

        private static bool IsSymbolValid(string mask, string str, ValueTypes typ)
        {
            switch (typ)
            {
                case ValueTypes.NoNumeric:
                    return true;
                case ValueTypes.Integer:
                    if (str == NumberFormatInfo.CurrentInfo.NegativeSign)
                        return true;
                    break;
                case ValueTypes.Double:
                    if (str == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator ||
                        str == NumberFormatInfo.CurrentInfo.NegativeSign)
                        return true;
                    break;
            }

            if (typ.Equals(ValueTypes.Integer) || typ.Equals(ValueTypes.Double))
            {
                foreach (char ch in str)
                {
                    if (!Char.IsDigit(ch))
                        return false;
                }

                return true;
            }

            return false;

        }

        #endregion

    }

    public enum ValueTypes
    {
        NoNumeric,
        Integer,
        Double
    }

}
