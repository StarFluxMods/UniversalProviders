using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

namespace UniversalProviders
{
	public struct CUniversalProvider : IApplianceProperty
	{
		public int Item;
	}
	public class UniversalProvider : CustomAppliance
	{
		public override int BaseGameDataObjectID => ApplianceReferences.OfficeDesk;
		public override string UniqueNameID => "UniversalProvider";
		public override GameObject Prefab => Main.bundle.LoadAsset<GameObject>("UniversalProvider");
		public override List<IApplianceProperty> Properties => new List<IApplianceProperty>
		{
			new CItemProvider
			{
				Available = 999,
				Maximum = 999
			},
			new CUniversalProvider()
		};

		public override List<(Locale, ApplianceInfo)> InfoList => new List<(Locale, ApplianceInfo)>
		{
			(Locale.English, new ApplianceInfo
			{
				Name = "Universal Provider"
			})
		};

		public override void OnRegister(GameDataObject gameDataObject)
		{
			Appliance appliance = (Appliance)gameDataObject;
			GameObject prefab = appliance.Prefab;

			prefab.AddComponent<ProviderView>();

			MaterialUtils.ApplyMaterial(prefab, "Blueprint/Blueprint/Cube", new Material[] { MaterialUtils.GetExistingMaterial("Blueprint Light") });
			MaterialUtils.ApplyMaterial(prefab, "Blueprint/Blueprint/Cube.001", new Material[] { MaterialUtils.GetExistingMaterial("Flat Image - Faded") });
		}
	}
}
