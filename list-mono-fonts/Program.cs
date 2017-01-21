namespace list_mono_fonts
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    internal static class Program
    {
        #region Constants and Fields
        /// <summary>
        /// HashSet to store unique font name
        /// </summary>
        private static readonly HashSet<string> FixedFonts = new HashSet<string>();
        #endregion

        #region Methods
        private static int EnumFontFamExProc(ref NativeMethods.ENUMLOGFONT lpelf, ref NativeMethods.NEWTEXTMETRIC lpntm, uint FontType, IntPtr lParam)
        {
            if ((lpelf.elfLogFont.lfPitchAndFamily & 0x3) == NativeMethods.FIXED_PITCH)
            {
                var faceName = lpelf.elfLogFont.lfFaceName;
                if (faceName == null) throw new NullReferenceException("faceName cannot be null");

                if (!string.IsNullOrEmpty(faceName) && faceName[0] == '@') faceName = faceName.Substring(1, faceName.Length - 1); // trim leading @

                FixedFonts.Add(faceName);
            }

            return 1;
        }

        private static void Main()
        {
            var graphics = Graphics.FromHwnd(IntPtr.Zero);
            var hdc = graphics.GetHdc();
            var logfont = new NativeMethods.LOGFONT { lfCharSet = NativeMethods.DEFAULT_CHARSET };
            NativeMethods.EnumFontFamiliesEx(hdc, ref logfont, EnumFontFamExProc, IntPtr.Zero, 0);
            graphics.ReleaseHdc();

            foreach (var font in FixedFonts.OrderBy(x => x)) Console.WriteLine(font);
        }
        #endregion
    }
}