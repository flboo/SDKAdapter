using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advertisers.Api;

namespace Qarth
{
    public class TaurusXProbeMgr : TSingleton<TaurusXProbeMgr>
    {

        private ProbeManager m_ProbeMgr;

        public void Init()
        {
            m_ProbeMgr = new ProbeManager();
            TrackListener listener = new TrackListener();
            listener.OnAdRequest += (sender, args) =>
            {
                
            };
            listener.OnAdLoaded += (sender, args) =>
            {
                
            };
            listener.OnAdFailedToLoad += (sender, args) =>
            {
                
            };
            listener.OnAdShown += (sender, args) =>
            {
                
            };
            listener.OnAdCallShow += (sender, args) =>
            {
                
            };
            listener.OnAdClicked += (sender, args) =>
            {
                
            };
            listener.OnAdClosed += (sender, args) =>
            {
                
            };
            listener.OnVideoStarted += (sender, args) =>
            {
                
            };
            listener.OnVideoCompleted += (sender, args) =>
            {
                
            };
            listener.OnRewarded += (sender, args) =>
            {
                
            };
            listener.OnRewardFailed += (sender, args) =>
            {
                
            };
            listener.OnAdUnitRequest += (sender, args) =>
            {
                
            };
            listener.OnAdUnitLoaded += (sender, args) =>
            {
                
            };
            listener.OnAdUnitFailedToLoad += (sender, args) =>
            {
               
            };
            listener.OnAdUnitShown += (sender, args) =>
            {

            };
            listener.OnAdUnitCallShow += (sender, args) =>
            {

            };
            listener.OnAdUnitClicked += (sender, args) =>
            {

            };
            listener.OnAdUnitClosed += (sender, args) =>
            {

            };
            listener.OnAdUnitVideoStarted += (sender, args) =>
            {

            };
            listener.OnAdUnitVideoCompleted += (sender, args) =>
            {

            };
            listener.OnAdUnitRewarded += (sender, args) =>
            {

            };
            listener.OnAdUnitRewardFailed += (sender, args) =>
            {

            };
            m_ProbeMgr.init();
            m_ProbeMgr.registerTrackListener(listener);
        }


    }
}
