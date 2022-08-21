using CommandLine;

namespace fdf.Options
{
    public class MainOptions
    {
        [Option('s', "source-path", Required = true, HelpText = "Source files folder")]
        public string SourcePath { get; set; }
        [Option('t', "target-path", Required = true, HelpText = "Target folder to search for source files")]
        public string TargetPath { get; set; }
        [Option('c', "copy-difference-path", Required = false, HelpText = "Copy files that were not found")]
        public string CopyDifferencePath { get; set; }

        // TODO: Implement different types of search!
        // [Option(
        //     'm',
        //     "search-mod",
        //     Required = false,
        //     HelpText = "File search mode."
        //     + "(h - checksum search|n - search by name | s - search by file size)"
        //     + " Examples: ns, s, n, h")
        // ]
        // public string SearchMod { get; set; }
    }
}