using System;
using System.Management.Automation;
using Microsoft.Management.Infrastructure;
using PSMore.Formatting;
using PSMore.FormattingAttributes;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
#pragma warning disable CS0169 // Ignore internal fields never assigned
#pragma warning disable CS0649 // Ignore internal fields never assigned

namespace PSMore.DefaultFormats
{
    internal class CimInstanceBindingRestrictions
    {
        public static bool Applies(object o, string className, string ns)
        {
            if (o is PSObject psObj) o = psObj.BaseObject;
            return o is CimInstance cimInstance
                && string.Equals(cimInstance.CimSystemProperties.ClassName, className, StringComparison.Ordinal)
                && string.Equals(cimInstance.CimSystemProperties.Namespace, ns, StringComparison.Ordinal);
        }
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_VolumeQuotaSetting_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_VolumeQuotaSetting", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Element;
        [DisplayProperty(Position = 1)] public object Setting;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_BaseBoard_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_BaseBoard", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Manufacturer;
        [DisplayProperty(Position = 1)] public object Model;
        [DisplayProperty(Position = 2)] public object Name;
        [DisplayProperty(Position = 3)] public object SerialNumber;
        [DisplayProperty(Position = 4)] public object SKU;
        [DisplayProperty(Position = 5)] public object Product;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_SystemEnclosure_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_SystemEnclosure", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Manufacturer;
        [DisplayProperty(Position = 1)] public object Model;
        [DisplayProperty(Position = 2)] public object LockPresent;
        [DisplayProperty(Position = 3)] public object SerialNumber;
        [DisplayProperty(Position = 4)] public object SMBIOSAssetTag;
        [DisplayProperty(Position = 5)] public object SecurityStatus;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_NTDomain_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_NTDomain", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object ClientSiteName;
        [DisplayProperty(Position = 1)] public object DcSiteName;
        [DisplayProperty(Position = 2)] public object Description;
        [DisplayProperty(Position = 3)] public object DnsForestName;
        [DisplayProperty(Position = 4)] public object DomainControllerAddress;
        [DisplayProperty(Position = 5)] public object DomainControllerName;
        [DisplayProperty(Position = 6)] public object DomainName;
        [DisplayProperty(Position = 7)] public object Roles;
        [DisplayProperty(Position = 8)] public object Status;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_NetworkLoginProfile_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_NetworkLoginProfile", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Caption;
        [DisplayProperty(Position = 1)] public object Privileges;
        [DisplayProperty(Position = 2)] public object Profile;
        [DisplayProperty(Position = 3)] public object UserId;
        [DisplayProperty(Position = 4)] public object UserType;
        [DisplayProperty(Position = 5)] public object Workstations;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_ComputerSystem_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_ComputerSystem", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Domain;
        [DisplayProperty(Position = 1)] public object Manufacturer;
        [DisplayProperty(Position = 2)] public object Model;
        [DisplayProperty(Position = 3)] public object Name;
        [DisplayProperty(Position = 4)] public object PrimaryOwnerName;
        [DisplayProperty(Position = 5)] public object TotalPhysicalMemory;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_TimeZone_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_TimeZone", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Bias;
        [DisplayProperty(Position = 1)] public object SettingID;
        [DisplayProperty(Position = 2)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_DiskQuota_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_DiskQuota", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object DiskSpaceUsed;
        [DisplayProperty(Position = 1)] public object Limit;
        [DisplayProperty(Position = 2)] public object QuotaVolume;
        [DisplayProperty(Position = 3)] public object User;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_SoftwareFeature_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_SoftwareFeature", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Caption;
        [DisplayProperty(Position = 1)] public object IdentifyingNumber;
        [DisplayProperty(Position = 2)] public object ProductName;
        [DisplayProperty(Position = 3)] public object Vendor;
        [DisplayProperty(Position = 4)] public object Version;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_Printer_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_Printer", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Location;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object PrinterState;
        [DisplayProperty(Position = 3)] public object PrinterStatus;
        [DisplayProperty(Position = 4)] public object ShareName;
        [DisplayProperty(Position = 5)] public object SystemName;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_VoltageProbe_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_VoltageProbe", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Status;
        [DisplayProperty(Position = 1)] public object Description;
        [DisplayProperty(Position = 2)] public object CurrentReading;
        [DisplayProperty(Position = 3)] public object MaxReadable;
        [DisplayProperty(Position = 4)] public object MinReadable;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_TapeDrive_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_TapeDrive", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object DeviceID;
        [DisplayProperty(Position = 1)] public object Id;
        [DisplayProperty(Position = 2)] public object Manufacturer;
        [DisplayProperty(Position = 3)] public object Name;
        [DisplayProperty(Position = 4)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_PortResource_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_PortResource", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Caption;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object Alias;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_LoadOrderGroup_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_LoadOrderGroup", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object GroupOrder;
        [DisplayProperty(Position = 1)] public object Name;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_WIN32_SoundDevice_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "WIN32_SoundDevice", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Manufacturer;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object Status;
        [DisplayProperty(Position = 3)] public object StatusInfo;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_BIOS_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_BIOS", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object SMBIOSBIOSVersion;
        [DisplayProperty(Position = 1)] public object Manufacturer;
        [DisplayProperty(Position = 2)] public object Name;
        [DisplayProperty(Position = 3)] public object SerialNumber;
        [DisplayProperty(Position = 4)] public object Version;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_WIN32_DESKTOPMONITOR_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "WIN32_DESKTOPMONITOR", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object DeviceID;
        [DisplayProperty(Position = 1)] public object DisplayType;
        [DisplayProperty(Position = 2)] public object MonitorManufacturer;
        [DisplayProperty(Position = 3)] public object Name;
        [DisplayProperty(Position = 4)] public object ScreenHeight;
        [DisplayProperty(Position = 5)] public object ScreenWidth;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_ScheduledJob_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_ScheduledJob", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object JobId;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object Owner;
        [DisplayProperty(Position = 3)] public object Priority;
        [DisplayProperty(Position = 4)] public object Command;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_NetworkProtocol_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_NetworkProtocol", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Caption;
        [DisplayProperty(Position = 1)] public object GuaranteesDelivery;
        [DisplayProperty(Position = 2)] public object GuaranteesSequencing;
        [DisplayProperty(Position = 3)] public object ConnectionlessService;
        [DisplayProperty(Position = 4)] public object Status;
        [DisplayProperty(Position = 5)] public object Name;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_WIN32_PROCESSOR_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "WIN32_PROCESSOR", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Caption;
        [DisplayProperty(Position = 1)] public object DeviceID;
        [DisplayProperty(Position = 2)] public object Manufacturer;
        [DisplayProperty(Position = 3)] public object MaxClockSpeed;
        [DisplayProperty(Position = 4)] public object Name;
        [DisplayProperty(Position = 5)] public object SocketDesignation;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_LogicalDisk_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_LogicalDisk", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object DeviceID;
        [DisplayProperty(Position = 1)] public object DriveType;
        [DisplayProperty(Position = 2)] public object ProviderName;
        [DisplayProperty(Position = 3)] public object FreeSpace;
        [DisplayProperty(Position = 4)] public object Size;
        [DisplayProperty(Position = 5)] public object VolumeName;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_WMISetting_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_WMISetting", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object BuildVersion;
        [DisplayProperty(Position = 1)] public object Caption;
        [DisplayProperty(Position = 2)] public object DatabaseDirectory;
        [DisplayProperty(Position = 3)] public object EnableEvents;
        [DisplayProperty(Position = 4)] public object LoggingLevel;
        [DisplayProperty(Position = 5)] public object SettingID;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_PrinterConfiguration_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_PrinterConfiguration", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object PrintQuality;
        [DisplayProperty(Position = 1)] public object DriverVersion;
        [DisplayProperty(Position = 2)] public object Name;
        [DisplayProperty(Position = 3)] public object PaperSize;
        [DisplayProperty(Position = 4)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_PageFileUsage_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_PageFileUsage", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Caption;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object PeakUsage;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_PhysicalMemoryArray_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_PhysicalMemoryArray", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Model;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object MaxCapacity;
        [DisplayProperty(Position = 3)] public object MemoryDevices;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_QuotaSetting_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_QuotaSetting", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Caption;
        [DisplayProperty(Position = 1)] public object DefaultLimit;
        [DisplayProperty(Position = 2)] public object SettingID;
        [DisplayProperty(Position = 3)] public object State;
        [DisplayProperty(Position = 4)] public object VolumePath;
        [DisplayProperty(Position = 5)] public object DefaultWarningLimit;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Msft_CliAlias_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Msft_CliAlias", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object FriendlyName;
        [DisplayProperty(Position = 1)] public object PWhere;
        [DisplayProperty(Position = 2)] public object Target;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_DMAChannel_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_DMAChannel", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object AddressSize;
        [DisplayProperty(Position = 1)] public object DMAChannel;
        [DisplayProperty(Position = 2)] public object MaxTransferSize;
        [DisplayProperty(Position = 3)] public object Name;
        [DisplayProperty(Position = 4)] public object Port;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_NTLogEvent_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_NTLogEvent", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Category;
        [DisplayProperty(Position = 1)] public object CategoryString;
        [DisplayProperty(Position = 2)] public object EventCode;
        [DisplayProperty(Position = 3)] public object EventIdentifier;
        [DisplayProperty(Position = 4)] public object TypeEvent;
        [DisplayProperty(Position = 5)] public object InsertionStrings;
        [DisplayProperty(Position = 6)] public object LogFile;
        [DisplayProperty(Position = 7)] public object Message;
        [DisplayProperty(Position = 8)] public object RecordNumber;
        [DisplayProperty(Position = 9)] public object SourceName;
        [DisplayProperty(Position = 10)] public object TimeGenerated;
        [DisplayProperty(Position = 11)] public object TimeWritten;
        [DisplayProperty(Position = 12)] public object Type;
        [DisplayProperty(Position = 13)] public object UserName;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_TemperatureProbe_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_TemperatureProbe", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object CurrentReading;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object Description;
        [DisplayProperty(Position = 3)] public object MinReadable;
        [DisplayProperty(Position = 4)] public object MaxReadable;
        [DisplayProperty(Position = 5)] public object Status;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_IRQResource_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_IRQResource", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Hardware;
        [DisplayProperty(Position = 1)] public object IRQNumber;
        [DisplayProperty(Position = 2)] public object Name;
        [DisplayProperty(Position = 3)] public object Shareable;
        [DisplayProperty(Position = 4)] public object TriggerLevel;
        [DisplayProperty(Position = 5)] public object TriggerType;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_OnBoardDevice_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_OnBoardDevice", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object DeviceType;
        [DisplayProperty(Position = 1)] public object SerialNumber;
        [DisplayProperty(Position = 2)] public object Enabled;
        [DisplayProperty(Position = 3)] public object Description;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_SystemSlot_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_SystemSlot", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object SlotDesignation;
        [DisplayProperty(Position = 1)] public object Tag;
        [DisplayProperty(Position = 2)] public object SupportsHotPlug;
        [DisplayProperty(Position = 3)] public object Status;
        [DisplayProperty(Position = 4)] public object Shared;
        [DisplayProperty(Position = 5)] public object PMESignal;
        [DisplayProperty(Position = 6)] public object MaxDataWidth;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_LogonSession_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_LogonSession", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object AuthenticationPackage;
        [DisplayProperty(Position = 1)] public object LogonId;
        [DisplayProperty(Position = 2)] public object LogonType;
        [DisplayProperty(Position = 3)] public object Name;
        [DisplayProperty(Position = 4)] public object StartTime;
        [DisplayProperty(Position = 5)] public object Status;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_PerfRawData_PerfNet_Server_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_PerfRawData_PerfNet_Server", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Caption;
        [DisplayProperty(Position = 1)] public object LogonPerSec;
        [DisplayProperty(Position = 2)] public object LogonTotal;
        [DisplayProperty(Position = 3)] public object Name;
        [DisplayProperty(Position = 4)] public object ServerSessions;
        [DisplayProperty(Position = 5)] public object WorkItemShortages;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_SystemDriver_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_SystemDriver", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object DisplayName;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object State;
        [DisplayProperty(Position = 3)] public object Status;
        [DisplayProperty(Position = 4)] public object Started;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_Group_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_Group", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Caption;
        [DisplayProperty(Position = 1)] public object Domain;
        [DisplayProperty(Position = 2)] public object Name;
        [DisplayProperty(Position = 3)] public object SID;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_Share_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_Share", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Name;
        [DisplayProperty(Position = 1)] public object Path;
        [DisplayProperty(Position = 2)] public object Description;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_StartupCommand_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_StartupCommand", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Command;
        [DisplayProperty(Position = 1)] public object User;
        [DisplayProperty(Position = 2)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_WIN32_NetworkClient_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "WIN32_NetworkClient", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Caption;
        [DisplayProperty(Position = 1)] public object InstallDate;
        [DisplayProperty(Position = 2)] public object Manufacturer;
        [DisplayProperty(Position = 3)] public object Name;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_QuickFixEngineering_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_QuickFixEngineering", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Description;
        [DisplayProperty(Position = 1)] public object FixComments;
        [DisplayProperty(Position = 2)] public object HotFixID;
        [DisplayProperty(Position = 3)] public object InstallDate;
        [DisplayProperty(Position = 4)] public object InstalledBy;
        [DisplayProperty(Position = 5)] public object InstalledOn;
        [DisplayProperty(Position = 6)] public object Name;
        [DisplayProperty(Position = 7)] public object ServicePackInEffect;
        [DisplayProperty(Position = 8)] public object Status;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_SCSIController_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_SCSIController", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object DriverName;
        [DisplayProperty(Position = 1)] public object Manufacturer;
        [DisplayProperty(Position = 2)] public object Name;
        [DisplayProperty(Position = 3)] public object ProtocolSupported;
        [DisplayProperty(Position = 4)] public object Status;
        [DisplayProperty(Position = 5)] public object StatusInfo;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_UninterruptiblePowerSupply_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_UninterruptiblePowerSupply", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object DeviceID;
        [DisplayProperty(Position = 1)] public object EstimatedRunTime;
        [DisplayProperty(Position = 2)] public object Name;
        [DisplayProperty(Position = 3)] public object TimeOnBackup;
        [DisplayProperty(Position = 4)] public object UPSPort;
        [DisplayProperty(Position = 5)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_DeviceMemoryAddress_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_DeviceMemoryAddress", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object MemoryType;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object Status;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_OperatingSystem_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_OperatingSystem", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object SystemDirectory;
        [DisplayProperty(Position = 1)] public object Organization;
        [DisplayProperty(Position = 2)] public object BuildNumber;
        [DisplayProperty(Position = 3)] public object RegisteredUser;
        [DisplayProperty(Position = 4)] public object SerialNumber;
        [DisplayProperty(Position = 5)] public object Version;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_ComputerSystemProduct_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_ComputerSystemProduct", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object IdentifyingNumber;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object Vendor;
        [DisplayProperty(Position = 3)] public object Version;
        [DisplayProperty(Position = 4)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_LogicalMemoryConfiguration_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_LogicalMemoryConfiguration", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Name;
        [DisplayProperty(Position = 1)] public object TotalVirtualMemory;
        [DisplayProperty(Position = 2)] public object TotalPhysicalMemory;
        [DisplayProperty(Position = 3)] public object TotalPageFileSpace;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_NetworkAdapterConfiguration_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_NetworkAdapterConfiguration", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object DHCPEnabled;
        [DisplayProperty(Position = 1)] public object IPAddress;
        [DisplayProperty(Position = 2)] public object DefaultIPGateway;
        [DisplayProperty(Position = 3)] public object DNSDomain;
        [DisplayProperty(Position = 4)] public object ServiceName;
        [DisplayProperty(Position = 5)] public object Description;
        [DisplayProperty(Position = 6)] public object Index;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_NetworkAdapter_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_NetworkAdapter", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object ServiceName;
        [DisplayProperty(Position = 1)] public object MACAddress;
        [DisplayProperty(Position = 2)] public object AdapterType;
        [DisplayProperty(Position = 3)] public object DeviceID;
        [DisplayProperty(Position = 4)] public object Name;
        [DisplayProperty(Position = 5)] public object NetworkAddresses;
        [DisplayProperty(Position = 6)] public object Speed;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_WIN32_CACHEMEMORY_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "WIN32_CACHEMEMORY", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object BlockSize;
        [DisplayProperty(Position = 1)] public object CacheSpeed;
        [DisplayProperty(Position = 2)] public object CacheType;
        [DisplayProperty(Position = 3)] public object DeviceID;
        [DisplayProperty(Position = 4)] public object InstalledSize;
        [DisplayProperty(Position = 5)] public object Level;
        [DisplayProperty(Position = 6)] public object MaxCacheSize;
        [DisplayProperty(Position = 7)] public object NumberOfBlocks;
        [DisplayProperty(Position = 8)] public object Status;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_SoftwareElement_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_SoftwareElement", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Caption;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object Path;
        [DisplayProperty(Position = 3)] public object SerialNumber;
        [DisplayProperty(Position = 4)] public object SoftwareElementID;
        [DisplayProperty(Position = 5)] public object Version;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_NetworkConnection_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_NetworkConnection", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object LocalName;
        [DisplayProperty(Position = 1)] public object RemoteName;
        [DisplayProperty(Position = 2)] public object ConnectionState;
        [DisplayProperty(Position = 3)] public object Status;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_ProcessXXX_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_ProcessXXX", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object ThreadCount;
        [DisplayProperty(Position = 1)] public object HandleCount;
        [DisplayProperty(Position = 2)] public object Name;
        [DisplayProperty(Position = 3)] public object Priority;
        [DisplayProperty(Position = 4)] public object ProcessId;
        [DisplayProperty(Position = 5)] public object WorkingSetSize;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_Product_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_Product", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object IdentifyingNumber;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object Vendor;
        [DisplayProperty(Position = 3)] public object Version;
        [DisplayProperty(Position = 4)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_UserAccount_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_UserAccount", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object AccountType;
        [DisplayProperty(Position = 1)] public object Caption;
        [DisplayProperty(Position = 2)] public object Domain;
        [DisplayProperty(Position = 3)] public object SID;
        [DisplayProperty(Position = 4)] public object FullName;
        [DisplayProperty(Position = 5)] public object Name;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_Process_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_Process", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object ProcessId;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object HandleCount;
        [DisplayProperty(Position = 3)] public object WorkingSetSize;
        [DisplayProperty(Position = 4)] public object VirtualSize;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_Service_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_Service", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object ExitCode;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object ProcessId;
        [DisplayProperty(Position = 3)] public object StartMode;
        [DisplayProperty(Position = 4)] public object State;
        [DisplayProperty(Position = 5)] public object Status;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_CDROMDrive_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_CDROMDrive", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Caption;
        [DisplayProperty(Position = 1)] public object Drive;
        [DisplayProperty(Position = 2)] public object Manufacturer;
        [DisplayProperty(Position = 3)] public object VolumeName;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_DiskDrive_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_DiskDrive", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Partitions;
        [DisplayProperty(Position = 1)] public object DeviceID;
        [DisplayProperty(Position = 2)] public object Model;
        [DisplayProperty(Position = 3)] public object Size;
        [DisplayProperty(Position = 4)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_Environment_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_Environment", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object VariableValue;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object UserName;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_Registry_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_Registry", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object CurrentSize;
        [DisplayProperty(Position = 1)] public object MaximumSize;
        [DisplayProperty(Position = 2)] public object Name;
        [DisplayProperty(Position = 3)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_PortConnector_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_PortConnector", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Tag;
        [DisplayProperty(Position = 1)] public object ConnectorType;
        [DisplayProperty(Position = 2)] public object SerialNumber;
        [DisplayProperty(Position = 3)] public object ExternalReferenceDesignator;
        [DisplayProperty(Position = 4)] public object PortType;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_DiskPartition_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_DiskPartition", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object NumberOfBlocks;
        [DisplayProperty(Position = 1)] public object BootPartition;
        [DisplayProperty(Position = 2)] public object Name;
        [DisplayProperty(Position = 3)] public object PrimaryPartition;
        [DisplayProperty(Position = 4)] public object Size;
        [DisplayProperty(Position = 5)] public object Index;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_IDEController_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_IDEController", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Manufacturer;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object ProtocolSupported;
        [DisplayProperty(Position = 3)] public object Status;
        [DisplayProperty(Position = 4)] public object StatusInfo;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_Directory_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_Directory", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Hidden;
        [DisplayProperty(Position = 1)] public object Archive;
        [DisplayProperty(Position = 2)] public object EightDotThreeFileName;
        [DisplayProperty(Position = 3)] public object FileSize;
        [DisplayProperty(Position = 4)] public object Name;
        [DisplayProperty(Position = 5)] public object Compressed;
        [DisplayProperty(Position = 6)] public object Encrypted;
        [DisplayProperty(Position = 7)] public object Readable;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_WIN32_DCOMApplication_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "WIN32_DCOMApplication", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object AppID;
        [DisplayProperty(Position = 1)] public object InstallDate;
        [DisplayProperty(Position = 2)] public object Name;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_SystemAccount_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_SystemAccount", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Caption;
        [DisplayProperty(Position = 1)] public object Domain;
        [DisplayProperty(Position = 2)] public object Name;
        [DisplayProperty(Position = 3)] public object SID;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_CIM_DataFile_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "CIM_DataFile", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Compressed;
        [DisplayProperty(Position = 1)] public object Encrypted;
        [DisplayProperty(Position = 2)] public object Size;
        [DisplayProperty(Position = 3)] public object Hidden;
        [DisplayProperty(Position = 4)] public object Name;
        [DisplayProperty(Position = 5)] public object Readable;
        [DisplayProperty(Position = 6)] public object System;
        [DisplayProperty(Position = 7)] public object Version;
        [DisplayProperty(Position = 8)] public object Writeable;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_WIN32_DESKTOP_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "WIN32_DESKTOP", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Name;
        [DisplayProperty(Position = 1)] public object ScreenSaverActive;
        [DisplayProperty(Position = 2)] public object ScreenSaverSecure;
        [DisplayProperty(Position = 3)] public object ScreenSaverTimeout;
        [DisplayProperty(Position = 4)] public object SettingID;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_BootConfiguration_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_BootConfiguration", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object BootDirectory;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object SettingID;
        [DisplayProperty(Position = 3)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_PageFileSetting_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_PageFileSetting", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object MaximumSize;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_PrintJob_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_PrintJob", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object Document;
        [DisplayProperty(Position = 1)] public object JobId;
        [DisplayProperty(Position = 2)] public object JobStatus;
        [DisplayProperty(Position = 3)] public object Owner;
        [DisplayProperty(Position = 4)] public object Priority;
        [DisplayProperty(Position = 5)] public object Size;
        [DisplayProperty(Position = 6)] public object Name;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_NTEventlogFile_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_NTEventlogFile", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object FileSize;
        [DisplayProperty(Position = 1)] public object LogfileName;
        [DisplayProperty(Position = 2)] public object Name;
        [DisplayProperty(Position = 3)] public object NumberOfRecords;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_OSRecoveryConfiguration_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object obj)
                => CimInstanceBindingRestrictions.Applies(obj, "Win32_OSRecoveryConfiguration", "root/cimv2");
        }

        [DisplayProperty(Position = 0)] public object DebugFilePath;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object SettingID;
    }

}
