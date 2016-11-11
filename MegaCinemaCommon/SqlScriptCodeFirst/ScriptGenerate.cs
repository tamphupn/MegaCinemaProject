using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCinemaCommon.SqlScriptCodeFirst
{
    public class ScriptGenerate
    {
        public string AlterAutoIndexTable(string NameTable, string NameColumn, string Prefix, string ID)
        {
            return string.Format("ALTER TABLE {0} DROP COLUMN {1} ALTER TABLE {0} ADD {1} AS {2} + CONVERT(VARCHAR(MAX),{3})", NameTable, NameColumn, Prefix, ID);
        }
    }
}
