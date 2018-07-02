using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace Werewolf_Control.Helpers
{
    public static class Updater
    {
        private static BuildConfiguration[] Builds = new[]
        {
            new BuildConfiguration
            {
                BotDirSuffix = "Beta",
                BuildName = "Beta",
                ControlExeName = "Werewolf Control Beta"
            },
            new BuildConfiguration
            {
                BotDirSuffix = "Bot 1",
                BuildName = "Release",
                ControlExeName = "Werewolf Control"
            },
            //new BuildConfiguration
            //{
            //    BotDirSuffix = "Bot 2",
            //    BuildName = "Release 2",
            //    ControlExeName = "Werewolf Control 2"
            //},
        };


        public static void DoBuild(CallbackQuery query)
        {
            var msg = query.Message.Text + $"\n\nBeginning build...\n";

            Bot.ReplyToCallback(query, msg);
            //determine what we are building
            var updateType = query.Data.Split('|')[1];
            var beta = updateType.StartsWith("beta");
            var control = !updateType.Contains("node");
            var node = !updateType.Contains("control");

            msg += "Build Definition(s) to Use:";
            var definitions = new List<string>();
            var env = beta ? "Beta" : "Release";
            //var what = control ? node ? "Both" : "Control" : "Node";
            if (control)
                definitions.Add($"{env} Control");
            if (node)
                definitions.Add($"{env} Node");

            msg = definitions.Aggregate(msg, (current, a) => current + "\n" + a);
            Thread.Sleep(500);

            Bot.Edit(query, msg);
            Thread.Sleep(500);
            //now let's actually kick off that build
            msg = definitions.Aggregate(msg, (current, def) => current + ("\n" + QueueBuild(def).Result));
            Bot.Edit(query, msg);
        }


        public static void DoUpdate(CallbackQuery query)
        {
            var msg = query.Message.Text + "\n\nBeginning file moving...";
            var updateType = query.Data.Split('|')[1];
            try
            {
                Bot.ReplyToCallback(query, msg);
                //directories
                var uDir = "c:\\build\\";
                var controlDir = uDir + "Werewolf Control\\bin\\";
                var nodeDir = uDir + "Werewolf Node\\bin\\";

                var botBaseDir = "c:\\BOT\\Werewolf 4.0 ";


                //files
                var baseFiles = new[]
                {
                    "Database.dll", "Database.pdb", "TcpFramework.dll", "TcpFramework.pdb", "Telegram.Bot.dll",
                    "Telegram.Bot.xml"
                };
                //control has different names for each bot
                //node we will just copy the entire folder

                //stage the control files in the update folder
                foreach (var b in Builds)
                {
                    if (!updateType.StartsWith(b.BuildName.ToLower())) continue;
                    //update types can contain 'node', 'control', or 'both'
                    if (!updateType.Contains("node")) //if nodes only, don't update control
                    {
                        foreach (
                            var file in
                            Directory.GetFiles(controlDir + b.BuildName)
                                .Where(
                                    x =>
                                        baseFiles.Contains(Path.GetFileName(x)) ||
                                        Path.GetFileName(x).Contains(b.ControlExeName))
                        )
                        {
                            var fName = Path.GetFileName(file);
                            File.Copy(file, botBaseDir + b.BotDirSuffix + "\\Control\\Update\\" + fName,
                                true);
                        }

                        msg += $"\nCopied {b.BuildName} Control files";
                        Bot.ReplyToCallback(query, msg);
                    }

                    if (!updateType.Contains("control")) //if control only, don't update nodes
                    {
                        //now find the oldest node folder

                        var copied = false;

                        foreach (
                            var d in Directory.GetDirectories(botBaseDir + b.BotDirSuffix, "*Node*"))
                        {
                            //get the version of werewolf
                            //copy the node files to it
                            foreach (var file in Directory.GetFiles(nodeDir + b.BuildName))
                            {
                                var fName = Path.GetFileName(file);
                                copied = true;
                                try
                                {
                                    File.Copy(file, Path.Combine(d, fName), true);
                                }
                                catch (Exception e)
                                {
                                    if (e.Message.Contains("because it is being used by another process"))
                                        //nodes in this folder are still active D:
                                    {
                                        copied = false;
                                        break;
                                    }
                                    else
                                    {
                                        throw;
                                    }
                                }
                            }

                            if (copied)
                            {
                                msg += $"\nCopied {b.BuildName} Node files to " + d.Substring(d.LastIndexOf("\\") + 1);
                                Bot.ReplyToCallback(query, msg);
                                break;
                            }
                        }

                        if (!copied)
                            throw new Exception("Unable to copy Node files to a directory.");
                    }
                }


                //tell each bot to replace nodes

                //tell each bot to update
                msg += "\n\nCompleted Call, bots should now auto load updated files";
                Bot.ReplyToCallback(query, msg);
            }
            catch (Exception e)
            {
                Bot.ReplyToCallback(query, msg + "\n" + e.Message);
            }
        }


        public static async Task<string> QueueBuild(string buildDefinitionName)
        {
            try
            {
                var url = "https://parabola949.VisualStudio.com/DefaultCollection/";
                var build = new BuildHttpClient(new Uri(url),
                    new VssCredentials(new VssBasicCredential("", RegHelper.GetRegValue("VSTSToken"))));

                // First we get project's GUID and buildDefinition's ID.
                // Get the list of build definitions.
                var definitions = await build.GetDefinitionsAsync(project: "Werewolf");

                // Get the specified name of build definition.
                var target = definitions.First(d => d.Name == buildDefinitionName);

                // Build class has many properties, hoqever we can set only these properties.
                //ref: https://www.visualstudio.com/integrate/api/build/builds#queueabuild
                //In this nuget librari, we should set Project property.
                //It requires project's GUID, so we're compelled to get GUID by API.
                try
                {
                    var res = await build.QueueBuildAsync(new Build
                    {
                        Definition = new DefinitionReference
                        {
                            Id = target.Id
                        },
                        Project = target.Project
                    });
                    return $"Queued build with id: {res.Id}";
                }
                catch (VssServiceException e)
                {
                    return $"{e.Message}";
                }
            }
            catch (Exception e)
            {
                var t = e.GetType();
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                }

                return $"{e.Message}\n{e.StackTrace}";
            }
        }
    }

    class BuildConfiguration
    {
        public string BuildName { get; set; }
        public string BotDirSuffix { get; set; }
        public string ControlExeName { get; set; }
    }
}