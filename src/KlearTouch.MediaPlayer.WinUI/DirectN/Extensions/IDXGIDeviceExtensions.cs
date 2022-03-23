namespace DirectN
{
    internal static class IDXGIDeviceExtensions
    {
        public static IComObject<IDXGIAdapter> GetAdapter(this IComObject<IDXGIDevice> output) => GetAdapter(output.Object);
        public static IComObject<IDXGIAdapter> GetAdapter(this IDXGIDevice device)
        {
            device.GetAdapter(out var adapter).ThrowOnError();
            return new ComObject<IDXGIAdapter>(adapter);
        }
    }
}