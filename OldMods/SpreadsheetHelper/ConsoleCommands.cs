using System;
using System.Collections.Generic;
using MIU;
using UnityEngine;

namespace OldMods.SpreadsheetHelper
{
    // Token: 0x02000002 RID: 2
    internal class ConsoleCommands
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        [ConsoleCommand(null, null, null, false, false, description = "Spreadsheet Helper, copy PBs into the clipboard", paramsDescription = "[chapter]")]
        public static string ssh(params string[] args)
        {
            bool flag = args.Length != 1;
            string text;
            if (flag)
            {
                text = "Requires one argument, the chapter number";
            }
            else
            {
                int num;
                bool flag2 = !int.TryParse(args[0], out num);
                if (flag2)
                {
                    text = args[0] + " is not a valid integer";
                }
                else
                {
                    bool flag3 = num <= 0 || num > 10;
                    if (flag3)
                    {
                        text = num.ToString() + " is not between 1 and 10";
                    }
                    else
                    {
                        List<MarbleChapter> chapters = GlobalContext.LevelData.chapters;
                        MarbleChapter marbleChapter = chapters[num - 1];
                        MonoBehaviour.print("Printing stats for " + marbleChapter.name);
                        string text2 = "";
                        HighScoreManager scores = GamePlayManager.Get(true).scores;
                        for (int i = 0; i < marbleChapter.levels.Count; i++)
                        {
                            MarbleLevel marbleLevel = marbleChapter.levels[i];
                            string text3 = (i + 1).ToString() + " - " + marbleLevel.name;
                            List<HighScoreRecord> list = new List<HighScoreRecord>();
                            scores.FetchLevelTopScores(marbleLevel.id, 1, list);
                            bool flag4 = text2 != "";
                            if (flag4)
                            {
                                text2 += "\n";
                            }
                            bool flag5 = list.Count > 0;
                            if (flag5)
                            {
                                string text4 = string.Format("{0}.{1:000}", (int)list[0].score, (int)(1000.0 * ((double)list[0].score % 1.0)));
                                text3 = string.Concat(new string[]
                                {
                                    text3,
                                    " - ",
                                    text4,
                                    " (",
                                    list[0].score.ToString(),
                                    ")"
                                });
                                text2 += text4;
                            }
                            MonoBehaviour.print(text3);
                        }
                        GUIUtility.systemCopyBuffer = text2;
                        text = "Scores copied to clipboard";
                    }
                }
            }
            return text;
        }
    }
}
