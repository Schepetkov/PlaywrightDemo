using NUnit.Framework;
using TechTalk.SpecFlow;

[assembly: Parallelizable(ParallelScope.Fixtures)]

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
