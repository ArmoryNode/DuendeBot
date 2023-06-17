namespace Helpers
    module ANSIEscapeSequences =
        open System
        let NEWLINE = Environment.NewLine
        let HIDE_CURSOR = "\x1b[?25l"
        let SHOW_CURSOR = "\x1b[?25h"
        let RESET = "\x1b[39m"
        let BLUE = "\x1b[94m"
        let GREEN = "\x1b[92m"
        let YELLOW = "\x1b[33m"
        let RED = "\x1b[91m"
        let BRIGHT_BLACK = "\x1b[90m"
        let CLEAR_LINE = "\x1b[2K"

    module CLI =
        let hideCursor = 
            printf "%s" ANSIEscapeSequences.HIDE_CURSOR
