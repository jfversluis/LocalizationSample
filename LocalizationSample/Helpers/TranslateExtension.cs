using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalizationSample
{
	[ContentProperty("Text")]
	public class TranslateExtension : IMarkupExtension
	{
		private readonly CultureInfo ci = null;
		private const string ResourceId = "LocalizationSample.Localizations.Language";

		static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(
			() => new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));

		public string Text { get; set; }

		public TranslateExtension()
		{
			if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
			{
				ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
			}
		}

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			if (Text == null)
				return string.Empty;

			var translation = ResMgr.Value.GetString(Text, ci);
			if (translation == null)
			{
#if DEBUG
				throw new ArgumentException(
					string.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResourceId, ci.Name));
#else
            translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
			}
			return translation;
		}
	}
}