using Kitchen;
using KitchenLib;
using KitchenLib.Event;
using KitchenLib.Preferences;
using KitchenMods;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace UniversalProviders
{
	public class Main : BaseMod
	{
		public const string MOD_ID = "universalproviders";
		public const string MOD_NAME = "Universal Providers";
		public const string MOD_AUTHOR = "StarFluxMods";
		public const string MOD_VERSION = "0.1.3";
		public const string MOD_BETA_VERSION = "";
		public const string MOD_COMPATIBLE_VERSIONS = ">=1.2.0";

		public static AssetBundle bundle;
		public static PreferenceManager manager;
		public Main() : base(MOD_ID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_BETA_VERSION, MOD_COMPATIBLE_VERSIONS, Assembly.GetExecutingAssembly()) { }

		protected override void OnPostActivate(Mod mod)
		{
			bundle = bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).ToList()[0];

			manager = new PreferenceManager(MOD_ID);

			manager.RegisterPreference(new PreferenceBool("usingModels", true));

			manager.Load();

			ModsPreferencesMenu<MenuAction>.RegisterMenu("Universal Providers", typeof(PreferenceMenu<MenuAction>), typeof(MenuAction));

			Events.PlayerPauseView_SetupMenusEvent += (s, args) =>
			{
				args.addMenu.Invoke(args.instance, new object[] { typeof(PreferenceMenu<MenuAction>), new PreferenceMenu<MenuAction>(args.instance.ButtonContainer, args.module_list) });
			};
			Events.MainMenuView_SetupMenusEvent += (s, args) =>
			{
				args.addMenu.Invoke(args.instance, new object[] { typeof(PreferenceMenu<MenuAction>), new PreferenceMenu<MenuAction>(args.instance.ButtonContainer, args.module_list) });
			};


			AddGameDataObject<UniversalProvider>();
			AddGameDataObject<GlobalProvider>();
		}
	}
}