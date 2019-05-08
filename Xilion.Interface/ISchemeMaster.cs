using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xilion.Models;

namespace Xilion.Interface
{
    public interface ISchemeMaster
    {
        bool AddSchemeMaster(SchemeMaster schemeMaster);
        List<SchemeMaster> GetSchemeMasterList();
        SchemeMaster GetSchemeMasterbyId(int schemeId);
        bool CheckSchemeNameExists(string schemeName);
        bool UpdateSchemeMaster(SchemeMaster schemeMaster);
        bool DeleteScheme(int schemeId);

        List<SchemeMaster> GetActiveSchemeMasterList();
    }
}
