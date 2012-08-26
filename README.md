onetug-twilio
=============

Code from the talk I gave at ONETUG on Twilio with C#. If you want to run the code as is, you must follow the following steps:

 1. Get an account at Twilio: http://twilio.com ... make note of the SID and AuthToken
 2. Purchase a phone number (or use the supplied sandbox number).
 3. Get an account at AppHarbor: http://appharbor.com
 4. Create an application.
 5. Add a MongoLab addon ... make note of the database name.
 6. Add the: SID, AuthToken, Phone number, and Mongo DB name to the Constants file: https://github.com/joelmartinez/onetug-twilio/blob/master/onetugdemo/Models/Constants.cs
 7. Deploy the code to appharbor. If you're not familiar with GIT, you can easily use the Github for Windows application: http://blog.appharbor.com/2012/05/25/deploy-to-appharbor-using-github-for-windows

Some of the dependencies used in this demo are:

 * Twilio-CSharp: https://github.com/twilio/twilio-csharp
 * SignalR: http://signalr.net/
 * Mongo CSharp Driver: http://www.mongodb.org/display/DOCS/CSharp+Language+Center
 * API Wrapper for the Internet Chuck Norris Database: https://github.com/joelmartinez/icndb-csharp