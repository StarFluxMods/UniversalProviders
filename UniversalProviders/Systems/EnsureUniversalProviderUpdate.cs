using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace UniversalProviders
{
	public class EnsureUniversalProviderUpdate : GameSystemBase, IModSystem
	{

		protected override void Initialise()
		{
			base.Initialise();
			query = GetEntityQuery(new ComponentType[] { typeof(CUniversalProvider), typeof(CItemProvider) });
		}

		protected override void OnUpdate()
		{
			NativeArray<Entity> nativeArray = query.ToEntityArray(Allocator.Temp);

			for (int i = 0; i < nativeArray.Length; i++)
			{
				Entity entity = nativeArray[i];
				if (Require(entity, out CUniversalProvider uProvider))
				{
					if (Require(entity, out CItemProvider provider))
					{
						if (provider.ProvidedItem != uProvider.ItemID)
						{
							if (uProvider.ItemID != 0)
							{
								provider.SetAsItem(uProvider.ItemID);
								SetComponent(entity, provider);
							}
						}
					}
				}
			}
		}

		private EntityQuery query;
	}
}
