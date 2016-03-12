
namespace LEDControl
{

    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    public class TVicPort
    {

        // - Ide Hdd hardware manufacturer info structure ---

        [StructLayout(LayoutKind.Explicit)]
        public struct TypeHddInfo
        {
            [FieldOffset(0)]
            public uint BufferSize;
            [FieldOffset(4)]
            public uint DoubleTransfer;
            [FieldOffset(8)]
            public uint ControllerType;
            [FieldOffset(12)]
            public uint ECCMode;
            [FieldOffset(16)]
            public uint SectorsPerInterrupt;
            [FieldOffset(20)]
            public uint Cylinders;
            [FieldOffset(24)]
            public uint Heads;
            [FieldOffset(28)]
            public uint SectorsPerTrack;
            [FieldOffset(32), MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
            public String Model;
            [FieldOffset(80), MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
            public String SerialNumber;
            [FieldOffset(104), MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public String Revision;
        }

        //----------------------------------------------------------------------------
        [DllImport("TVicPort.dll", EntryPoint = "OpenTVicPort", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern unsafe uint OpenTVicPort();
        [DllImport("TVicPort.dll", EntryPoint = "CloseTVicPort", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void CloseTVicPort();
        [DllImport("TVicPort.dll", EntryPoint = "IsDriverOpened", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint IsDriverOpened();
        //----------------------------------------------------------------------------
        [DllImport("TVicPort.dll", EntryPoint = "ReadPort", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern byte ReadPort(ushort PortAddr);
        [DllImport("TVicPort.dll", EntryPoint = "ReadPortW", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern UInt16 ReadPortW(ushort PortAddr);
        [DllImport("TVicPort.dll", EntryPoint = "ReadPortL", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint ReadPortL(ushort PortAddr);
        [DllImport("TVicPort.dll", EntryPoint = "WritePort", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void WritePort(ushort PortAddr, byte bValue);
        [DllImport("TVicPort.dll", EntryPoint = "WritePortW", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void WritePortW(ushort PortAddr, UInt16 wValue);
        [DllImport("TVicPort.dll", EntryPoint = "WritePortL", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void WritePortL(ushort PortAddr, uint lValue);
        [DllImport("TVicPort.dll", EntryPoint = "SetHardAccess", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void SetHardAccess(uint newstate);
        [DllImport("TVicPort.dll", EntryPoint = "TestHardAccess", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint SetHardAccess();
        //----------------------------------------------------------------------------
        [DllImport("TVicPort.dll", EntryPoint = "ReadPortFIFO", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern unsafe void ReadPortFIFO(ushort PortAddr, ushort NumValues, byte* Buffer);
        [DllImport("TVicPort.dll", EntryPoint = "ReadPortWFIFO", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern unsafe void ReadPortWFIFO(ushort PortAddr, ushort NumValues, ushort* Buffer);
        [DllImport("TVicPort.dll", EntryPoint = "ReadPortLFIFO", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern unsafe void ReadPortLFIFO(ushort PortAddr, ushort NumValues, uint* Buffer);
        [DllImport("TVicPort.dll", EntryPoint = "WritePortFIFO", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern unsafe void WritePortFIFO(ushort PortAddr, ushort NumValues, byte* Buffer);
        [DllImport("TVicPort.dll", EntryPoint = "WritePortWFIFO", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern unsafe void WritePortWFIFO(ushort PortAddr, ushort NumValues, ushort* Buffer);
        [DllImport("TVicPort.dll", EntryPoint = "WritePortLFIFO", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern unsafe void WritePortLFIFO(ushort PortAddr, ushort NumValues, uint* Buffer);

        //----------------------------------------------------------------------------
        [DllImport("TVicPort.dll", EntryPoint = "GetHDDInfoVb", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void GetHDDInfo(UInt16 IdeNumber, UInt16 Master, ref TypeHddInfo Rec);

        //----------------------------------------------------------------------------
        [DllImport("TVicPort.dll", EntryPoint = "GetLPTBasePort", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern ushort GetLPTBasePort();
        [DllImport("TVicPort.dll", EntryPoint = "GetLPTNumPorts", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern byte GetLPTNumPorts();
        [DllImport("TVicPort.dll", EntryPoint = "SetLPTNumber", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void SetLPTNumber(ushort LptN);
        [DllImport("TVicPort.dll", EntryPoint = "GetLPTNumber", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern ushort GetLPTNumber();
        [DllImport("TVicPort.dll", EntryPoint = "GetPin", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint GetPin(byte nPin);
        [DllImport("TVicPort.dll", EntryPoint = "SetPin", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void SetPin(byte nPin, uint State);
        //----------------------------------------------------------------------------
        [DllImport("TVicPort.dll", EntryPoint = "GetLPTAckwl", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint GetLPTAckwl();
        [DllImport("TVicPort.dll", EntryPoint = "GetLPTBusy", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint GetLPTBusy();
        [DllImport("TVicPort.dll", EntryPoint = "GetLPTError", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint GetLPTError();
        [DllImport("TVicPort.dll", EntryPoint = "GetLPTPaperEnd", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint GetLPTPaperEnd();
        [DllImport("TVicPort.dll", EntryPoint = "GetLPTSlct", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint GetLPTSlct();
        //----------------------------------------------------------------------------
        [DllImport("TVicPort.dll", EntryPoint = "MapPhysToLinear", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern unsafe void* MapPhysToLinear(uint PhysAddr, uint Len);
        [DllImport("TVicPort.dll", EntryPoint = "UnmapMemory", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void UnmapMemory(uint PhysAddr, uint Len);

        [DllImport("TVicPort.dll", EntryPoint = "LaunchWeb", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void LaunchWeb();
        [DllImport("TVicPort.dll", EntryPoint = "LaunchMail", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void LaunchMail();
        [DllImport("TVicPort.dll", EntryPoint = "EvaluationDaysLeft", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int EvaluationDaysLeft();

    }
}

