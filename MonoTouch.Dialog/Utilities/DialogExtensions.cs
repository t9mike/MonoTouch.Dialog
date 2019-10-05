using System;
using UIKit;

namespace MonoTouch.Dialog.Utilities
{
    public static class DialogExtensions
    {
        internal static UIFont Scalable(this UIFont font) => Scalable_Font_Callback(font);

        internal static float Scaled(this float size) => Scalable_Value_Callback(size);

        public static Func<UIFont, UIFont> Scalable_Font_Callback = delegate(UIFont font)
        {
            return UIFontMetrics.DefaultMetrics.GetScaledFont(font);
        };

        public static Func<float, float> Scalable_Value_Callback = delegate(float value)
        {
            return (float)UIFontMetrics.DefaultMetrics.GetScaledValue((nfloat)value);
        };

    }
}