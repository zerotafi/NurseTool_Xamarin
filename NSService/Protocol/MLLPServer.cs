﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using NSService.Protocol;


namespace NSService
{
    public class MLLPServer : AppServer<MLLPSession, HL7RequestInfo>
    {
        public MLLPServer() : base(new DefaultReceiveFilterFactory<MLLPBeginEndMarkReceiveFilter, HL7RequestInfo>())
        { }

        protected override void ExecuteCommand(MLLPSession session, HL7RequestInfo requestInfo)
        {
            try
            {
                base.ExecuteCommand(session, requestInfo);

                if (!requestInfo.WasUnknownRequest)
                    session.Send(requestInfo);
            }
            catch (Exception e)
            {
                requestInfo.ErrorMessage = e.Message;
                session.Send(requestInfo);
            }
        }
    }
}
