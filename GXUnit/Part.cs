using System;
using System.Runtime.InteropServices;

using Artech.Common;
using Artech.Architecture.Common.Objects;
using Artech.Architecture.UI.Framework.Objects;
using Artech.Architecture.Common.Descriptors;
using Artech.Common.Helpers.Generics;

namespace PGGXUnit.Packages.GXUnit
{
    [Guid("8c65640e-46d7-4c1d-ad27-56fe81f23485")]
    [KBObjectPartDescriptor("Result Viewer", "PGGXUnit.Packages.GXUnit.Items, Object")]
    public class ResultadoPart : KBObjectPart
    {
        public ResultadoPart(KBObject kbObject)
            : base(typeof(ResultadoPart).GUID, kbObject)
        {
        }

        public override string Name
        {
            get
            {
                return "ResultadoPart";
            }
            set
            {
            }
        }
        
        private string archivo_resultado;


        //[Artech.Common.Helpers.Kmw.KmwElement(ReferenceType = Artech.Common.Helpers.Kmw.KmwReferenceType.Full)]
        [Artech.Common.Helpers.Kmw.KmwElement(Name = "ArchivoResultado")]
        public string ArchivoResultado
        {
            get
            {
                base.EnsureDeserialization();
                if (archivo_resultado == null)
                {
                    archivo_resultado = "";
                }
                return archivo_resultado;
            }
            set
            {
                archivo_resultado = value;

                SetModeModified(Modification.Data, null);
            }
        }

        #region Serialization

        protected override byte[] SerializeData()
        {
            string listAsText = XmlSerializer<String>.ToString(archivo_resultado);
            return Artech.Common.Helpers.Convert.ToByteArray(listAsText,this);
        }

        protected override void DeserializeData(byte[] data)
        {
            string listAsText = Artech.Common.Helpers.Convert.ToString(data, this);
            archivo_resultado = XmlSerializer<String>.DeserializeFromString(listAsText);
        }

        #endregion
    }
}
