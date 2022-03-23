// c:\program files (x86)\windows kits\10\include\10.0.22000.0\shared\dxgi1_2.h(1687,5)
using System;
using System.Runtime.InteropServices;

namespace DirectN
{
    [ComImport, Guid("50c83a1c-e072-4c48-87b0-3630fa36a6d0"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDXGIFactory2 : IDXGIObject
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

        // IDXGIFactory
        [PreserveSig]
        HRESULT EnumAdapters();

        [PreserveSig]
        HRESULT MakeWindowAssociation();

        [PreserveSig]
        HRESULT GetWindowAssociation();

        [PreserveSig]
        HRESULT CreateSwapChain();

        [PreserveSig]
        HRESULT CreateSoftwareAdapter();

        // IDXGIFactory1
        [PreserveSig]
        HRESULT EnumAdapters1();

        [PreserveSig]
        bool IsCurrent();

        // IDXGIFactory2
        [PreserveSig]
        bool IsWindowedStereoEnabled();

        [PreserveSig]
        HRESULT CreateSwapChainForHwnd();

        [PreserveSig]
        HRESULT CreateSwapChainForCoreWindow();

        [PreserveSig]
        HRESULT GetSharedResourceAdapterLuid();

        [PreserveSig]
        HRESULT RegisterStereoStatusWindow();

        [PreserveSig]
        HRESULT RegisterStereoStatusEvent();

        [PreserveSig]
        void UnregisterStereoStatus();

        [PreserveSig]
        HRESULT RegisterOcclusionStatusWindow();

        [PreserveSig]
        HRESULT RegisterOcclusionStatusEvent();

        [PreserveSig]
        void UnregisterOcclusionStatus();

        [PreserveSig]
        HRESULT CreateSwapChainForComposition(/* [annotation][in] _In_ */ [MarshalAs(UnmanagedType.IUnknown)] object pDevice, /* [annotation][in] _In_ */ ref DXGI_SWAP_CHAIN_DESC1 pDesc, /* [annotation][in] _In_opt_ */ [MarshalAs(UnmanagedType.IUnknown)] object? pRestrictToOutput, /* [annotation][out] _COM_Outptr_ */ out IDXGISwapChain1 ppSwapChain);
    }
}