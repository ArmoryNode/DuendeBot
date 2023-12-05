module Bot

open DSharpPlus
open DSharpPlus.Entities
open DSharpPlus.SlashCommands
open Microsoft.Extensions.Logging

type BotSlashCommands() =
    inherit ApplicationCommandModule()

    [<SlashCommand("test", "A test slash command")>]
    let TestCommand (ctx: InteractionContext) =
        task {
            do!
                ctx.CreateResponseAsync(
                    InteractionResponseType.ChannelMessageWithSource,
                    DiscordInteractionResponseBuilder().WithContent("Success!")
                )
        }

let InitializeBot token =
    task {
        let discordClient =
            new DiscordClient(
                new DiscordConfiguration(
                    Token = token,
                    TokenType = TokenType.Bot,
                    Intents = (DiscordIntents.AllUnprivileged ||| DiscordIntents.MessageContents),
                    MinimumLogLevel = LogLevel.Warning
                )
            )

        discordClient.UseSlashCommands().RegisterCommands<BotSlashCommands>()

        do! discordClient.ConnectAsync()
    }
