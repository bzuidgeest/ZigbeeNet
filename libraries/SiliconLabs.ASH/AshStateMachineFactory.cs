using Microsoft.Extensions.Logging;
using SiliconLabs.ASH.Common;
using SiliconLabs.ASH.V2;
using SiliconLabs.ASH.V3;
using System;

namespace SiliconLabs.ASH
{
    /// <summary>
    /// Factory for creating version-specific state machines
    /// </summary>
    public static class AshStateMachineFactory
    {
        /// <summary>
        /// Create state machine for specified version
        /// </summary>
        public static IAshStateMachine Create(AshVersion version, ILoggerFactory loggerFactory)
        {
            return version switch
            {
                AshVersion.V2 => new AshStateMachineV2(loggerFactory.CreateLogger<AshStateMachineV2>()),
                AshVersion.V3 => new AshStateMachineV3(loggerFactory.CreateLogger<AshStateMachineV3>()),
                _ => throw new ArgumentException($"Unsupported ASH version: {version}")
            };
        }
    }
}
