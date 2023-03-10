using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalProviders
{

	public class GlobalProvider : CustomAppliance
	{
		public override int BaseGameDataObjectID => ApplianceReferences.OfficeDesk;
		public override string UniqueNameID => "GlobalProvider";
		public override GameObject Prefab => Main.bundle.LoadAsset<GameObject>("GlobalProvider");
		public override List<IApplianceProperty> Properties => new List<IApplianceProperty>
		{
			new CItemProvider
			{
				Maximum = 0,
				Available = 0
			},
			new CGlobalProvider()
		};

		public override List<(Locale, ApplianceInfo)> InfoList => new List<(Locale, ApplianceInfo)>
		{
			(Locale.English, new ApplianceInfo
			{
				Name = "Global Provider",
				Description = "Ever wanted a provider that can provide any item? Well, now you can!"
			})
		};

		public override void OnRegister(GameDataObject gameDataObject)
		{
			Appliance appliance = (Appliance)gameDataObject;
			GameObject prefab = appliance.Prefab;

			GlobalProviderView view = prefab.AddComponent<GlobalProviderView>();
			view.HoldPoint = GameObjectUtils.GetChildObject(prefab, "HoldPoint");

			MaterialUtils.ApplyMaterial(prefab, "Blueprint/Blueprint/Cube", new Material[] { MaterialUtils.GetCustomMaterial("Blueprint Light - Green") });
			MaterialUtils.ApplyMaterial(prefab, "Blueprint/Blueprint/Cube.001", new Material[] { MaterialUtils.GetExistingMaterial("Flat Image - Faded") });
		}
	}
}
