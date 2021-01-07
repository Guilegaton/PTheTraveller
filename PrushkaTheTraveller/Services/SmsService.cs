using Android.Telephony;
using System;
using System.Collections.Generic;

namespace PrushkaTheTraveller.Services
{
    public class SmsService : IDisposable
    {
        private readonly SmsManager _smsManager;

        public SmsService()
        {
            _smsManager = SmsManager.Default;
        }

        public void SendSms(string message, string number)
        {
            IList<string> divideContents = _smsManager.DivideMessage(message);

            foreach (string text in divideContents)
            {
                _smsManager.SendTextMessage(number, null, text, null, null);
            }
        }

        public void Dispose()
        {
            _smsManager.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}