using System;
#if DEBUG
using System.Diagnostics;
#endif
using System.Runtime.InteropServices;
using System.Threading;

namespace DirectN
{
    internal class ComObject : IDisposable
    {
        private object? _object;

        public ComObject(object comObject)
        {
            if (comObject == null)
                throw new ArgumentNullException(nameof(comObject));

            if (!Marshal.IsComObject(comObject))
                throw new ArgumentException("Argument is not a COM object", nameof(comObject));

            _object = comObject;

#if DEBUG
            Id = Interlocked.Increment(ref _id);
            ConstructorThreadId = Environment.CurrentManagedThreadId;
            if (LogEnabled) Trace("+");
#endif
        }

        public bool IsDisposed => _object == null;
        public object Object
        {
            get
            {
                var obj = _object;
                if (obj == null)
                {
#if DEBUG
                    if (LogEnabled) Trace("!!!", "Already disposed");
#endif
                    throw new ObjectDisposedException(nameof(Object));
                }

                return obj;
            }
        }

        public T As<T>() where T : class
        {
            return (T)Object; // will throw
        }

        private void Dispose(bool disposing)
        {
            //#if DEBUG
            //            Trace("~", "disposing: " + disposing + " duration: " + Duration.Milliseconds);
            //#endif
            if (!IsDisposed)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                var obj = Interlocked.Exchange(ref _object, null);
                if (obj != null)
                {
#if DEBUG
                    //var typeName = GetType().FullName;
                    //if (typeName.IndexOf("textlayout", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    //    typeName.IndexOf("textformat", StringComparison.OrdinalIgnoreCase) >= 0)
                    //    return;

                    var count = Marshal.ReleaseComObject(obj);
                    if (LogEnabled) Trace("~", "disposing: " + disposing + " count: " + count);
#else
                    Marshal.ReleaseComObject(obj);
#endif

                }
            }
        }

        ~ComObject() { Dispose(false); }
        public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }

#if DEBUG
        private static bool LogEnabled => false;
        protected virtual string? ObjectTypeName => null;
        private static long _id;

        protected void Trace(string methodName, string? message = null)
        {
            // many COM objects (like DXGI ones) dont' like to be used on different threads
            // so we tracks calls on different threads
            string s = Id.ToString();

            var tid = Thread.CurrentThread.ManagedThreadId;
            if (tid != ConstructorThreadId)
            {
                s += "!" + ConstructorThreadId + "!";
            }

            var tn = ObjectTypeName;
            if (tn != null)
            {
                s += "<" + tn + ">";
            }

            if (message != null)
            {
                s += " " + message;
            }

            Debug.WriteLine(DateTime.Now.ToLongTimeString() + " - " + s, methodName);
        }

        public long Id { get; }
        public int ConstructorThreadId { get; }

        public override string ToString()
        {
            string? s = null;
            if (IsDisposed)
            {
                s = "<disposed>";
            }

            var ot = ObjectTypeName;
            if (ot != null)
            {
                if (s != null)
                {
                    s += " ";
                }
                s += ot;
            }

            if (s != null)
                return Id + " " + s;

            return Id.ToString();
        }
#endif
    }

    internal class ComObject<T> : ComObject, IComObject<T>
    {
        public ComObject(T comObject)
            : base(comObject!)
        {
        }

        public new T Object => (T)base.Object;

#if DEBUG
        protected override string ObjectTypeName => typeof(T).Name;
#endif

        //public static implicit operator ComObject<T>(T value) => new ComObject<T>(value);
        //public static implicit operator T(ComObject<T> value) => value.Object;
    }

    internal interface IComObject<out T> : IDisposable
    {
        T Object { get; }
        bool IsDisposed { get; }
        I As<I>() where I : class;
    }
}