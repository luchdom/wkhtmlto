using System.Globalization;
using System.Reflection;
using System.Text;

namespace WkHtmlShared.Options;

public class Margins
{
    [OptionFlag("-B")]
    public int? Bottom;

    [OptionFlag("-L")]
    public int? Left;

    [OptionFlag("-R")]
    public int? Right;

    [OptionFlag("-T")]
    public int? Top;

    public Margins()
    {
    }

    public Margins(int top, int right, int bottom, int left)
    {
        Top = top;
        Right = right;
        Bottom = bottom;
        Left = left;
    }

    public override string ToString()
    {
        var result = new StringBuilder();

        FieldInfo[] fields = GetType().GetFields();
        foreach (FieldInfo fi in fields)
        {
            var of = fi.GetCustomAttributes(typeof(OptionFlagAttribute), true).FirstOrDefault() as OptionFlagAttribute;
            if (of == null)
                continue;

            object value = fi.GetValue(this);
            if (value != null)
                result.AppendFormat(CultureInfo.InvariantCulture, " {0} {1}", of.Name, value);
        }

        return result.ToString().Trim();
    }
}
