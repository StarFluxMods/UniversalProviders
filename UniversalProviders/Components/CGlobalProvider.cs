using KitchenData;
using Unity.Entities;

namespace UniversalProviders
{
	public struct CGlobalProvider : IApplianceProperty, IAttachableProperty, IComponentData
	{
		public int ItemID;

		public CGlobalProvider()
		{
			ItemID = 0;
		}
	}
}
