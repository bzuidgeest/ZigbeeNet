using SiliconLabs.ASH.Common;

namespace SiliconLabs.ASH;

public class AshFrameReceivedEventArgs(IAshFrame frame)
{
	public IAshFrame Frame { get; set; } = frame;
}
