using Abp.Web.Mvc.Views;

namespace Park.Web.Views
{
    public abstract class ParkWebViewPageBase : ParkWebViewPageBase<dynamic>
    {

    }

    public abstract class ParkWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected ParkWebViewPageBase()
        {
            LocalizationSourceName = ParkConsts.LocalizationSourceName;
        }
    }
}