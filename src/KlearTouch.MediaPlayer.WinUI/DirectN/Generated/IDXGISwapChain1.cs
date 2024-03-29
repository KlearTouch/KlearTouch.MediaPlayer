﻿// c:\program files (x86)\windows kits\10\include\10.0.22000.0\shared\dxgi1_2.h(1307,5)
using System;
using System.Runtime.InteropServices;

namespace DirectN
{
    [ComImport, Guid("790a45f7-0d42-4876-983a-0a55cfe6f4aa"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDXGISwapChain1 : IDXGISwapChain
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
        new HRESULT GetDevice();

        // IDXGISwapChain
        [PreserveSig]
        new HRESULT Present(/* [in] */ uint SyncInterval, /* [in] */ uint Flags);

        [PreserveSig]
        new HRESULT GetBuffer(/* [in] */ uint Buffer, /* [annotation][in] _In_ */ [MarshalAs(UnmanagedType.LPStruct)] Guid riid, /* [annotation][out][in] _COM_Outptr_ */ [MarshalAs(UnmanagedType.IUnknown)] out object ppSurface);

        [PreserveSig]
        new HRESULT SetFullscreenState();

        [PreserveSig]
        new HRESULT GetFullscreenState();

        [PreserveSig]
        new HRESULT GetDesc(/* [annotation][out] _Out_ */ out DXGI_SWAP_CHAIN_DESC pDesc);

        [PreserveSig]
        new HRESULT ResizeBuffers(/* [in] */ uint BufferCount, /* [in] */ uint Width, /* [in] */ uint Height, /* [in] */ DXGI_FORMAT NewFormat, /* [in] */ uint SwapChainFlags);

        [PreserveSig]
        new HRESULT ResizeTarget();

        [PreserveSig]
        new HRESULT GetContainingOutput();

        [PreserveSig]
        new HRESULT GetFrameStatistics();

        [PreserveSig]
        new HRESULT GetLastPresentCount();

        // IDXGISwapChain1
        [PreserveSig]
        HRESULT GetDesc1();

        [PreserveSig]
        HRESULT GetFullscreenDesc();

        [PreserveSig]
        HRESULT GetHwnd();

        [PreserveSig]
        HRESULT GetCoreWindow();

        [PreserveSig]
        HRESULT Present1();

        [PreserveSig]
        bool IsTemporaryMonoSupported();

        [PreserveSig]
        HRESULT GetRestrictToOutput();

        [PreserveSig]
        HRESULT SetBackgroundColor();

        [PreserveSig]
        HRESULT GetBackgroundColor();

        [PreserveSig]
        HRESULT SetRotation();

        [PreserveSig]
        HRESULT GetRotation();
    }

    [StructLayout(LayoutKind.Sequential, Size = 72)]
    internal struct DXGI_SWAP_CHAIN_DESC // Simplified
    {
        public uint Width;
        public uint Height;
    }
}