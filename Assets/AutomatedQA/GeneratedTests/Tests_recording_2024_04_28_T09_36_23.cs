using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.AutomatedQA;
using Unity.CloudTesting;

namespace GeneratedAutomationTests
{
    /// <summary>
    /// These tests were generated by Unity Automated QA for the recording from the Assets folder at "\Recordings\recording-2024-04-28-T09-36-23.json".
    /// You can regenerate this file from the Unity Editor Menu: Automated QA > Generate Recorded Tests
    /// </summary>
    public class Tests_recording_2024_04_28_T09_36_23 : AutomatedQATestsBase
    {
        /// Generated from recording file: "\Recordings\recording-2024-04-28-T09-36-23.json".
        [UnityTest]
        [CloudTest]
        public IEnumerator CanPlayToEnd()
        {
            yield return Driver.Perform.Click(Scene_Main_Game_PageObject.Clickable_Main_Game_ShopButton);
            yield return Driver.Perform.Click(Scene_Main_Game_PageObject.Clickable_Main_Game_Tower1BuyButton);
            yield return Driver.Perform.Click(Scene_Main_Game_PageObject.Clickable_Main_Game_GameMap);
            yield return Driver.Perform.Click(Scene_Main_Game_PageObject.Clickable_Main_Game_ShopButton);
            yield return Driver.Perform.Click(Scene_Main_Game_PageObject.Clickable_Main_Game_Tower1BuyButton);
            yield return Driver.Perform.Click(Scene_Main_Game_PageObject.Clickable_Main_Game_GameMap);
        }
        // Initialize test run data.
        protected override void SetUpTestRun()
        {
            Test.entryScene = Scene_Main_Game_PageObject.SceneName;
            Test.recordedAspectRatio = new Vector2(321f,573f);
            Test.recordedResolution = new Vector2(1920f,1080f);
        }

    }
}
