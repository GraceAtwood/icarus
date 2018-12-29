using System.IO;

namespace Icarus.Engine.Framework.Modding
{
    public class BlueprintSourceInfo
    {
        public Mod SourceMod { get; }
        public FileInfo SourceFile { get; }

        public BlueprintSourceInfo(Mod sourceMod, FileInfo sourceFile)
        {
            SourceMod = sourceMod;
            SourceFile = sourceFile;
        }
    }
}