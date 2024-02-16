using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class MouseController
{
    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out Rectangle rect);

    [DllImport("user32.dll")]
    private static extern bool PrintWindowRect(IntPtr hWnd, out Rectangle rect);

    [DllImport("user32.dll")]
    private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll")]
    private static extern bool GetExitCodeProcess(IntPtr hProcess, out int lpExitCode);

    [DllImport("kernel32.dll")]
    private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

    [StructLayout(LayoutKind.Sequential)]
    private struct Rectangle
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    public static void Click(int x, int y)
    {
        var process = Process.GetCurrentProcess();
        var mainWindow = process.MainWindowHandle;

        if (mainWindow == IntPtr.Zero)
        {
            throw new Exception("Could not find main window handle.");
        }

        GetWindowRect(mainWindow, out Rectangle rect);

        if (rect.Left < 0 || rect.Top < 0 || rect.Right > Screen.PrimaryScreen.Bounds.Width || rect.Bottom > Screen.PrimaryScreen.Bounds.Height)
        {
            throw new Exception("Main window is not visible.");
        }

        var centerX = rect.Left + rect.Width / 2;
        var centerY = rect.Top + rect.Height / 2;

        var moveToX = x - (centerX - rect.Left);
        var moveToY = y - (centerY - rect.Top);

        MoveWindow(mainWindow, moveToX, moveToY, 0, 0, true);
        ShowWindow(mainWindow, 3); // 3 = ShowWindowAsync

        // Wait for the window to be active
        var windowThreadProcessId = GetWindowThreadProcessId(mainWindow, out int _);
        var currentProcess = Process.GetCurrentProcess();
        while (currentProcess.ProcessId != windowThreadProcessId)
        {
            Thread.Sleep(100);
            currentProcess = Process.GetCurrentProcess();
        }

        // Click the window
        var mouseInput = new INPUT();
        mouseInput.Type = INPUT_MOUSE;
        mouseInput.Mouse.X = x;
        mouseInput.Mouse.Y = y;
        mouseInput.Mouse.Flags = MOUSEEVENTF_LEFTDOWN;
        SendInput(1, ref mouseInput, Marshal.SizeOf(typeof(INPUT)));

        // Wait for the click action to complete
        Thread.Sleep(100);

        // Release the mouse button
        mouseInput.Mouse.Flags = MOUSEEVENTF_LEFTUP;
        SendInput(1, ref mouseInput, Marshal.SizeOf(typeof(INPUT)));
    }

    public static void Move(int x, int y)
    {
        var process = Process.GetCurrentProcess();
        var mainWindow = process.MainWindowHandle;

        if (mainWindow == IntPtr.Zero)
        {
            throw new Exception("Could not find main window handle.");
        }

        GetWindowRect(mainWindow, out Rectangle rect);

        if (rect.Left < 0 || rect.Top < 0 || rect.Right > Screen.PrimaryScreen.Bounds.Width || rect.Bottom > Screen.PrimaryScreen.Bounds.Height)
        {
            throw new Exception("Main window is not visible.");
        }

        var centerX = rect.Left + rect.Width / 2;
        var centerY = rect.Top + rect.Height / 2;

        var moveToX = x - (centerX - rect.Left);
        var moveToY = y - (centerY - rect.Top);

        MoveWindow(mainWindow, moveToX, moveToY, 0, 0, true);
    }

    private const int INPUT_MOUSE = 0;
    private const int MOUSEEVENTF_LEFTDOWN = 0x0000;
    private const int MOUSEEVENTF_LEFTUP = 0x0004;

    [StructLayout(LayoutKind.Sequential)]
    private struct INPUT
    {
        public int Type;
        public MOUSEINPUT Mouse;
    }

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out Rectangle rect);

    [DllImport("user32.dll")]
    private static extern bool PrintWindowRect(IntPtr hWnd, out Rectangle rect);

    [DllImport("user32.dll")]
    private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll")]
    private static extern bool GetExitCodeProcess(IntPtr hProcess, out int lpExitCode);

    [DllImport("kernel32.dll")]
    private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

    private static IntPtr SendInput(int nInputs, ref INPUT pInputs, int cbSize)
    {
        return User32.SendInput(nInputs, ref pInputs, cbSize);
    }
}

public static class User32
{
    [DllImport("user32.dll")]
    public static extern IntPtr SendInput(int nInputs, ref INPUT pInputs, int cbSize);
}

public static class Process
{
    public static ProcessInfo GetCurrentProcess()
    {
        var process = Process.GetProcessesByName("CodeGeeX").FirstOrDefault();
        if (process == null)
        {
            throw new Exception("Could not find current process.");
        }
        return process;
    }

    public static ProcessInfo GetProcessesByName(string name)
    {
        var processes = new List<ProcessInfo>();
        var processInfo = new PROCESSENTRY32();
        processInfo.dwSize = Marshal.SizeOf(processInfo);

        var snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
        if (snapshot == IntPtr.Zero)
        {
            throw new Exception("Could not create toolhelp32 snapshot.");
        }

        var current = ProcessFirst(snapshot, ref processInfo);
        while (current != IntPtr.Zero)
        {
            var process = new ProcessInfo();
            process.ProcessName = processInfo.szExeFile;
            process.ProcessId = processInfo.th32ProcessID;
            processes.Add(process);

            current = ProcessNext(snapshot, ref processInfo);
        }

        CloseHandle(snapshot);
        return processes.FirstOrDefault(p => p.ProcessName.Contains(name));
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr CreateToolhelp32Snapshot(int dwFlags, uint th32ProcessID);

    [DllImport("kernel32.dll")]
    private static extern IntPtr ProcessFirst(IntPtr snapshot, ref PROCESSENTRY32 processInfo);

    [DllImport("kernel32.dll")]
    private static extern IntPtr ProcessNext(IntPtr snapshot, ref PROCESSENTRY32 processInfo);

    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr hObject);

    [StructLayout(LayoutKind.Sequential)]
    private struct PROCESSENTRY32
    {
        public int dwSize;
        public int th32ProcessID;
        public IntPtr hwnd;
        public int rgn;
        public string szExeFile;
        public int qty;
        public int flags;
    }
}

public class ProcessInfo
{
    public string ProcessName { get; set; }
    public int ProcessId { get; set; }
}

