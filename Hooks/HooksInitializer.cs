using TechTalk.SpecFlow;
using Xunit;

[assembly: CollectionBehavior(MaxParallelThreads = 2)]

namespace PlaywrightTests.Hooks
{
    [Binding]
    internal class HooksInitializer
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            // do all what you need before test start
        }

        [AfterScenario]
        public void AfterScenario()
        {
            // call after test will finished
        }
    }
}
