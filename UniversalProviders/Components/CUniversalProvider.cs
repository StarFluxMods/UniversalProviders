using KitchenData;
using KitchenMods;
using Unity.Entities;

namespace UniversalProviders
{
	public struct CUniversalProvider : IModComponent, IApplianceProperty, IAttachableProperty, IComponentData
	{
		public int ItemID;

		public CUniversalProvider()
		{
			ItemID = 0;
		}
	}
}
