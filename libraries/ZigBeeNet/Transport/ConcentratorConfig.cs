using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Transport
{
    /// <summary>
    /// Defines a the configuration for the ZigBee Concentrator
    /// </summary>
    public class ConcentratorConfig
    {
        /// <summary>
        /// Defines the {@link ConcentratorType}
        /// </summary>
        public ConcentratorType Type { get; set; }

        /// <summary>
        /// The minimum time between MTORR transmissions
        /// </summary>
        public ushort MinTime { get; set; }

        /// <summary>
        /// The maximum time between MTORR transmissions
        /// </summary>
        public ushort MaxTime { get; set; }

        /// <summary>
        /// The maximum number of hops the MTORR will be sent to
        /// </summary>
        public int MaxHops { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte DeliveryFailureThreshold { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public byte RouteErrorThreshold { get; set; }

    }

}
