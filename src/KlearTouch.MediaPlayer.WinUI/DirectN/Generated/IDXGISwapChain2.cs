// c:\program files (x86)\windows kits\10\include\10.0.22000.0\shared\dxgi1_3.h(395,5)
using System;
using System.Runtime.InteropServices;

namespace DirectN
{
    [ComImport, Guid("a8be2ac4-199f-4946-b331-79599fb98de7"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDXGISwapChain2 : IDXGISwapChain1
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
        new HRESULT GetDesc1();

        [PreserveSig]
        new HRESULT GetFullscreenDesc();

        [PreserveSig]
        new HRESULT GetHwnd();

        [PreserveSig]
        new HRESULT GetCoreWindow();

        [PreserveSig]
        new HRESULT Present1();

        [PreserveSig]
        new bool IsTemporaryMonoSupported();

        [PreserveSig]
        new HRESULT GetRestrictToOutput();

        [PreserveSig]
        new HRESULT SetBackgroundColor();

        [PreserveSig]
        new HRESULT GetBackgroundColor();

        [PreserveSig]
        new HRESULT SetRotation();

        [PreserveSig]
        new HRESULT GetRotation();

        // IDXGISwapChain2
        [PreserveSig]
        HRESULT SetSourceSize();

        [PreserveSig]
        HRESULT GetSourceSize();

        [PreserveSig]
        HRESULT SetMaximumFrameLatency();

        [PreserveSig]
        HRESULT GetMaximumFrameLatency();

        [PreserveSig]
        IntPtr GetFrameLatencyWaitableObject();

        [PreserveSig]
        HRESULT SetMatrixTransform(ref DXGI_MATRIX_3X2_F pMatrix);

        [PreserveSig]
        HRESULT GetMatrixTransform(/* [annotation][out] _Out_ */ out DXGI_MATRIX_3X2_F pMatrix);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DXGI_MATRIX_3X2_F
    {
        public float _11;
        public float _12;
        public float _21;
        public float _22;
        public float _31;
        public float _32;
    }
}
