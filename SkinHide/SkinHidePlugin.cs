﻿using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using SkinHide.Patches;
using EFT.Visual;
using System.Collections.Generic;

namespace SkinHide
{
    [BepInPlugin("com.kmyuhkyuk.SkinHide", "kmyuhkyuk-SkinHide", "1.0.0")]
    public class SkinHidePlugin : BaseUnityPlugin
    {
        public static GameObject Player;

        public static GameObject PlayerModelView;

        public SkinDress[] PlayerMVSkinDress;

        public Dress[] PlayerMVDress;

        public SkinDress[] PlayerSkinDress;

        public Dress[] PlayerDress;

        public HashSet<GameObject> PlayerSkinGameObject = new HashSet<GameObject>();

        public static List <GameObject> Bot = new List<GameObject>();

        public SkinDress[] BotSkinDress;

        public Dress[] BotDress;

        public HashSet<GameObject> BotSkinGameObject = new HashSet<GameObject>();

        public static ConfigEntry<bool> KeyPlayerSkinHide { get; set; }
        public static ConfigEntry<bool> KeyBotSkinHide { get; set; }

        private void Start()
        {
            Logger.LogInfo("Loaded: kmyuhkyuk-SkinHide");
            KeyPlayerSkinHide = Config.Bind<bool>("玩家服装隐藏 Player Skin Hide", "开关 Swithc", false);
            KeyBotSkinHide = Config.Bind<bool>("Bot服装隐藏 Bot Skin Hide", "开关 Swithc", false);

            new PlayerModelViewPatch().Enable();
            new GamePlayerOwnerPatch().Enable();
            new BotOwnerPatch().Enable();
        }
        void Update()
        {
            //PlayerModelView Skin Hide
            if (PlayerModelView != null && KeyPlayerSkinHide.Value)
            {
                //Get PlayerModelView all SkinDress and Dress
                PlayerMVSkinDress = PlayerModelView.GetComponentsInChildren<SkinDress>();
                PlayerMVDress = PlayerModelView.GetComponentsInChildren<Dress>();

                //false SkinDress and Dress GameObject
                if (PlayerMVSkinDress != null)
                {
                    foreach (SkinDress skindress in PlayerMVSkinDress)
                    {
                        skindress.gameObject.SetActive(false);
                    }
                }
                if (PlayerMVDress != null)
                {
                    foreach (Dress dress in PlayerMVDress)
                    {
                        dress.gameObject.SetActive(false);
                    }
                }
            }
            //Player Skin Hide
            if (Player != null)
            {
                //Get Player all SkinDress and Dress
                PlayerSkinDress = Player.GetComponentsInChildren<SkinDress>();
                PlayerDress = Player.GetComponentsInChildren<Dress>();

                //false SkinDress and Dress GameObject
                if (PlayerSkinDress != null)
                {
                    foreach (SkinDress skindress in PlayerSkinDress)
                    {
                        PlayerSkinGameObject.Add(skindress.gameObject);
                    }
                }
                if (PlayerDress != null)
                {
                    foreach (Dress dress in PlayerDress)
                    {
                        PlayerSkinGameObject.Add(dress.gameObject);
                    }   
                }

                //false or true SkinDress and Dress GameObject
                if (PlayerSkinGameObject != null)
                {
                    foreach (GameObject skin in PlayerSkinGameObject)
                    {
                        skin.SetActive(!KeyPlayerSkinHide.Value);
                    }
                }
            }
            else
            {
                //Quit Raid Clear GameObject List
                PlayerSkinGameObject.Clear();
            }

            //Clear List null Bot
            Bot.RemoveAll(x => x == null);
            //Bot Skin Hide
            if (Bot.Count > 0)
            {
                //Get Bot all SkinDress and Dress
                foreach (GameObject bot in Bot)
                {
                    BotSkinDress = bot.GetComponentsInChildren<SkinDress>();
                    BotDress = bot.GetComponentsInChildren<Dress>();
                }

                if (BotSkinDress != null)
                {
                    foreach (SkinDress botskindess in BotSkinDress)
                    {
                        BotSkinGameObject.Add(botskindess.gameObject);
                    }
                }
                if (BotDress != null)
                {
                    foreach (Dress botdess in BotDress)
                    {
                        BotSkinGameObject.Add(botdess.gameObject);
                    }
                }

                //false or true SkinDress and Dress GameObject
                if (BotSkinGameObject != null)
                {
                    foreach (GameObject botskin in BotSkinGameObject)
                    {
                        botskin.SetActive(!KeyBotSkinHide.Value);
                    }
                }
            }
            else
            {
                //Quit Raid Clear GameObject List
                BotSkinGameObject.Clear();
            }
        }

    }
}