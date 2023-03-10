using KitchenData;
using Unity.Entities;

namespace UniversalProviders
{
	public struct CUniversalProvider : IApplianceProperty, IAttachableProperty, IComponentData
	{
		public int ItemID;

		public CUniversalProvider()
		{
			ItemID = 0;
		}
	}
}
