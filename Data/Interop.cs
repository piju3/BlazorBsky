using System.Runtime.InteropServices.JavaScript;
namespace BlazorBsky
{
    internal static partial class LocalStorage
    {
        //https://developer.mozilla.org/en-US/docs/Web/API/Storage
        //Should this be async? Can it be async?
        [JSImport("globalThis.localStorage.getItem")]
        public static partial string? GetItem(string key);
        [JSImport("globalThis.localStorage.setItem")]
        public static partial void SetItem(string key, string value);
    }

    /// <summary>
    /// This class will use <see href="https://developer.mozilla.org/en-US/docs/Web/API/Window/requestAnimationFrame">requestAnimationFrame</see>
    /// to call a method every render frame of the page. If the page is not visible, it will not be called.
    /// </summary>
    internal partial class AnimationFrameCallback : IDisposable
    {
        [JSImport("globalThis.requestAnimationFrame")]
        private static partial int RequestAnimationFrame([JSMarshalAs<JSType.Function<JSType.Number>>()] Action<double> method);

        private readonly Action<double> callback;
        private bool disposed = false;

        private void Handler(double timestamp)
        {
            callback(timestamp);
            if (!disposed)
            {
                _ = RequestAnimationFrame(Handler);
            }
        }
        public AnimationFrameCallback(Action<double> callback)
        {
            this.callback = callback;
            _ = RequestAnimationFrame(Handler);
        }
        //Remember to dispose of the object to remove the callback.
        public void Dispose()
        {
            disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}