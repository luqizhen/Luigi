using System.Collections.Generic;

namespace ReleaseEmailMaker
{
    class Constants
    {
        public const string FORMAT_TWO_VERSIONS = @"The {0} {1} Version {2} (debug build) and {3} (release build) are ready, the packages are in the {4} now.

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

        public const string FORMAT_ONE_VERSION = @"The {0} {1} Version {2} ({3} build) is ready, the package in in the {4} now.

{0} {1}
{2} ({3} build)：

The change list as follows:

Fix Bugs: 
{5}

Completed Stories:
{6}

Known issues:
{7}

";

        public const string FORMAT_EMAIL_START = @"Dell - Internal Use - Confidential 

Hi All,

";

        public const string FORMAT_EMAIL_END = @"
Thanks!

Best regards,
";

        public static List<string> EMAIL_TO_EXCALIBUR = new List<string>()
        {

        };

        public static List<string> EMAIL_CC_EXCALIBUR = new List<string>()
        {

        };
    }
}
