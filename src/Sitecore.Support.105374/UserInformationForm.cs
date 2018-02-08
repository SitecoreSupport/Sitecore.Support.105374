using System;
using Sitecore.Diagnostics;
using Sitecore.Resources;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;
using Sitecore.Security.Accounts;

namespace Sitecore.Support.Shell.Applications.ControlPanel.Preferences.UserInformation
{
    public class UserInformationForm :Sitecore.Shell.Applications.ControlPanel.Preferences.UserInformation.UserInformationForm
  { 
    protected override void OnOK(object sender, EventArgs args)
  {
    Assert.ArgumentNotNull(sender, "sender");
    Assert.ArgumentNotNull(args, "args");
    bool flag = base.Validate();
    if (flag)
    {
    User user = Context.User;
    try
    {
      string text = ImageBuilder.ResizeImageSrc(StringUtil.GetString(Context.ClientPage.ServerProperties["Portrait"]), 16, 16);
      user.Profile.FullName = WebUtil.HtmlEncode(this.Fullname.Value);
      user.Profile.Email = this.Email.Value;
      user.Profile.Portrait = text;
      Log.Audit(this, "Set user information, fullname: {0}, email: {1}, portrait:{2}", new string[]
      {
        this.Fullname.Value,
        this.Email.Value,
        text
      });
    }
    finally
    {
      user.Profile.Save();
    }
    Context.ClientPage.ClientResponse.Alert("Some changes will first take effect, when the browser is refreshed.");
    SheerResponse.CloseWindow();
  }
  }
}
}