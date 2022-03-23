// c:\program files (x86)\windows kits\10\include\10.0.22000.0\shared\dxgi.h(321,5)
using System;
using System.Runtime.InteropServices;

namespace DirectN
{
    [ComImport, Guid("aec22fb8-76f3-4639-9be0-28eb43a67a2e"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDXGIObject
    {
        [PreserveSig]
        HRESULT SetPrivateData();

        [PreserveSig]
        HRESULT SetPrivateDataInterface();

        [PreserveSig]
        HRESULT GetPrivateData();

        [PreserveSig]
        HRESULT GetParent(/* [annotation][in] _In_ */ [MarshalAs(UnmanagedType.LPStruct)] Guid riid, /* [annotation][retval][out] _COM_Outptr_ */ [MarshalAs(UnmanagedType.IUnknown)] out object ppParent);
    }
}