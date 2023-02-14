using System.Collections;

namespace TaurusXAdSdk.Api
{
    public class InteractiveArea
    {
        private ArrayList mViewList;

        private InteractiveArea() {
            mViewList = new ArrayList();
        }

        public static InteractiveArea All() {
            return Builder()
                      .AddTitle()
                      .AddSubTitle()
                      .AddBody()
                      .AddAdvertiser()
                      .AddCallToAction()
                      .AddIconLayout()
                      .AddMediaViewLayout()
                      .AddRatingBar()
                      .AddRatingTextView()
                      .AddPrice()
                      .AddStore();
        }

        public static InteractiveArea Builder() {
            return new InteractiveArea();
        }

        public InteractiveArea AddTitle()
        {
            mViewList.Add(InteractiveView.Title);
            return this;
        }

        public InteractiveArea AddSubTitle()
        {
            mViewList.Add(InteractiveView.SubTitle);
            return this;
        }

        public InteractiveArea AddBody()
        {
            mViewList.Add(InteractiveView.Body);
            return this;
        }

        public InteractiveArea AddAdvertiser()
        {
            mViewList.Add(InteractiveView.Advertiser);
            return this;
        }

        public InteractiveArea AddCallToAction()
        {
            mViewList.Add(InteractiveView.CallToAction);
            return this;
        }

        public InteractiveArea AddIconLayout()
        {
            mViewList.Add(InteractiveView.IconLayout);
            return this;
        }

        public InteractiveArea AddMediaViewLayout()
        {
            mViewList.Add(InteractiveView.MediaViewLayout);
            return this;
        }

        public InteractiveArea AddAdChoicesLayout()
        {
            mViewList.Add(InteractiveView.AdChoicesLayout);
            return this;
        }

        public InteractiveArea AddRatingBar()
        {
            mViewList.Add(InteractiveView.RatingBar);
            return this;
        }

        public InteractiveArea AddRatingTextView()
        {
            mViewList.Add(InteractiveView.RatingTextView);
            return this;
        }

        public InteractiveArea AddPrice()
        {
            mViewList.Add(InteractiveView.Price);
            return this;
        }

        public InteractiveArea AddStore()
        {
            mViewList.Add(InteractiveView.Store);
            return this;
        }

        public InteractiveArea AddRootLayout()
        {
            mViewList.Add(InteractiveView.RootLayout);
            return this;
        }

        public InteractiveArea AddCustomView()
        {
            mViewList.Add(InteractiveView.CustomView);
            return this;
        }




        public bool HasTitle()
        {
            return mViewList.Contains(InteractiveView.Title);
        }

        public bool HasSubTitle()
        {
            return mViewList.Contains(InteractiveView.SubTitle);
        }

        public bool HasBody()
        {
            return mViewList.Contains(InteractiveView.Body);
        }

        public bool HasAdvertiser()
        {
            return mViewList.Contains(InteractiveView.Advertiser);
        }

        public bool HasCallToAction()
        {
            return mViewList.Contains(InteractiveView.CallToAction);
        }

        public bool HasIconLayout()
        {
            return mViewList.Contains(InteractiveView.IconLayout);
        }

        public bool HasMediaViewLayout()
        {
            return mViewList.Contains(InteractiveView.MediaViewLayout);
        }

        public bool HasAdChoicesLayout()
        {
            return mViewList.Contains(InteractiveView.AdChoicesLayout);
        }

        public bool HasRatingBar()
        {
            return mViewList.Contains(InteractiveView.RatingBar);
        }

        public bool HasRatingTextView()
        {
            return mViewList.Contains(InteractiveView.RatingTextView);
        }

        public bool HasRatingView()
        {
            return HasRatingBar() || HasRatingTextView();
        }

        public bool HasPrice()
        {
            return mViewList.Contains(InteractiveView.Price);
        }

        public bool HasStore()
        {
            return mViewList.Contains(InteractiveView.Store);
        }

        public bool HasRootLayout()
        {
            return mViewList.Contains(InteractiveView.RootLayout);
        }

        public bool HasCustomView()
        {
            return mViewList.Contains(InteractiveView.CustomView);
        }
    }
}
