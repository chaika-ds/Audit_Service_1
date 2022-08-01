using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Handlers.Consts;
using Nest;

namespace AuditService.Tests.AuditService.Handlers.Fakes
{
    internal class PlayerChangesLogDomainRequestHandlerTestRequestData
    {
        /// <summary>
        /// Input test data for ApplyFilter
        /// </summary>
        //private static IEnumerable<object> ApplyFilterData()
        //{
        //    var queryContainerDescriptor = new QueryContainerDescriptor<PlayerChangesLogDomainModel>();
        //    var filter = new BlockedPlayersLogFilterDto();
        //    var container = new QueryContainer();


        //    filter.BlockingDateFrom = DateTime.Now.AddMonths(-1);
        //    container &= queryContainerDescriptor.DateRange(t => t.Field(w => w.BlockingDate).GreaterThan(filter.BlockingDateFrom.Value));
        //    yield return new object[] { queryContainerDescriptor, filter, container };


        //    filter.BlockingDateTo = DateTime.Now.AddMonths(1);
        //    container &= queryContainerDescriptor.DateRange(t => t.Field(w => w.BlockingDate).LessThan(filter.BlockingDateTo.Value));
        //    yield return new object[] { queryContainerDescriptor, filter, container };


        //    filter.PreviousBlockingDateFrom = DateTime.Now.AddMonths(-2);
        //    container &= queryContainerDescriptor.DateRange(t => t.Field(w => w.PreviousBlockingDate).GreaterThan(filter.PreviousBlockingDateFrom.Value));
        //    yield return new object[] { queryContainerDescriptor, filter, container };


        //    filter.PreviousBlockingDateTo = DateTime.Now.AddMonths(-1);
        //    container &= queryContainerDescriptor.DateRange(t => t.Field(w => w.PreviousBlockingDate).LessThan(filter.PreviousBlockingDateTo.Value));
        //    yield return new object[] { queryContainerDescriptor, filter, container };


        //    filter.PlayerLogin = "player@gmail.com";
        //    container &= queryContainerDescriptor.Match(t => t.Field(x => x.PlayerLogin).Query(filter.PlayerLogin));
        //    yield return new object[] { queryContainerDescriptor, filter, container };


        //    filter.PlayerId = Guid.NewGuid();
        //    container &= queryContainerDescriptor.Term(t => t.PlayerId.Suffix(ElasticConst.SuffixKeyword), filter.PlayerId.Value);
        //    yield return new object[] { queryContainerDescriptor, filter, container };


        //    filter.PlayerIp = "0.0.0.0";
        //    container &= queryContainerDescriptor.Match(t => t.Field(x => x.LastVisitIpAddress).Query(filter.PlayerIp));
        //    yield return new object[] { queryContainerDescriptor, filter, container };


        //    filter.HallId = Guid.NewGuid();
        //    container &= queryContainerDescriptor.Term(t => t.HallId.Suffix(ElasticConst.SuffixKeyword), filter.HallId.Value);
        //    yield return new object[] { queryContainerDescriptor, filter, container };


        //    filter.Platform = "windows";
        //    container &= queryContainerDescriptor.Match(t => t.Field(x => x.Platform).Query(filter.Platform));
        //    yield return new object[] { queryContainerDescriptor, filter, container };


        //    filter.Browser = "chrome";
        //    container &= queryContainerDescriptor.Match(t => t.Field(x => x.Browser).Query(filter.Browser));
        //    yield return new object[] { queryContainerDescriptor, filter, container };


        //    filter.Version = "1";
        //    container &= queryContainerDescriptor.Match(t => t.Field(x => x.BrowserVersion).Query(filter.Version));
        //    yield return new object[] { queryContainerDescriptor, filter, container };

        //    filter.Language = "wn";
        //    container &= queryContainerDescriptor.Match(t => t.Field(x => x.Language).Query(filter.Language));
        //    yield return new object[] { queryContainerDescriptor, filter, container };
        //}



    }
}
