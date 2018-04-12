using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHapi.Base.Model;
using NHapi.Base.Util;
using NHapiTools.Base.Net;

namespace NSService.Controllers
{
    [Route("api/HL7Com")]
    public class HL7ComController : Controller
    {
        // Test class.
        SimpleMLLPClient mllpClient; // = new SimpleMLLPClient(hostname, port);

        [HttpGet()]
        [HttpPost()]
        public IActionResult HL7messageFromServer([FromHeader] string content)
        {
            String hl7MessageRaw;

            using (Stream Body = HttpContext.Request.Body)
            {
                hl7MessageRaw = new StreamReader(HttpContext.Request.Body).ReadToEnd();
            }

            // Test Code.

            string hostname = "localhost";
            int port = 2020;
            mllpClient = new SimpleMLLPClient(hostname, port, System.Text.Encoding.UTF8);

            string testMessage =
                   @"MSH|^~\&|OAZIS||||201[FromBody] 40202232501||ADT^A04|07112951|P|2.3||||||ASCII"
                   +"EVN | A04 | 20140202232501 |||| 201402022324"
                   +"PID | 1 || 9012214504 | 90122124631 ^^^^ NN | Kasmi ^ Nora ^^^ Mevr.|| 19901221 | F ||| Borsbeeksesteenweg 96 ^^ DEURNE(ANTWERPEN) ^^ 2100 ^ B ^ H || 0496076965 ^^ CP || NL | 0 || 16037779 ^^^^ VN | 896076704 | 90122124631 |||||| B |||| N"
                   +"PD1 |||| 115854 ^ Claeys ^ Margaretha |||||||| N"
                   +"PV1 | 1 | E | 1521 ^ 01U ^ 01 ^ 002 ^ 0 ^ 0 | NULL ||| 115854 ^ Claeys ^ Margaretha || 002802 ^ Moni ^ Diane | 1502 ||||||| 002802 ^ Moni ^ Diane | 0 | 16037779 ^^^^ VN | 1 ^ 20140202 | 40 |||||||||||||||||| O ||||| 201402022324"
                   +"PV2 || 040 ^^^ 40 | NULL |||||||||||||||||| 0 | N |||||||| T |||||||| 0 / 1 / 1 / 09 / A"
                   +"OBX | 1 | CE | CODE_ADM | RCM | A"
                   +"OBX | 2 | CE | REF_BY | RCM | 1 ^ 1 Op eigen initiatief"
                   +"OBX | 3 | CE | COMING_FROM | RCM | 1 ^ 1 Thuis"
                   +"OBX | 4 | CE | URG_CAUSE | RCM | Z ^ Z Somatische Ziekte"
                   +"IN1 | 1 | 1 | 101000 | CHRIST.VERBOND ZIEKENFONDSEN | MOLENBERGSTRAAT, 2 ^^ ANTWERPEN 1 ^^ 2000 ^ B ||||||| 20100401 ||||| 0 || |||||||||||||||||||||||||||| 111 / 111 || 90122124631"
                   +"FTS | 0 | 7112951";

            mllpClient.SendHL7Message(testMessage);

            return Ok();
        }

    }
}