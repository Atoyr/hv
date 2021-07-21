using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace hv.Models
{
    public partial class VirtualMachine
    {
        [JsonProperty("ConfigurationLocation")]
        public string ConfigurationLocation { get; set; }

        [JsonProperty("SmartPagingFileInUse")]
        public bool SmartPagingFileInUse { get; set; }

        [JsonProperty("SmartPagingFilePath")]
        public string SmartPagingFilePath { get; set; }

        [JsonProperty("SnapshotFileLocation")]
        public string SnapshotFileLocation { get; set; }

        [JsonProperty("AutomaticStartAction")]
        public long AutomaticStartAction { get; set; }

        [JsonProperty("AutomaticStartDelay")]
        public long AutomaticStartDelay { get; set; }

        [JsonProperty("AutomaticStopAction")]
        public long AutomaticStopAction { get; set; }

        [JsonProperty("AutomaticCriticalErrorAction")]
        public long AutomaticCriticalErrorAction { get; set; }

        [JsonProperty("AutomaticCriticalErrorActionTimeout")]
        public long AutomaticCriticalErrorActionTimeout { get; set; }

        [JsonProperty("AutomaticCheckpointsEnabled")]
        public bool AutomaticCheckpointsEnabled { get; set; }

        [JsonProperty("CPUUsage")]
        public long CpuUsage { get; set; }

        [JsonProperty("MemoryAssigned")]
        public long MemoryAssigned { get; set; }

        [JsonProperty("MemoryDemand")]
        public long MemoryDemand { get; set; }

        [JsonProperty("MemoryStatus")]
        public string MemoryStatus { get; set; }

        [JsonProperty("NumaAligned")]
        public bool? NumaAligned { get; set; }

        [JsonProperty("NumaNodesCount")]
        public long NumaNodesCount { get; set; }

        [JsonProperty("NumaSocketCount")]
        public long NumaSocketCount { get; set; }

        [JsonProperty("Heartbeat")]
        public long? Heartbeat { get; set; }

        [JsonProperty("IntegrationServicesState")]
        public string IntegrationServicesState { get; set; }

        [JsonProperty("IntegrationServicesVersion")]
        public IntegrationServicesVersion IntegrationServicesVersion { get; set; }

        [JsonProperty("Uptime")]
        public DateTime Uptime { get; set; }

        [JsonProperty("OperationalStatus")]
        public long[] OperationalStatus { get; set; }

        [JsonProperty("PrimaryOperationalStatus")]
        public long PrimaryOperationalStatus { get; set; }

        [JsonProperty("SecondaryOperationalStatus")]
        public object SecondaryOperationalStatus { get; set; }

        [JsonProperty("StatusDescriptions")]
        public string[] StatusDescriptions { get; set; }

        [JsonProperty("PrimaryStatusDescription")]
        public string PrimaryStatusDescription { get; set; }

        [JsonProperty("SecondaryStatusDescription")]
        public object SecondaryStatusDescription { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("ReplicationHealth")]
        public long ReplicationHealth { get; set; }

        [JsonProperty("ReplicationMode")]
        public long ReplicationMode { get; set; }

        [JsonProperty("ReplicationState")]
        public long ReplicationState { get; set; }

        [JsonProperty("ResourceMeteringEnabled")]
        public bool ResourceMeteringEnabled { get; set; }

        [JsonProperty("CheckpointType")]
        public long CheckpointType { get; set; }

        [JsonProperty("EnhancedSessionTransportType")]
        public long EnhancedSessionTransportType { get; set; }

        [JsonProperty("Groups")]
        public object[] Groups { get; set; }

        [JsonProperty("Version")]
        public string Version { get; set; }

        [JsonProperty("VirtualMachineType")]
        public long VirtualMachineType { get; set; }

        [JsonProperty("VirtualMachineSubType")]
        public long VirtualMachineSubType { get; set; }

        [JsonProperty("Notes")]
        public string Notes { get; set; }

        [JsonProperty("State")]
        public long State { get; set; }

        [JsonProperty("ComPort1")]
        public ComPort ComPort1 { get; set; }

        [JsonProperty("ComPort2")]
        public ComPort ComPort2 { get; set; }

        [JsonProperty("DVDDrives")]
        public string[] DvdDrives { get; set; }

        [JsonProperty("FibreChannelHostBusAdapters")]
        public object[] FibreChannelHostBusAdapters { get; set; }

        [JsonProperty("FloppyDrive")]
        public object FloppyDrive { get; set; }

        [JsonProperty("HardDrives")]
        public string[] HardDrives { get; set; }

        [JsonProperty("RemoteFxAdapter")]
        public object RemoteFxAdapter { get; set; }

        [JsonProperty("VMIntegrationService")]
        public string[] VmIntegrationService { get; set; }

        [JsonProperty("DynamicMemoryEnabled")]
        public bool DynamicMemoryEnabled { get; set; }

        [JsonProperty("MemoryMaximum")]
        public long MemoryMaximum { get; set; }

        [JsonProperty("MemoryMinimum")]
        public long MemoryMinimum { get; set; }

        [JsonProperty("MemoryStartup")]
        public long MemoryStartup { get; set; }

        [JsonProperty("ProcessorCount")]
        public long ProcessorCount { get; set; }

        [JsonProperty("BatteryPassthroughEnabled")]
        public bool BatteryPassthroughEnabled { get; set; }

        [JsonProperty("Generation")]
        public long Generation { get; set; }

        [JsonProperty("IsClustered")]
        public bool IsClustered { get; set; }

        [JsonProperty("ParentSnapshotId")]
        public Guid? ParentSnapshotId { get; set; }

        [JsonProperty("ParentSnapshotName")]
        public string ParentSnapshotName { get; set; }

        [JsonProperty("Path")]
        public string Path { get; set; }

        [JsonProperty("SizeOfSystemFiles")]
        public long SizeOfSystemFiles { get; set; }

        [JsonProperty("GuestControlledCacheTypes")]
        public bool GuestControlledCacheTypes { get; set; }

        [JsonProperty("LowMemoryMappedIoSpace")]
        public long LowMemoryMappedIoSpace { get; set; }

        [JsonProperty("HighMemoryMappedIoSpace")]
        public long HighMemoryMappedIoSpace { get; set; }

        [JsonProperty("LockOnDisconnect")]
        public long LockOnDisconnect { get; set; }

        [JsonProperty("CreationTime")]
        public string CreationTime { get; set; }

        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("NetworkAdapters")]
        public string[] NetworkAdapters { get; set; }

        [JsonProperty("CimSession")]
        public CimSession CimSession { get; set; }

        [JsonProperty("ComputerName")]
        public string ComputerName { get; set; }

        [JsonProperty("IsDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("ParentCheckpointId")]
        public Guid? ParentCheckpointId { get; set; }

        [JsonProperty("ParentCheckpointName")]
        public string ParentCheckpointName { get; set; }

        [JsonProperty("VMName")]
        public string VmName { get; set; }

        [JsonProperty("VMId")]
        public Guid VmId { get; set; }

        [JsonProperty("CheckpointFileLocation")]
        public string CheckpointFileLocation { get; set; }
    }

    public partial class CimSession
    {
        [JsonProperty("ComputerName")]
        public object ComputerName { get; set; }

        [JsonProperty("InstanceId")]
        public Guid InstanceId { get; set; }
    }

    public partial class ComPort
    {
        [JsonProperty("Path")]
        public string Path { get; set; }

        [JsonProperty("DebuggerMode")]
        public long DebuggerMode { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("VMId")]
        public Guid VmId { get; set; }

        [JsonProperty("VMName")]
        public string VmName { get; set; }

        [JsonProperty("VMSnapshotId")]
        public Guid VmSnapshotId { get; set; }

        [JsonProperty("VMSnapshotName")]
        public string VmSnapshotName { get; set; }

        [JsonProperty("CimSession")]
        public string CimSession { get; set; }

        [JsonProperty("ComputerName")]
        public string ComputerName { get; set; }

        [JsonProperty("IsDeleted")]
        public bool IsDeleted { get; set; }
    }

    public partial class IntegrationServicesVersion
    {
        [JsonProperty("Major")]
        public long Major { get; set; }

        [JsonProperty("Minor")]
        public long Minor { get; set; }

        [JsonProperty("Build")]
        public long Build { get; set; }

        [JsonProperty("Revision")]
        public long Revision { get; set; }

        [JsonProperty("MajorRevision")]
        public long MajorRevision { get; set; }

        [JsonProperty("MinorRevision")]
        public long MinorRevision { get; set; }
    }

    public partial class VirtualMachine
    {
        public static VirtualMachine[] FromJson(string json) => JsonConvert.DeserializeObject<VirtualMachine[]>(json, hv.Models.Converter.VirtualMachneSettings);
    }

}

