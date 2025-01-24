using System.Collections.Generic;

namespace Entry.Models;

public sealed class RegisteredAssemblyModel
{
    public IEnumerable<string> Assemblies { get; set; } = [];
}
