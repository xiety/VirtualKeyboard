using System.Runtime.InteropServices;

namespace VirtualKeyboard;

public static class WinApiHelper
{
    public static uint SendKeyPress(Keys keyEnum)
    {
        var key = (ushort)keyEnum;
        var scanCode = (ushort)MapVirtualKey(key, MAPVK_VK_TO_VSC);

        static INPUT CreateInput(ushort key, ushort scanCode, uint dwFlags)
            => new()
            {
                type = INPUT_KEYBOARD,
                u = new() { ki = new() { wVk = key, wScan = scanCode, dwFlags = dwFlags } }
            };

        INPUT[] inputs =
        [
            CreateInput(key, scanCode, KEYEVENTF_KEYDOWN),
            CreateInput(key, scanCode, KEYEVENTF_KEYUP),
        ];

        return SendInput((uint)inputs.Length, inputs, Marshal.SizeOf<INPUT>());
    }

    public static int SetNoActivateStyle(IntPtr handle)
    {
        var exStyle = GetWindowLong(handle, GWL_EXSTYLE);
        return SetWindowLong(handle, GWL_EXSTYLE, exStyle | WS_EX_NOACTIVATE);
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);

    [DllImport("user32.dll")]
    private static extern uint MapVirtualKey(uint uCode, uint uMapType);

    private const int GWL_EXSTYLE = -20;
    private const uint WS_EX_NOACTIVATE = 0x08000000;
    private const uint INPUT_KEYBOARD = 1;
    private const uint KEYEVENTF_KEYDOWN = 0x0000;
    private const uint KEYEVENTF_KEYUP = 0x0002;
    private const uint MAPVK_VK_TO_VSC = 0;

    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        public uint type;
        public InputUnion u;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct InputUnion
    {
        [FieldOffset(0)] public MOUSEINPUT mi;
        [FieldOffset(0)] public KEYBDINPUT ki;
        [FieldOffset(0)] public HARDWAREINPUT hi;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        public uint uMsg;
        public ushort wParamL;
        public ushort wParamH;
    }
}
