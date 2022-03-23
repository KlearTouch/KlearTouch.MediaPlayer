namespace DirectN
{
    internal static class IDXGIObjectExtensions
    {
        public static IComObject<T> GetParent<T>(this IComObject<IDXGIAdapter> surface) => GetParent<T>(surface.Object);
        public static IComObject<T> GetParent<T>(this IDXGIObject obj)
        {
            obj.GetParent(typeof(T).GUID, out var parent).ThrowOnError();
            return new ComObject<T>((T)parent);
        }
    }
}