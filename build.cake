#addin "nuget:?package=Cake.Coveralls&version=0.9.0"
#addin "nuget:?package=Cake.Git&version=0.19.0"
#addin "nuget:?package=Cake.ReSharperReports&version=0.10.0"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var configuration = Argument("configuration", "Debug");
var revision = EnvironmentVariable("BUILD_NUMBER") ?? Argument("revision", "9999");
var target = Argument("target", "Default");


//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define git commit id
var commitId = "SNAPSHOT";

// Define product name and version
var product = "Htc.Vita.Core";
var companyName = "HTC";
var version = "0.9.10";
var semanticVersion = string.Format("{0}.{1}", version, revision);
var ciVersion = string.Format("{0}.{1}", version, "0");
var nugetTags = new [] {"htc", "vita", "core"};
var projectUrl = "https://github.com/ViveportSoftware/vita_core_csharp/";
var description = "HTC Vita Core module";

// Define copyright
var copyright = string.Format("Copyright © 2017 - {0}", DateTime.Now.Year);

// Define timestamp for signing
var lastSignTimestamp = DateTime.Now;
var signIntervalInMilli = 1000 * 5;

// Define path
var solutionFile = File(string.Format("./source/{0}.sln", product));

// Define directories.
var distDir = Directory("./dist");
var tempDir = Directory("./temp");
var generatedDir = Directory("./source/generated");
var packagesDir = Directory("./source/packages");
var nugetDir = distDir + Directory(configuration) + Directory("nuget");
var homeDir = Directory(EnvironmentVariable("USERPROFILE") ?? EnvironmentVariable("HOME"));
var reportDotCoverDirAnyCPU = distDir + Directory(configuration) + Directory("report/dotCover/AnyCPU");
var reportDotCoverDirX86 = distDir + Directory(configuration) + Directory("report/dotCover/x86");
var reportOpenCoverDirAnyCPU = distDir + Directory(configuration) + Directory("report/OpenCover/AnyCPU");
var reportOpenCoverDirX86 = distDir + Directory(configuration) + Directory("report/OpenCover/x86");
var reportXUnitDirAnyCPU = distDir + Directory(configuration) + Directory("report/xUnit/AnyCPU");
var reportXUnitDirX86 = distDir + Directory(configuration) + Directory("report/xUnit/x86");
var reportReSharperDupFinder = distDir + Directory(configuration) + Directory("report/ReSharper/DupFinder");
var reportReSharperInspectCode = distDir + Directory(configuration) + Directory("report/ReSharper/InspectCode");

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
    Information("Build target: {0}", target);
    Information("Build configuration: {0}", configuration);
    Information("Build commitId: {0}", commitId);
    if ("Release".Equals(configuration))
    {
        Information("Build version: {0}", semanticVersion);
    }
    else
    {
        Information("Build version: {0}-CI{1}", ciVersion, revision);
    }
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
    NuGetRestore(string.Format("./source/{0}.sln", product));
});

Task("Generate-AssemblyInfo")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    CreateDirectory(generatedDir);
    var file = "./source/Generated/SharedAssemblyInfo.cs";
    var assemblyVersion = semanticVersion;
    if (!"Release".Equals(configuration))
    {
        assemblyVersion = ciVersion;
    }
    CreateAssemblyInfo(
            file,
            new AssemblyInfoSettings
            {
                    Company = companyName,
                    Copyright = copyright,
                    Product = string.Format("{0} : {1}", product, commitId),
                    Version = version,
                    FileVersion = assemblyVersion,
                    InformationalVersion = assemblyVersion
            }
    );
});

Task("Build-Assemblies")
    .IsDependentOn("Generate-AssemblyInfo")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
        // Use MSBuild
        MSBuild(
                solutionFile,
                settings => settings.SetConfiguration(configuration)
        );
    }
    else
    {
        // Use XBuild
        XBuild(
                solutionFile,
                settings => settings.SetConfiguration(configuration)
        );
    }
});

Task("Prepare-Unit-Test-Data")
    .IsDependentOn("Build-Assemblies")
    .Does(() =>
{
    if (!FileExists(homeDir + File("TestData.Md5.txt")))
    {
        CopyFileToDirectory("source/" + product + ".Tests/TestData.Md5.txt", homeDir);
    }
    if (!FileExists(homeDir + File("TestData.Sha1.txt")))
    {
        CopyFileToDirectory("source/" + product + ".Tests/TestData.Sha1.txt", homeDir);
    }
});

Task("Run-Unit-Tests-Under-AnyCPU")
    .IsDependentOn("Prepare-Unit-Test-Data")
    .Does(() =>
{
    CreateDirectory(reportXUnitDirAnyCPU);
    if(IsRunningOnWindows())
    {
        DotCoverAnalyse(
                tool =>
                {
                        tool.XUnit2(
                                "./temp/" + configuration + "/" + product + ".Tests/bin/AnyCPU/*.Tests.dll",
                                new XUnit2Settings
                                {
                                        Parallelism = ParallelismOption.All,
                                        HtmlReport = true,
                                        NUnitReport = true,
                                        OutputDirectory = reportXUnitDirAnyCPU
                                }
                        );
                },
                new FilePath(reportDotCoverDirAnyCPU.ToString() + "/" + product + ".html"),
                new DotCoverAnalyseSettings
                {
                        ReportType = DotCoverReportType.HTML
                }
        );
        CreateDirectory(reportOpenCoverDirAnyCPU);
        var openCoverSettings = new OpenCoverSettings
        {
                MergeByHash = true,
                NoDefaultFilters = true,
                Register = "user",
                SkipAutoProps = true
        }.WithFilter("+[*]*")
        .WithFilter("-[xunit.*]*")
        .WithFilter("-[*.NunitTest]*")
        .WithFilter("-[*.Tests]*")
        .WithFilter("-[*.XunitTest]*");
        OpenCover(
                tool =>
                {
                        tool.XUnit2(
                                "./temp/" + configuration + "/" + product + ".Tests/bin/AnyCPU/*.Tests.dll",
                                new XUnit2Settings
                                {
                                        ShadowCopy = false,
                                        Parallelism = ParallelismOption.All,
                                        OutputDirectory = reportXUnitDirAnyCPU
                                }
                        );
                },
                new FilePath(reportOpenCoverDirAnyCPU.ToString() + "/" + product + ".xml"),
                openCoverSettings
        );
    }
    else
    {
        XUnit2(
                "./temp/" + configuration + "/" + product + ".Tests/bin/AnyCPU/*.Tests.dll",
                new XUnit2Settings
                {
                        Parallelism = ParallelismOption.All,
                        HtmlReport = true,
                        NUnitReport = true,
                        OutputDirectory = reportXUnitDirAnyCPU
                }
        );
    }
});

Task("Run-Unit-Tests-Under-X86")
    .IsDependentOn("Run-Unit-Tests-Under-AnyCPU")
    .Does(() =>
{
    CreateDirectory(reportXUnitDirX86);
    if(IsRunningOnWindows())
    {
        DotCoverAnalyse(
                tool =>
                {
                        tool.XUnit2(
                                "./temp/" + configuration + "/" + product + ".Tests/bin/x86/*.Tests.dll",
                                new XUnit2Settings
                                {
                                        Parallelism = ParallelismOption.All,
                                        HtmlReport = true,
                                        NUnitReport = true,
                                        UseX86 = true,
                                        OutputDirectory = reportXUnitDirX86
                                }
                        );
                },
                new FilePath(reportDotCoverDirX86.ToString() + "/" + product + ".html"),
                new DotCoverAnalyseSettings
                {
                        ReportType = DotCoverReportType.HTML
                }
        );
    }
    else
    {
        XUnit2(
                "./temp/" + configuration + "/" + product + ".Tests/bin/x86/*.Tests.dll",
                new XUnit2Settings
                {
                        Parallelism = ParallelismOption.All,
                        HtmlReport = true,
                        NUnitReport = true,
                        UseX86 = true,
                        OutputDirectory = reportXUnitDirX86
                }
        );
    }
});

Task("Run-DupFinder")
    .IsDependentOn("Run-Unit-Tests-Under-X86")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
        DupFinder(
                string.Format("./source/{0}.sln", product),
                new DupFinderSettings()
                {
                        ShowStats = true,
                        ShowText = true,
                        OutputFile = new FilePath(reportReSharperDupFinder.ToString() + "/" + product + ".xml"),
                        ThrowExceptionOnFindingDuplicates = false
                }
        );
        ReSharperReports(
                new FilePath(reportReSharperDupFinder.ToString() + "/" + product + ".xml"),
                new FilePath(reportReSharperDupFinder.ToString() + "/" + product + ".html")
        );
    }
});

Task("Run-InspectCode")
    .IsDependentOn("Run-DupFinder")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
        InspectCode(
                string.Format("./source/{0}.sln", product),
                new InspectCodeSettings() {
                        SolutionWideAnalysis = true,
                        OutputFile = new FilePath(reportReSharperInspectCode.ToString() + "/" + product + ".xml"),
                        ThrowExceptionOnFindingViolations = false
                }
        );
        ReSharperReports(
                new FilePath(reportReSharperInspectCode.ToString() + "/" + product + ".xml"),
                new FilePath(reportReSharperInspectCode.ToString() + "/" + product + ".html")
        );
    }
});

Task("Sign-Assemblies")
    .WithCriteria(() => "Release".Equals(configuration) && !"NOTSET".Equals(signPass) && !"NOTSET".Equals(signKeyEnc))
    .IsDependentOn("Run-InspectCode")
    .Does(() =>
{
    var currentSignTimestamp = DateTime.Now;
    Information("Last timestamp:    " + lastSignTimestamp);
    Information("Current timestamp: " + currentSignTimestamp);
    var totalTimeInMilli = (DateTime.Now - lastSignTimestamp).TotalMilliseconds;

    var signKey = "./temp/key.pfx";
    System.IO.File.WriteAllBytes(signKey, Convert.FromBase64String(signKeyEnc));

    var file = string.Format("./temp/{0}/{1}/bin/net45/{1}.dll", configuration, product);

    if (totalTimeInMilli < signIntervalInMilli)
    {
        System.Threading.Thread.Sleep(signIntervalInMilli - (int)totalTimeInMilli);
    }
    Sign(
            file,
            new SignToolSignSettings
            {
                    TimeStampUri = signSha1Uri,
                    CertPath = signKey,
                    Password = signPass
            }
    );
    lastSignTimestamp = DateTime.Now;

    System.Threading.Thread.Sleep(signIntervalInMilli);
    Sign(
            file,
            new SignToolSignSettings
            {
                    AppendSignature = true,
                    TimeStampUri = signSha256Uri,
                    DigestAlgorithm = SignToolDigestAlgorithm.Sha256,
                    TimeStampDigestAlgorithm = SignToolDigestAlgorithm.Sha256,
                    CertPath = signKey,
                    Password = signPass
            }
    );
    lastSignTimestamp = DateTime.Now;
});

Task("Build-NuGet-Package")
    .IsDependentOn("Sign-Assemblies")
    .Does(() =>
{
    CreateDirectory(nugetDir);
    var nugetPackVersion = semanticVersion;
    if (!"Release".Equals(configuration))
    {
        nugetPackVersion = string.Format("{0}-CI{1}", ciVersion, revision);
    }
    Information("Pack version: {0}", nugetPackVersion);
    var nuGetPackSettings = new NuGetPackSettings
    {
            Id = product,
            Version = nugetPackVersion,
            Authors = new[] {"HTC"},
            Description = description + " [CommitId: " + commitId + "]",
            Copyright = copyright,
            ProjectUrl = new Uri(projectUrl),
            Tags = nugetTags,
            RequireLicenseAcceptance= false,
            Files = new []
            {
                    new NuSpecContent
                    {
                            Source = string.Format("{0}/bin/net45/{0}.dll", product),
                            Target = "lib\\net45"
                    },
                    new NuSpecContent
                    {
                            Source = string.Format("{0}/bin/net45/{0}.pdb", product),
                            Target = "lib\\net45"
                    },
            },
            Properties = new Dictionary<string, string>
            {
                    {"Configuration", configuration}
            },
            BasePath = tempDir + Directory(configuration),
            OutputDirectory = nugetDir
    };

    NuGetPack(nuGetPackSettings);
});

Task("Update-Coverage-Report")
    .WithCriteria(() => !"NOTSET".Equals(coverallsApiKey))
    .IsDependentOn("Build-NuGet-Package")
    .Does(() =>
{
    CoverallsIo(
            reportOpenCoverDirAnyCPU.ToString() + "/" + product + ".xml",
            new CoverallsIoSettings()
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
    var nugetPushVersion = semanticVersion;
    if (!"Release".Equals(configuration))
    {
        nugetPushVersion = string.Format("{0}-CI{1}", ciVersion, revision);
    }
    Information("Publish version: {0}", nugetPushVersion);
    var package = string.Format("./dist/{0}/nuget/{1}.{2}.nupkg", configuration, product, nugetPushVersion);
    NuGetPush(
            package,
            new NuGetPushSettings
            {
                    Source = nugetSource,
                    ApiKey = nugetApiKey
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
