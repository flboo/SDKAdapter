using System;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using ROXStrategy.Api;
using UnityEngine;
using UnityEngine.UI;


namespace GameWish.Game
{
    public class RichOXDemo : MonoBehaviour
    {
        private bool m_Open;

        public Button demoBtn;
        public CanvasGroup demoGroup;
        public InputField taskIDInputField;
        public InputField realNameInputField;
        public InputField cardInputField;
        public InputField phoneInputField;
        public Button button;
        public Button button1;
        public Button button2;
        public Button button3;
        public Button button4;
        public Button button5;
        public Button button6;
        public Button button7;
        public Button button8;
        public Button button9;
        public Button button10;
        public Button button11;
        public Button button12;
        public Button button13;
        public Button button14;
        public Button button15;
        public Button button16;

        void Awake()
        {
            demoGroup.alpha = 0;
            demoGroup.interactable = false;
            demoGroup.blocksRaycasts = false;

            demoBtn.onClick.AddListener(delegate
            {
                m_Open = !m_Open;
                demoGroup.alpha = m_Open ? 1 : 0;
                demoGroup.interactable = m_Open;
                demoGroup.blocksRaycasts = m_Open;
            });

            button.onClick.AddListener(delegate
            {
                RichOXMgr.S.GetUserInfo((respond) =>
                {
                    if (respond.success)
                    {
                        Debug.Log("ox_user_info:" + respond.result.Name + "/" + respond.result.DeviceId + "/" +
                                  respond.result.IsNew + "/" + respond.result.Avatar + "/" + respond.result.WechatInfoCurrent.WXAppId);
                    }
                });
            });

            button1.onClick.AddListener(delegate
            {
                RichOXMgr.S.FetchAppNormalStrategy(SDKConfig.S.richOXConfig.strategyId, (respond) =>
                {
                    if (respond.success)
                    {
                        Debug.Log("ox_fetch_normal_strategy:" + respond.result.ToString());
                    }
                });
            });

            button2.onClick.AddListener(delegate
            {
                string taskId = taskIDInputField.text;
                double value = 0;
                if (!string.IsNullOrEmpty(phoneInputField.text))
                    value = Convert.ToDouble(phoneInputField.text);

                RichOXMgr.S.DoVariableMission(taskId, value, (respond) =>
                {
                    if (respond.success)
                    {
                        Debug.Log("ox_do_variable_mission:" + respond.result.ToString());
                    }
                });

            });

            button3.onClick.AddListener(delegate
            {
                string taskId = taskIDInputField.text;

                RichOXMgr.S.DoMission(taskId, (respond) =>
                {
                    if (respond.success)
                    {
                        Debug.Log("ox_do_mission:" + respond.result.ToString());
                    }

                });
            });

            button4.onClick.AddListener(delegate
            {
                RichOXMgr.S.QueryAssetInfo((respond) =>
                {
                    if (respond.success)
                    {
                        Debug.Log("ox_query_asset:" + respond.result.ToString());
                    }
                });
            });

            button5.onClick.AddListener(delegate
            {
                string taskId = taskIDInputField.text;
                RichOXMgr.S.ExtremeWithdraw(taskId, (respond) =>
                {
                    if (respond.success)
                    {
                        Debug.Log("ox_extremeWithdraw:" + respond.result.ToString());
                    }
                });
            });

            button6.onClick.AddListener(delegate
            {
                string taskId = taskIDInputField.text;
                string realName = realNameInputField.text;
                string cardId = cardInputField.text;
                string phone = phoneInputField.text;

                RichOXMgr.S.Withdraw(taskId, realName, cardId, phone, (respond) =>
                {
                    if (respond.success)
                    {
                        Debug.Log("ox_withdraw:" + respond.result.ToString());
                    }
                });
            });

            button7.onClick.AddListener(delegate
            {
                string taskId = taskIDInputField.text;
                double value = 0;
                if (!string.IsNullOrEmpty(realNameInputField.text))
                {
                    value = Convert.ToDouble(realNameInputField.text);
                }

                RichOXMgr.S.VariableTransform(taskId, value, (respond) =>
                {
                    if (respond.success)
                    {
                        Debug.Log("ox_variable_transform:" + respond.result.ToString());
                    }
                });
            });

            button8.onClick.AddListener(delegate
            {
                string taskId = taskIDInputField.text;
                RichOXMgr.S.Transform(taskId, (respond) =>
                {
                    if (respond.success)
                    {
                        Debug.Log("ox_transform:" + respond.result.ToString());
                    }
                });
            });

            button9.onClick.AddListener(delegate
            {
                string taskId = taskIDInputField.text;
                RichOXMgr.S.QueryMissionRecord(taskId, (respond) =>
                {
                    if (respond.success)
                    {
                        if (respond.success)
                        {
                            Debug.Log("ox_mission_record:" + respond.result.ToString());
                        }
                    }
                });
            });

            button10.onClick.AddListener(delegate
            {
                RichOXMgr.S.GetSectInfo((respond) =>
                {
                    if (respond.success)
                    {
                        respond.result.ToString();
                    }
                });
            });

            button11.onClick.AddListener(delegate
            {
                RichOXMgr.S.GetSectSettings((respond) =>
                {
                    if (respond.success)
                    {
                        respond.result.ToString();
                    }
                });
            });

            button12.onClick.AddListener(delegate
            {
                RichOXMgr.S.QueryAllMissionRecord((respond) =>
                {
                    if (respond.success)
                    {
                        Debug.Log("ox_all_mission_record:" + respond.result.ToString());
                    }
                });
            });

            button13.onClick.AddListener(delegate
            {
                RichOXMgr.S.GetInstallParams((respond) =>
                {
                    Debug.Log(respond.success);

                    if (respond.success)
                    {
                        foreach (DictionaryEntry dictionaryEntry in respond.result)
                        {
                            Debug.Log("ox_mob_param:" + dictionaryEntry.Key + "/" + dictionaryEntry.Value);
                        }
                    }
                });
            });

            button14.onClick.AddListener(delegate
            {
                Hashtable table = new Hashtable();
                table.Add("uid", RichOXSaveDataHandler.data.userId);
                RichOXMgr.S.GenShareUrl("test", table, (respond) =>
                 {
                     Debug.Log("ox_gen_url:" + respond.result);
                 });
            });

            button15.onClick.AddListener(delegate
            {
                RichOXMgr.S.QueryPiggyBankList((respond) =>
                {
                    Debug.Log("ox_piggy_query:" + respond.success);
                    if (respond.success)
                    {
                        for (int i = 0; i < respond.result.Count; i++)
                        {
                            Debug.Log("ox_piggy_query:" + respond.result[i].ToString());
                        }
                    }
                });
            });

            button16.onClick.AddListener(delegate
            {
                RichOXMgr.S.PiggyBankWithdraw(1030, (respond) =>
                {
                    Debug.Log("ox_piggy_withdraw:" + respond.success);
                });
            });
        }
    }

}