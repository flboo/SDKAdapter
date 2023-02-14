namespace RichOX.Common
{
    public interface INativeInfoClient
    {
        // 标题信息，类似原生广告中 Title 字段
        string GetTitle();

        // ICON 资源对应的 URL，类似原生广告中 ICON 字段
        string GetIconUrl();

        // 描述信息，类似原生广告中 DESC 字段
        string GetDesc();

        // CTA （call_to_action), 类似原生广告中 CTA 字段
        string GetCTA();

        // Media URL
        string GetMediaUrl();
    }
}
