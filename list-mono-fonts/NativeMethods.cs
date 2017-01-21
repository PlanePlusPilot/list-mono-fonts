namespace list_mono_fonts
{
    #region using
    using System;
    using System.Runtime.InteropServices;
    #endregion

    internal static class NativeMethods
    {
        public const int LF_FACESIZE = 32;
        public const int LF_FULLFACESIZE = 64;
        public const int DEFAULT_CHARSET = 1;
        public const int FIXED_PITCH = 1;
        public const int TRUETYPE_FONTTYPE = 0x0004;

        public delegate int FONTENUMPROC(ref ENUMLOGFONT lpelf, ref NEWTEXTMETRIC lpntm, uint FontType, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct LOGFONT
        {
            public int lfHeight;
            public int lfWidth;
            public int lfEscapement;
            public int lfOrientation;
            public int lfWeight;
            public byte lfItalic;
            public byte lfUnderline;
            public byte lfStrikeOut;
            public byte lfCharSet;
            public byte lfOutPrecision;
            public byte lfClipPrecision;
            public byte lfQuality;
            public byte lfPitchAndFamily;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = LF_FACESIZE)]
            public string lfFaceName;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct ENUMLOGFONT
        {
            public LOGFONT elfLogFont;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = LF_FULLFACESIZE)]
            public string elfFullName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = LF_FACESIZE)]
            public string elfStyle;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct NEWTEXTMETRIC
        {
            public int tmHeight;
            public int tmAscent;
            public int tmDescent;
            public int tmInternalLeading;
            public int tmExternalLeading;
            public int tmAveCharWidth;
            public int tmMaxCharWidth;
            public int tmWeight;
            public int tmOverhang;
            public int tmDigitizedAspectX;
            public int tmDigitizedAspectY;
            public char tmFirstChar;
            public char tmLastChar;
            public char tmDefaultChar;
            public char tmBreakChar;
            public byte tmItalic;
            public byte tmUnderlined;
            public byte tmStruckOut;
            public byte tmPitchAndFamily;
            public byte tmCharSet;
            public uint ntmFlags;
            public uint ntmSizeEM;
            public uint ntmCellHeight;
            public uint ntmAvgWidth;
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern int EnumFontFamiliesEx(IntPtr hdc, ref LOGFONT lpLogfont, FONTENUMPROC lpEnumFontFamExProc, IntPtr lParam, uint dwFlags);
    }
}