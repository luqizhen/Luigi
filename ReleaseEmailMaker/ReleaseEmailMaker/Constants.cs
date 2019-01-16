using System.Collections.Generic;

namespace ReleaseEmailMaker
{
    class Constants
    {
        public const bool ENABLE_RELEASE_TOOLS = true;
        public const string APPLICATION_NAME = "Dell Commitment & Release Assistant";

        public const string FORMAT_PR = @"Base: {0}
Dependency: {1}


** Updated files: **
>{2}

** Main modifications:**
1) Function change:
`*{3}`

2) New functions:
`*{4}`

3) BUGFIX:
`*{5}`
    
4) Code refine:
`*{6}`

5) Limitation:
`*{7}`

**Root Cause:**
`{8}`

**Solution:**
`{9}`

**Smoke test: **

| Test Case Type | Test Content | Status |
| --------|---------|---------|
| Smoke test |  | PASS |
| Build | | PASS |
| WINRE test | | PASS |
| Any other test | | PASS|


**How to test :**
{10}";

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
