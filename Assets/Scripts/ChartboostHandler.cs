using UnityEngine;
using System.Collections;
using ChartboostSDK;

public class ChartboostHandler
{
	private bool autocache = true;
	private bool showInterstitial = true;
	private bool showMoreApps = true;
	private bool showRewardedVideo = true;
	static ChartboostHandler m_instance = null;
	static bool m_isInitialize = false;

	private static ChartboostHandler getInstance()
	{
		if (m_instance == null) {
			m_instance = new ChartboostHandler();
		}
		return m_instance;
	}
	
	// Use this for initialization
	public static void initialize () {
		if (m_isInitialize) {
			return;
		}
		m_isInitialize = true;
		getInstance().SetupDelegates();
		Chartboost.setAutoCacheAds(getInstance().autocache);
		Chartboost.cacheInterstitial (CBLocation.Default);
		Debug.Log("Arslan::Chartboost::Is Initialized: " + Chartboost.isInitialized());
	}

	void SetupDelegates()
	{
		// Listen to all impression-related events
		Chartboost.didInitialize += didInitialize;
		Chartboost.didFailToLoadInterstitial += didFailToLoadInterstitial;
		Chartboost.didDismissInterstitial += didDismissInterstitial;
		Chartboost.didCloseInterstitial += didCloseInterstitial;
		Chartboost.didClickInterstitial += didClickInterstitial;
		Chartboost.didCacheInterstitial += didCacheInterstitial;
		Chartboost.shouldDisplayInterstitial += shouldDisplayInterstitial;
		Chartboost.didDisplayInterstitial += didDisplayInterstitial;
		Chartboost.didFailToLoadMoreApps += didFailToLoadMoreApps;
		Chartboost.didDismissMoreApps += didDismissMoreApps;
		Chartboost.didCloseMoreApps += didCloseMoreApps;
		Chartboost.didClickMoreApps += didClickMoreApps;
		Chartboost.didCacheMoreApps += didCacheMoreApps;
		Chartboost.shouldDisplayMoreApps += shouldDisplayMoreApps;
		Chartboost.didDisplayMoreApps += didDisplayMoreApps;
		Chartboost.didFailToRecordClick += didFailToRecordClick;
		Chartboost.didFailToLoadRewardedVideo += didFailToLoadRewardedVideo;
		Chartboost.didDismissRewardedVideo += didDismissRewardedVideo;
		Chartboost.didCloseRewardedVideo += didCloseRewardedVideo;
		Chartboost.didClickRewardedVideo += didClickRewardedVideo;
		Chartboost.didCacheRewardedVideo += didCacheRewardedVideo;
		Chartboost.shouldDisplayRewardedVideo += shouldDisplayRewardedVideo;
		Chartboost.didCompleteRewardedVideo += didCompleteRewardedVideo;
		Chartboost.didDisplayRewardedVideo += didDisplayRewardedVideo;
		Chartboost.didCacheInPlay += didCacheInPlay;
		Chartboost.didFailToLoadInPlay += didFailToLoadInPlay;
		Chartboost.didPauseClickForConfirmation += didPauseClickForConfirmation;
		Chartboost.willDisplayVideo += willDisplayVideo;
	}

	void didInitialize(bool status) {
		Debug.Log(string.Format("Arslan::Chartboost::didInitialize: {0}", status));
	}

	void didFailToLoadInterstitial(CBLocation location, CBImpressionError error) {
		Debug.Log(string.Format("Arslan::Chartboost::didFailToLoadInterstitial: {0} at location {1}", error, location));
	}
	
	void didDismissInterstitial(CBLocation location) {
		Debug.Log("Arslan::Chartboost::didDismissInterstitial: " + location);
	}
	
	void didCloseInterstitial(CBLocation location) {
		Debug.Log("Arslan::Chartboost::didCloseInterstitial: " + location);
	}
	
	void didClickInterstitial(CBLocation location) {
		Debug.Log("Arslan::Chartboost::didClickInterstitial: " + location);
	}
	
	void didCacheInterstitial(CBLocation location) {
		Debug.Log("Arslan::Chartboost::didCacheInterstitial: " + location);
	}
	
	bool shouldDisplayInterstitial(CBLocation location) {
		// return true if you want to allow the interstitial to be displayed
		Debug.Log("Arslan::Chartboost::shouldDisplayInterstitial @" + location + " : " + showInterstitial);
		return showInterstitial;
	}
	
	void didDisplayInterstitial(CBLocation location){
		Debug.Log("Arslan::Chartboost::didDisplayInterstitial: " + location);
	}
	
	void didFailToLoadMoreApps(CBLocation location, CBImpressionError error) {
		Debug.Log(string.Format("Arslan::Chartboost::didFailToLoadMoreApps: {0} at location: {1}", error, location));
	}
	
	void didDismissMoreApps(CBLocation location) {
		Debug.Log(string.Format("Arslan::Chartboost::didDismissMoreApps at location: {0}", location));
	}
	
	void didCloseMoreApps(CBLocation location) {
		Debug.Log(string.Format("Arslan::Chartboost::didCloseMoreApps at location: {0}", location));
	}
	
	void didClickMoreApps(CBLocation location) {
		Debug.Log(string.Format("Arslan::Chartboost::didClickMoreApps at location: {0}", location));
	}
	
	void didCacheMoreApps(CBLocation location) {
		Debug.Log(string.Format("Arslan::Chartboost::didCacheMoreApps at location: {0}", location));
	}
	
	bool shouldDisplayMoreApps(CBLocation location) {
		Debug.Log(string.Format("Arslan::Chartboost::shouldDisplayMoreApps at location: {0}: {1}", location, showMoreApps));
		return showMoreApps;
	}
	
	void didDisplayMoreApps(CBLocation location){
		Debug.Log("Arslan::Chartboost::didDisplayMoreApps: " + location);
	}
	
	void didFailToRecordClick(CBLocation location, CBClickError error) {
		Debug.Log(string.Format("Arslan::Chartboost::didFailToRecordClick: {0} at location: {1}", error, location));
	}
	
	void didFailToLoadRewardedVideo(CBLocation location, CBImpressionError error) {
		Debug.Log(string.Format("Arslan::Chartboost::didFailToLoadRewardedVideo: {0} at location {1}", error, location));
	}
	
	void didDismissRewardedVideo(CBLocation location) {
		Debug.Log("Arslan::Chartboost::didDismissRewardedVideo: " + location);
	}
	
	void didCloseRewardedVideo(CBLocation location) {
		Debug.Log("Arslan::Chartboost::didCloseRewardedVideo: " + location);
	}
	
	void didClickRewardedVideo(CBLocation location) {
		Debug.Log("Arslan::Chartboost::didClickRewardedVideo: " + location);
	}
	
	void didCacheRewardedVideo(CBLocation location) {
		Debug.Log("Arslan::Chartboost::didCacheRewardedVideo: " + location);
	}
	
	bool shouldDisplayRewardedVideo(CBLocation location) {
		Debug.Log("Arslan::Chartboost::shouldDisplayRewardedVideo @" + location + " : " + showRewardedVideo);
		return showRewardedVideo;
	}
	
	void didCompleteRewardedVideo(CBLocation location, int reward) {
		Debug.Log(string.Format("Arslan::Chartboost::didCompleteRewardedVideo: reward {0} at location {1}", reward, location));
	}
	
	void didDisplayRewardedVideo(CBLocation location){
		Debug.Log("Arslan::Chartboost::didDisplayRewardedVideo: " + location);
	}
	
	void didCacheInPlay(CBLocation location) {
		Debug.Log("Arslan::Chartboost::didCacheInPlay called: "+location);
	}
	
	void didFailToLoadInPlay(CBLocation location, CBImpressionError error) {
		Debug.Log(string.Format("Arslan::Chartboost::didFailToLoadInPlay: {0} at location: {1}", error, location));
	}
	
	void didPauseClickForConfirmation() {
		Debug.Log("Arslan::Chartboost::didPauseClickForConfirmation called");
	}
	
	void willDisplayVideo(CBLocation location) {
		Debug.Log("Arslan::Chartboost::willDisplayVideo: " + location);
	}

	void OnDisable() {
		// Remove event handlers
		Chartboost.didInitialize -= didInitialize;
		Chartboost.didFailToLoadInterstitial -= didFailToLoadInterstitial;
		Chartboost.didDismissInterstitial -= didDismissInterstitial;
		Chartboost.didCloseInterstitial -= didCloseInterstitial;
		Chartboost.didClickInterstitial -= didClickInterstitial;
		Chartboost.didCacheInterstitial -= didCacheInterstitial;
		Chartboost.shouldDisplayInterstitial -= shouldDisplayInterstitial;
		Chartboost.didDisplayInterstitial -= didDisplayInterstitial;
		Chartboost.didFailToLoadMoreApps -= didFailToLoadMoreApps;
		Chartboost.didDismissMoreApps -= didDismissMoreApps;
		Chartboost.didCloseMoreApps -= didCloseMoreApps;
		Chartboost.didClickMoreApps -= didClickMoreApps;
		Chartboost.didCacheMoreApps -= didCacheMoreApps;
		Chartboost.shouldDisplayMoreApps -= shouldDisplayMoreApps;
		Chartboost.didDisplayMoreApps -= didDisplayMoreApps;
		Chartboost.didFailToRecordClick -= didFailToRecordClick;
		Chartboost.didFailToLoadRewardedVideo -= didFailToLoadRewardedVideo;
		Chartboost.didDismissRewardedVideo -= didDismissRewardedVideo;
		Chartboost.didCloseRewardedVideo -= didCloseRewardedVideo;
		Chartboost.didClickRewardedVideo -= didClickRewardedVideo;
		Chartboost.didCacheRewardedVideo -= didCacheRewardedVideo;
		Chartboost.shouldDisplayRewardedVideo -= shouldDisplayRewardedVideo;
		Chartboost.didCompleteRewardedVideo -= didCompleteRewardedVideo;
		Chartboost.didDisplayRewardedVideo -= didDisplayRewardedVideo;
		Chartboost.didCacheInPlay -= didCacheInPlay;
		Chartboost.didFailToLoadInPlay -= didFailToLoadInPlay;
		Chartboost.didPauseClickForConfirmation -= didPauseClickForConfirmation;
		Chartboost.willDisplayVideo -= willDisplayVideo;
	}

	public static bool isCached() {
		if (Chartboost.hasInterstitial (CBLocation.Default)) {
			Debug.Log("Arslan::Chartboost::isCached: true");
			return true;
		}
		Debug.Log("Arslan::Chartboost::isCached: false");
		Chartboost.cacheInterstitial (CBLocation.Default);
		return false;
	}

	public static bool showInterstatialAd() {
		if (isCached ()) {
			Debug.Log("Arslan::Chartboost::showInterstatialAd: true");
			Chartboost.showInterstitial(CBLocation.Default);
			Chartboost.cacheInterstitial (CBLocation.Default);
			return true;
		}
		Debug.Log("Arslan::Chartboost::showInterstatialAd: false");
		return false;
	}
}
