using System;
using UIKit;

namespace MonoTouch.Dialog.Utilities
{
    public static class MiscExtensions
    {
        public static UIFont Scalable(this UIFont font)
        { 
            return UIFontMetrics.DefaultMetrics.GetScaledFont(font);
        }
        
        public static nfloat Scaled(this nfloat size)
        { 
            return UIFontMetrics.DefaultMetrics.GetScaledValue(size);
        }
        
        public static float Scaled(this float size)
        { 
            return (float)UIFontMetrics.DefaultMetrics.GetScaledValue(size);
        }

    }
}