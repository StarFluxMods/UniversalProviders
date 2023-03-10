using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalProviders
{

	public class UniversalProvider : CustomAppliance
	{
		public override int BaseGameDataObjectID => ApplianceReferences.OfficeDesk;
		public override string UniqueNameID => "UniversalProvider";
		public override GameObject Prefab => Main.bundle.LoadAsset<GameObject>("UniversalProvider");
		public override List<IApplianceProperty> Properties => new List<IApplianceProperty>
		{
			new CItemProvider
			{
				Maximum = 0,
				Available = 0
			},
			new CUniversalProvider()
		};

		public override List<(Locale, ApplianceInfo)> InfoList => new List<(Locale, ApplianceInfo)>
		{
			(Locale.English, new ApplianceInfo
			{
				Name = "Universal Provider",
				Description = "Ever wanted a provider that can provide any item? Well, now you can!"
			})
		};

		public override void OnRegister(GameDataObject gameDataObject)
		{
			Appliance appliance = (Appliance)gameDataObject;
			GameObject prefab = appliance.Prefab;

			UniversalProviderView view = prefab.AddComponent<UniversalProviderView>();
			view.HoldPoint = GameObjectUtils.GetChildObject(prefab, "HoldPoint");

			MaterialUtils.ApplyMaterial(prefab, "Blueprint/Blueprint/Cube", new Material[] { MaterialUtils.GetExistingMaterial("Blueprint Light") });
			MaterialUtils.ApplyMaterial(prefab, "Blueprint/Blueprint/Cube.001", new Material[] { MaterialUtils.GetExistingMaterial("Flat Image - Faded") });
		}
	}
}
