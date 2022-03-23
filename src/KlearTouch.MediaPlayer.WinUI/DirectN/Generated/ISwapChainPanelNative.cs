// c:\program files (x86)\windows kits\10\include\10.0.22000.0\um\windows.ui.xaml.media.dxinterop.h(854,5)
using System.Runtime.InteropServices;

namespace DirectN
{
    [ComImport, Guid(
#if WINDOWS_UWP
        "f92f19d2-3ade-45a6-a20c-f6f1ea90554b"
#else
        "63aad0b8-7c24-40ff-85a8-640d944cc325"
#endif
        ), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ISwapChainPanelNative
    {
        [PreserveSig]
        HRESULT SetSwapChain(/* [annotation][in] _In_ */ IDXGISwapChain? swapChain);
    }
}