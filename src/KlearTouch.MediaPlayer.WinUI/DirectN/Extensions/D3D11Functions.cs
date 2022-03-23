﻿using System;
using System.Runtime.InteropServices;

namespace DirectN
{
    internal static class D3D11Functions
    {
        public static IComObject<ID3D11Device> D3D11CreateDevice(
            IDXGIAdapter? adapter,
            D3D_DRIVER_TYPE driverType,
            D3D11_CREATE_DEVICE_FLAG flags,
            D3D_FEATURE_LEVEL[]? featureLevels = null,
            uint sdkVersion = Constants.D3D11_SDK_VERSION)
        {
            //featureLevels = featureLevels ?? new D3D_FEATURE_LEVEL[]
            //{
            //        D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_1,
            //        D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_0,
            //        D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_10_1,
            //        D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_10_0,
            //        D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_3,
            //        D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_2,
            //        D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_1
            //};

            D3D11CreateDevice(
                adapter,
                driverType,
                IntPtr.Zero,
                (uint)flags,
                featureLevels,
                (uint)(featureLevels?.Length).GetValueOrDefault(),
                sdkVersion,
                out ID3D11Device device,
                out _,
                out IntPtr dc).ThrowOnError();
            if (dc != IntPtr.Zero)
            {
                Marshal.Release(dc);
            }
            return new ComObject<ID3D11Device>(device);
        }

        [DllImport("d3d11", ExactSpelling = true)]
        public static extern HRESULT D3D11CreateDevice(IDXGIAdapter? pAdapter, D3D_DRIVER_TYPE DriverType, IntPtr Software, uint Flags, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] D3D_FEATURE_LEVEL[]? pFeatureLevels, uint FeatureLevels, uint SDKVersion, out ID3D11Device ppDevice, out D3D_FEATURE_LEVEL pFeatureLevel, out IntPtr ppImmediateContext);
    }
}