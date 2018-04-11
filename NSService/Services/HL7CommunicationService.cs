using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHapi.Base.Model;
using NHapi.Base.Util;
using NHapiTools.Base.Net;

namespace NSService.Services
{
    public class HL7CommunicationService
    {
        // MLLPClient.
        SimpleMLLPClient mllpClient; 

        public HL7CommunicationService(string mllpHostname, int mllpPort)
        {
            mllpClient = new SimpleMLLPClient(mllpHostname, mllpPort, System.Text.Encoding.UTF8);
        }

        public string StringSendHl7MessageToRemote(string message)
        {
           return  mllpClient.SendHL7Message(message);
        }
    }
}
