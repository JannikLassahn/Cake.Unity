using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;

namespace Cake.Unity.Actions
{
    public class UnityTestAction : UnityAction
    {
        private const string CategoriesWarning = "Category filters are currently not supported by Unity.";
        private const string TestNamesWarning = "Test name filters are currently not supported by Unity.";

        private readonly UnityTestConfiguration _configuration;

        public UnityTestAction()
        {
            _configuration = new UnityTestConfiguration();
        }

        public UnityTestAction(UnityTestConfiguration configuration)
        {
            _configuration = configuration ?? new UnityTestConfiguration();
        }

        public override void BuildArguments(ICakeContext context, ProcessArgumentBuilder arguments)
        {
            base.BuildArguments(context, arguments);

            arguments.Append("-runTests");

            arguments.Append("-testPlatform");
            arguments.Append(_configuration.TestPlatform.ToString().ToLowerInvariant());

            if (_configuration.TestResultsFile != null)
            {
                arguments.Append("-testResults");
                arguments.AppendQuoted(_configuration.TestResultsFile.MakeAbsolute(context.Environment).FullPath);
            }

            if(_configuration.BuildTarget != UnityBuildTarget.None)
            {
                arguments.Append("-buildTarget {0}", _configuration.BuildTarget.ToString());
            }

            if(_configuration.Categories != null)
            {
                context.Log.Write(Verbosity.Normal, LogLevel.Warning, CategoriesWarning);

                arguments.Append("-testsCategories");
                arguments.AppendQuoted(string.Join(",", _configuration.Categories));
            }

            if (_configuration.Tests != null)
            {
                context.Log.Write(Verbosity.Normal, LogLevel.Warning, TestNamesWarning);

                arguments.Append("-testsFilter");
                arguments.AppendQuoted(string.Join(",", _configuration.Tests));
            }
        }
    }
}
