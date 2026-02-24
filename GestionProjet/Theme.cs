// =============================================================================
// Thème global de l'application GestionProjet
// Cette classe statique centralise toutes les couleurs et polices utilisées
// dans l'interface, permettant une cohérence visuelle sur l'ensemble du projet.
// =============================================================================

using System.Drawing;

namespace GestionProjet
{
    /// <summary>
    /// Classe statique définissant la charte graphique (couleurs et polices)
    /// utilisée dans toute l'application. Modifier une couleur ici l'applique
    /// partout automatiquement.
    /// </summary>
    public static class Theme
    {
        // ── Couleurs principales ──────────────────────────────────────────────

        /// <summary>Couleur primaire de l'application (Indigo).</summary>
        public static Color Primary = Color.FromArgb(63, 81, 181);

        /// <summary>Variante sombre de la couleur primaire.</summary>
        public static Color PrimaryDark = Color.FromArgb(48, 63, 159);

        /// <summary>Variante claire de la couleur primaire.</summary>
        public static Color PrimaryLight = Color.FromArgb(197, 202, 233);

        /// <summary>Couleur d'accent pour les éléments mis en valeur (Rose).</summary>
        public static Color Accent = Color.FromArgb(255, 64, 129);

        // ── Couleurs de la barre latérale (Sidebar) ───────────────────────────

        /// <summary>Couleur de fond de la barre latérale (Gris foncé).</summary>
        public static Color SidebarBack = Color.FromArgb(33, 37, 41);

        /// <summary>Couleur du texte dans la barre latérale.</summary>
        public static Color SidebarText = Color.FromArgb(248, 249, 250);

        /// <summary>Couleur de fond de l'élément sélectionné dans la sidebar.</summary>
        public static Color SidebarSelected = Color.FromArgb(73, 80, 87);

        // ── Couleurs de fond ──────────────────────────────────────────────────

        /// <summary>Couleur de fond général des pages.</summary>
        public static Color Background = Color.FromArgb(240, 242, 245);

        /// <summary>Couleur de fond des cartes / panneaux blancs.</summary>
        public static Color CardBack = Color.White;

        // ── Couleurs de texte ─────────────────────────────────────────────────

        /// <summary>Couleur principale du texte (Gris très foncé).</summary>
        public static Color TextPrimary = Color.FromArgb(33, 37, 41);

        /// <summary>Couleur secondaire du texte (Gris moyen).</summary>
        public static Color TextSecondary = Color.FromArgb(108, 117, 125);

        // ── Couleurs sémantiques ──────────────────────────────────────────────

        /// <summary>Couleur de succès (Vert).</summary>
        public static Color Success = Color.FromArgb(40, 167, 69);

        /// <summary>Couleur d'avertissement (Jaune).</summary>
        public static Color Warning = Color.FromArgb(255, 193, 7);

        /// <summary>Couleur d'erreur / danger (Rouge).</summary>
        public static Color Danger = Color.FromArgb(220, 53, 69);

        /// <summary>Couleur d'information (Bleu clair).</summary>
        public static Color Info = Color.FromArgb(23, 162, 184);

        // ── Utilitaire de police ──────────────────────────────────────────────

        /// <summary>
        /// Retourne une police "Segoe UI" à la taille et au style spécifiés.
        /// </summary>
        /// <param name="size">Taille de la police en points.</param>
        /// <param name="style">Style de la police (Regular par défaut).</param>
        /// <returns>Un objet <see cref="Font"/> prêt à l'emploi.</returns>
        public static Font AppFont(float size, FontStyle style = FontStyle.Regular)
        {
            return new Font("Segoe UI", size, style);
        }
    }
}
