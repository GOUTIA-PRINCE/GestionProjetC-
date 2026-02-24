using System.Drawing;

namespace GestionProjet
{
    public static class Theme
    {
        public static Color Primary = Color.FromArgb(63, 81, 181); // Indigo
        public static Color PrimaryDark = Color.FromArgb(48, 63, 159);
        public static Color PrimaryLight = Color.FromArgb(197, 202, 233);
        
        public static Color Accent = Color.FromArgb(255, 64, 129); // Pink
        
        public static Color SidebarBack = Color.FromArgb(33, 37, 41); // Dark grey
        public static Color SidebarText = Color.FromArgb(248, 249, 250);
        public static Color SidebarSelected = Color.FromArgb(73, 80, 87);
        
        public static Color Background = Color.FromArgb(240, 242, 245);
        public static Color CardBack = Color.White;
        
        public static Color TextPrimary = Color.FromArgb(33, 37, 41);
        public static Color TextSecondary = Color.FromArgb(108, 117, 125);
        
        public static Color Success = Color.FromArgb(40, 167, 69);
        public static Color Warning = Color.FromArgb(255, 193, 7);
        public static Color Danger = Color.FromArgb(220, 53, 69);
        public static Color Info = Color.FromArgb(23, 162, 184);

        public static Font AppFont(float size, FontStyle style = FontStyle.Regular)
        {
            return new Font("Segoe UI", size, style);
        }
    }
}
