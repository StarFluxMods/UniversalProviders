using Kitchen;
using KitchenData;
using KitchenMods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;

namespace UniversalProviders
{
	public class EnsureProvider : GenericSystemBase, IModSystem
	{
		protected override void Initialise()
		{
			base.Initialise();
			query = GetEntityQuery(new ComponentType[]
			{
				typeof(CUniversalProvider),
				typeof(CItemProvider)
			});
		}

		protected override void OnUpdate()
		{
			var entities = query.ToEntityArray(Allocator.TempJob);
			foreach (var entity in entities)
			{
				Require(entity, out CUniversalProvider universalProvider);
				Require(entity, out CItemProvider provider);

				provider.ProvidedItem = universalProvider.Item;
				Set(entity, provider);
			}
		}

		private EntityQuery query;
	}
}
