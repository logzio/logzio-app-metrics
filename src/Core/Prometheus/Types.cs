// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: types.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Core.Prometheus {

  /// <summary>Holder for reflection information generated from types.proto</summary>
  public static partial class TypesReflection {

    #region Descriptor
    /// <summary>File descriptor for types.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static TypesReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cgt0eXBlcy5wcm90bxIPY29yZS5wcm9tZXRoZXVzIjAKBlNhbXBsZRINCgV2",
            "YWx1ZRgBIAEoARIXCg90aW1lc3RhbXBNaWxsaXMYAiABKAMiXgoKVGltZVNl",
            "cmllcxImCgZsYWJlbHMYASADKAsyFi5jb3JlLnByb21ldGhldXMuTGFiZWwS",
            "KAoHc2FtcGxlcxgCIAMoCzIXLmNvcmUucHJvbWV0aGV1cy5TYW1wbGUiJAoF",
            "TGFiZWwSDAoEbmFtZRgBIAEoCRINCgV2YWx1ZRgCIAEoCUIdChtpby5sb2d6",
            "LnByb21ldGhldXMucHJvdG9idWZiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Core.Prometheus.Sample), global::Core.Prometheus.Sample.Parser, new[]{ "Value", "TimestampMillis" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Core.Prometheus.TimeSeries), global::Core.Prometheus.TimeSeries.Parser, new[]{ "Labels", "Samples" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Core.Prometheus.Label), global::Core.Prometheus.Label.Parser, new[]{ "Name", "Value" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Sample : pb::IMessage<Sample>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Sample> _parser = new pb::MessageParser<Sample>(() => new Sample());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Sample> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Core.Prometheus.TypesReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Sample() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Sample(Sample other) : this() {
      value_ = other.value_;
      timestampMillis_ = other.timestampMillis_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Sample Clone() {
      return new Sample(this);
    }

    /// <summary>Field number for the "value" field.</summary>
    public const int ValueFieldNumber = 1;
    private double value_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public double Value {
      get { return value_; }
      set {
        value_ = value;
      }
    }

    /// <summary>Field number for the "timestampMillis" field.</summary>
    public const int TimestampMillisFieldNumber = 2;
    private long timestampMillis_;
    /// <summary>
    /// Asaf: Renamed to TimestampMillis from Timestamp to clarify this is millisecond unit
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long TimestampMillis {
      get { return timestampMillis_; }
      set {
        timestampMillis_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Sample);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Sample other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(Value, other.Value)) return false;
      if (TimestampMillis != other.TimestampMillis) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Value != 0D) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(Value);
      if (TimestampMillis != 0L) hash ^= TimestampMillis.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Value != 0D) {
        output.WriteRawTag(9);
        output.WriteDouble(Value);
      }
      if (TimestampMillis != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(TimestampMillis);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Value != 0D) {
        output.WriteRawTag(9);
        output.WriteDouble(Value);
      }
      if (TimestampMillis != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(TimestampMillis);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Value != 0D) {
        size += 1 + 8;
      }
      if (TimestampMillis != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(TimestampMillis);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Sample other) {
      if (other == null) {
        return;
      }
      if (other.Value != 0D) {
        Value = other.Value;
      }
      if (other.TimestampMillis != 0L) {
        TimestampMillis = other.TimestampMillis;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 9: {
            Value = input.ReadDouble();
            break;
          }
          case 16: {
            TimestampMillis = input.ReadInt64();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 9: {
            Value = input.ReadDouble();
            break;
          }
          case 16: {
            TimestampMillis = input.ReadInt64();
            break;
          }
        }
      }
    }
    #endif

  }

  /// <summary>
  /// TimeSeries represents samples and labels for a single time series.
  /// </summary>
  public sealed partial class TimeSeries : pb::IMessage<TimeSeries>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<TimeSeries> _parser = new pb::MessageParser<TimeSeries>(() => new TimeSeries());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<TimeSeries> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Core.Prometheus.TypesReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TimeSeries() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TimeSeries(TimeSeries other) : this() {
      labels_ = other.labels_.Clone();
      samples_ = other.samples_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TimeSeries Clone() {
      return new TimeSeries(this);
    }

    /// <summary>Field number for the "labels" field.</summary>
    public const int LabelsFieldNumber = 1;
    private static readonly pb::FieldCodec<global::Core.Prometheus.Label> _repeated_labels_codec
        = pb::FieldCodec.ForMessage(10, global::Core.Prometheus.Label.Parser);
    private readonly pbc::RepeatedField<global::Core.Prometheus.Label> labels_ = new pbc::RepeatedField<global::Core.Prometheus.Label>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Core.Prometheus.Label> Labels {
      get { return labels_; }
    }

    /// <summary>Field number for the "samples" field.</summary>
    public const int SamplesFieldNumber = 2;
    private static readonly pb::FieldCodec<global::Core.Prometheus.Sample> _repeated_samples_codec
        = pb::FieldCodec.ForMessage(18, global::Core.Prometheus.Sample.Parser);
    private readonly pbc::RepeatedField<global::Core.Prometheus.Sample> samples_ = new pbc::RepeatedField<global::Core.Prometheus.Sample>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Core.Prometheus.Sample> Samples {
      get { return samples_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as TimeSeries);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(TimeSeries other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!labels_.Equals(other.labels_)) return false;
      if(!samples_.Equals(other.samples_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= labels_.GetHashCode();
      hash ^= samples_.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      labels_.WriteTo(output, _repeated_labels_codec);
      samples_.WriteTo(output, _repeated_samples_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      labels_.WriteTo(ref output, _repeated_labels_codec);
      samples_.WriteTo(ref output, _repeated_samples_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += labels_.CalculateSize(_repeated_labels_codec);
      size += samples_.CalculateSize(_repeated_samples_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(TimeSeries other) {
      if (other == null) {
        return;
      }
      labels_.Add(other.labels_);
      samples_.Add(other.samples_);
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            labels_.AddEntriesFrom(input, _repeated_labels_codec);
            break;
          }
          case 18: {
            samples_.AddEntriesFrom(input, _repeated_samples_codec);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            labels_.AddEntriesFrom(ref input, _repeated_labels_codec);
            break;
          }
          case 18: {
            samples_.AddEntriesFrom(ref input, _repeated_samples_codec);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class Label : pb::IMessage<Label>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Label> _parser = new pb::MessageParser<Label>(() => new Label());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Label> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Core.Prometheus.TypesReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Label() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Label(Label other) : this() {
      name_ = other.name_;
      value_ = other.value_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Label Clone() {
      return new Label(this);
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 1;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "value" field.</summary>
    public const int ValueFieldNumber = 2;
    private string value_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Value {
      get { return value_; }
      set {
        value_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Label);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Label other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      if (Value != other.Value) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (Value.Length != 0) hash ^= Value.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      if (Value.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Value);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      if (Value.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Value);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Value.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Value);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Label other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.Value.Length != 0) {
        Value = other.Value;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
          case 18: {
            Value = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
          case 18: {
            Value = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
