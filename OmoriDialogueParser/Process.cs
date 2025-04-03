using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OmoriDialogueParser.Model.Data;
using OmoriDialogueParser.Model;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace OmoriDialogueParser
{
    internal class Process
    {
        // W:\OMORI Modding\rainymari\www_playtest\languages\en
        public static void run(string pathToProject, string pathToOuput)
        {
            //var langDir = new DirectoryInfo(pathToProject);
            var projectDir = new DirectoryInfo(pathToProject);

            var system = JsonConvert.DeserializeObject<Model.System>(File.ReadAllText(Path.Combine(pathToProject, "data", "System.json")));

            var fileParsedDict = new Dictionary<string, IEnumerable<ExportMessage>>();

            var langDir = new DirectoryInfo(Path.Combine(pathToProject, "languages", "en"));

            Util.Log("Exporting dialogue.\n");

            foreach (var file in langDir.EnumerateFiles("*.yaml"))
            {
                /*// Skip the monster book
                if (file.Name == "Bestiary.yaml")
                    continue;

                // Skip the database
                if (file.Name == "Database.yaml")
                    continue;

                // Skip menus
                if (file.Name == "menus.yaml")
                    continue;

                // Skip System
                if (file.Name == "System.yaml")
                    continue;*/

                Util.Log($"-> {file.Name}");
                var textReader = file.OpenText();

                try
                {
                    var languageFile = YamlInterpreter.Parse(textReader, system);

                    if (languageFile == null)
                    {
                        Util.Log($"   -> {file.Name} was empty!");
                        continue;
                    }

                    var messages = languageFile.Messages.Select(languageFileMessage => new ExportMessage
                    {
                        Html = languageFileMessage.Value.Text.ToHtml(),
                        Speaker = languageFileMessage.Value.Text.GetSpeaker(),
                        Background = languageFileMessage.Value.Background,
                        FaceIndex = languageFileMessage.Value.Faceindex,
                        FaceSet = languageFileMessage.Value.Faceset
                    }).ToList();

                    fileParsedDict.Add(Path.GetFileNameWithoutExtension(file.Name), messages);
                }
                catch (Exception ex)
                {
                    Util.Log("  -> YamlInterpreter ERROR: \n" + ex);
                }
                finally
                {
                    textReader.Close();
                }
            }

            // Create html output directory
            //Directory.CreateDirectory(Path.Combine(pathToOuput, "html"));
            string sourceFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "html");
            string destinationFolder = Path.Combine(pathToOuput, "html");

            try
            {
                FileSystem.CopyDirectory(sourceFolder, destinationFolder, overwrite:true);
                Util.Log("Template folder copied successfully!");
            }
            catch (Exception ex)
            {
                Util.Log("Error copying folder:\n" + ex.Message + "\n" + ex.Data);
            }

            File.WriteAllText(Path.Combine(pathToOuput, "html", "text.js"), $"var t = {JsonConvert.SerializeObject(fileParsedDict)}");

            // Create another version of the object, sorted by character name
            var byCharaDict = new Dictionary<string, Dictionary<string, List<ExportMessage>>>();

            foreach (var parsed in fileParsedDict)
            {
                foreach (var message in parsed.Value)
                {
                    if (string.IsNullOrEmpty(message.Speaker))
                        continue;

                    // We want to filter all of the "time" messages
                    if (Regex.IsMatch(message.Speaker, "[0-9].*"))
                        continue;

                    if (!byCharaDict.ContainsKey(message.Speaker))
                        byCharaDict.Add(message.Speaker, new Dictionary<string, List<ExportMessage>>());

                    if (!byCharaDict[message.Speaker].ContainsKey(parsed.Key))
                        byCharaDict[message.Speaker].Add(parsed.Key, new List<ExportMessage>());

                    byCharaDict[message.Speaker][parsed.Key].Add(message);
                }
            }

            File.WriteAllText(Path.Combine(pathToOuput, "html", "text_bychara.js"), $"var t = {JsonConvert.SerializeObject(byCharaDict)}");

            // Make variable page
            var variables = string.Empty;
            for (var i = 0; i < system.variables.Count; i++)
            {
                variables +=
                    $"\n<tr>\r\n            <td><a id=\"v{i}\" href=\"#v{i}\">{i}</a></td>\r\n            <td>{system.variables[i]}</td>\r\n        </tr>";
            }

            var varTemplate = File.ReadAllText(Path.Combine("template", "variables.htmt"));
            File.WriteAllText(Path.Combine(pathToOuput, "html", "variables.html"), string.Format(varTemplate, variables));

            var switches = string.Empty;
            for (var i = 0; i < system.switches.Count; i++)
            {
                switches +=
                    $"\n<tr>\r\n            <td><a id=\"v{i}\" href=\"#v{i}\">{i}</a></td>\r\n            <td>{system.switches[i]}</td>\r\n        </tr>";
            }

            var switchesTemplate = File.ReadAllText(Path.Combine("template", "switches.htmt"));
            File.WriteAllText(Path.Combine(pathToOuput, "html", "switches.html"), string.Format(switchesTemplate, switches));

            ExportMaps(pathToProject, pathToOuput);

            try
            {
                FileSystem.CopyDirectory(Path.Combine(pathToProject, "img", "faces"), Path.Combine(pathToOuput, "html", "faces"), overwrite: true);
                //FileSystem.CopyDirectory(sourceFolder, destinationFolder);
                Util.Log("Copied portraits.");
            }
            catch (Exception ex)
            {
                Util.Log("Error copying portraits:\n" + ex.Message + "\n" + ex.Data);
            }

            Util.Log("Done!");
        }

        private static void ExportMaps(string pathToProject, string pathToOuput)
        {
            Directory.CreateDirectory(Path.Combine(pathToOuput, "html", "map", "doc"));

            var mapInfos =
                JsonConvert.DeserializeObject<List<MapEntry>>(File.ReadAllText(Path.Combine(pathToProject, "data", "MapInfos.json")))
                    .Where(x => x != null).OrderBy(x => x.Order).ToArray();

            var maps = string.Empty;
            for (var i = 0; i < mapInfos.Length; i++)
            {
                var id = mapInfos[i].Id;

                var mapLink = $"<a href=\"map.html#{id}\">{mapInfos[i].Name}</a>";
                if (!File.Exists(Path.Combine(pathToOuput, "html", "map", "img", $"map{id}.png")))
                    mapLink = mapInfos[i].Name;

                maps +=
                    $"\n<tr>\r\n            <td><a id=\"m{id}\" href=\"#m{id}\">{id}</a></td>\r\n            <td>{mapLink}</td>\r\n        </tr>";

                var eMap = new ExportMap();
                eMap.Name = mapInfos[i].Name;
                var realDataPath = Path.Combine(pathToProject, "data", $"Map{id:D3}.json");
                if (File.Exists(realDataPath))
                    eMap.DataMap = JsonConvert.DeserializeObject(File.ReadAllText(realDataPath));
                File.WriteAllText(Path.Combine(pathToOuput, "html", "map", "doc", $"map{id}.json"), JsonConvert.SerializeObject(eMap));
            }

            var mapsTemplate = File.ReadAllText(Path.Combine("template", "maps.htmt"));
            File.WriteAllText(Path.Combine(pathToOuput, "html", "maps.html"), string.Format(mapsTemplate, maps));
        }
    }
}
