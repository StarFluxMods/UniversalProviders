using Controllers;
using Kitchen;
using KitchenData;
using KitchenMods;
using System.Collections.Generic;
using Unity.Entities;

namespace UniversalProviders
{
	public class ProviderSystem : ItemInteractionSystem, IModSystem
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
			bool result = true;
			if (Require(data.Interactor, out CInputData input))
			{
				if (Require(data.Target, out CUniversalProvider provider))
				{
					result = true;
				}
				else
					result = false;
			}
			else
				result = false;
			return result;
		}

		protected override void Perform(ref InteractionData data)
		{
			int x = 0;
			if (Require(data.Interactor, out CInputData input))
			{
				if (Require(data.Target, out CUniversalProvider provider))
				{
					if ((input.State.StopMoving == ButtonState.Held))
						x = -1;
					else
						x = 1;

					provider.ItemID = items[(items.IndexOf(provider.ItemID) + x) % items.Count];
					EntityManager.SetComponentData<CUniversalProvider>(data.Target, provider);

				}
			}
		}
	}
}
