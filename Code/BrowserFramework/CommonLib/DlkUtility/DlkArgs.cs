using System;

namespace CommonLib.DlkUtility
{
    public static class DlkArgs
    {
        public static String GetArg(String ArgName)
        {
            String ArgValue = "";
            String[] arguments = Environment.GetCommandLineArgs();
            for (int i = 0; i < arguments.Length; i++)
            {
                if (ArgName.ToLower() == arguments[i].ToLower())
                {
                    ArgValue = arguments[i + 1];
                    break;
                }
            }
            return ArgValue;
        }
    }
}
