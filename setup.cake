var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var solution = "./src/Cake.Unity.sln";
var project = "./src/Cake.Unity/Cake.Unity.csproj";
var test = "./src/Cake.Unity.Tests/Cake.Unity.Tests.csproj";

Task("Clean")
    .Does(() =>
    {
        DotNetCoreClean(solution);
        DeleteFiles(GetFiles("./artifacts/*.nupkg"));
    });

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        DotNetCoreRestore(solution);
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        DotNetCoreBuild(solution, new DotNetCoreBuildSettings { Configuration = configuration });
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() => 
    {
        DotNetCoreTest(test, new DotNetCoreTestSettings()
        {
            Configuration = configuration,
            NoBuild = true
        });
    });

Task("Package")
    .IsDependentOn("Test")
    .Does(() =>
    {
        DotNetCorePack(project, new DotNetCorePackSettings 
        {
            Configuration = configuration,
            OutputDirectory = "./artifacts/"
        });
    });

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);