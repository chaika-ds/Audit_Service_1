using AuditService.Common.Models.Domain.LossesLog;
using AuditService.Tests.Fakes.ServiceData;
using AuditService.Tests.Resources;
using KIT.Minio.Commands.SaveFileWithSharing;
using KIT.Minio.Commands.SaveFileWithSharing.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;
using Tolar.Export.Enumerations;
using Tolar.Export.Extenisons;
using Tolar.Export.Services;

namespace AuditService.Tests.Tests.Minio
{
    public class SaveFileWithSharingCommandTest
    {
        /// <summary>
        ///     Test file name
        /// </summary>
        private const string fileName = "test";

        /// <summary>
        ///     Test export type
        /// </summary>
        private const ExportType exportType = ExportType.Excel;

        /// <summary>
        ///     Check if the SaveFileWithSharingCommand returned throw exception
        /// </summary>
        [Fact]
        public async Task SaveFileWithSharingCommandThrowExceptionReturning_CreateStreamWithDocuments_ReturnThrowException()
        {
            //Arrange
            var serviceProvider = ServiceProviderFake.GetServiceProviderForMinio();

            var minioCommand = serviceProvider.GetRequiredService<ISaveFileWithSharingCommand>();

            var stream = GetRequestStream();
            
            var request = new SaveFileWithSharingModel(stream, fileName, exportType.GetExportContentType());

            //Assert
            Throws<AggregateException>(() =>
                //Act 
                minioCommand.ExecuteAsync(request, new TaskCanceledException().CancellationToken).Result);
        }

        /// <summary>
        ///     Get stream for SaveFileWithSharingCommand request
        /// </summary>
        /// <returns>Stream</returns>
        private Stream GetRequestStream()
        {
            var serviceProvider = ServiceProviderFake
                .GetServiceProviderForLogHandlers<LossesLogDomainModel>(TestResources.ElasticSearchLossesLogResponse, TestResources.DefaultIndex);

            var exportFactory = serviceProvider.GetRequiredService<IExportFactory>();

            var logEntries = JsonConvert.DeserializeObject<List<LossesLogDomainModel>>(Encoding.Default.GetString(TestResources.ElasticSearchLossesLogResponse));

            var stream = exportFactory.GetExporter(exportType).Export(logEntries, null);

            return stream;
        }
    }
}
