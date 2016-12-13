using Common.CommandLine;

namespace HttpWatch
{
    public sealed class CmdLineArgs
    {
        [Argument(ArgumentTypes.AtMostOnce, LongName = "start")]
        public bool Start { get; set; }

        [Argument(ArgumentTypes.AtMostOnce, LongName = "stop")]
        public bool Stop { get; set; }

        public CmdLineArgs()
        {
        }
    }
}
