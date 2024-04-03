#if (MMHOOKLocation == "")
using System;

#endif
namespace MonoMod._ModTemplate.Hooks;

public class ExampleShoppingCartPatch
{
#if (MMHOOKLocation != "")
    internal static void SwitchTVPatch(On.ShoppingCart.orig_AddItemToCart original, ShoppingCart self)
#else
    internal static void SwitchTVPatch(Action<ShoppingCart> original, ShoppingCart self)
#endif
    {
        // Call Original Method
        original(self);

#if (PublicizeGameAssemblies)
        /*
         * Adding a random value to the visible price of the shopping cart (not actual) typically is slightly
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
        AccessTools.PropertySetter(typeof(ShoppingCart), "CartValue").Invoke(ShoppingCart.CartValue + new Random().Next(0, 100));
#endif
    }
}
