using System;
#if (!UseHookGen)
using HarmonyLib;
#endif

namespace MonoMod._ModTemplate.Hooks;

public class ExampleShoppingCartPatch
{
#if (UseHookGen)
    internal static void AddItemToCart(On.ShoppingCart.orig_AddItemToCart original, ShoppingCart self, ShopItem item)
#else
    internal static void AddItemToCart(Action<ShoppingCart, ShopItem> original, ShoppingCart self, ShopItem item)
#endif
    {
        // Call Original Method
        original(self, item);

#if (PublicizeGameAssemblies)
        /*
         * Adding a random value to the visible price of the shopping cart typically is slightly
         * complicated due to the private setter of the CartValue property. However, as we have publicized the
         * game assembly, we do not have to worry about it, since it now is public.
         */
        self.CartValue += new Random().Next(0, 100);
#else
        /*
         * Adding a random value to the visible price of the shopping cart is slightly complicated
         * due to the private setter of the CartValue property. So to change the value, we must get the setter
         * via reflection, and call it with the new value.
         */
        AccessTools.PropertySetter(typeof(ShoppingCart), "CartValue").Invoke(
            self, new object[] { self.CartValue + new Random().Next(0, 100) });
#endif
    }
}
