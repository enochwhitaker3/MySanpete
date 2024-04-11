namespace MySanpeteWeb.Data;

public static class FeatureFlag
{
    public static bool IsAvailable { get; set; } = false;
    public static string FeatureFlagName { get; set; } = "FeatureFlag";
}
