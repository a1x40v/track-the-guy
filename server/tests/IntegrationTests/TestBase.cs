using System.Threading.Tasks;
using NUnit.Framework;

namespace IntegrationTests
{
    using static Testing;

    public class TestBase
    {
        [SetUp]
        public async Task SetUp()
        {
            await ResetState();
        }
    }
}