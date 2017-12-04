using System;
using System.Management.Automation;
using PSMore.FormatAttributes;
using Microsoft.Management.Infrastructure;
using PSMore.Formatting;

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
            var cimInstance = o as CimInstance;
            return cimInstance != null
                && string.Equals(cimInstance.CimSystemProperties.ClassName, className, StringComparison.Ordinal)
                && string.Equals(cimInstance.CimSystemProperties.Namespace, ns, StringComparison.Ordinal);
        }
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_VolumeQuotaSetting_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_VolumeQuotaSetting", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Element;
        [DefaultDisplayProperty(1)] public object Setting;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_BaseBoard_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_BaseBoard", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Manufacturer;
        [DefaultDisplayProperty(1)] public object Model;
        [DefaultDisplayProperty(2)] public object Name;
        [DefaultDisplayProperty(3)] public object SerialNumber;
        [DefaultDisplayProperty(4)] public object SKU;
        [DefaultDisplayProperty(5)] public object Product;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_SystemEnclosure_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_SystemEnclosure", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Manufacturer;
        [DefaultDisplayProperty(1)] public object Model;
        [DefaultDisplayProperty(2)] public object LockPresent;
        [DefaultDisplayProperty(3)] public object SerialNumber;
        [DefaultDisplayProperty(4)] public object SMBIOSAssetTag;
        [DefaultDisplayProperty(5)] public object SecurityStatus;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_NTDomain_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_NTDomain", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object ClientSiteName;
        [DefaultDisplayProperty(1)] public object DcSiteName;
        [DefaultDisplayProperty(2)] public object Description;
        [DefaultDisplayProperty(3)] public object DnsForestName;
        [DefaultDisplayProperty(4)] public object DomainControllerAddress;
        [DefaultDisplayProperty(5)] public object DomainControllerName;
        [DefaultDisplayProperty(6)] public object DomainName;
        [DefaultDisplayProperty(7)] public object Roles;
        [DefaultDisplayProperty(8)] public object Status;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_NetworkLoginProfile_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_NetworkLoginProfile", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Caption;
        [DefaultDisplayProperty(1)] public object Privileges;
        [DefaultDisplayProperty(2)] public object Profile;
        [DefaultDisplayProperty(3)] public object UserId;
        [DefaultDisplayProperty(4)] public object UserType;
        [DefaultDisplayProperty(5)] public object Workstations;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_ComputerSystem_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_ComputerSystem", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Domain;
        [DefaultDisplayProperty(1)] public object Manufacturer;
        [DefaultDisplayProperty(2)] public object Model;
        [DefaultDisplayProperty(3)] public object Name;
        [DefaultDisplayProperty(4)] public object PrimaryOwnerName;
        [DefaultDisplayProperty(5)] public object TotalPhysicalMemory;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_TimeZone_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_TimeZone", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Bias;
        [DefaultDisplayProperty(1)] public object SettingID;
        [DefaultDisplayProperty(2)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_DiskQuota_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_DiskQuota", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object DiskSpaceUsed;
        [DefaultDisplayProperty(1)] public object Limit;
        [DefaultDisplayProperty(2)] public object QuotaVolume;
        [DefaultDisplayProperty(3)] public object User;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_SoftwareFeature_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_SoftwareFeature", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Caption;
        [DefaultDisplayProperty(1)] public object IdentifyingNumber;
        [DefaultDisplayProperty(2)] public object ProductName;
        [DefaultDisplayProperty(3)] public object Vendor;
        [DefaultDisplayProperty(4)] public object Version;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_Printer_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_Printer", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Location;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object PrinterState;
        [DefaultDisplayProperty(3)] public object PrinterStatus;
        [DefaultDisplayProperty(4)] public object ShareName;
        [DefaultDisplayProperty(5)] public object SystemName;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_VoltageProbe_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_VoltageProbe", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Status;
        [DefaultDisplayProperty(1)] public object Description;
        [DefaultDisplayProperty(2)] public object CurrentReading;
        [DefaultDisplayProperty(3)] public object MaxReadable;
        [DefaultDisplayProperty(4)] public object MinReadable;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_TapeDrive_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_TapeDrive", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object DeviceID;
        [DefaultDisplayProperty(1)] public object Id;
        [DefaultDisplayProperty(2)] public object Manufacturer;
        [DefaultDisplayProperty(3)] public object Name;
        [DefaultDisplayProperty(4)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_PortResource_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_PortResource", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Caption;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object Alias;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_LoadOrderGroup_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_LoadOrderGroup", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object GroupOrder;
        [DefaultDisplayProperty(1)] public object Name;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_WIN32_SoundDevice_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "WIN32_SoundDevice", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Manufacturer;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object Status;
        [DefaultDisplayProperty(3)] public object StatusInfo;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_BIOS_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_BIOS", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object SMBIOSBIOSVersion;
        [DefaultDisplayProperty(1)] public object Manufacturer;
        [DefaultDisplayProperty(2)] public object Name;
        [DefaultDisplayProperty(3)] public object SerialNumber;
        [DefaultDisplayProperty(4)] public object Version;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_WIN32_DESKTOPMONITOR_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "WIN32_DESKTOPMONITOR", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object DeviceID;
        [DefaultDisplayProperty(1)] public object DisplayType;
        [DefaultDisplayProperty(2)] public object MonitorManufacturer;
        [DefaultDisplayProperty(3)] public object Name;
        [DefaultDisplayProperty(4)] public object ScreenHeight;
        [DefaultDisplayProperty(5)] public object ScreenWidth;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_ScheduledJob_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_ScheduledJob", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object JobId;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object Owner;
        [DefaultDisplayProperty(3)] public object Priority;
        [DefaultDisplayProperty(4)] public object Command;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_NetworkProtocol_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_NetworkProtocol", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Caption;
        [DefaultDisplayProperty(1)] public object GuaranteesDelivery;
        [DefaultDisplayProperty(2)] public object GuaranteesSequencing;
        [DefaultDisplayProperty(3)] public object ConnectionlessService;
        [DefaultDisplayProperty(4)] public object Status;
        [DefaultDisplayProperty(5)] public object Name;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_WIN32_PROCESSOR_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "WIN32_PROCESSOR", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Caption;
        [DefaultDisplayProperty(1)] public object DeviceID;
        [DefaultDisplayProperty(2)] public object Manufacturer;
        [DefaultDisplayProperty(3)] public object MaxClockSpeed;
        [DefaultDisplayProperty(4)] public object Name;
        [DefaultDisplayProperty(5)] public object SocketDesignation;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_LogicalDisk_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_LogicalDisk", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object DeviceID;
        [DefaultDisplayProperty(1)] public object DriveType;
        [DefaultDisplayProperty(2)] public object ProviderName;
        [DefaultDisplayProperty(3)] public object FreeSpace;
        [DefaultDisplayProperty(4)] public object Size;
        [DefaultDisplayProperty(5)] public object VolumeName;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_WMISetting_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_WMISetting", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object BuildVersion;
        [DefaultDisplayProperty(1)] public object Caption;
        [DefaultDisplayProperty(2)] public object DatabaseDirectory;
        [DefaultDisplayProperty(3)] public object EnableEvents;
        [DefaultDisplayProperty(4)] public object LoggingLevel;
        [DefaultDisplayProperty(5)] public object SettingID;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_PrinterConfiguration_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_PrinterConfiguration", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object PrintQuality;
        [DefaultDisplayProperty(1)] public object DriverVersion;
        [DefaultDisplayProperty(2)] public object Name;
        [DefaultDisplayProperty(3)] public object PaperSize;
        [DefaultDisplayProperty(4)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_PageFileUsage_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_PageFileUsage", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Caption;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object PeakUsage;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_PhysicalMemoryArray_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_PhysicalMemoryArray", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Model;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object MaxCapacity;
        [DefaultDisplayProperty(3)] public object MemoryDevices;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_QuotaSetting_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_QuotaSetting", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Caption;
        [DefaultDisplayProperty(1)] public object DefaultLimit;
        [DefaultDisplayProperty(2)] public object SettingID;
        [DefaultDisplayProperty(3)] public object State;
        [DefaultDisplayProperty(4)] public object VolumePath;
        [DefaultDisplayProperty(5)] public object DefaultWarningLimit;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Msft_CliAlias_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Msft_CliAlias", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object FriendlyName;
        [DefaultDisplayProperty(1)] public object PWhere;
        [DefaultDisplayProperty(2)] public object Target;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_DMAChannel_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_DMAChannel", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object AddressSize;
        [DefaultDisplayProperty(1)] public object DMAChannel;
        [DefaultDisplayProperty(2)] public object MaxTransferSize;
        [DefaultDisplayProperty(3)] public object Name;
        [DefaultDisplayProperty(4)] public object Port;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_NTLogEvent_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_NTLogEvent", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Category;
        [DefaultDisplayProperty(1)] public object CategoryString;
        [DefaultDisplayProperty(2)] public object EventCode;
        [DefaultDisplayProperty(3)] public object EventIdentifier;
        [DefaultDisplayProperty(4)] public object TypeEvent;
        [DefaultDisplayProperty(5)] public object InsertionStrings;
        [DefaultDisplayProperty(6)] public object LogFile;
        [DefaultDisplayProperty(7)] public object Message;
        [DefaultDisplayProperty(8)] public object RecordNumber;
        [DefaultDisplayProperty(9)] public object SourceName;
        [DefaultDisplayProperty(10)] public object TimeGenerated;
        [DefaultDisplayProperty(11)] public object TimeWritten;
        [DefaultDisplayProperty(12)] public object Type;
        [DefaultDisplayProperty(13)] public object UserName;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_TemperatureProbe_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_TemperatureProbe", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object CurrentReading;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object Description;
        [DefaultDisplayProperty(3)] public object MinReadable;
        [DefaultDisplayProperty(4)] public object MaxReadable;
        [DefaultDisplayProperty(5)] public object Status;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_IRQResource_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_IRQResource", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Hardware;
        [DefaultDisplayProperty(1)] public object IRQNumber;
        [DefaultDisplayProperty(2)] public object Name;
        [DefaultDisplayProperty(3)] public object Shareable;
        [DefaultDisplayProperty(4)] public object TriggerLevel;
        [DefaultDisplayProperty(5)] public object TriggerType;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_OnBoardDevice_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_OnBoardDevice", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object DeviceType;
        [DefaultDisplayProperty(1)] public object SerialNumber;
        [DefaultDisplayProperty(2)] public object Enabled;
        [DefaultDisplayProperty(3)] public object Description;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_SystemSlot_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_SystemSlot", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object SlotDesignation;
        [DefaultDisplayProperty(1)] public object Tag;
        [DefaultDisplayProperty(2)] public object SupportsHotPlug;
        [DefaultDisplayProperty(3)] public object Status;
        [DefaultDisplayProperty(4)] public object Shared;
        [DefaultDisplayProperty(5)] public object PMESignal;
        [DefaultDisplayProperty(6)] public object MaxDataWidth;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_LogonSession_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_LogonSession", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object AuthenticationPackage;
        [DefaultDisplayProperty(1)] public object LogonId;
        [DefaultDisplayProperty(2)] public object LogonType;
        [DefaultDisplayProperty(3)] public object Name;
        [DefaultDisplayProperty(4)] public object StartTime;
        [DefaultDisplayProperty(5)] public object Status;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_PerfRawData_PerfNet_Server_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_PerfRawData_PerfNet_Server", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Caption;
        [DefaultDisplayProperty(1)] public object LogonPerSec;
        [DefaultDisplayProperty(2)] public object LogonTotal;
        [DefaultDisplayProperty(3)] public object Name;
        [DefaultDisplayProperty(4)] public object ServerSessions;
        [DefaultDisplayProperty(5)] public object WorkItemShortages;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_SystemDriver_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_SystemDriver", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object DisplayName;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object State;
        [DefaultDisplayProperty(3)] public object Status;
        [DefaultDisplayProperty(4)] public object Started;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_Group_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_Group", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Caption;
        [DefaultDisplayProperty(1)] public object Domain;
        [DefaultDisplayProperty(2)] public object Name;
        [DefaultDisplayProperty(3)] public object SID;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_Share_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_Share", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Name;
        [DefaultDisplayProperty(1)] public object Path;
        [DefaultDisplayProperty(2)] public object Description;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_StartupCommand_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_StartupCommand", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Command;
        [DefaultDisplayProperty(1)] public object User;
        [DefaultDisplayProperty(2)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_WIN32_NetworkClient_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "WIN32_NetworkClient", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Caption;
        [DefaultDisplayProperty(1)] public object InstallDate;
        [DefaultDisplayProperty(2)] public object Manufacturer;
        [DefaultDisplayProperty(3)] public object Name;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_QuickFixEngineering_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_QuickFixEngineering", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Description;
        [DefaultDisplayProperty(1)] public object FixComments;
        [DefaultDisplayProperty(2)] public object HotFixID;
        [DefaultDisplayProperty(3)] public object InstallDate;
        [DefaultDisplayProperty(4)] public object InstalledBy;
        [DefaultDisplayProperty(5)] public object InstalledOn;
        [DefaultDisplayProperty(6)] public object Name;
        [DefaultDisplayProperty(7)] public object ServicePackInEffect;
        [DefaultDisplayProperty(8)] public object Status;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_SCSIController_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_SCSIController", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object DriverName;
        [DefaultDisplayProperty(1)] public object Manufacturer;
        [DefaultDisplayProperty(2)] public object Name;
        [DefaultDisplayProperty(3)] public object ProtocolSupported;
        [DefaultDisplayProperty(4)] public object Status;
        [DefaultDisplayProperty(5)] public object StatusInfo;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_UninterruptiblePowerSupply_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_UninterruptiblePowerSupply", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object DeviceID;
        [DefaultDisplayProperty(1)] public object EstimatedRunTime;
        [DefaultDisplayProperty(2)] public object Name;
        [DefaultDisplayProperty(3)] public object TimeOnBackup;
        [DefaultDisplayProperty(4)] public object UPSPort;
        [DefaultDisplayProperty(5)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_DeviceMemoryAddress_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_DeviceMemoryAddress", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object MemoryType;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object Status;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_OperatingSystem_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_OperatingSystem", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object SystemDirectory;
        [DefaultDisplayProperty(1)] public object Organization;
        [DefaultDisplayProperty(2)] public object BuildNumber;
        [DefaultDisplayProperty(3)] public object RegisteredUser;
        [DefaultDisplayProperty(4)] public object SerialNumber;
        [DefaultDisplayProperty(5)] public object Version;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_ComputerSystemProduct_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_ComputerSystemProduct", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object IdentifyingNumber;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object Vendor;
        [DefaultDisplayProperty(3)] public object Version;
        [DefaultDisplayProperty(4)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_LogicalMemoryConfiguration_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_LogicalMemoryConfiguration", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Name;
        [DefaultDisplayProperty(1)] public object TotalVirtualMemory;
        [DefaultDisplayProperty(2)] public object TotalPhysicalMemory;
        [DefaultDisplayProperty(3)] public object TotalPageFileSpace;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_NetworkAdapterConfiguration_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_NetworkAdapterConfiguration", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object DHCPEnabled;
        [DefaultDisplayProperty(1)] public object IPAddress;
        [DefaultDisplayProperty(2)] public object DefaultIPGateway;
        [DefaultDisplayProperty(3)] public object DNSDomain;
        [DefaultDisplayProperty(4)] public object ServiceName;
        [DefaultDisplayProperty(5)] public object Description;
        [DefaultDisplayProperty(6)] public object Index;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_NetworkAdapter_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_NetworkAdapter", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object ServiceName;
        [DefaultDisplayProperty(1)] public object MACAddress;
        [DefaultDisplayProperty(2)] public object AdapterType;
        [DefaultDisplayProperty(3)] public object DeviceID;
        [DefaultDisplayProperty(4)] public object Name;
        [DefaultDisplayProperty(5)] public object NetworkAddresses;
        [DefaultDisplayProperty(6)] public object Speed;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_WIN32_CACHEMEMORY_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "WIN32_CACHEMEMORY", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object BlockSize;
        [DefaultDisplayProperty(1)] public object CacheSpeed;
        [DefaultDisplayProperty(2)] public object CacheType;
        [DefaultDisplayProperty(3)] public object DeviceID;
        [DefaultDisplayProperty(4)] public object InstalledSize;
        [DefaultDisplayProperty(5)] public object Level;
        [DefaultDisplayProperty(6)] public object MaxCacheSize;
        [DefaultDisplayProperty(7)] public object NumberOfBlocks;
        [DefaultDisplayProperty(8)] public object Status;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_SoftwareElement_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_SoftwareElement", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Caption;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object Path;
        [DefaultDisplayProperty(3)] public object SerialNumber;
        [DefaultDisplayProperty(4)] public object SoftwareElementID;
        [DefaultDisplayProperty(5)] public object Version;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_NetworkConnection_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_NetworkConnection", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object LocalName;
        [DefaultDisplayProperty(1)] public object RemoteName;
        [DefaultDisplayProperty(2)] public object ConnectionState;
        [DefaultDisplayProperty(3)] public object Status;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_ProcessXXX_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_ProcessXXX", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object ThreadCount;
        [DefaultDisplayProperty(1)] public object HandleCount;
        [DefaultDisplayProperty(2)] public object Name;
        [DefaultDisplayProperty(3)] public object Priority;
        [DefaultDisplayProperty(4)] public object ProcessId;
        [DefaultDisplayProperty(5)] public object WorkingSetSize;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_Product_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_Product", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object IdentifyingNumber;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object Vendor;
        [DefaultDisplayProperty(3)] public object Version;
        [DefaultDisplayProperty(4)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_UserAccount_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_UserAccount", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object AccountType;
        [DefaultDisplayProperty(1)] public object Caption;
        [DefaultDisplayProperty(2)] public object Domain;
        [DefaultDisplayProperty(3)] public object SID;
        [DefaultDisplayProperty(4)] public object FullName;
        [DefaultDisplayProperty(5)] public object Name;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_Process_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_Process", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object ProcessId;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object HandleCount;
        [DefaultDisplayProperty(3)] public object WorkingSetSize;
        [DefaultDisplayProperty(4)] public object VirtualSize;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_Service_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_Service", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object ExitCode;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object ProcessId;
        [DefaultDisplayProperty(3)] public object StartMode;
        [DefaultDisplayProperty(4)] public object State;
        [DefaultDisplayProperty(5)] public object Status;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_CDROMDrive_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_CDROMDrive", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Caption;
        [DefaultDisplayProperty(1)] public object Drive;
        [DefaultDisplayProperty(2)] public object Manufacturer;
        [DefaultDisplayProperty(3)] public object VolumeName;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_DiskDrive_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_DiskDrive", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Partitions;
        [DefaultDisplayProperty(1)] public object DeviceID;
        [DefaultDisplayProperty(2)] public object Model;
        [DefaultDisplayProperty(3)] public object Size;
        [DefaultDisplayProperty(4)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_Environment_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_Environment", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object VariableValue;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object UserName;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_Registry_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_Registry", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object CurrentSize;
        [DefaultDisplayProperty(1)] public object MaximumSize;
        [DefaultDisplayProperty(2)] public object Name;
        [DefaultDisplayProperty(3)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_PortConnector_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_PortConnector", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Tag;
        [DefaultDisplayProperty(1)] public object ConnectorType;
        [DefaultDisplayProperty(2)] public object SerialNumber;
        [DefaultDisplayProperty(3)] public object ExternalReferenceDesignator;
        [DefaultDisplayProperty(4)] public object PortType;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_DiskPartition_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_DiskPartition", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object NumberOfBlocks;
        [DefaultDisplayProperty(1)] public object BootPartition;
        [DefaultDisplayProperty(2)] public object Name;
        [DefaultDisplayProperty(3)] public object PrimaryPartition;
        [DefaultDisplayProperty(4)] public object Size;
        [DefaultDisplayProperty(5)] public object Index;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_IDEController_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_IDEController", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Manufacturer;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object ProtocolSupported;
        [DefaultDisplayProperty(3)] public object Status;
        [DefaultDisplayProperty(4)] public object StatusInfo;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_Directory_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_Directory", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Hidden;
        [DefaultDisplayProperty(1)] public object Archive;
        [DefaultDisplayProperty(2)] public object EightDotThreeFileName;
        [DefaultDisplayProperty(3)] public object FileSize;
        [DefaultDisplayProperty(4)] public object Name;
        [DefaultDisplayProperty(5)] public object Compressed;
        [DefaultDisplayProperty(6)] public object Encrypted;
        [DefaultDisplayProperty(7)] public object Readable;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_WIN32_DCOMApplication_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "WIN32_DCOMApplication", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object AppID;
        [DefaultDisplayProperty(1)] public object InstallDate;
        [DefaultDisplayProperty(2)] public object Name;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_SystemAccount_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_SystemAccount", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Caption;
        [DefaultDisplayProperty(1)] public object Domain;
        [DefaultDisplayProperty(2)] public object Name;
        [DefaultDisplayProperty(3)] public object SID;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_CIM_DataFile_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "CIM_DataFile", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Compressed;
        [DefaultDisplayProperty(1)] public object Encrypted;
        [DefaultDisplayProperty(2)] public object Size;
        [DefaultDisplayProperty(3)] public object Hidden;
        [DefaultDisplayProperty(4)] public object Name;
        [DefaultDisplayProperty(5)] public object Readable;
        [DefaultDisplayProperty(6)] public object System;
        [DefaultDisplayProperty(7)] public object Version;
        [DefaultDisplayProperty(8)] public object Writeable;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_WIN32_DESKTOP_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "WIN32_DESKTOP", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Name;
        [DefaultDisplayProperty(1)] public object ScreenSaverActive;
        [DefaultDisplayProperty(2)] public object ScreenSaverSecure;
        [DefaultDisplayProperty(3)] public object ScreenSaverTimeout;
        [DefaultDisplayProperty(4)] public object SettingID;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_BootConfiguration_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_BootConfiguration", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object BootDirectory;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object SettingID;
        [DefaultDisplayProperty(3)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_PageFileSetting_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_PageFileSetting", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object MaximumSize;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object Caption;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_PrintJob_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_PrintJob", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object Document;
        [DefaultDisplayProperty(1)] public object JobId;
        [DefaultDisplayProperty(2)] public object JobStatus;
        [DefaultDisplayProperty(3)] public object Owner;
        [DefaultDisplayProperty(4)] public object Priority;
        [DefaultDisplayProperty(5)] public object Size;
        [DefaultDisplayProperty(6)] public object Name;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_NTEventlogFile_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_NTEventlogFile", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object FileSize;
        [DefaultDisplayProperty(1)] public object LogfileName;
        [DefaultDisplayProperty(2)] public object Name;
        [DefaultDisplayProperty(3)] public object NumberOfRecords;
    }

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_Win32_OSRecoveryConfiguration_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "Win32_OSRecoveryConfiguration", "root/cimv2");
        }

        [DefaultDisplayProperty(0)] public object DebugFilePath;
        [DefaultDisplayProperty(1)] public object Name;
        [DefaultDisplayProperty(2)] public object SettingID;
    }

}
