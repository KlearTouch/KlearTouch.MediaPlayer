// c:\program files (x86)\windows kits\10\include\10.0.22000.0\shared\dxgi.h(1808,5)
using System;
using System.Runtime.InteropServices;

namespace DirectN
{
    [ComImport, Guid("310d36a0-d2e7-4c0a-aa04-6a9d23b8886a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDXGISwapChain : IDXGIObject
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

        // IDXGIDeviceSubObject
        [PreserveSig]
        HRESULT GetDevice();

        // IDXGISwapChain
        [PreserveSig]
        HRESULT Present(/* [in] */ uint SyncInterval, /* [in] */ uint Flags);

        [PreserveSig]
        HRESULT GetBuffer(/* [in] */ uint Buffer, /* [annotation][in] _In_ */ [MarshalAs(UnmanagedType.LPStruct)] Guid riid, /* [annotation][out][in] _COM_Outptr_ */ [MarshalAs(UnmanagedType.IUnknown)] out object ppSurface);

        [PreserveSig]
        HRESULT SetFullscreenState();

        [PreserveSig]
        HRESULT GetFullscreenState();

        [PreserveSig]
        HRESULT GetDesc(/* [annotation][out] _Out_ */ out DXGI_SWAP_CHAIN_DESC pDesc);

        [PreserveSig]
        HRESULT ResizeBuffers(/* [in] */ uint BufferCount, /* [in] */ uint Width, /* [in] */ uint Height, /* [in] */ DXGI_FORMAT NewFormat, /* [in] */ uint SwapChainFlags);

        [PreserveSig]
        HRESULT ResizeTarget();

        [PreserveSig]
        HRESULT GetContainingOutput();

        [PreserveSig]
        HRESULT GetFrameStatistics();

        [PreserveSig]
        HRESULT GetLastPresentCount();
    }
}