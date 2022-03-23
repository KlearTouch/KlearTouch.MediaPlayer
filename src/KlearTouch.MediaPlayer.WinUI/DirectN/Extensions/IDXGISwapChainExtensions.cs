namespace DirectN
{
    internal static class IDXGISwapChainExtensions
    {
        public static IComObject<T> GetBuffer<T>(this IComObject<IDXGISwapChain> swapChain, uint index) => GetBuffer<T>(swapChain.Object, index);
        public static IComObject<T> GetBuffer<T>(this IDXGISwapChain swapChain, uint index)
        {
            swapChain.GetBuffer(index, typeof(T).GUID, out var dc).ThrowOnError();
            return new ComObject<T>((T)dc);
        }
    }
}