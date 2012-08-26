using System.Linq;
using System.Web.Mvc;
using onetugdemo.Models;
using Twilio;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace onetugdemo.Controllers
{
    public class RefTwilioController : TwilioController
    {
        public ActionResult txt1(SmsRequest request)
        {
            Demo.LogNumber(request);

            var response = new TwilioResponse();
            response.Sms(string.Format("You said '{0}'", request.Body));

            Demo.Log("{0} says '{1}'", request.From, request.Body);

            return TwiML(response);
        }

        public ActionResult broadcast(string message)
        {
            var all = Demo.GetAll();

            foreach (var user in all)
            {
                TwilioRestClient t = new TwilioRestClient(Constants.SID, Constants.AuthToken);
                t.SendSmsMessage(Constants.From, user.Number, message);
            }

            return Json(new { success = true, users = all.Select(u => u.Number).ToArray() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult voice1(string From)
        {
            Demo.LogNumber(From);

            var response = new TwilioResponse();
            response.Say("welcome to the 1 tug meeting");

            Demo.Log("Caller - #{0}", From);

            return TwiML(response);
        }

        public ActionResult voice2(string From)
        {
            Demo.LogNumber(From);

            var response = new TwilioResponse();
            response.Say("Welcome to 1 tug, conferencing you in");
            response.DialConference("onetug");

            Demo.Log("{0} joined the conference", From);

            return TwiML(response);
        }

        public ActionResult txt2(SmsRequest request)
        {
            Demo.LogNumber(request);

            TwilioRestClient t = new TwilioRestClient(Constants.SID, Constants.AuthToken);
            t.InitiateOutboundCall(request.To, request.From, "http://onetugdemo.apphb.com/reftwilio/voice2");

            TwilioResponse response = new TwilioResponse();
            return TwiML(response);
        }

        public ActionResult voice3(string From)
        {
            Demo.LogNumber(From);

            var response = new TwilioResponse();
            response.Say("Hello, and prepare to be frustrated.");
            response.BeginGather(new { finishOnKey = "#", action = "http://onetugdemo.apphb.com/reftwilio/voice4" });
            response.Say("Type in your account number and press pound.");
            response.EndGather();

            return TwiML(response);
        }

        public ActionResult voice4(string From, string Digits)
        {
            var response = new TwilioResponse();
            response.Say(string.Format("You said {0}, thanks for calling", Digits));

            Demo.Log("#{0}'s account number is {1}", From, Digits);

            return TwiML(response);
        }


        public ActionResult chuck(SmsRequest request)
        {
            Demo.LogNumber(request);

            var roundhouse = ChuckNorris.API.Random(null, new string[] { "explicit" });
            var joke = roundhouse.Result.Text;

            var response = new TwilioResponse();
            response.Sms(joke);

            Demo.Log("{0} got roundhoused '{1}'", request.From, joke);

            return TwiML(response);
        }

        public ActionResult random()
        {
            CallOptions call = new CallOptions
            {
                From = Demo.GetRandom().Number,
                To = Demo.GetRandom().Number
            };

            TwilioRestClient twilio = new TwilioRestClient(Constants.SID, Constants.AuthToken);
            twilio.InitiateOutboundCall(call);

            return Content("done");
        }
    }
}
