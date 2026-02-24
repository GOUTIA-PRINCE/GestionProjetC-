-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1
-- Généré le : mar. 24 fév. 2026 à 10:57
-- Version du serveur : 10.4.32-MariaDB
-- Version de PHP : 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `gestionprojet`
--

-- --------------------------------------------------------

--
-- Structure de la table `membres_projet`
--

CREATE TABLE `membres_projet` (
  `projet_id` int(11) NOT NULL,
  `utilisateur_id` int(11) NOT NULL,
  `role` varchar(50) DEFAULT 'Membre'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `membres_projet`
--

INSERT INTO `membres_projet` (`projet_id`, `utilisateur_id`, `role`) VALUES
(1, 3, 'Admin');

-- --------------------------------------------------------

--
-- Structure de la table `priorites`
--

CREATE TABLE `priorites` (
  `id` int(11) NOT NULL,
  `libelle` varchar(50) NOT NULL,
  `couleur_hex` varchar(7) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `priorites`
--

INSERT INTO `priorites` (`id`, `libelle`, `couleur_hex`) VALUES
(1, 'Basse', '#BCBCBC'),
(2, 'Moyenne', '#3498DB'),
(3, 'Haute', '#E67E22'),
(4, 'Urgente', '#E74C3C');

-- --------------------------------------------------------

--
-- Structure de la table `projets`
--

CREATE TABLE `projets` (
  `id` int(11) NOT NULL,
  `nom` varchar(100) NOT NULL,
  `description` text DEFAULT NULL,
  `date_creation` datetime DEFAULT current_timestamp(),
  `date_fin_prevue` date DEFAULT NULL,
  `createur_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `projets`
--

INSERT INTO `projets` (`id`, `nom`, `description`, `date_creation`, `date_fin_prevue`, `createur_id`) VALUES
(1, 'test', '', '2026-02-22 15:51:28', NULL, 3);

-- --------------------------------------------------------

--
-- Structure de la table `statuts`
--

CREATE TABLE `statuts` (
  `id` int(11) NOT NULL,
  `libelle` varchar(50) NOT NULL,
  `ordre` int(11) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `statuts`
--

INSERT INTO `statuts` (`id`, `libelle`, `ordre`) VALUES
(1, 'À faire', 1),
(2, 'En cours', 2),
(3, 'En test', 3),
(4, 'Terminé', 4);

-- --------------------------------------------------------

--
-- Structure de la table `taches`
--

CREATE TABLE `taches` (
  `id` int(11) NOT NULL,
  `titre` varchar(255) NOT NULL,
  `description` text DEFAULT NULL,
  `date_creation` datetime DEFAULT current_timestamp(),
  `date_echeance` datetime DEFAULT NULL,
  `projet_id` int(11) NOT NULL,
  `statut_id` int(11) NOT NULL,
  `priorite_id` int(11) DEFAULT NULL,
  `assignee_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `taches`
--

INSERT INTO `taches` (`id`, `titre`, `description`, `date_creation`, `date_echeance`, `projet_id`, `statut_id`, `priorite_id`, `assignee_id`) VALUES
(1, 'Creation de la base de donnees', 'cette tache consiste a concevoir la base de donnees a partir des differents diagrammes.', '2026-02-22 15:51:56', '2026-02-24 10:44:11', 1, 3, 1, 3);

-- --------------------------------------------------------

--
-- Structure de la table `utilisateurs`
--

CREATE TABLE `utilisateurs` (
  `id` int(11) NOT NULL,
  `nom` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `mot_de_passe` varchar(255) NOT NULL,
  `date_creation` datetime DEFAULT current_timestamp(),
  `est_actif` tinyint(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `utilisateurs`
--

INSERT INTO `utilisateurs` (`id`, `nom`, `email`, `mot_de_passe`, `date_creation`, `est_actif`) VALUES
(3, 'GOUTIA PRINCE', 'goutiaprince10@gmail.com', '1e835eee9c5e72180cda1790b7169ae8975f62b5f801613fb4c922586cc30de1', '2026-02-22 15:14:23', 1),
(4, 'zozo', 'test@gmail.com', '937e8d5fbb48bd4949536cd65b8d35c426b80d2f830c5c308e2cdec422ae2244', '2026-02-24 10:17:30', 1),
(5, 'GOUTIA PRINCE', 'goutiaprince@gmail.com', 'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', '2026-02-24 10:50:24', 0);

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `membres_projet`
--
ALTER TABLE `membres_projet`
  ADD PRIMARY KEY (`projet_id`,`utilisateur_id`),
  ADD KEY `utilisateur_id` (`utilisateur_id`);

--
-- Index pour la table `priorites`
--
ALTER TABLE `priorites`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `projets`
--
ALTER TABLE `projets`
  ADD PRIMARY KEY (`id`),
  ADD KEY `createur_id` (`createur_id`);

--
-- Index pour la table `statuts`
--
ALTER TABLE `statuts`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `taches`
--
ALTER TABLE `taches`
  ADD PRIMARY KEY (`id`),
  ADD KEY `projet_id` (`projet_id`),
  ADD KEY `statut_id` (`statut_id`),
  ADD KEY `priorite_id` (`priorite_id`),
  ADD KEY `assignee_id` (`assignee_id`);

--
-- Index pour la table `utilisateurs`
--
ALTER TABLE `utilisateurs`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `email` (`email`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `priorites`
--
ALTER TABLE `priorites`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT pour la table `projets`
--
ALTER TABLE `projets`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT pour la table `statuts`
--
ALTER TABLE `statuts`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT pour la table `taches`
--
ALTER TABLE `taches`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT pour la table `utilisateurs`
--
ALTER TABLE `utilisateurs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `membres_projet`
--
ALTER TABLE `membres_projet`
  ADD CONSTRAINT `membres_projet_ibfk_1` FOREIGN KEY (`projet_id`) REFERENCES `projets` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `membres_projet_ibfk_2` FOREIGN KEY (`utilisateur_id`) REFERENCES `utilisateurs` (`id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `projets`
--
ALTER TABLE `projets`
  ADD CONSTRAINT `projets_ibfk_1` FOREIGN KEY (`createur_id`) REFERENCES `utilisateurs` (`id`) ON DELETE SET NULL;

--
-- Contraintes pour la table `taches`
--
ALTER TABLE `taches`
  ADD CONSTRAINT `taches_ibfk_1` FOREIGN KEY (`projet_id`) REFERENCES `projets` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `taches_ibfk_2` FOREIGN KEY (`statut_id`) REFERENCES `statuts` (`id`),
  ADD CONSTRAINT `taches_ibfk_3` FOREIGN KEY (`priorite_id`) REFERENCES `priorites` (`id`),
  ADD CONSTRAINT `taches_ibfk_4` FOREIGN KEY (`assignee_id`) REFERENCES `utilisateurs` (`id`) ON DELETE SET NULL;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
