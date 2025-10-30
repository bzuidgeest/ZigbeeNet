using System;
using System.Collections.Generic;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Structure;

namespace ZigBeeNet.Hardware.EmberV8Plus.Transaction
{
    /// <summary>
    /// Single EZSP transaction response handling. This matches a {@link EzspFrameRequest} with a single
    /// {@link EzspFrameResponse}. {@link EzspFrame#frameId} must also match.
    /// </summary>
    public class EzspSingleResponseTransaction : IEzspTransaction
    {
        private EzspFrameRequestV8Plus _request;
        private EzspFrameResponseV8Plus _response;
        private Type _requiredResponse;

        public EzspSingleResponseTransaction(EzspFrameRequestV8Plus request, Type requiredResponse) 
        {
            this._request = request;
            this._requiredResponse = requiredResponse;
        }

        public bool IsMatch(EzspFrameResponseV8Plus response) 
        {
            if (response.GetType() == _requiredResponse && _request.GetSequenceNumber() == response.GetSequenceNumber()) 
            {
                this._response = response;
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
            if (_response == null) 
                return EmberStatus.UNKNOWN;

            // TODO: Fix the response status!
            return EmberStatus.UNKNOWN;
        }

        public EzspFrameResponseV8Plus GetResponse() 
        {
            return _response;
        }

        public List<EzspFrameResponseV8Plus> GetResponses() 
        {
            if (_response == null) 
                return null;

            // This transaction only allows a single response
            return new List<EzspFrameResponseV8Plus>() { _response };
        }
    }
}
