using UnityEngine;
using System.Collections;
using Soomla.Store;
using Soomla;
using System.Collections.Generic;
using GameAnalyticsSDK;
using GameAnalyticsSDK.Events;

public class InAppPurchaseManager : MonoBehaviour {

    private static InAppPurchaseManager instance = null;

    void Awake(){
        if(instance == null)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);

        } else 
        {
            GameObject.Destroy(this);
        }
    }

	void Start ()
    {
        StoreEvents.OnSoomlaStoreInitialized += onSoomlaStoreInitialized;
        StoreEvents.OnCurrencyBalanceChanged += onCurrencyBalanceChanged;
        StoreEvents.OnUnexpectedStoreError += onUnexpectedStoreError;

        StoreEvents.OnMarketPurchase += onMarketPurchase;
        StoreEvents.OnItemPurchased += onItemPurchased;
        StoreEvents.OnMarketPurchaseStarted += onMarketPurchaseStarted;
        StoreEvents.OnItemPurchaseStarted += onItemPurchaseStarted;
        StoreEvents.OnMarketPurchaseCancelled += onMarketPurchaseCancelled;
        StoreEvents.OnMarketPurchaseDeferred += onMarketPurchaseDeferred;
        StoreEvents.OnRestoreTransactionsStarted += onRestoreTransactionsStarted;
        StoreEvents.OnRestoreTransactionsFinished += onRestoreTransactionsFinished;

        SoomlaStore.Initialize(new SoomlaAssets());
	}
	
    public void onUnexpectedStoreError(int errorCode) {
        Debug.Log ("Arslan::InApp::onUnexpectedStoreError error with code: " + errorCode);
    }

    public void onSoomlaStoreInitialized() 
    {
        if (StoreInfo.Currencies.Count>0) {
            try {
//                //First launch reward
//                if(!firstLaunchReward.Owned)
//                {
//                    firstLaunchReward.Give();
//                }
                Debug.Log ("Arslan::InApp::onSoomlaStoreInitialized Currency balance: " + StoreInventory.GetItemBalance(StoreInfo.Currencies[0].ItemId));
            } catch (VirtualItemNotFoundException ex){
                Debug.Log ("Arslan::InApp::onSoomlaStoreInitialized " + ex.Message);
            }
        }
    }

    public void onCurrencyBalanceChanged(VirtualCurrency virtualCurrency, int balance, int amountAdded) {
        Debug.Log ("Arslan::InApp::onCurrencyBalanceChanged balance = " + balance + " amountAdded = " + amountAdded);
    }

    public void onMarketPurchase(PurchasableVirtualItem pvi, string payload, Dictionary<string, string> extra)
    {
        Debug.Log ("Arslan::InApp::onMarketPurchase pvi id = " + pvi.ID);

		if(pvi.ID.Equals("stars100"))
		{
			GameManager.Instance.addStars(100);
			GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Stars", 100, "IAP", "Stars100");
		}
		else if(pvi.ID.Equals("stars500"))
		{
			GameManager.Instance.addStars(500);
			GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Stars", 500, "IAP", "Stars500");
		}
		else if(pvi.ID.Equals("stars1000"))
		{
			GameManager.Instance.addStars(1000);
			GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Stars", 1000, "IAP", "Stars1000");
		}
		else if(pvi.ID.Equals("removead"))
		{
			AdHandler.GetInstance().hideAdmobBanner();
			ZPlayerPrefs.SetInt("ads", 0);
			GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "RemoveAd", 100, "IAP", "RemoveAd");
		}
    }

    /// <summary>
    /// Handles a market refund event.
    /// </summary>
    /// <param name="pvi">Purchasable virtual item.</param>
    public void onMarketRefund(PurchasableVirtualItem pvi) {
        Debug.Log ("Arslan::InApp::onMarketRefund pvi id = " + pvi.ID);
    }

    /// <summary>
    /// Handles an item purchase event.
    /// </summary>
    /// <param name="pvi">Purchasable virtual item.</param>
    public void onItemPurchased(PurchasableVirtualItem pvi, string payload)
	{
        Debug.Log ("Arslan::InApp::onItemPurchased pvi id = " + pvi.ID);

    }

    /// <summary>
    /// Handles a market purchase started event.
    /// </summary>
    /// <param name="pvi">Purchasable virtual item.</param>
    public void onMarketPurchaseStarted(PurchasableVirtualItem pvi) {
        Debug.Log ("Arslan::InApp::onMarketPurchaseStarted pvi id = " + pvi.ID);
    }

    /// <summary>
    /// Handles an item purchase started event.
    /// </summary>
    /// <param name="pvi">Purchasable virtual item.</param>
    public void onItemPurchaseStarted(PurchasableVirtualItem pvi) {
        Debug.Log ("Arslan::InApp::onItemPurchaseStarted pvi id = " + pvi.ID);
    }

    /// <summary>
    /// Handles an item purchase cancelled event.
    /// </summary>
    /// <param name="pvi">Purchasable virtual item.</param>
    public void onMarketPurchaseCancelled(PurchasableVirtualItem pvi) {
        Debug.Log ("Arslan::InApp::onMarketPurchaseCancelled pvi id = " + pvi.ID);
    }

    /// <summary>
    /// Handles an item purchase deferred event.
    /// </summary>
    /// <param name="pvi">Purchasable virtual item.</param>
    /// <param name="payload">Developer supplied payload.</param>
    public void onMarketPurchaseDeferred(PurchasableVirtualItem pvi, string payload) {
        Debug.Log ("Arslan::InApp::onMarketPurchaseDeferred pvi id = " + pvi.ID);
    }

    /// <summary>
    /// Handles a restore Transactions process started event.
    /// </summary>
    public void onRestoreTransactionsStarted() {
        Debug.Log ("Arslan::InApp::onRestoreTransactionsStarted");

    }

    /// <summary>
    /// Handles a restore transactions process finished event.
    /// </summary>
    /// <param name="success">If set to <c>true</c> success.</param>
    public void onRestoreTransactionsFinished(bool success) {
        Debug.Log ("Arslan::InApp::onRestoreTransactionsFinished success = " + success);
    }
}
