using System;
#if (!UseHookGen)
using HarmonyLib;
using MonoMod.RuntimeDetour;
#endif

namespace MonoMod._ModTemplate.Patches;

public class ExampleShoppingCartPatch
{
    internal static void Init()
    {
        /*
#if (MMHOOKLocation != "")
         *  Subscribe with 'On.Namespace.Type.Method += CustomMethod;' for each method you're patching.
         *  Or if you are writing an ILHook, use 'IL.' instead of 'On.'
         *  Note that not all types are in a namespace, especially in Unity games.
#else
         *  Add to the Hooks list for each method you're patching with:
         *  
#if (PublicizeGameAssemblies)
         *  Hooks.Add(new Hook(
         *      typeof(Class).GetMethod(nameof(Class.Method), AccessTools.allDeclared),
         *      CustomClass.CustomMethod));
#else
         *  Hooks.Add(new Hook(
         *      typeof(Class).GetMethod("Method", AccessTools.allDeclared),
         *      CustomClass.CustomMethod));
#endif
#endif
         */

#if (UseHookGen)
        On.ShoppingCart.AddItemToCart += ShoppingCart_AddItemToCart;
#else
        AnotherExampleMod.Hooks.Add(new Hook(
#if (PublicizeGameAssemblies)
                typeof(ShoppingCart).GetMethod(nameof(ShoppingCart.AddItemToCart), AccessTools.allDeclared),
#else
                typeof(ShoppingCart).GetMethod("AddItemToCart", AccessTools.allDeclared),
#endif
                ExampleShoppingCartPatch.ShoppingCart_AddItemToCart));
#endif
    }

#if (UseHookGen)
    private static void ShoppingCart_AddItemToCart(On.ShoppingCart.orig_AddItemToCart orig, ShoppingCart self, ShopItem item)
#else
    private static void ShoppingCart_AddItemToCart(Action<ShoppingCart, ShopItem> orig, ShoppingCart self, ShopItem item)
#endif
    {
        // Call the Trampoline for the Original method or another method in the Detour Chain if any exist
        orig(self, item);

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
