using System;

namespace AutoUpdateTool.Helper
{
    public class VersionHelper
    {
        public static int VersionCompare(string left, string right)
        {
            left ??= "";
            right ??= "";
            string leftVersionStr = left.Split('_')[0];
            string rightVersionStr = right.Split('_')[0];
            Version versionLeft = string.IsNullOrWhiteSpace(leftVersionStr) ? new Version() : new Version(leftVersionStr);
            Version versionRight = string.IsNullOrWhiteSpace(rightVersionStr) ? new Version() : new Version(rightVersionStr);
            if (versionLeft.CompareTo(versionRight) != 0)
            {
                return versionLeft.CompareTo(versionRight);
            }
            else
            {
                if (left.Split('_').Length < 2 || right.Split('_').Length < 2)
                {
                    return 0;
                }
                string leftDate = left.Split('_')[1];
                string rightDate = right.Split('_')[1];
                return leftDate.CompareTo(rightDate);
            }
        }
    }
}
