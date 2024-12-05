using System;
using System.Reflection;

namespace OldMods.CustomCosmeticLoader.Patches
{
    // Token: 0x02000013 RID: 19
    internal static class MarbleHolderValues
    {
        // Token: 0x17000012 RID: 18
        // (get) Token: 0x06000054 RID: 84 RVA: 0x00003ACD File Offset: 0x00001CCD
        public static FieldInfo MbcField
        {
            get
            {
                return MarbleHolderValues._mbcField;
            }
        }

        // Token: 0x04000026 RID: 38
        private static FieldInfo _mbcField = typeof(MarbleHolder).GetField("mbc", BindingFlags.Instance | BindingFlags.NonPublic);
    }
}
