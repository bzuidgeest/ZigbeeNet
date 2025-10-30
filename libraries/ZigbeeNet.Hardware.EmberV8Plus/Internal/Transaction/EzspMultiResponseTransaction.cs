using System;
using System.Collections.Generic;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Structure;

namespace ZigBeeNet.Hardware.EmberV8Plus.Transaction
{
    /// <summary>
    /// Multiple EZSP transaction response handling. This matches a {@link EzspFrameRequest} which returns with multiple
    ///  {@link EzspFrameResponse}. {@link EzspFrame#frameId} must also match on the first response.
    ///  
    ///  As example of this transaction is the SCAN requests. Such requests to the EZSP NCP result in multiple responses -:
    ///  
    ///   - An initial acknowledgement response
    ///   - Subsequent scan result responses
    ///   - A scan complete response
    ///  
    ///  This transaction handler will record all responses related to the request, but only complete on the final response.
    /// </summary>
	public class EzspMultiResponseTransaction : IEzspTransaction
    {
        /**
		 * The request we sent
		 */
        private EzspFrameRequestV8Plus _request;

        /**
		 * A list of responses received in relation to this transaction
		 */
        private List<EzspFrameResponseV8Plus> _responses = new List<EzspFrameResponseV8Plus>();

        /**
		 * The response required to complete the transaction
		 */
        private Type _requiredResponse;

        /**
		 * The response required to complete the transaction
		 */
        private HashSet<Type> _relatedResponses;

        public EzspMultiResponseTransaction(EzspFrameRequestV8Plus request, Type requiredResponse,
                HashSet<Type> relatedResponses)
        {
            this._request = request;
            this._requiredResponse = requiredResponse;
            this._relatedResponses = relatedResponses;
        }
        public bool IsMatch(EzspFrameResponseV8Plus response)
        {
            // Check if this response is related to this transaction
            if (_relatedResponses.Contains(response.GetType()))
            {
                // TODO: Check for a failure

                // Add the response to our responses received list
                _responses.Add(response);
                return false;
            }

            // Check if this response completes the transaction
            if (response.GetType() == _requiredResponse)
            {
                _responses.Add(response);
                return true;
            }
            else
            {
                return false;
            }
        }

        public EzspFrameRequestV8Plus GetRequest()
        {
            return _request;
        }
        public EmberStatus GetStatus()
        {
            if (_responses.Count == 0)
            {
                return EmberStatus.UNKNOWN;
            }

            // TODO: Fix the response status! Needs a common response method?
            // for(EzspFrameResponse response : responses) {
            // }

            return EmberStatus.UNKNOWN;
        }
        public EzspFrameResponseV8Plus GetResponse()
        {
            if (_responses.Count > 0)
            {
                return _responses[_responses.Count - 1];
            }
            return null;
        }

        public List<EzspFrameResponseV8Plus> GetResponses() 
        {
            if (_responses.Count == 0)
            {
                return null;
            }

            return _responses;
        }
    }
}
