using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qarth.Plugin
{
    public class RateHandler_Android : RateHandler
    {
        #region Features

        public override void RequestReview()
        {
            UIMgr.S.OpenTopPanel(EngineUI.RatePanel, null);
        }

        public override void OpenRatingPage()
        {
            SocialMgr.S.OpenMarketRatePage();
        }

        #endregion
    }
}
