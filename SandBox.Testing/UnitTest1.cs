using Moq;
using System;
using Xunit;
using SandBox.DataAccess.Repositories.Weather;
using SandBox.DTOs.DTOs.Weather;
using System.Collections.Generic;
using System.Threading.Tasks;
using SandBox.DataAccess.Repositories.Weather.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SandBox.Areas.ML.Controllers;

namespace SandBox.Testing
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var moqIWeatherRepository = new Mock<WeatherRepository>();
            moqIWeatherRepository
                                .Setup(x => x.GetTemperature())
                                .Returns(new Task<List<TemperaturesDTO>>(() =>
                                                                                {
                                                                                    return new List<TemperaturesDTO>();
                                                                                }));

            var serviceMock = new Mock<IWeatherRepository>();
            serviceMock
                              .Setup(x => x.GetTemperature())
                              .Returns(new Task<List<TemperaturesDTO>>(() =>
                              {
                                  return new List<TemperaturesDTO>();
                              })); ;
            var serviceDescriptor = new ServiceDescriptor(typeof(IWeatherRepository), serviceMock.Object);

            using (var testServer = new ConfigurableServer(sc => sc.Replace(serviceDescriptor)))
            {
                var client = testServer.CreateClient();
                var value = await client.GetStringAsync(Constantes.Areas.ML + "/ML/" + nameof(SandBox.Areas.ML.Controllers.MLController.TrainModel));
                Assert.Equal("Hello mockworld", value);
            }
        }
    }
}
