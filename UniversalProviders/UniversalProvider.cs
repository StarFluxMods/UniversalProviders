using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Entities;

namespace UniversalProviders
{
	public struct CUniversalProvider : IApplianceProperty, IAttachableProperty, IComponentData
	{
		public int ItemID;

		public CUniversalProvider()
		{
			ItemID = ItemReferences.Apple;
		}
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
				Available = 500,
				Maximum = 999
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

			ProviderView view = prefab.AddComponent<ProviderView>();
			view.HoldPoint = GameObjectUtils.GetChildObject(prefab, "HoldPoint");

			MaterialUtils.ApplyMaterial(prefab, "Blueprint/Blueprint/Cube", new Material[] { MaterialUtils.GetExistingMaterial("Blueprint Light") });
			MaterialUtils.ApplyMaterial(prefab, "Blueprint/Blueprint/Cube.001", new Material[] { MaterialUtils.GetExistingMaterial("Flat Image - Faded") });
		}
	}
}
