#r "paket:
nuget Fake.DotNet.Cli
nuget Fake.IO.FileSystem
nuget Fake.Core.Target //"
#load ".fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators

let buildConfig = "Release"
let buildVersion = "2.0.0"
let apiClientPath = "src/projects/Paynova.Api.Client"
let apiClientProjectName = "Paynova.Api.Client.csproj"

Target.initEnvironment()

Target.create "Clean" (fun _ -> !!"src/**/bin" ++ "src/**/obj" |> Shell.cleanDirs)

Target.create "Build" (fun _ ->
    let buildParams (buildOptions: DotNet.BuildOptions) =
        { buildOptions with Configuration = DotNet.BuildConfiguration.fromString (buildConfig) }
    !!"src/**/*.*proj" |> Seq.iter (DotNet.build buildParams))

Target.create "UnitTest" (fun _ ->
    let testParams (testOptions: DotNet.TestOptions) =
        { testOptions with Configuration = DotNet.BuildConfiguration.fromString (buildConfig) }

    !!"src/**/*UnitTests.*proj" |> Seq.iter (DotNet.test testParams))

Target.create "IntegrationTest" (fun _ ->
    let testParams (testOptions: DotNet.TestOptions) =
        { testOptions with Configuration = DotNet.BuildConfiguration.fromString (buildConfig) }

    !!"src/**/*IntegrationTest.*proj" |> Seq.iter (DotNet.test testParams))

Target.create "Pack" (fun _ ->
    let packParams (packOptions: DotNet.PackOptions) =
        { packOptions with
              Configuration = DotNet.BuildConfiguration.fromString (buildConfig)
              OutputPath = Some apiClientPath
              VersionSuffix = Some buildVersion }

    !!(apiClientPath + "/" + apiClientProjectName) |> Seq.iter (DotNet.pack packParams))


Target.create "All" ignore

"Clean" ==> "Build" ==> "UnitTest" ==> "All"

Target.runOrDefault "All"
