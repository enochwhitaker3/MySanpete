using System.Diagnostics.Metrics;

namespace MySanpeteWeb.Telemetry;

public static class MySanpeteMetrics
{
    public static int EventsCalled = 0;
    public static int _totalTicketsUnscanned = 0;
    public static int TotalUsersActive = 0;
    public static readonly string Name = "MySanpeteMetrics";
    public static Meter Meter = new(Name, "1.0.0");
    public static Counter<int> VoucherPageHitCount { get; set; } = Meter.CreateCounter<int>("voucher_page_hit_count");
    public static Counter<int> EventPageHitCount { get; set; } = Meter.CreateCounter<int>("event_page_hit_count");
    public static Counter<int> BlogPageHitCount { get; set; } = Meter.CreateCounter<int>("blog_page_hit_count");
    public static Counter<int> PodcastPageHitCount { get; set; } = Meter.CreateCounter<int>("podcast_page_hit_count");
    public static Counter<int> BusinessPageHitCount { get; set; } = Meter.CreateCounter<int>("business_page_hit_count");
    public static ObservableCounter<long> TotalStorageTaken { get; set; } = Meter.CreateObservableCounter<long>("total_storage_taken", () => GC.GetTotalMemory(true));

    public static UpDownCounter<int> UsersOnHomePageCount { get; set; } = Meter.CreateUpDownCounter<int>("users_on_home_page_count");

    public static Histogram<double> ImageRetrieval { get; set; } = Meter.CreateHistogram<double>("image_retrieval");
}
