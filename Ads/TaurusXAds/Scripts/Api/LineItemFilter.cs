using System;
namespace TaurusXAdSdk.Api
{
    public interface LineItemFilter
    {
        bool Accept(LineItem lineItem);
    }
}