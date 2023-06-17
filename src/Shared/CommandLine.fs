namespace CommandLine

open Argu
open Helpers.ANSIEscapeSequences

type CliArguments =
    | [<Mandatory; AltCommandLine("-t")>] Token of string

    interface IArgParserTemplate with
        member arg.Usage =
            match arg with
            | Token _ -> "The token issued for the bot by Discord"
    
    module Display =
        let private loader = [| '\\'; '|'; '/'; '-' |]

        let PrintSuccessMessage message =
            printfn "%s[✓]%s %s" GREEN RESET message

        let PrintFailureMessage message =
            printfn "%s[x]%s %s" RED RESET message

        let PrintInfoMessage message =
            printfn "%s[i]%s %s" BLUE RESET message

        let PrintWarningMessage message =
            printfn "%s[!]%s %s" YELLOW RESET message

        let private PrintBusyMessage message loaderChar =
            printf "\r%s[%c]%s %s\r" BLUE loaderChar RESET message

        let private PrintStandbyMessage message loaderChar =
            printf "\r%s[%c]%s %s\r" BRIGHT_BLACK loaderChar RESET message

        let DisplayProgressIndicator message =
            async {
                let rec loop iterator =
                    async {
                        let loaderChar = loader[iterator % 4]
                        PrintBusyMessage message loaderChar
                        do! Async.Sleep 100
                        return! loop(iterator + 1)
                    }

                do! loop 0
            }
        
        let DisplayStandbyIndicator message =
            async {
                try
                    let rec loop iterator =
                        async {
                            let loaderChar = loader[iterator % 4]
                            PrintStandbyMessage message loaderChar
                            do! Async.Sleep 1000
                            return! loop(iterator + 1)
                        }

                    do! loop 0
                finally
                    // Clear the current line when the task is cancelled
                    printf "%s\r" CLEAR_LINE
            }
