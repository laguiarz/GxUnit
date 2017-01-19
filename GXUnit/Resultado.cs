using System.Runtime.InteropServices;

using Artech.Architecture.Common.Descriptors;
using Artech.Architecture.Common;
using Artech.Architecture.Common.Objects;
using Artech.Architecture.UI.Framework.Objects;
using Artech.Genexus.Common.Parts;

namespace PGGXUnit.Packages.GXUnit
{
    [Guid("89283150-9b94-410a-955c-92a1eef442aa")]
#if GXTILO
    [KBObjectDescriptor("ef0f3552-a93d-4f10-9b94-4442e32ea719", "Result", "Result", "PGGXUnit.Packages.GXUnit.Items, document", ObjectTypeFlags.NoCreatable)]
#else
    [KBObjectDescriptor("ef0f3552-a93d-4f10-9b94-4442e32ea719", "Result", "Result", "PGGXUnit.Packages.GXUnit.Items, document", ObjectTypeFlags.NoCreateble)]
#endif
    [KBObjectComposition(false, typeof(ResultadoPart))]
    class Resultado : KBObject
    {
        public Resultado(KBModel model)
            : base(model, typeof(Resultado).GUID)
        {
        }
    }
}
