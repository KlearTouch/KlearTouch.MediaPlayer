using System;
using System.Runtime.InteropServices;

namespace DirectN
{
    internal static class ComError
    {
        public static Exception? GetError()
        {
            GetErrorInfo(0, out var info);
            if (info == null)
                return null;

            COMException error;
            info.GetDescription(out var description);

            info.GetSource(out var source);
            if (!string.IsNullOrWhiteSpace(source))
            {
                if (description == null)
                {
                    description = source;
                }
                else
                {
                    description = source + ": " + description;
                }
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                error = new COMException();
            }
            else
            {
                error = new COMException(description);
            }

            info.GetHelpFile(out var help);
            if (!string.IsNullOrWhiteSpace(help))
            {
                error.HelpLink = help;
            }

            return error;
        }

        [ComImport, Guid("1CF2B120-547D-101B-8E65-08002B2BD119"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IErrorInfo
        {
            [PreserveSig]
            HRESULT GetGUID(out Guid pguid);

            [PreserveSig]
            HRESULT GetSource([MarshalAs(UnmanagedType.BStr)] out string pBstrSource);

            [PreserveSig]
            HRESULT GetDescription([MarshalAs(UnmanagedType.BStr)] out string pBstrDescription);

            [PreserveSig]
            HRESULT GetHelpFile([MarshalAs(UnmanagedType.BStr)] out string pBstrHelpFile);

            [PreserveSig]
            HRESULT GetHelpContext(out int pdwHelpContext);
        }

        [DllImport("oleaut32")]
        private static extern HRESULT GetErrorInfo(int dwReserved, out IErrorInfo perrinfo);
    }
}