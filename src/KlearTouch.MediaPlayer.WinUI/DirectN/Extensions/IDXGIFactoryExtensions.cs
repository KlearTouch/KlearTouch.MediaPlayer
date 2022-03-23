namespace DirectN
{
    internal static class IDXGIFactoryExtensions
    {
        public static IComObject<T> CreateSwapChainForComposition<T>(this IComObject<IDXGIFactory2> factory,
            IComObject<ID3D11Device> device, DXGI_SWAP_CHAIN_DESC1 desc) where T : IDXGISwapChain1 => CreateSwapChainForComposition<T>(factory.Object, device.Object, desc);

        public static IComObject<T> CreateSwapChainForComposition<T>(this IDXGIFactory2 factory,
            ID3D11Device device, DXGI_SWAP_CHAIN_DESC1 desc) where T : IDXGISwapChain1
        {
            factory.CreateSwapChainForComposition(device, ref desc, null, out var swapChain).ThrowOnError();
            return new ComObject<T>((T)swapChain);
        }
    }
}