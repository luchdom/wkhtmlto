using System.Diagnostics;
using System.Text;

namespace WkHtmlShared
{
    public class WkHtmlDriver
    {
        /// <summary>
        /// Converts URL or HTML string 
        /// </summary>
        /// <param name="wkhtmlPath">Render path.</param>
        /// <param name="switches">Replacements.</param>
        /// <param name="html">Html content or url.</param>
        /// <returns>PDF como array de bytes.</returns>
        public static byte[] Convert(string switches, string html, string rendererPath)
        {

            switches = "-q " + switches + " -";

            if (!string.IsNullOrEmpty(html))
            {
                switches += " -";
                html = SpecialCharsEncode(html);
            }

            var proc = new Process();
            try
            {
                proc.StartInfo = new ProcessStartInfo
                {
                    FileName = rendererPath,
                    Arguments = switches,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = true
                };
                proc.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

            if (!string.IsNullOrEmpty(html))
            {
                using (var sIn = proc.StandardInput)
                {
                    sIn.WriteLine(html);
                }
            }

            using (var ms = new MemoryStream())
            {
                using (var sOut = proc.StandardOutput.BaseStream)
                {
                    byte[] buffer = new byte[4096];
                    int read;
                    while ((read = sOut.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                }

                string error = proc.StandardError.ReadToEnd();
                if (ms.Length == 0)
                    throw new Exception(error);

                proc.WaitForExit();
                return ms.ToArray();
            }
        }

        public static string SpecialCharsEncode(string text)
        {
            var chars = text.ToCharArray();
            var result = new StringBuilder(text.Length + (int)(text.Length * 0.1));

            foreach (var c in chars)
            {
                var value = System.Convert.ToInt32(c);
                if (value > 127)
                    result.AppendFormat("&#{0};", value);
                else
                    result.Append(c);
            }

            return result.ToString();
        }
    }
}