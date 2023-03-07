using KitchenLib;
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
		public const string MOD_VERSION = "0.1.0";
		public const string MOD_BETA_VERSION = "";
		public const string MOD_COMPATIBLE_VERSIONS = "1.1.4";

		public static AssetBundle bundle;
		public Main() : base(MOD_ID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_BETA_VERSION, MOD_COMPATIBLE_VERSIONS, Assembly.GetExecutingAssembly()) { }

		protected override void OnPostActivate(Mod mod)
		{
			bundle = bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).ToList()[0];

			AddGameDataObject<UniversalProvider>();
		}
	}
}