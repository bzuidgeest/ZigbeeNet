using System.Collections.Generic;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Internal
{
    /// <summary>
    /// This class provides utility functions to configure, and read the configuration from the Ember stack.
    /// </summary>
    public class EmberStackConfiguration
    {
        /**
         * The {@link EmberNcp} used to send the Ezsp frames to the NCP
         */
        private readonly EmberNcp _ncp;

        /**
         * Constructor to set the {@link EmberNcp}
         *
         * @param ncp the {@link EmberNcp} used to communicate with the NCP
         */
        public EmberStackConfiguration(EmberNcp ncp) 
        {
            this._ncp = ncp;
        }

        /**
         * Configuration utility. Takes a {@link Map} of {@link ZigbeeEzspConfigId} to {@link Integer} and will work through
         * setting them before returning.
         *
         * @param configuration {@link Map} of {@link ZigbeeEzspConfigId} to {@link Integer} with configuration to set
         * @return true if all configuration were set successfully
         */
        public bool SetConfiguration(Dictionary<ZigbeeEzspConfigId, ushort> configuration) 
        {
            bool success = true;

            foreach (var config in configuration) 
            {
                if (_ncp.SetConfigurationValue(config.Key, config.Value) != Status.SL_STATUS_OK) 
                {
                    success = false;
                }
            }
            return success;
        }

        /**
         * Configuration utility. Takes a {@link Set} of {@link ZigbeeEzspConfigId} and will work through
         * requesting them before returning.
         *
         * @param configuration {@link Set} of {@link ZigbeeEzspConfigId} to request
         * @return map of configuration data mapping {@link ZigbeeEzspConfigId} to {@link Integer}. Value will be null if error
         *         occurred.
         */
        public Dictionary<ZigbeeEzspConfigId, int?> GetConfiguration(IEnumerable<ZigbeeEzspConfigId> configuration) 
        {
            Dictionary<ZigbeeEzspConfigId, int?> response = new Dictionary<ZigbeeEzspConfigId, int?>();

            foreach (ZigbeeEzspConfigId configId in configuration) 
            {
                response.Add(configId, _ncp.GetConfigurationValue(configId).Value);
            }

            return response;
        }

        /**
         * Configuration utility. Takes a {@link Map} of {@link ZigbeeEzspConfigId} to {@link ZigbeeEzspDecisionId} and will work
         * through setting them before returning.
         *
         * @param policies {@link Map} of {@link ZigbeeEzspPolicyId} to {@link ZigbeeEzspDecisionId} with configuration to set
         * @return true if all policies were set successfully
         */
        public bool SetPolicy(Dictionary<ZigbeeEzspPolicyId, ZigbeeEzspDecisionId> policies) 
        {
            bool success = true;

            foreach (KeyValuePair<ZigbeeEzspPolicyId, ZigbeeEzspDecisionId> policy in policies) 
            {
                if (_ncp.SetPolicy(policy.Key, policy.Value) != Status.SL_STATUS_OK) 
                {
                    success = false;
                }
            }
            return success;
        }

        /**
         * Configuration utility. Takes a {@link Set} of {@link ZigbeeEzspPolicyId} and will work through
         * requesting them before returning.
         *
         * @param policies {@link Set} of {@link ZigbeeEzspPolicyId} to request
         * @return map of configuration data mapping {@link ZigbeeEzspPolicyId} to {@link ZigbeeEzspDecisionId}. Value will be null if
         *         error occurred.
         */
        public Dictionary<ZigbeeEzspPolicyId, ZigbeeEzspDecisionId> GetPolicy(IEnumerable<ZigbeeEzspPolicyId> policies) 
        {
            Dictionary<ZigbeeEzspPolicyId, ZigbeeEzspDecisionId> response = new Dictionary<ZigbeeEzspPolicyId, ZigbeeEzspDecisionId>();

            foreach (ZigbeeEzspPolicyId policyId in policies) 
            {
                response.Add(policyId, _ncp.GetPolicy(policyId).DecisionId);
            }

            return response;
        }

    }
}
