namespace SRDebugger
{
    public class CategoryColorsMap
    {
        public const string GreenColor = "#00ff00"; 
        public const string BlueColor = "#00ffff"; 
        public const string PinkColor = "#dda0dd"; 
        public const string YellowColor = "#E6CC22";
        public const string OrangeColor = "#FFA07A";
        
        public const string GreenColor_lightMode = "#005500"; 
        public const string BlueColor_lightMode = "#3455DB"; 
        public const string PinkColor_lightMode = "#B500B5"; 
        public const string YellowColor_lightMode = "#634806";
        public const string OrangeColor_lightMode = "#aa2e00";
        
        public static string ToLightModeColor(string colorHex)
        {
            switch (colorHex)
            {
                case GreenColor: return GreenColor_lightMode;
                case BlueColor: return BlueColor_lightMode;
                case PinkColor: return PinkColor_lightMode;
                case YellowColor: return YellowColor_lightMode;
                case OrangeColor: return OrangeColor_lightMode;
            }

            return colorHex;
        }
    }
}