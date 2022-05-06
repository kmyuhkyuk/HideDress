﻿using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using System.Reflection;
using EFT.UI;
using System.Threading.Tasks;

namespace SkinHide.Patches
{
    public class PlayerModelViewPatch : ModulePatch
    {

        protected override MethodBase GetTargetMethod()
        {
            return typeof(PlayerModelView).GetMethod("method_0", PatchConstants.PrivateFlags);
        }

        [PatchPostfix]
        private static async void PatchPostfix(Task __result, PlayerModelView __instance)
        {
            await __result;

            SkinHidePlugin.PlayerModelView = __instance.gameObject;            
        }
    }
}