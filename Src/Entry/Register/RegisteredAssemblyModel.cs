using System.Collections.Generic;

namespace Entry.Register;

public sealed class RegisteredAssemblyModel
{
    public AssemblyModel Assembly { get; set; }

    public sealed class AssemblyModel
    {
        public IEnumerable<string> Core { get; set; }

        public IEnumerable<string> External { get; set; }
    }
}
