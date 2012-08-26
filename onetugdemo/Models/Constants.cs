
namespace onetugdemo.Models
{
    public static class Constants
    {
#error You must configure these variables from your own account on twilio and appharbor

        // You can get the SID and AuthToken from your twilio dashboard
        public static readonly string SID = "";
        public static readonly string AuthToken = "";

        // should be the phone number you bought from twilio.
        // the format is "+19998887777"
        public static readonly string From = "";

        // This is the mongolab database name, defined as an addon in appharbor
        public static readonly string MongolabDB = "";
    }
}