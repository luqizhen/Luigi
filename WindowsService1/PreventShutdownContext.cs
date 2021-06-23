using System;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using Vanara.PInvoke;
using static Vanara.PInvoke.User32;

namespace WindowsService1
{
    /// <summary>Used to define a set of operations within which any shutdown request will be met with a reason why this application is blocking it.</summary>
    /// <remarks>This is to be used in either a 'using' statement or for the life of the application.
    /// <para>To use for the life of the form, define a class field:
    public class PreventShutdownContext : IDisposable
    {
        private HandleRef href;

        /// <summary>Initializes a new instance of the <see cref="PreventShutdownContext"/> class.</summary>
        /// <param name="window">The <see cref="Form"/> or <see cref="Control"/> that contains a valid window handle.</param>
        /// <param name="reason">The reason the application must block system shutdown. Because users are typically in a hurry when shutting down the system, they may spend only a few seconds looking at the shutdown reasons that are displayed by the system. Therefore, it is important that your reason strings are short and clear.</param>
        public PreventShutdownContext(ServiceBase svc, IntPtr handle, string reason)
        {
            href = new HandleRef(svc, handle);
            Reason = reason;
        }

        /// <summary>The reason the application must block system shutdown. Because users are typically in a hurry when shutting down the system, they may spend only a few seconds looking at the shutdown reasons that are displayed by the system. Therefore, it is important that your reason strings are short and clear.</summary>
        /// <value>The reason string.</value>
        public string Reason
        {
            get
            {
                if (!ShutdownBlockReasonQuery(href.Handle, out var reason))
                    Win32Error.ThrowLastError();
                return reason;
            }
            set
            {
                if (value == null) value = string.Empty;
                if (ShutdownBlockReasonQuery(href.Handle, out var _))
                    ShutdownBlockReasonDestroy(href.Handle);
                if (!ShutdownBlockReasonCreate(href.Handle, value))
                    Win32Error.ThrowLastError();
            }
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            ShutdownBlockReasonDestroy(href.Handle);
        }
    }
}
