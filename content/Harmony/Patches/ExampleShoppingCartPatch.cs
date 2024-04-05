using System;
using System.Reflection;
using HarmonyLib;

namespace Harmony._ModTemplate.Patches;

[HarmonyPatch(typeof(ShoppingCart))]
public class ExampleShoppingCartPatch
{
#if (PublicizeGameAssemblies)
    [HarmonyPatch(nameof(ShoppingCart.AddItemToCart))]
#else
    [HarmonyPatch("AddItemToCart")]
#endif
    [HarmonyPostfix]
    private static void AddItemToCartPostfix(ShoppingCart __instance)
    {
#if (PublicizeGameAssemblies)
        /*
         * Adding a random value to the visible price of the shopping cart typically is slightly
         * complicated due to the private setter of the CartValue property. However, as we have publicized the
         * game assembly, we do not have to worry about it, since it now is public.
         */
        __instance.CartValue += new Random().Next(0, 100);
#else
        /*
         * Adding a random value to the visible price of the shopping cart (not actual) is slightly complicated
         * due to the private setter of the CartValue property. So to change the value, we must get the setter
         * via reflection, and call it with the new value.
         */
        AccessTools.PropertySetter(typeof(ShoppingCart), "CartValue").Invoke(
            __instance, new object[] { __instance.CartValue + new Random().Next(0, 100) });
#endif
    }
}
