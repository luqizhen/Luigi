using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReleaseEmailMaker
{
    class Constants
    {
        public const string FORMAT = @"
The {0} {1} Version {2} (debug build) and {3} (release build) are ready, the packages are in the {4} now.

{0} {1}
{2} (debug build) and {3} (release build)：

The change list as follows:

Fix Bugs: 
{5}

Completed Stories:
{6}

Known issues:
{7}

";
    }
}
