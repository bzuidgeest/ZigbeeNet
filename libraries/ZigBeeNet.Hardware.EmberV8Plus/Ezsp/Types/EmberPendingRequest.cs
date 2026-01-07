using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Types
{
	internal class PendingEmberRequest
	{
		public TaskCompletionSource<List<byte[]>> TaskCompletionSource { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);

		public List<byte[]> Replies { get; } = [];
	}
}
