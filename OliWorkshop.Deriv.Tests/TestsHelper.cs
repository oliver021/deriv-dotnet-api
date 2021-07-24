using System;
using System.Collections.Generic;
using System.Text;

namespace OliWorkshop.Deriv.Tests
{
    public class TestsHelper
    {
        /// <summary>
        /// Simple deriv service initialization
        /// </summary>
        /// <returns></returns>
        public static DerivApiService MockService()
        {
            return new DerivApiService(new WebSocketStream("wss://ws.binaryws.com/websockets/v3?app_id=11", null));
        }
    }
}
