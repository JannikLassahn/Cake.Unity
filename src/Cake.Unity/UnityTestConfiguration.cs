using Cake.Core.IO;
using System.Collections.Generic;

namespace Cake.Unity
{
    public class UnityTestConfiguration
    {
        public FilePath TestResultsFile { get; set; }

        public UnityTestPlatform TestPlatform { get; set; }

        public UnityBuildTarget BuildTarget { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<string> Tests { get; set; }

    }
}
