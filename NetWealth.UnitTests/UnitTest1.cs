using System;
using System.Threading;
using Xunit;
using Moq;

namespace NetWealth.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var mockMediatr = new Mock<IMediator>();
           // mockMediatr.Setup(m => m.Send(It.IsAny(), It.IsAny())).Returns(async (GetBlockedCustomersAndGroupsQuery q, CancellationToken token) => await _getBlockedCustomersAndGroupsHandler.Handle(new GetBlockedCustomersAndGroupsQuery { Active = q.Active }, token));
        }
    }
}
