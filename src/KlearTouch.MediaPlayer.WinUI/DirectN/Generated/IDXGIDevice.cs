// c:\program files (x86)\windows kits\10\include\10.0.22000.0\shared\dxgi.h(2313,5)
using System;
using System.Runtime.InteropServices;

namespace DirectN
{
    [ComImport, Guid("54ec77fa-1377-44e6-8c32-88fd5f44c84c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDXGIDevice : IDXGIObject
    {
        // IDXGIObject
        [PreserveSig]
        new HRESULT SetPrivateData();
        
        [PreserveSig]
        new HRESULT SetPrivateDataInterface();
        
        [PreserveSig]
        new HRESULT GetPrivateData();
        
        [PreserveSig]
        new HRESULT GetParent(/* [annotation][in] _In_ */ [MarshalAs(UnmanagedType.LPStruct)] Guid riid, /* [annotation][retval][out] _COM_Outptr_ */ [MarshalAs(UnmanagedType.IUnknown)] out object ppParent);

        // IDXGIDevice
        [PreserveSig]
        HRESULT GetAdapter(/* [annotation][out] _COM_Outptr_ */ out IDXGIAdapter pAdapter);
    }
}