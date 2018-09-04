using System.Globalization;

namespace LocalizationSample
{
	public interface ILocalize
	{
		CultureInfo GetCurrentCultureInfo();
		void SetLocale(CultureInfo ci);
	}
}