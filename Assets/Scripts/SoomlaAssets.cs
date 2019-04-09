using UnityEngine;
using System.Collections;
using Soomla.Store;
using System.Collections.Generic;

public class SoomlaAssets : IStoreAssets
{
    public int GetVersion() {
        return 0;
    }

    public VirtualCurrency[] GetCurrencies() {
		return new VirtualCurrency[]{};//ORBS_CURRENCY};
    }

    public VirtualGood[] GetGoods() {
		return new VirtualGood[] {REMOVEAD_LTVG, STARS_GOOD1, STARS_GOOD2, STARS_GOOD3};
    }

    public VirtualCurrencyPack[] GetCurrencyPacks() {
		return new VirtualCurrencyPack[] {};//ORBS_PACK1, ORBS_PACK2, ORBS_PACK3, ORBS_PACK4};
    }

    public VirtualCategory[] GetCategories() {
		return new VirtualCategory[]{};//GENERAL_CATEGORY};
    }

//    public const string ORBS_PACK1_PRODUCT_ID = "android.test.refunded";

//    public static VirtualCurrency ORBS_CURRENCY = new VirtualCurrency(
//        "Orbs",                                      // name
//        "",                                             // description
//        ORBS_CURRENCY_ITEM_ID                         // item id
//    );

	public const string STARS_PACK1_PRODUCT_ID = "com.magebear.star100";//"android.test.purchased";
	public const string STARS_PACK2_PRODUCT_ID = "com.magebear.star500";
	public const string STARS_PACK3_PRODUCT_ID = "com.magebear.star1000";

	public const string STARS_PACK1_ITEM_ID = "stars100";
	public const string STARS_PACK2_ITEM_ID = "stars500";
	public const string STARS_PACK3_ITEM_ID = "stars1000";

    /** Virtual Currency Packs **/

//    public static VirtualCurrencyPack ORBS_PACK2 = new VirtualCurrencyPack(
//		"3 Zap",                                   // name
//        "Buy 3 orbs",                 // description
//        ORBS2_ITEM_ID,                                   // item id
//        3,                                             // number of currencies in the pack
//		"zaps",                        // the currency associated with this pack
//		new PurchaseWithMarket(ORBS_PACK1_PRODUCT_ID, 1.99)
//    );
	

//    public static VirtualGood ORBS_GOOD1 = new SingleUseVG(
//        "1 Zap",                                               // name
//        "Buy 1 Zap", // description
//		ORBS1_ITEM_ID,                                               // item id
//		new PurchaseWithMarket(ORBS_CURRENCY_ITEM_ID, 0.99)); // the way this virtual good is purchased

	public static VirtualGood STARS_GOOD1 = new SingleUseVG(
		"100 Stars",                                               // name
		"Buy 100 Stars", // description
		STARS_PACK1_ITEM_ID,                                               // item id
		new PurchaseWithMarket(STARS_PACK1_PRODUCT_ID, 0.99)); // the way this virtual good is purchased

	public static VirtualGood STARS_GOOD2 = new SingleUseVG(
		"500 Stars",                                               // name
		"Buy 500 Stars", // description
		STARS_PACK2_ITEM_ID,                                               // item id
		new PurchaseWithMarket(STARS_PACK2_PRODUCT_ID, 2.99)); // the way this virtual good is purchased

	public static VirtualGood STARS_GOOD3 = new SingleUseVG(
		"1000 Stars",                                               // name
		"Buy 1000 Stars", // description
		STARS_PACK3_ITEM_ID,                                               // item id
		new PurchaseWithMarket(STARS_PACK3_PRODUCT_ID, 4.99)); // the way this virtual good is purchased

    /** Virtual Categories **/
    // The muffin rush theme doesn't support categories, so we just put everything under a general category.
//    public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory(
//        "General", new List<string>(new string[] { "1", "2" })
//    );

	public const string ADREMOVE_PRODUCT_ID = "com.magebear.removead";//"android.test.purchased";

	public const string ADREMOVE_ITEM_ID   = "removead";

    /** LifeTimeVGs **/
    // Note: create non-consumable items using LifeTimeVG with PuchaseType of PurchaseWithMarket
    public static VirtualGood REMOVEAD_LTVG = new LifetimeVG(
        "Remove Ads",                                             // name
        "Remove all ads",                                         // description
		ADREMOVE_ITEM_ID,                                              // item id
		new PurchaseWithMarket(ADREMOVE_PRODUCT_ID, 0.99));  			// the way this virtual good is purchased
}
