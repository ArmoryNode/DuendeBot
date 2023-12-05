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

        let! taskResult = 
            InitializeBot botToken 
            |> Async.AwaitTask
            |> Async.Catch

        match taskResult with
        | Choice2Of2 exn -> 
            PrintFailureMessage "Failed to start bot"
            raise exn
        | Choice1Of2 _ ->
            cts.Cancel()
            PrintSuccessMessage "Bot successfully started"
            do! DisplayStandbyIndicator "Bot running..."
            
    }
    |> Async.RunSynchronously
    0