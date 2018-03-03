using Cake.Core.IO;
using Cake.Unity.Actions;
using Cake.Unity.Tests.Fixtures;
using Xunit;

namespace Cake.Unity.Tests.Unit.Actions
{
    public sealed class UnityTestActionTests
    {
        [Fact]
        public void Should_Pass_Test()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityTestAction();

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-runTests -testPlatform editmode", builder.Render());
        }

        [Fact]
        public void Should_Use_PlayMode_TestPlatform()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityTestAction(new UnityTestConfiguration { TestPlatform = UnityTestPlatform.PlayMode });

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-runTests -testPlatform playmode", builder.Render());
        }

        [Fact]
        public void Should_Use_TestResultsFile()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityTestAction(new UnityTestConfiguration { TestResultsFile = "./result.xml" });

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-runTests -testPlatform editmode -testResults \"/Working/result.xml\"", builder.Render());
        }

        [Fact]
        public void Should_Use_Android_BuildTarget()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityTestAction(new UnityTestConfiguration { BuildTarget = UnityBuildTarget.Android });

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-runTests -testPlatform editmode -buildTarget Android", builder.Render());
        }

        [Fact]
        public void Should_Use_Categories()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityTestAction(new UnityTestConfiguration { Categories = new string[] { "Special", "Other"} });

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-runTests -testPlatform editmode -testsCategories \"Special,Other\"", builder.Render());
        }

        [Fact]
        public void Should_Use_Tests()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityTestAction(new UnityTestConfiguration { Tests = new string[] { "MyTest", "MyOtherTest" } });

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-runTests -testPlatform editmode -testsFilter \"MyTest,MyOtherTest\"", builder.Render());
        }
    }
}
