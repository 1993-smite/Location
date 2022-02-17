using LocationOsmApi.Models;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PlaceOsmApi.Controllers;
using PlaceOsmApi.Models;
using PlaceOsmApi.Services;
using PlaceOsmApi.Services.RouteService;
using System.Collections.Generic;

namespace PlaceOsmApi.Test
{
    [TestFixture]
    public class RouteControllerTest
    {
        Mock<IMapManager> _mapManagerMoq;
        RouteController _routeController;

        #region constants

        IList<Place> _places = new List<Place>()
        {
            new Place()
        };
        double distanceTest = double.MaxValue;
        double durationTest = double.MaxValue / 2;

        #endregion

        private Mock<IMapManager> CreateMapManagerMoq()
        {
            var mapManagerMoq = new Mock<IMapManager>();
            mapManagerMoq.Setup(x => x.Test(It.IsAny<double>(), It.IsAny<double>())).Returns(new RouteStat(distanceTest, durationTest));
            mapManagerMoq.Setup(x => x.Route(_places, null)).Returns(new RouteStat(distanceTest, durationTest));
            mapManagerMoq.Setup(x => x.RouteDetail(_places, It.IsAny<LinkedListNode<IRouteService>>())).Returns(new RouteMap[] { new RouteMap() });

            return mapManagerMoq;
        }


        public RouteControllerTest()
        {
            _mapManagerMoq = CreateMapManagerMoq();
            _routeController = new RouteController(_mapManagerMoq.Object, It.IsAny<IGeoLocationService>());
        }

        [Test]
        public void RouteTest()
        {
            var result = _routeController.GetRoute(_places);

            _mapManagerMoq.Verify(x => x.Route(_places, It.IsAny<LinkedListNode<IRouteService>>()));
        }

        [Test]
        public void RouteDetailTest()
        {
            var result = _routeController.GetRouteDetails(_places);

            _mapManagerMoq.Verify(x => x.RouteDetail(_places, It.IsAny<LinkedListNode<IRouteService>>()));
        }

    }
}
