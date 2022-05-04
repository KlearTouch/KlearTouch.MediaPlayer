// © 2022 KlearTouch, Pierre Henri KT. Licensed under the MIT license. See the LICENSE.txt file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using Windows.Graphics.DirectX.Direct3D11;
#if WINDOWS_UWP
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
#endif
using DirectN;

namespace Microsoft.UI.Xaml.Controls;

internal class SwapChainSurface : IDisposable
{
    private bool _rendering;
    private IComObject<IDXGISwapChain2>? _swapChain;

    private SwapChainPanel SwapChainPanel { get; }

    private uint PanelWidth => Math.Max(1, (uint)Math.Ceiling(SwapChainPanel.ActualWidth * SwapChainPanel.CompositionScaleX));
    private uint PanelHeight => Math.Max(1, (uint)Math.Ceiling(SwapChainPanel.ActualHeight * SwapChainPanel.CompositionScaleY));

    public SwapChainSurface(SwapChainPanel swapChainPanel, Action onResize)
    {
        SwapChainPanel = swapChainPanel;
        Initialize();

        void ResizeBuffers()
        {
            try
            {
                OnSizeChanged();
                onResize();
            }
            catch (ObjectDisposedException)
            {
                Reinitialize();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("\nException: " + ex, nameof(SwapChainSurface) + '.' + nameof(ResizeBuffers));
            }
        }

        SwapChainPanel.SizeChanged += (_, _) => ResizeBuffers();
        SwapChainPanel.CompositionScaleChanged += (_, _) => ResizeBuffers();
    }

    private void Reinitialize()
    {
        Dispose();
        Initialize();
    }

    private void Initialize()
    {
        var featureLevels = new[] { D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_0 };
        var flags = D3D11_CREATE_DEVICE_FLAG.D3D11_CREATE_DEVICE_BGRA_SUPPORT;

        using var d3dDevice = D3D11Functions.D3D11CreateDevice(null, D3D_DRIVER_TYPE.D3D_DRIVER_TYPE_HARDWARE, flags, featureLevels);

        var swapChainDescription = new DXGI_SWAP_CHAIN_DESC1
        {
            Width = PanelWidth,
            Height = PanelHeight,
            Format = DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM,
            Stereo = false,
            SampleDesc = new DXGI_SAMPLE_DESC { Count = 1, Quality = 0 },
            BufferUsage = Constants.DXGI_USAGE_RENDER_TARGET_OUTPUT,
            BufferCount = 2,
            Scaling = DXGI_SCALING.DXGI_SCALING_STRETCH,
            SwapEffect = DXGI_SWAP_EFFECT.DXGI_SWAP_EFFECT_FLIP_SEQUENTIAL,
            AlphaMode = DXGI_ALPHA_MODE.DXGI_ALPHA_MODE_PREMULTIPLIED,
            Flags = 0
        };

        var dxgiDevice = d3dDevice.As<IDXGIDevice>();
        using var dxgiFactory = dxgiDevice.GetAdapter().GetParent<IDXGIFactory2>();
        _swapChain = dxgiFactory.CreateSwapChainForComposition<IDXGISwapChain2>(d3dDevice, swapChainDescription);

        SetSwapChain(false);
        OnSizeChanged(true);
    }

    public void Dispose()
    {
        SetSwapChain(true);
        if (_swapChain is not null && !_swapChain.IsDisposed) _swapChain.Dispose();
        _swapChain = null;
    }

    private void SetSwapChain(bool toNull)
    {
#if WINDOWS_UWP
        // ReSharper disable once SuspiciousTypeConversion.Global
        var nativePanel = (ISwapChainPanelNative)SwapChainPanel;
#else
        var guidNativePanel = typeof(ISwapChainPanelNative).GUID;
        var swapChainPanelPtr = Marshal.GetIUnknownForObject(SwapChainPanel);
        new HRESULT(Marshal.QueryInterface(swapChainPanelPtr, ref guidNativePanel, out var pNativePanel)).ThrowOnError();
        Marshal.Release(swapChainPanelPtr);
        var nativePanel = (ISwapChainPanelNative)Marshal.GetObjectForIUnknown(pNativePanel);
#endif
        nativePanel.SetSwapChain(toNull ? null : _swapChain?.Object).ThrowOnError();
#if !WINDOWS_UWP
        Marshal.ReleaseComObject(nativePanel);
#endif
    }

    public void OnNewSurfaceAvailable(Action<IDirect3DSurface> updateSurface)
    {
        if (_rendering)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Warning: Still rendering the previous frame, so skipping this one", nameof(SwapChainSurface) + '.' + nameof(OnNewSurfaceAvailable));
#endif
            return;
        }
        _rendering = true;
#if WINDOWS_UWP
        _ = SwapChainPanel.Dispatcher?.TryRunAsync(CoreDispatcherPriority.Normal, () =>
#else
        SwapChainPanel.DispatcherQueue?.TryEnqueue(() =>
#endif
        {
            try
            {
                if (_swapChain is null) return;
                var swapChain = _swapChain.Object;
                swapChain.GetDesc(out var desc).ThrowOnError();
                if (desc.Width != PanelWidth || desc.Height != PanelHeight)
                    OnSizeChanged();

                using var dxgiSurface = swapChain.GetBuffer<IDXGISurface>(0);
                var dxgiSurfacePtr = Marshal.GetComInterfaceForObject<IDXGISurface, IDXGISurface>(dxgiSurface.Object);
                CreateDirect3D11SurfaceFromDXGISurface(dxgiSurfacePtr, out var graphicsSurface).ThrowOnError();
                Marshal.Release(dxgiSurfacePtr);
#if WINDOWS_UWP
                using var d3DSurface = (IDirect3DSurface)Marshal.GetObjectForIUnknown(graphicsSurface)!;
#else
                using var d3DSurface = WinRT.MarshalInterface<IDirect3DSurface>.FromAbi(graphicsSurface)!;
#endif
                Marshal.Release(graphicsSurface);

                updateSurface(d3DSurface);

                swapChain.Present(1, 0).ThrowOnError();
            }
            catch (ObjectDisposedException)
            {
                Reinitialize();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("\nException: " + ex, nameof(SwapChainSurface) + '.' + nameof(OnNewSurfaceAvailable));
            }
            _rendering = false;
        });
    }

    private void OnSizeChanged(bool setMatrixTransformOnly = false)
    {
        DXGI_MATRIX_3X2_F inverseScale = new()
        {
            _11 = 1.0f / SwapChainPanel.CompositionScaleX,
            _22 = 1.0f / SwapChainPanel.CompositionScaleY
        };
        _swapChain?.Object.SetMatrixTransform(inverseScale).ThrowOnError(); // Cancel out the scaling
        if (!setMatrixTransformOnly)
            _swapChain?.Object.ResizeBuffers(2, PanelWidth, PanelHeight, DXGI_FORMAT.DXGI_FORMAT_UNKNOWN, 0).ThrowOnError();
    }

    [DllImport("d3d11.dll", EntryPoint = nameof(CreateDirect3D11SurfaceFromDXGISurface), SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    private static extern HRESULT CreateDirect3D11SurfaceFromDXGISurface(IntPtr dxgiSurface, out IntPtr graphicsSurface);
}