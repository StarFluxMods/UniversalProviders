using KitchenData;
using KitchenMods;
using Unity.Entities;

namespace UniversalProviders
{
	public struct CGlobalProvider : IModComponent, IApplianceProperty, IAttachableProperty, IComponentData
	{
		public int ItemID;

		public CGlobalProvider()
		{
			ItemID = 0;
		}
	}
}
