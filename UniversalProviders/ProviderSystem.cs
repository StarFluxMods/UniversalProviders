using Kitchen;
using KitchenData;
using Unity.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine.Networking.Types;
using KitchenMods;
using Controllers;
using UnityEngine;
using System.Reflection;
using KitchenLib.Utils;

namespace UniversalProviders
{
	public class ProviderSystem : RotateAppliances, IModSystem
	{
		private List<int> items = new List<int>();
		protected override void Initialise()
		{
			foreach (Item item in GameData.Main.Get<Item>())
			{
				if (item.DedicatedProvider != null)
				{
					items.Add(item.ID);
				}
			}
		}

		protected override bool IsPossible(ref InteractionData data)
		{
			return base.IsPossible(ref data);
		}

		protected override void Perform(ref InteractionData data)
		{
			if (Require(data.Target, out CUniversalProvider provider))
			{
				Require(data.Interactor, out CInputData input);
				if ((input.State.StopMoving == ButtonState.Held))
				{
					if ((items.IndexOf(provider.Item) - 1) > 0)
					{
						provider.Item = items[(items.IndexOf(provider.Item) + 1) % items.Count];
					}
				}
				else
				{
					base.Perform(ref data);
				}

				EntityManager.SetComponentData<CUniversalProvider>(data.Target, provider);
			}
			else
			{
				base.Perform(ref data);
			}
		}
	}

	public class test : GameSystemBase, IModSystem
	{
		protected override void Initialise()
		{
			base.Initialise();
			UniversalProviders = GetEntityQuery(new ComponentType[] { typeof(CUniversalProvider) } );
		}

		protected override void OnUpdate()
		{
			if (Has<SPracticeMode>())
			{
				var entities = UniversalProviders.ToEntityArray(Allocator.TempJob);
				CUniversalProviders.Clear();
				foreach (var entity in entities)
				{
					Require(entity, out CUniversalProvider universalProvider);
					CUniversalProviders.Add(universalProvider);
				}
			}
		}

		public override void AfterLoading()
		{
			base.AfterLoading();
			if (CUniversalProviders != null)
			{
				NativeArray<Entity> nativeArray = UniversalProviders.ToEntityArray(Allocator.Temp);
				NativeArray<CUniversalProvider> nativeArray2 = UniversalProviders.ToComponentDataArray<CUniversalProvider>(Allocator.Temp);
				NativeArray<CItemProvider> nativeArray3 = UniversalProviders.ToComponentDataArray<CItemProvider>(Allocator.Temp);
				for (int i = 0; i < nativeArray.Length; i++)
				{
					CUniversalProvider provider = nativeArray2[i];
					provider.Item = nativeArray3[i].ProvidedItem;
					SetComponent(nativeArray[i], provider);
				}
				CUniversalProviders.Clear();
				nativeArray.Dispose();
				nativeArray2.Dispose();
				nativeArray3.Dispose();
			}
		}

		private EntityQuery UniversalProviders;
		private static List<CUniversalProvider> CUniversalProviders = new List<CUniversalProvider>();
	}
}
