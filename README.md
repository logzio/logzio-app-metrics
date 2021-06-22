## Send Custom metrics from your .NET Core application to Logz.io using App Metrics
An open-source and cross-platform .NET library used to record metrics within an application.
App Metrics can run on .NET Core or on the full .NET framework also supporting .NET 4.5.2.

For more information about App Metrics and how to use it [click here](https://www.app-metrics.io/)

- [Usage](#usage)
- [Getting Started](#getting-started)
  - [MetricsBuilder](#metricsbuilder)
    - [Option 1](#option-1)
    - [Option 2](#option-2)
    - [Option 3](#option-3)
  - [Record Metrics](#record-metrics)
    - [Metric Types](#metric-types)
    - [Gauges](#gauges)
    - [Counters](#counters)
    - [Meters](#meters)
    - [Histograms](#histograms)
    - [Timers](#timers)
    - [Apdex](#apdex)
  - [Scheduler](#scheduler)
- [.NET Core Runtime Metrics](#net-core-runtime-metrics)
- [Get Current Snapshot](#get-current-snapshot)
- [App Metrics Logs](#app-metrics-logs)
- [Code Sample](#code-sample)

## Usage

Install the App.Metrics.Logzio package from the Package Manager Console:

    Install-Package App.Metrics.Logzio

If you prefer to install the library manually, download the latest version from the releases page.

## Getting Started

### MetricsBuilder

Create MetricsBuilder that reports to Logz.io. Here are some of the possible options to do so:

- Replace the placeholders in the code (indicated by the double square brackets `[[ ]]`) to match your specifics.

| Variable | Description |
| --- | --- |
| logzio_endpoint |  The Logz.io Listener URL for for your region, configured to use port **8052** for http traffic, or port **8053** for https traffic. For more details, see the [regions page](https://docs.logz.io/user-guide/accounts/account-region.html) in logz.io docs. |
| metrics_token | The Logz.io Prometheus Metrics account token. Find it under **Settings > Manage accounts**. [Look up your Metrics account token.](https://docs.logz.io/user-guide/accounts/finding-your-metrics-account-token/) |

#### Option 1

The straightforward way:

```C#
var metrics = new MetricsBuilder()
                .Report.ToLogzioHttp("[[logzio_endpoint]]", "[[metrics_token]]")
                .Build();
```

#### Option 2

Using config file:

```xml
<?xml version="1.0" encoding="utf-8"?>

<Configuration>
    <LogzioConnection>
        <Endpoint> [[logzio_endpoit]] </Endpoint>
        <Token> [[metrics_token]] </Token>
    </LogzioConnection>
</Configuration>
```

```C#
var metrics = new MetricsBuilder()
                .Report.ToLogzioHttp("[[ config_file_path ]]")
                .Build();
```

- Remember to change in properties of your config file the "Copy to output directory" to "Copy if newer" or "Copy always".

#### Option 3

Configure ToLogzioHttp options:

```C#
var metrics = new MetricsBuilder()
                .Report.ToLogzioHttp(options =>
                {
                    options.Logzio.EndpointUri = new Uri("[[logzio_endpoint]]");
                    options.Logzio.Token = "[[metrics_token]]";
                    options.FlushInterval = TimeSpan.FromSeconds(15);
                    options.Filter = new MetricsFilter().WhereType(MetricType.Counter);
                    options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
                    options.HttpPolicy.FailuresBeforeBackoff = 5;
                    options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
                })
                .Build();
```

The configuration options provided are:

| Option | Description |
| --- | --- |
| Logzio.EndpointUri | The Logz.io Listener URL for for your region, configured to use port **8052** for http traffic, or port **8053** for https traffic. For more details, see the [regions page](https://docs.logz.io/user-guide/accounts/account-region.html) in logz.io docs. |
| Logzio.Token | The Logz.io Prometheus Metrics account token. Find it under **Settings > Manage accounts**. [Look up your Metrics account token.](https://docs.logz.io/user-guide/accounts/finding-your-metrics-account-token/) |
| FlushInterval | The delay between reporting metrics. |
| Filter |  The filter used to filter metrics just for this reporter. |
| HttpPolicy.BackoffPeriod | The TimeSpan to back-off when metrics are failing to report to the metrics ingress endpoint. |
| HttpPolicy.FailuresBeforeBackoff | The number of failures before backing-off when metrics are failing to report to the metrics ingress endpoint. |
| HttpPolicy.Timeout | The HTTP timeout duration when attempting to report metrics to the metrics ingress endpoint. |

### Record Metrics

#### Metric Types

For App Metrics metric types [click here](https://www.app-metrics.io/getting-started/metric-types/)

Each metric has the following data as its labels:

| Data Name | Description | Label Key Name In Logz.io |
| --- | --- | --- |
| Name | The name of the metric. Required for each metric. | Metric name if not stated otherwise |
| MeasurementUnit | The unit you use to measure. By default it is None | unit |
| Context | the context which the metric belong to. By default it is Application. | context |
| Tags | Pairs of key and value of the metric. It is not required to have tags for a metric. | Tags keys |

For more information about App Metrics Tags and Context [click here](https://www.app-metrics.io/getting-started/fundamentals/tagging-organizing/) 

#### Gauges

For Gauge code example and information [click here](https://www.app-metrics.io/getting-started/metric-types/gauges/)

#### Counters

For Counter code example and information [click here](https://www.app-metrics.io/getting-started/metric-types/counters/)

#### Meters

For Meter code example and information [click here](https://www.app-metrics.io/getting-started/metric-types/meters/)

Meter's available data:

- Replace [[meter_name]] with your Meter's name.

| Meter Data Name | Meter Data Name In Logz.io |
| --- | --- |
| Count | [[meter_name]]_count |
| One Min Rate | [[meter_name]]_one_min_rate |
| Five Min Rate | [[meter_name]]_five_min_rate |
| Fifteen Min Rate | [[meter_name]]_fifteen_min_rate |
| Mean Rate | [[meter_name]]_mean_rate |

Each Meter metric has the following labels:

| Meter Label Name | Meter Label Key Name In Logz.io |
| --- | --- |
| RateUnit | rate_unit |

#### Histograms

For Histogram code example and information [click here](https://www.app-metrics.io/getting-started/metric-types/histograms/)

Histogram's available data:

- Replace [[histogram_name]] with your Histogram's name.

| Meter Data Name | Meter Data Name In Logz.io |
| --- | --- |
| Count | [[histogram_name]]_count |
| Sum | [[histogram_name]]_sum |
| Last Value | [[histogram_name]]_lastValue |
| Max | [[histogram_name]]_max |
| Mean | [[histogram_name]]_mean |
| Median | [[histogram_name]]_median |
| Min | [[histogram_name]]_min |
| Percentile 75 | [[histogram_name]]_percentile75 |
| Percentile 95 | [[histogram_name]]_percentile95 |
| Percentile 98 | [[histogram_name]]_percentile98 |
| Percentile 99 | [[histogram_name]]_percentile99 |
| Percentile 999 | [[histogram_name]]_percentile999 |
| Sample Size | [[histogram_name]]_sample_size |
| Std Dev | [[histogram_name]]_std_dev |

Each Histogram metric can have the following labels:

| Histogram Label Name | Histogram Label Kay Name In Logz.io |
| --- | --- |
| Last User Value | last_user_value |
| Max User Value | max_user_value |
| Min User Value | min_user_value |

#### Timers

For Timer code example and information [click here](https://www.app-metrics.io/getting-started/metric-types/timers/)

Timer's available data:

- Replace [[timer_name]] with your Timer's name.

| Timer Data Name | Timer Data Name In Logz.io |
| --- | --- |
| Count | [[timer_name]]_count |
| Histogram Active Session | [[timer_name]]_histogram_active_session |
| Histogram Sum | [[timer_name]]_histogram_sum |
| Histogram Last Value | [[timer_name]]_histogram_lastValue |
| Histogram Max | [[timer_name]]_histogram_max |
| Histogram Mean | [[timer_name]]_histogram_mean |
| Histogram Median | [[timer_name]]_histogram_median |
| Histogram Min | [[timer_name]]_histogram_min |
| Histogram Percentile 75 | [[timer_name]]_histogram_percentile75 |
| Histogram Percentile 95 | [[timer_name]]_histogram_percentile95 |
| Histogram Percentile 98 | [[timer_name]]_histogram_percentile98 |
| Histogram Percentile 99 | [[timer_name]]_histogram_percentile99 |
| Histogram Percentile 999 | [[timer_name]]_histogram_percentile999 |
| Histogram Sample Size | [[timer_name]]_histogram_sample_size |
| Histogram Std Dev | [[timer_name]]_histogram_std_dev |
| Rate One Min Rate | [[timer_name]]_rate_one_min_rate |
| Rate Five Min Rate | [[timer_name]]_rate_five_min_rate |
| Rate Fifteen Min Rate | [[timer_name]]_rate_fifteen_min_rate |
| Rate Mean Rate | [[timer_name]]_rate_mean_rate |

Each Timer metric has the following labels:

| Timer Label Name | Timer Label Kay Name In Logz.io |
| --- | --- |
| Duration Unit | duration_unit |
| Rate Unit | rate_unit |

#### Apdex

For Apdex code example and information [click here](https://www.app-metrics.io/getting-started/metric-types/apdex/)

Apdex's available data:

- Replace [[apdex_name]] with your Apdex's name.

| Apdex Data Name | Apdex Data Name In Logz.io |
| --- | --- |
| Sample Size | [[apdex_name]]_sample_size |
| Score | [[apdex_name]]_score |
| Frustrating | [[apdex_name]]_frustrating |
| Satisfied | [[apdex_name]]_satisfied |
| Tolerating | [[apdex_name]]_tolerating |

### Scheduler

Create scheduler to tell the reporter to send metrics to Logz.io every x-seconds:

Will run every reporter the MerticsBuilder has:

```C#
var scheduler = new AppMetricsTaskScheduler(
                TimeSpan.FromSeconds(15),
                async () => { await Task.WhenAll(metrics.ReportRunner.RunAllAsync()); });
            scheduler.Start();
```

Will run the Logz.io reporter only:

```C#
var scheduler = new AppMetricsTaskScheduler(
                TimeSpan.FromSeconds(15),
                async () => { await Task.Run(() => metrics.ReportRunner.RunAsync<LogzioMetricsReporter>()); });
            scheduler.Start();
```

To run all reporters once:

```C#
Task.WhenAll(metrics.ReportRunner.RunAllAsync());
```

To run Logz.io reporter once:

```C#
Task.Run(() => metrics.ReportRunner.RunAsync<LogzioMetricsReporter>());
```

- When running reporter once, use await on the task to make sure it finished running before program ends.

## .NET Core Runtime Metrics

.Net core runtime metrics including:

- Garbage collection collection frequencies and timings by generation/type, pause timings and GC CPU consumption ratio.
- Heap size by generation.
- Bytes allocated by small/large object heap.
- JIT compilations and JIT CPU consumption ratio.
- Thread pool size, scheduling delays and reasons for growing/shrinking.
- Lock contention.

Start the collector:

```C#
// metrics is the MetricsBuilder
IDisposable collector = DotNetRuntimeStatsBuilder.Default(metrics).StartCollecting();
```

You can customize the types of .NET metrics collected via the Customize method:

```C#
IDisposable collector = DotNetRuntimeStatsBuilder
    .Customize()
    .WithContentionStats()
    .WithJitStats()
    .WithThreadPoolSchedulingStats()
    .WithThreadPoolStats()
    .WithGcStats()
    .StartCollecting(metrics);          // metrics is the MetricsBuilder
```

This data can be found in Logz.io under these Contexts label:

- process
- dotnet

## Get Current Snapshot

You can get current snapshot (current metrics in MetricsBuilder) in Logz.io format.

```C#
var metrics = new MetricsBuilder()
                .OutputMetrics.AsLogzioCompressedProtobuf()
                .Build();

var snapshot = metrics.Snapshot.Get();
            
using (var stream = new MemoryStream())
{
    await metrics.DefaultOutputMetricsFormatter.WriteAsync(stream, snapshot);

    // Your code here...
}
```

## App Metrics Logs

App Metrics supports the following .NET logging providers:

- Serilog
- NLog
- log4net
- EntLib
- Loupe

To see in-app logs, simply configure your desired log provider.

- Remember to change in properties of your config file the "Copy to output directory" to "Copy if newer" or "Copy always".

## Code Sample

```C#
using System;
using System.Threading;
using System.Threading.Tasks;
using App.Metrics.Counter;
using App.Metrics.Scheduling;

namespace App.Metrics.Logzio
{
    class Program
    {
        static void Main(string[] args)
        {
            var metrics = new MetricsBuilder()
                .Report.ToLogzioHttp("[[logzio_endpoint]]", "[[metrics_token]]")
                .Build();

            var scheduler = new AppMetricsTaskScheduler(
                TimeSpan.FromSeconds(5),
                async () => { await Task.Run(() => metrics.ReportRunner.RunAsync<LogzioMetricsReporter>()); });
            scheduler.Start();

            var counter = new CounterOptions {Name = "my_counter", Tags = new MetricTags("test", "my_test")};
            metrics.Measure.Counter.Increment(counter);

            Thread.Sleep(10000);

            metrics.Measure.Counter.Increment(counter);

            Thread.Sleep(100000);                   // Lets the program to continue running so that the scheduler wiil be able to continue sending metrics to Logz.io 
        }
    }
}
```