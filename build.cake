#addin "nuget:?package=Cake.Coveralls&version=1.1.0"
#addin "nuget:?package=Cake.Coverlet&version=2.5.4"
#addin "nuget:?package=Cake.Git&version=1.1.0"
#addin "nuget:?package=Cake.Sonar&version=1.1.26"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var configuration = Argument("configuration", "Debug");
var revision = EnvironmentVariable("BUILD_NUMBER") ?? Argument("revision", "9999");
var target = Argument("target", "Default");
var buildWithUnitTesting = EnvironmentVariable("BUILD_WITH_UNITTESTING") ?? "ON";


//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define git commit id
var commitId = "SNAPSHOT";

// Define product name and version
var product = "Htc.Vita.Core";
var companyName = "HTC";
var version = "0.10.6";
var semanticVersion = $"{version}.{revision}";
var ciVersion = $"{version}.0";
var buildVersion = "Release".Equals(configuration) ? semanticVersion : $"{ciVersion}-CI{revision}";

// Define copyright
var copyright = $"Copyright © 2017 - {DateTime.Now.Year}";

// Define timestamp for signing
var lastSignTimestamp = DateTime.Now;
var signIntervalInMilli = 1000 * 5;

// Define path
var solutionFile = File($"./source/{product}.sln");

// Define directories.
var distDir = Directory("./dist");
var tempDir = Directory("./temp");
var generatedDir = Directory("./source/generated");
var packagesDir = Directory("./source/packages");
var nugetDir = distDir + Directory(configuration) + Directory("nuget");
var homeDir = Directory(EnvironmentVariable("USERPROFILE") ?? EnvironmentVariable("HOME"));
var testDataDir = homeDir + Directory(".htc_test");
var reportDotCoverDirAnyCPU = distDir + Directory(configuration) + Directory("report/dotCover/AnyCPU");
var reportDotCoverDirX86 = distDir + Directory(configuration) + Directory("report/dotCover/x86");
var reportOpenCoverDirAnyCPU = distDir + Directory(configuration) + Directory("report/OpenCover/AnyCPU");
var reportOpenCoverDirX86 = distDir + Directory(configuration) + Directory("report/OpenCover/x86");
var reportXUnitDirAnyCPU = distDir + Directory(configuration) + Directory("report/xUnit/AnyCPU");
var reportXUnitDirX86 = distDir + Directory(configuration) + Directory("report/xUnit/x86");

// Define signing key, password and timestamp server
var signKeyEnc = EnvironmentVariable("SIGNKEYENC") ?? "NOTSET";
var signPass = EnvironmentVariable("SIGNPASS") ?? "NOTSET";
var signSha1Uri = new Uri("http://timestamp.digicert.com");
var signSha256Uri = new Uri("http://timestamp.digicert.com");

// Define coveralls update key
var coverallsApiKey = EnvironmentVariable("COVERALLS_APIKEY") ?? "NOTSET";

// Define nuget push source and key
var nugetApiKey = EnvironmentVariable("NUGET_PUSH_TOKEN") ?? EnvironmentVariable("NUGET_APIKEY") ?? "NOTSET";
var nugetSource = EnvironmentVariable("NUGET_PUSH_PATH") ?? EnvironmentVariable("NUGET_SOURCE") ?? "NOTSET";

// Define sonarcloud key
var sonarcloudApiKey = EnvironmentVariable("SONARCLOUD_APIKEY") ?? "NOTSET";
var sonarcloudProjectKey = EnvironmentVariable("SONARCLOUD_PROJECTKEY") ?? "NOTSET";
var sonarcloudProjectOrg = EnvironmentVariable("SONARCLOUD_PROJECTORG") ?? "NOTSET";
var sonarcloudUrl = "https://sonarcloud.io";


//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Fetch-Git-Commit-ID")
    .ContinueOnError()
    .Does(() =>
{
    var lastCommit = GitLogTip(MakeAbsolute(Directory(".")));
    commitId = lastCommit.Sha;
});

Task("Display-Config")
    .IsDependentOn("Fetch-Git-Commit-ID")
    .Does(() =>
{
    Information($"Build target:        {target}");
    Information($"Build configuration: {configuration}");
    Information($"Build commitId:      {commitId}");
    Information($"Build version:       {buildVersion}");
});

Task("Clean-Workspace")
    .IsDependentOn("Display-Config")
    .Does(() =>
{
    CleanDirectory(distDir);
    CleanDirectory(tempDir);
    CleanDirectory(generatedDir);
    CleanDirectory(packagesDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean-Workspace")
    .Does(() =>
{
    NuGetRestore(new FilePath($"./source/{product}.sln"));
});

Task("Generate-AssemblyInfo")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    CreateDirectory(generatedDir);
    var assemblyVersion = "Release".Equals(configuration) ? semanticVersion : ciVersion;
    CreateAssemblyInfo(
            new FilePath("./source/generated/SharedAssemblyInfo.cs"),
            new AssemblyInfoSettings
            {
                    Company = companyName,
                    Copyright = copyright,
                    FileVersion = assemblyVersion,
                    InformationalVersion = assemblyVersion,
                    Product = $"{product} : {commitId}",
                    Version = version
            }
    );
});

Task("Run-Sonar-Begin")
    .WithCriteria(() => !"NOTSET".Equals(sonarcloudApiKey) && !"NOTSET".Equals(sonarcloudProjectKey) && !"NOTSET".Equals(sonarcloudProjectOrg))
    .IsDependentOn("Generate-AssemblyInfo")
    .Does(() =>
{
    SonarBegin(
            new SonarBeginSettings
            {
                    Key = sonarcloudProjectKey,
                    Login = sonarcloudApiKey,
                    OpenCoverReportsPath = "**/*.OpenCover.xml",
                    Organization = sonarcloudProjectOrg,
                    Url = sonarcloudUrl
            }
    );
});

Task("Build-Assemblies")
    .IsDependentOn("Run-Sonar-Begin")
    .Does(() =>
{
    DotNetCoreBuild(
            "./source/",
            new DotNetCoreBuildSettings
            {
                    Configuration = configuration
            }
    );
});

Task("Prepare-Unit-Test-Data")
    .WithCriteria(() => "ON".Equals(buildWithUnitTesting))
    .IsDependentOn("Build-Assemblies")
    .Does(() =>
{
    if (!DirectoryExists(testDataDir))
    {
        CreateDirectory(testDataDir);
    }
    if (!FileExists(testDataDir + File("TestData.Md5.txt")))
    {
        CopyFileToDirectory(
                $"source/{product}.Tests/TestData.Md5.txt",
                testDataDir
        );
    }
    if (!FileExists(testDataDir + File("TestData.Sha1.txt")))
    {
        CopyFileToDirectory(
                $"source/{product}.Tests/TestData.Sha1.txt",
                testDataDir
        );
    }
});

Task("Run-Unit-Tests-Under-AnyCPU-1")
    .WithCriteria(() => "ON".Equals(buildWithUnitTesting))
    .IsDependentOn("Prepare-Unit-Test-Data")
    .Does(() =>
{
    CreateDirectory(reportXUnitDirAnyCPU);
    var testFilePattern = $"./temp/{configuration}/{product}.Tests/bin/AnyCPU/net452/*.Tests.dll";
    var xUnit2Settings = new XUnit2Settings
    {
            HtmlReport = true,
            NUnitReport = true,
            OutputDirectory = reportXUnitDirAnyCPU,
            Parallelism = ParallelismOption.None
    };

    if(IsRunningOnWindows())
    {
        DotCoverAnalyse(
                tool =>
                {
                        tool.XUnit2(
                                testFilePattern,
                                xUnit2Settings
                        );
                },
                new FilePath($"{reportDotCoverDirAnyCPU.ToString()}/{product}.html"),
                new DotCoverAnalyseSettings
                {
                        ReportType = DotCoverReportType.HTML
                }.WithFilter("+:*")
                .WithFilter("-:xunit.*")
                .WithFilter("-:*.NunitTest")
                .WithFilter("-:*.Tests")
                .WithFilter("-:*.XunitTest")
        );
    }
    else
    {
        XUnit2(
                testFilePattern,
                xUnit2Settings
        );
    }
});

Task("Run-Unit-Tests-Under-AnyCPU-2")
    .WithCriteria(() => "ON".Equals(buildWithUnitTesting))
    .IsDependentOn("Run-Unit-Tests-Under-AnyCPU-1")
    .Does(() =>
{
    CreateDirectory(reportOpenCoverDirAnyCPU);
    DotNetCoreTest(
            $"./source/{product}.Tests/{product}.Tests.AnyCPU.csproj",
            new DotNetCoreTestSettings
            {
            },
            new CoverletSettings
            {
                    CollectCoverage = true,
                    CoverletOutputDirectory = reportOpenCoverDirAnyCPU,
                    CoverletOutputFormat = CoverletOutputFormat.opencover,
                    CoverletOutputName = $"{product}.OpenCover.xml"
            }
    );
});

Task("Run-Unit-Tests-Under-X86")
    .WithCriteria(() => "ON".Equals(buildWithUnitTesting))
    .IsDependentOn("Run-Unit-Tests-Under-AnyCPU-2")
    .Does(() =>
{
    CreateDirectory(reportXUnitDirX86);
    var testFilePattern = $"./temp/{configuration}/{product}.Tests/bin/x86/net452/*.Tests.dll";
    var xUnit2Settings = new XUnit2Settings
    {
            HtmlReport = true,
            NUnitReport = true,
            OutputDirectory = reportXUnitDirX86,
            Parallelism = ParallelismOption.None,
            UseX86 = true
    };

    if(IsRunningOnWindows())
    {
        DotCoverAnalyse(
                tool =>
                {
                        tool.XUnit2(
                                testFilePattern,
                                xUnit2Settings
                        );
                },
                new FilePath($"{reportDotCoverDirX86.ToString()}/{product}.html"),
                new DotCoverAnalyseSettings
                {
                        ReportType = DotCoverReportType.HTML
                }.WithFilter("+:*")
                .WithFilter("-:xunit.*")
                .WithFilter("-:*.NunitTest")
                .WithFilter("-:*.Tests")
                .WithFilter("-:*.XunitTest")
        );
    }
    else
    {
        XUnit2(
                testFilePattern,
                xUnit2Settings
        );
    }
});

Task("Run-Sonar-End")
    .WithCriteria(() => !"NOTSET".Equals(sonarcloudApiKey))
    .IsDependentOn("Run-Unit-Tests-Under-X86")
    .Does(() =>
{
    SonarEnd(
            new SonarEndSettings
            {
                    Login = sonarcloudApiKey
            }
    );
});

Task("Sign-Assemblies")
    .WithCriteria(() => "Release".Equals(configuration) && !"NOTSET".Equals(signPass) && !"NOTSET".Equals(signKeyEnc))
    .IsDependentOn("Run-Sonar-End")
    .Does(() =>
{
    var signKey = "./temp/key.pfx";
    System.IO.File.WriteAllBytes(
            signKey,
            Convert.FromBase64String(signKeyEnc)
    );

    var targetPlatforms = new[]
    {
            "net45",
            "net5.0",
            "netstandard2.0"
    };
    foreach (var targetPlatform in targetPlatforms)
    {
        System.Threading.Thread.Sleep(signIntervalInMilli);
        Sign(
                $"./temp/{configuration}/{product}/bin/{targetPlatform}/{product}.dll",
                new SignToolSignSettings
                {
                        AppendSignature = true,
                        CertPath = signKey,
                        DigestAlgorithm = SignToolDigestAlgorithm.Sha256,
                        Password = signPass,
                        TimeStampDigestAlgorithm = SignToolDigestAlgorithm.Sha256,
                        TimeStampUri = signSha256Uri
                }
        );
    }
});

Task("Build-NuGet-Package")
    .IsDependentOn("Sign-Assemblies")
    .Does(() =>
{
    CreateDirectory(nugetDir);
    DotNetCorePack(
            $"./source/{product}/",
            new DotNetCorePackSettings
            {
                    ArgumentCustomization = (args) =>
                    {
                            return args.Append($"/p:Version={buildVersion}");
                    },
                    Configuration = configuration,
                    NoBuild = true,
                    OutputDirectory = nugetDir
            }
    );
});

Task("Update-Coverage-Report")
    .WithCriteria(() => !"NOTSET".Equals(coverallsApiKey))
    .IsDependentOn("Build-NuGet-Package")
    .Does(() =>
{
    CoverallsIo(
            new FilePath($"{reportOpenCoverDirAnyCPU.ToString()}/{product}.OpenCover.xml"),
            new CoverallsIoSettings
            {
                    RepoToken = coverallsApiKey
            }
    );
});

Task("Publish-NuGet-Package")
    .WithCriteria(() => "Release".Equals(configuration) && !"NOTSET".Equals(nugetApiKey) && !"NOTSET".Equals(nugetSource))
    .IsDependentOn("Update-Coverage-Report")
    .Does(() =>
{
    NuGetPush(
            new FilePath($"./dist/{configuration}/nuget/{product}.{buildVersion}.nupkg"),
            new NuGetPushSettings
            {
                    ApiKey = nugetApiKey,
                    Source = nugetSource
            }
    );
});


//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Update-Coverage-Report");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
