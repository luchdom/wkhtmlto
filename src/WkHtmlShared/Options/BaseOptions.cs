using System.Globalization;
using System.Text;

namespace WkHtmlShared.Options;
public abstract class BaseOptions
{
    public virtual string GetConvertOptionFlags()
    {
        return GetConvertBaseOptionFlags();
    }

    private string GetConvertBaseOptionFlags()
    {
        var result = new StringBuilder();

        var fields = GetType().GetProperties();
        foreach (var fi in fields)
        {
            var of = fi.GetCustomAttributes(typeof(OptionFlagAttribute), true).FirstOrDefault() as OptionFlagAttribute;
            if (of == null)
                continue;

            object value = fi.GetValue(this, null);
            if (value == null)
                continue;

            if (fi.PropertyType == typeof(Dictionary<string, string>))
            {
                var dictionary = (Dictionary<string, string>)value;
                foreach (var d in dictionary)
                {
                    result.AppendFormat(" {0} \"{1}\" \"{2}\"", of.Name, d.Key, d.Value);
                }
            }
            else if (fi.PropertyType == typeof(bool))
            {
                if ((bool)value)
                    result.AppendFormat(CultureInfo.InvariantCulture, " {0}", of.Name);
            }
            else
            {
                result.AppendFormat(CultureInfo.InvariantCulture, " {0} {1}", of.Name, value);
            }
        }

        return result.ToString().Trim();
    }
}