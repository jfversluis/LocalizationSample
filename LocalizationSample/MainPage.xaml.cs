using System.Globalization;
using I18n.net;
using Xamarin.Forms;

namespace LocalizationSample
{
	public partial class MainPage : ContentPage
	{
		public II18N Strings => I18N.Current;

		public Command SwitchLanguageCommand { get; set; }

		public MainPage()
		{
			InitializeComponent();

			SwitchLanguageCommand = new Command<string>(SwitchLanguage);

			BindingContext = this;
		}

		private void SwitchLanguage(string language)
		{
			I18N.Current.Locale = language;

			DependencyService.Resolve<ILocalize>().SetLocale(new CultureInfo(language));

			OnPropertyChanged(nameof(Strings));
		}
	}
}