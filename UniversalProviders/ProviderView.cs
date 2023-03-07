using Kitchen;
using KitchenData;
using KitchenLib.Utils;
using KitchenMods;
using MessagePack;
using System.Reflection;
using TMPro;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace UniversalProviders
{
	public class ProviderView : UpdatableObjectView<ProviderView.ViewData>
	{
		#region ECS View System (Runs on host and updates views to be broadcasted to clients)
		public class UpdateView : IncrementalViewSystemBase<ViewData>, IModSystem
		{
			private EntityQuery Views;

			protected override void Initialise()
			{
				base.Initialise();

				Views = GetEntityQuery(new QueryHelper()
					.All(typeof(CUniversalProvider), typeof(CLinkedView)));
			}

			protected override void OnUpdate()
			{
				using var entities = Views.ToEntityArray(Allocator.Temp);
				using var views = Views.ToComponentDataArray<CLinkedView>(Allocator.Temp);

				for (var i = 0; i < views.Length; i++)
				{

					var view = views[i];

					int active = 0;
					if (Require(entities[i], out CUniversalProvider itemProvider))
					{
						active = itemProvider.Item;
					}
					ViewData data = new ViewData
					{
						ActiveID = active
					};

					SendUpdate(view, data);
				}
			}
		}
		#endregion




		#region Message Packet
		[MessagePackObject(false)]
		public struct ViewData : ISpecificViewData, IViewData.ICheckForChanges<ViewData>
		{
			[Key(1)] public int ActiveID;

			public IUpdatableObject GetRelevantSubview(IObjectView view) => view.GetSubView<ProviderView>();

			public bool IsChangedFrom(ViewData cached)
			{
				return ActiveID != cached.ActiveID;
			}
		}
		#endregion

		protected int _ActiveID;
		protected override void UpdateData(ViewData view_data)
		{
			_ActiveID = view_data.ActiveID;
		}

		void Update()
		{
			if (_ActiveID != 0)
			{
				GameObject image = GameObjectUtils.GetChildObject(transform.gameObject, "Blueprint/Blueprint/Cube.001");
				GameObject text = GameObjectUtils.GetChildObject(transform.gameObject, "Blueprint/Blueprint/Text");
				MeshRenderer renderer = image.GetComponent<MeshRenderer>();
				TextMeshPro tmp = text.GetComponent<TextMeshPro>();
				Item item = GameData.Main.Get<Item>(_ActiveID);
				renderer.material.SetTexture("_Image", PrefabSnapshot.GetItemSnapshot(item.Prefab));
				tmp.text = item.name;
			}
		}
	}
}