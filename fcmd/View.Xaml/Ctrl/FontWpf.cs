using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fcmd.View.ctrl
{
    public static class FontWpf
    {
        public static float emSizeDefault = 12F;

        public static Font SystemFont { get { return new Font(familyName: "Tahoma", emSize: emSizeDefault); } }

        public static Font FromName(string fontFamily, float? sizeEm = null)
        {
            return new Font(fontFamily, sizeEm ?? emSizeDefault);
        }
    }
}
