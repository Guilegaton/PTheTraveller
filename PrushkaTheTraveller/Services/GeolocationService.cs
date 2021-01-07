using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PrushkaTheTraveller.Services
{
    public class GeolocationService : IDisposable
    {
        private CancellationTokenSource _cancellationToken;

        public async Task<Position> GetGeolocation()
        {
            _cancellationToken = new CancellationTokenSource();
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(new TimeSpan(0,0,10), _cancellationToken.Token);
            return position;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}