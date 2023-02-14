using UnityEngine;
using TaurusXAdSdk.Api;

namespace TaurusXAdSdk.Platforms.Android
{
    public class AndroidLineItemFilter : AndroidJavaProxy
    {
        private LineItemFilter mLineItemFilter;

        public AndroidLineItemFilter(LineItemFilter filter) : base(Utils.LineItemFilterClassName)
        {
            mLineItemFilter = filter;
        }

        public bool accept(AndroidJavaObject lineItem)
        {
            return mLineItemFilter.Accept(new LineItem(new LineItemClient(lineItem)));
        }
    }
}