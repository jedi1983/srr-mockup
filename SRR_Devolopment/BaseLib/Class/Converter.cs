using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using SRR_Devolopment.BaseLib;

namespace SRR_Devolopment.BaseLib.Class
{
    public class NumericConverter : IValueConverter
    {
        public object Convert(object value,
                              Type targetType,
                              object parameter,
                              System.Globalization.CultureInfo culture)
        {
            if (null == value)
                return null;
            else
                if (targetType.UnderlyingSystemType == typeof(String))
                    return value.ToString();
                else
                    return value;
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            //We create numbers from formatted text.
            if (null == value)
            { return null; }
            else
            {
                HashSet<Type> numericTypes = new HashSet<Type>
                                        {typeof(Byte),
		                                 typeof(Decimal),
		                                 typeof(Double),
		                                 typeof(Int16),
		                                 typeof(Int32),
		                                 typeof(Int64),
		                                 typeof(SByte),
		                                 typeof(Single),
		                                 typeof(UInt16),
		                                 typeof(UInt32),
		                                 typeof(UInt64)
     	                                };

                if (numericTypes.Contains(targetType.UnderlyingSystemType))
                {
                    HashSet<Type> intTypes = new HashSet<Type>
                                        {typeof(Byte),
 			                             typeof(Int16),
				                         typeof(Int32),
				                         typeof(Int64),
				                         typeof(SByte),
				                         typeof(UInt16),
				                         typeof(UInt32),
 				                         typeof(UInt64)
  			                            };
                    string text = value.ToString().Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, String.Empty);
                    if (intTypes.Contains(targetType.UnderlyingSystemType))
                    {
                        try { return Int32.Parse(text); }
                        catch { return null; }
                    }
                    else
                    {
                        try { return Double.Parse(text); }
                        catch { return null; }
                    }
                }
                else { return value; }
            }
        }
    }

    public class NumericToValueTypeConverter : IValueConverter
    {
        public object Convert(object value,
                              Type targetType,
                              object parameter,
                              System.Globalization.CultureInfo culture)
        {
            if (null == value)
            { return ValueTypes.NoNumeric; }
            else
            {
                HashSet<Type> numericTypes = new HashSet<Type>
                                        {typeof(Byte),
		                                 typeof(Decimal),
		                                 typeof(Double),
		                                 typeof(Int16),
		                                 typeof(Int32),
		                                 typeof(Int64),
		                                 typeof(SByte),
		                                 typeof(Single),
		                                 typeof(UInt16),
		                                 typeof(UInt32),
		                                 typeof(UInt64)
     	                                };

                if (numericTypes.Contains(value.GetType()))
                {
                    HashSet<Type> intTypes = new HashSet<Type>
                                        {typeof(Byte),
 			                             typeof(Int16),
				                         typeof(Int32),
				                         typeof(Int64),
				                         typeof(SByte),
				                         typeof(UInt16),
				                         typeof(UInt32),
 				                         typeof(UInt64)
  			                            };
                    if (intTypes.Contains(value.GetType()))
                    {
                        return ValueTypes.Integer;
                    }
                    else
                    {
                        return ValueTypes.Double;
                    }
                }
                else { return ValueTypes.NoNumeric; }
            }
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
