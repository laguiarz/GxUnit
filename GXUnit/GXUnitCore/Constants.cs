using System;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    public static class Constants
    {
        public const String PARM_INOUT  = "PARM_INOUT";
        public const String PARM_IN     = "PARM_IN";
        public const String PARM_OUT    = "PARM_OUT";

        public enum Estructurado {BC, SDT};

        public enum Tipo { Boolean, CHARACTER, DATE, DATETIME, BC, BC_LEVEL, EXTERNAL_OBJECT, SDT, INT, LONGVARCHAR, NUMERIC, VARCHAR };

        public const String RESULTADO = "Result";
        public const String VALOR_ESPERADO = "Expected";
        public const String ITEM = "Item";

        public const String GXUNIT_FOLDER = "GXUnit";
        public const String SUITES_FOLDER = "GXUnitSuites";
     //   public const String carpetaResults = "GXUnitResults";

        public const String ABOUT = "Proyecto GXUnit\r\nNicolás Carro\r\nMarcos Olivera\r\nJuan Pablo Goyení\r\n2010 - 2011\r\n" +
                                    "Resumed by Laura Aguiar 2017\r\n";

        public const String RESULT_PATH = "\\GXUnitResults\\";

        public const String RUNNER_PROC = "GXUnit_RunTests";
    }
}
