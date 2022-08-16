using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Tests.Fakes.Minio;
using AuditService.Tests.Fakes.ServiceData;
using KIT.Minio.Commands.SaveFileWithSharing;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Export.Enumerations;

namespace AuditService.Tests.Helpers.ExportLogTest
{
    /// <summary>
    ///     Helper for ExportLog tests
    /// </summary>
    /// <typeparam name="TFilter">Filter model type</typeparam>
    /// <typeparam name="TSort">Sort model type</typeparam>
    /// <typeparam name="TDomainModel">Domain model type</typeparam>
    internal static class ExportLogTestHelper<TFilter, TSort, TDomainModel>
        where TFilter : class, ILogFilter, new()
        where TSort : class, ISort, new()
        where TDomainModel : class
    {
        /// <summary>
        ///    Check if the result is coming from export log handler
        /// </summary>
        /// <param name="elkIndex">Elc index</param>
        /// <param name="testDataJsonModel">Test data json model</param>
        internal static async Task CheckReturnResult(string elkIndex,
            byte[] testDataJsonModel)
        {
            //Arrange
            var serviceProvider = ServiceProviderFake
                .GetServiceProviderForExportLogHandlers<TDomainModel>(testDataJsonModel, elkIndex);

            var mediatorService = serviceProvider.GetRequiredService<IMediator>();

            var filter = new ExportLogFilterRequestDto<TFilter, TSort>()
            {
                FileType = ExportType.Csv,
                Filter = new()
                {
                    TimestampFrom = DateTime.Now,
                    TimestampTo = DateTime.Now
                }
            };

            //Act 
            var result = await mediatorService.Send(filter, new TaskCanceledException().CancellationToken);

            //Assert
            True(result.FileLink == string.Empty);
        }

        /// <summary>
        ///     Check if the document created
        /// </summary>
        /// <param name="elkIndex">Elc index</param>
        /// <param name="testDataJsonModel">Test data json model</param>
        internal static async Task CheckCreateDocument(string elkIndex,
            byte[] testDataJsonModel)
        {
            //Arrange
            var serviceProvider = ServiceProviderFake
                .GetServiceProviderForExportLogHandlers<TDomainModel>(testDataJsonModel, elkIndex);

            var mediatorService = serviceProvider.GetRequiredService<IMediator>();

            var filter = new ExportLogFilterRequestDto<TFilter, TSort>()
            {
                FileType = ExportType.Csv,
                Filter = new()
                {
                    TimestampFrom = DateTime.Now,
                    TimestampTo = DateTime.Now
                }
            };

            //Act 
            _ = await mediatorService.Send(filter, new TaskCanceledException().CancellationToken);

            var saveFileWithSharingCommand = (SaveFileWithSharingCommandFake)serviceProvider.GetRequiredService<ISaveFileWithSharingCommand>();

            var fileStream = saveFileWithSharingCommand.FileStreamDocument;

            //Assert
            NotNull(fileStream);
        }

        /// <summary>
        ///     Check if the document extantion is expected
        /// </summary>
        /// <param name="elkIndex">Elc index</param>
        /// <param name="testDataJsonModel">Test data json model</param>
        /// <param name="expectedExtension">Expected extension</param>
        /// <param name="exportType">Export type</param>
        internal static async Task CheckDocumentExtension(string elkIndex,
            byte[] testDataJsonModel,
            string expectedExtension,
            ExportType exportType)
        {
            //Arrange
            var serviceProvider = ServiceProviderFake
                .GetServiceProviderForExportLogHandlers<TDomainModel>(testDataJsonModel, elkIndex);

            var filter = new ExportLogFilterRequestDto<TFilter, TSort>()
            {
                FileType = exportType,
                Filter = new()
                {
                    TimestampFrom = DateTime.Now,
                    TimestampTo = DateTime.Now
                }
            };

            //Act 
            var actualExtension = await GetExtensionOfGeneratedDocument(serviceProvider, filter, new TaskCanceledException().CancellationToken);

            //Assert
            Equal(expectedExtension, actualExtension);
        }

        /// <summary>
        ///     Check if the exception is coming from export log domain handler
        /// </summary>
        /// <param name="elkIndex">Elc index</param>
        /// <param name="testDataJsonModel">Test data json model</param>>
        internal static async Task CheckCreateDocumentThrow(string elkIndex,
            byte[] testDataJsonModel)
        {
            //Arrange
            var serviceProvider = ServiceProviderFake
               .GetServiceProviderForExportLogHandlers<TDomainModel>(testDataJsonModel, elkIndex);

            var mediatorService = serviceProvider.GetRequiredService<IMediator>();

            var filter = new ExportLogFilterRequestDto<TFilter, TSort>()
            {
                FileType = ExportType.None,
                Filter = new()
                {
                    TimestampFrom = DateTime.Now,
                    TimestampTo = DateTime.Now
                }
            };

            //Assert
            Throws<AggregateException>(() =>
                //Act 
                mediatorService.Send(filter, new TaskCanceledException().CancellationToken).Result);
        }

        /// <summary>
        ///     Get extension of generated document
        /// </summary>
        /// <param name="serviceProvider">Curent service provider</param>
        /// <param name="filter">Filter for ExportLogRequestHandler</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Extension</returns>
        private static async Task<string> GetExtensionOfGeneratedDocument(IServiceProvider serviceProvider,
            ExportLogFilterRequestDto<TFilter, TSort> filter,
            CancellationToken cancellationToken)
        {
            var mediatorService = serviceProvider.GetRequiredService<IMediator>();

            _ = await mediatorService.Send(filter, new TaskCanceledException().CancellationToken);

            var saveFileWithSharingCommand = (SaveFileWithSharingCommandFake)serviceProvider.GetRequiredService<ISaveFileWithSharingCommand>();

            var fileStream = saveFileWithSharingCommand.FileStreamDocument;

            var extension = Path.GetExtension(fileStream.FileName);

            return extension;
        }
    }
}
