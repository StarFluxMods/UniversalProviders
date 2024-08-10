using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using KitchenLib.Preferences;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalProviders
{
	public class PreferenceMenu<T> : KLMenu<T>
	{
		public PreferenceMenu(Transform container, ModuleList module_list) : base(container, module_list)
		{
		}

		public override void Setup(int player_id)
		{
			AddLabel("Use 3D Models");
			AddSelect<bool>(is3DModel);
			is3DModel.OnChanged += delegate (object _, bool result)
			{
				Main.manager.GetPreference<PreferenceBool>("usingModels").Set(result);
				Main.manager.Save();
			};

			New<SpacerElement>(true);
			New<SpacerElement>(true);

			AddButton(base.Localisation["MENU_BACK_SETTINGS"], delegate (int i)
			{
				Main.manager.Save();
				this.RequestPreviousMenu();
			}, 0, 1f, 0.2f);
		}

		private Option<bool> is3DModel = new Option<bool>(new List<bool> { true, false }, Main.manager.GetPreference<PreferenceBool>("usingModels").Get(), new List<string> { "Enabled", "Disabled" });
	}
}
