#r @"packages/FAKE/tools/FakeLib.dll"
#load "build-helpers.fsx"
open Fake
open System
open System.IO
open System.Linq
open BuildHelpers
open Fake.XamarinHelper

// Let's set up some variables.

let mobileAppPath = "src/MobileApp/"

let solutionFile = (mobileAppPath + "XamarinCRM.sln")

let packageOutputPath = (mobileAppPath + "packages")

let iOSProject = "src/MobileApp/XamarinCRM.iOS"

let androidProject = "src/MobileApp/XamarinCRM.Android"

let RestorePackagesToHintPath = Exec "tools/NuGet/NuGet.exe" ("restore " + solutionFile + " -PackagesDirectory " + packageOutputPath)

// You may or may not want all of the following targets for your purposes. Modify to your liking.

// This target is mostly for a sanity check, to make sure the app builds with debug settings.
Target "ios-iphone-debug" (fun () ->
    RestorePackagesToHintPath
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = solutionFile
            Platform = "iPhone"
            Configuration = "Debug"
            Target = "Build"
            BuildIpa = true
        })
    TeamCityHelper.PublishArtifact (iOSProject + "/bin/iPhone/Debug/*.ipa")
)

// This target is a release build, signed for App Store distribution.
Target "ios-iphone-appstore" (fun () ->
    RestorePackagesToHintPath
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = solutionFile
            Platform = "iPhone"
            Configuration = "AppStore"
            Target = "Build"
            BuildIpa = true
        })
    TeamCityHelper.PublishArtifact (iOSProject + "/bin/iPhone/AppStore/*.ipa")
)

// This target is a release build, signed InHouse distribution.
Target "ios-iphone-inhouse" (fun () ->
    RestorePackagesToHintPath
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = solutionFile
            Platform = "iPhone"
            Configuration = "Inhouse"
            Target = "Build"
            BuildIpa = true
        })
    TeamCityHelper.PublishArtifact (iOSProject + "/bin/iPhone/Inhouse/*.ipa")
)

// This target is a release build, signed for Ad-Hoc distribution.
Target "ios-iphone-adhoc" (fun () ->
    RestorePackagesToHintPath
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = solutionFile
            Platform = "iPhone"
            Configuration = "Ad-Hoc"
            Target = "Build"
            BuildIpa = true
        })
    TeamCityHelper.PublishArtifact (iOSProject + "/bin/iPhone/Ad-Hoc/*.ipa")
)

RunTarget() 