using System;
using System.Globalization;
using System.Threading;

namespace Foundation.Core
{
    public class CultureScope : IDisposable
    {
        private readonly CultureInfo _previousCultureInfo;
        private readonly CultureInfo _previousUiCultureInfo;


        public CultureScope(string culture) : this(CultureInfo.GetCultureInfo(culture)) { }

        public CultureScope(CultureInfo cultureInfo)
        {
            _previousCultureInfo = Thread.CurrentThread.CurrentCulture;
            _previousUiCultureInfo = Thread.CurrentThread.CurrentUICulture;

            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = cultureInfo;
        }

        public void Dispose()
        {
            Thread.CurrentThread.CurrentUICulture = _previousUiCultureInfo;
            Thread.CurrentThread.CurrentCulture = _previousCultureInfo;
        }
    }
}
