open System.Threading
open Helpers.CLI
open CommandLine.Display
open Argu
open CommandLine
open Bot

let parseCommandLineArguments args =
    ArgumentParser.Create<CliArguments>(programName = "Duende Bot").Parse args

[<EntryPoint>]
let main args =
    async {
        let cts = new CancellationTokenSource()

        let parsedArguments = parseCommandLineArguments args
        let botToken = parsedArguments.GetResult Token

        Async.Start(DisplayProgressIndicator "Starting bot", cts.Token)

        do! InitializeBot botToken
            |> Async.AwaitTask

        cts.Cancel()

        PrintSuccessMessage "Bot successfully started"

        do! DisplayStandbyIndicator "Bot running..."
    }
    |> Async.RunSynchronously
    0