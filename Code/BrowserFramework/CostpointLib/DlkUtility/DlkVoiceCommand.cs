using CSCore.CoreAudioAPI;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Globalization;

namespace CostpointLib.DlkUtility
{
    public static class DlkVoiceCommand
    {
        public static bool Listening = false;
        public static bool CommandCompleted { get; set; }

        public static VoiceStatus Status { get; private set; }

        private static SpeechSynthesizer synthesizer;

        public static void Listen()
        {
            CommandCompleted = false;
            Listening = true;
            Status = VoiceStatus.Paused;
            Task.Run(() => ListenMediaVoice()).ConfigureAwait(false);
        }

        public static void ExecuteVoiceCommand(string textCommand, bool maleVoice)
        {
            synthesizer = new SpeechSynthesizer();
            synthesizer.SpeakCompleted += Synthesizer_SpeakCompleted;
            synthesizer.Volume = 100;
            synthesizer.SetOutputToDefaultAudioDevice();
            VoiceGender voiceGender = maleVoice ? VoiceGender.Male : VoiceGender.Female;
            synthesizer.SelectVoiceByHints(voiceGender);
            //if (textCommand.Split(' ').Length == 1)
            //{
            //    Thread.Sleep(1000);

            //    PromptBuilder style = new PromptBuilder();
            //    style.AppendText(textCommand, PromptRate.Slow);
            //    synthesizer.SpeakAsync(style);
            //}
            //else
            //{
                Prompt promptText = new Prompt(textCommand);
                synthesizer.SpeakAsync(promptText);
            //}

            while (!CommandCompleted) ;
        }

        private static void Synthesizer_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            CommandCompleted = true;
        }

        public static void Stop()
        {
            CommandCompleted = false;
            Listening = false;
            synthesizer.SpeakCompleted -= Synthesizer_SpeakCompleted;
            synthesizer = null;
        }

        public static void Wait(bool addWaitTime = false)
        {
            while (Status == VoiceStatus.Playing || Status != VoiceStatus.Idle) ;

            if(addWaitTime)
            {
                //Thread.Sleep(500);
                int pauseCount = 0;
                while (Status == VoiceStatus.Playing || Status != VoiceStatus.Idle)
                {
                    if (Status == VoiceStatus.Paused)
                        ++pauseCount;

                    if (pauseCount == 5)
                        return;
                }
            }
        }

        public static bool Responding()
        {
            int retryCount = 1;

            while (retryCount != 15)
            {
                if (Status == VoiceStatus.Playing)
                    return true;
                retryCount++;
                Thread.Sleep(300);
            }
            return false;
        }

        private static void ListenMediaVoice()
        {
            int playCount = 0;
            int pauseCount = 0;
            int prevPlayCount = 0;
            int repeatedPlayPatternCounter = 0;

            while (Listening)
            {
                bool playing = IsAudioPlaying();
                
                if (playing)
                {
                    Status = VoiceStatus.Playing;

                    if (CommandCompleted)
                    {
                        ++playCount;                
                    }
                    pauseCount = 0;
                }
                else
                {
                    if (CommandCompleted && playCount > 0)
                    {
                        if (prevPlayCount == playCount)
                        {
                            ++repeatedPlayPatternCounter;
                        }
                        prevPlayCount = playCount;
                    }
                    else
                        prevPlayCount = 0;

                    if (pauseCount == 15 || repeatedPlayPatternCounter == 3)
                    {
                        Status = VoiceStatus.Idle;
                    }
                    else
                    {
                        Status = VoiceStatus.Paused;
                        ++pauseCount;                        
                    }
                    
                }
                Thread.Sleep(CommandCompleted ? 100 : 350);
            }
        }

        private static bool IsAudioPlaying()
        {
            using (var meter = AudioMeterInformation.FromDevice(GetDefaultRenderDevice()))
            {
                return meter.PeakValue > 0;
            }
        }

        private static MMDevice GetDefaultRenderDevice()
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                return enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            }
        }
    }

    public enum VoiceStatus
    {
        Playing,
        Paused,
        Idle
    }
}
