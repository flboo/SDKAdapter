namespace TaurusXAdSdk.Api
{
    public class NativeAdLayout
    {
        private string mLayout;
        private InteractiveArea mInteractiveArea;

        public NativeAdLayout(string layout) {
            mLayout = layout;
        }

        public void SetInteractiveArea(InteractiveArea area) {
            mInteractiveArea = area;
        }

        public string GetLayout() {
            return mLayout;
        }

        public InteractiveArea GetInteractiveArea() {
            return mInteractiveArea;
        }
    }
}
