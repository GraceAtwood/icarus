using Icarus.Engine.Framework.Modding;

namespace Icarus.Engine
{
    public interface ITemplatable
    {
        string TemplateId { get; set; }
        Mod SourceMod { get; set; }
    }
}