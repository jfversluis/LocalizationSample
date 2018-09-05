using System;
using System.Diagnostics;
using System.Reflection;
using I18n.net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LocalizationSample
{
	public partial class App : Application
	{
		public App()
		{
			if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
			{
				var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
				Localizations.Language.Culture = ci; // set the RESX for resource localization
				DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
			}

			I18N.Current
				.SetNotFoundSymbol("$") // Optional: when a key is not found, it will appear as $key$ (defaults to "$")
				.SetFallbackLocale("en") // Optional but recommended: locale to load in case the system locale is not supported
				.SetThrowWhenKeyNotFound(true) // Optional: Throw an exception when keys are not found (recommended only for debugging)
				.SetLogger(text => Debug.WriteLine(text)) // action to output traces
				.SetResourcesFolder("I18NPortable") // Optional: The directory containing the resource files (defaults to "Locales")
				.Init(GetType().GetTypeInfo().Assembly); // assembly where locales live

			InitializeComponent();

			MainPage = new MainPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
