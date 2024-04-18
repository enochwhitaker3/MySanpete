using System.Diagnostics.Metrics;

namespace MySanpeteWeb.Telemetry;

public static class MySanpeteMetrics
{
    public static int EventsCalled = 0;
    public static int _totalTicketsUnscanned = 0;
    public static int TotalUsersActive = 0;
    public static readonly string Name = "MySanpeteMetrics";
    public static Meter Meter = new(Name, "1.0.0");
    public static Counter<int> VoucherPageHitCount = Meter.CreateCounter<int>("voucher_page_hit_count");
    public static Counter<int> EventPageHitCount = Meter.CreateCounter<int>("event_page_hit_count");
    public static Counter<int> BlogPageHitCount = Meter.CreateCounter<int>("blog_page_hit_count");
    public static Counter<int> PodcastPageHitCount = Meter.CreateCounter<int>("podcast_page_hit_count");

    public static UpDownCounter<int> UsersOnHomePageCount = Meter.CreateUpDownCounter<int>("users_on_home_page_count");

    public static Histogram<double> ImageRetrieval = Meter.CreateHistogram<double>("image_retrieval");
}
