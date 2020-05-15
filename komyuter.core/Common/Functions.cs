using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TransitRealtime;
using static TransitRealtime.TranslatedString;

namespace komyuter.core.Common
{
    public class Functions
    {
        public static string CleanNumber(string dirtyNumber)
        {
            if (dirtyNumber.Length < 10)
                throw new Exception("Mobile number must be at least 10 characters long.");

            string cleanNumber = dirtyNumber.Substring(dirtyNumber.Length - 10);

            if (cleanNumber[0] != '9')
                throw new Exception("Mobile number is invalid.");

            if (!Regex.IsMatch(cleanNumber, @"^\d+$"))
                throw new Exception("Mobile number is invalid.");

            return cleanNumber;
        }

        public static bool ValidateMessage(string message)
        {
            string cleanMessage = message.ToUpper();
            string[] msgParts = message.ToUpper().Split(' ');

            switch (msgParts[0])
            {
                case "START":
                    return ValidateTrip(msgParts);
                case "ALERT":
                    return ValidateAlert(msgParts);
                default:
                    throw new Exception("Invalid message format (" + msgParts[0] + ").");
            }
        }

        public static void CreatePBFile(CloudStorageAccount storageAccount, string filename, byte[] feed)
        {
            // Create a CloudFileClient object for credentialed access to Azure Files.
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();

            // Get a reference to the file share we created previously.
            CloudFileShare share = fileClient.GetShareReference("gtfsrt");
            share.CreateIfNotExists();

            var rootDir = share.GetRootDirectoryReference();
            using (var stream = new MemoryStream(feed, writable: false))
            {
                rootDir.GetFileReference(filename).UploadFromStream(stream);//.UploadFromByteArray(feed,);
            }
        }

        public static bool IsValidTimeFormat(string input)
        {
            return TimeSpan.TryParse(input, out var dummyOutput);
        }

        public static int ComputeDelay(DateTime scheduledTime, DateTime actualTime)
        {
            TimeSpan span = (actualTime - scheduledTime);
            return Convert.ToInt32(Math.Ceiling(span.TotalSeconds));
        }

        public static TranslatedString GenerateTranslatedString(string text)
        {
            Translation transStr = new Translation();
            transStr.Text = text;
            transStr.Language = "en";

            TranslatedString translatedString = new TranslatedString();
            translatedString.Translations.Add(transStr);

            return translatedString;
        }

        //public SqlParameter getDayParam(DateTime phDate)
        public static string GetDayParam(DateTime phDate)
        {
            string dayOfWeek = "";

            switch (phDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dayOfWeek = "1______";
                    break;
                case DayOfWeek.Tuesday:
                    dayOfWeek = "_1_____";
                    break;
                case DayOfWeek.Wednesday:
                    dayOfWeek = "__1____";
                    break;
                case DayOfWeek.Thursday:
                    dayOfWeek = "___1___";
                    break;
                case DayOfWeek.Friday:
                    dayOfWeek = "____1__";
                    break;
                case DayOfWeek.Saturday:
                    dayOfWeek = "_____1_";
                    break;
                default:
                    dayOfWeek = "______1";
                    break;
            }

            return dayOfWeek;
        }

        public static byte[] ProtoSerialize<T>(T record) where T : class
        {
            if (null == record) return null;

            try
            {
                using (var stream = new MemoryStream())
                {
                    Serializer.Serialize(stream, record);
                    return stream.ToArray();
                }
            }
            catch
            {
                // Log error
                throw;
            }
        }

        public static UInt64 ToEpoch(DateTime value)
        {
            TimeSpan t = value - new DateTime(1970, 1, 1);
            UInt64 secondsSinceEpoch = (UInt64)t.TotalSeconds;

            return secondsSinceEpoch;
        }

        private static bool ValidateTrip(string[] msgParts)
        {
            // START MRT3 NB 0915

            if (msgParts.Length > 5)
                throw new Exception("Invalid message format (length).");

            if ("LRT1-LRT2-MRT3-PNR".IndexOf(msgParts[1]) <= -1)
                throw new Exception("Invalid message format (" + msgParts[1] + ").");

            if (("NB-SB-WB-EB".IndexOf(msgParts[2]) <= -1) 
                || (msgParts[1] == "LRT2" && (msgParts[2] == "NB" || msgParts[2] == "SB")) 
                || (msgParts[1] != "LRT2" && (msgParts[2] == "WB" || msgParts[2] == "EB")))
                throw new Exception("Invalid message format (" + msgParts[2] + ").");

            if ((msgParts[3].Length != 4) ||
                !TimeSpan.TryParse(msgParts[3].Substring(0, 2) + ":" + msgParts[3].Substring(2, 2), out TimeSpan startTime))
                throw new Exception("Invalid message format (" + msgParts[3] + ").");

            return true;
        }

        private static bool ValidateAlert(string[] msgParts)
        {
            if (msgParts.Length < 3)
                throw new Exception("Invalid message format (length).");

            if ("LRT1-LRT2-MRT3-PNR".IndexOf(msgParts[1]) <= -1)
                throw new Exception("Invalid message format (" + msgParts[1] + ").");

            return true;
        }

    }
}
