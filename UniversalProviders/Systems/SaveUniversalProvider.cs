using Kitchen;
using KitchenMods;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;

namespace UniversalProviders
{
	public class SaveUniversalProvider : GameSystemBase, IModSystem
	{
		protected override void Initialise()
		{
			base.Initialise();

			query = GetEntityQuery(new ComponentType[]
			{
				typeof(CUniversalProvider),
				typeof(CPosition)
			});
		}

		protected override void OnUpdate()
		{
			if (Has<SPracticeMode>())
			{
				using (NativeArray<CUniversalProvider> nativeArray = query.ToComponentDataArray<CUniversalProvider>(Allocator.Temp))
				{
					using (NativeArray<CPosition> nativeArray2 = query.ToComponentDataArray<CPosition>(Allocator.Temp))
					{
						CUniversalProviders.Clear();
						CPositions.Clear();
						foreach (CUniversalProvider item in nativeArray)
						{
							CUniversalProviders.Add(item);
						}
						foreach (CPosition item2 in nativeArray2)
						{
							CPositions.Add(item2);
						}
					}
				}
			}
		}

		public override void AfterLoading()
		{
			base.AfterLoading();
			if (CUniversalProviders != null)
			{
				NativeArray<Entity> nativeArray = query.ToEntityArray(Allocator.Temp);
				NativeArray<CUniversalProvider> nativeArray2 = query.ToComponentDataArray<CUniversalProvider>(Allocator.Temp);
				NativeArray<CPosition> nativeArray3 = query.ToComponentDataArray<CPosition>(Allocator.Temp);

				for (int i = 0; i < nativeArray.Length; i++)
				{
					for (int j = 0; j < CUniversalProviders.Count; j++)
					{
						bool flag2 = (nativeArray3[i].Position - CPositions[j].Position).Chebyshev() < 0.1f;
						if (flag2)
						{
							CUniversalProvider component = nativeArray2[i];
							component.ItemID = CUniversalProviders[j].ItemID;
							base.SetComponent<CUniversalProvider>(nativeArray[i], component);
							break;
						}
					}
				}

				CUniversalProviders.Clear();
				nativeArray.Dispose();
				nativeArray2.Dispose();
			}
		}

		private EntityQuery query;
		private static List<CUniversalProvider> CUniversalProviders = new List<CUniversalProvider>();
		private static List<CPosition> CPositions = new List<CPosition>();
	}
}
