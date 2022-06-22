using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Domain
{
    class ResourceHelpers
    {
        public static string GetResourceFile(string resourceName)
        {
            var stream1 = Assembly.GetExecutingAssembly();

            var stream = stream1.GetManifestResourceStream(resourceName);

            if (stream == null)
                return null;

            using (var reader = new StreamReader(stream))
            {
                var data = reader.ReadToEnd();
                return data;
            }
        }
    }
}
