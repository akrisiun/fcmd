using System;

namespace pluginner
{

    public struct ShortenPolicies
    {
        // FileListPanel.SizeDisplayPolicy
        public SizeDisplayPolicy KB { get; set; }
        public SizeDisplayPolicy MB { get; set; }
        public SizeDisplayPolicy GB { get; set; }

        public static ShortenPolicies Empty { get; private set; }

        static ShortenPolicies()
        {
            Empty = new ShortenPolicies { KB = 0, MB = 0, GB = 0 };
        }
    }

    /// <summary>Defines the size shortening policy</summary>
    public enum SizeDisplayPolicy
    {
        DontShorten = 0, OneNumeral = 1, TwoNumeral = 2
        //2048 B, 2 KB, 2.0 KB
    }

}
