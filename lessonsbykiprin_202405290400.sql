﻿
﻿--
-- Script was generated by Devart dbForge Studio for MySQL, Version 10.0.150.0
-- Product home page: http://www.devart.com/dbforge/mysql/studio
-- Script date 29.05.2024 4:00:23
-- Server version: 8.0.36
--

--
-- Disable foreign keys
--
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

--
-- Set SQL mode
--
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

--
-- Set character set the client will use to send SQL statements to the server
--
SET NAMES 'utf8';

CREATE DATABASE lessonsbykiprin


--
-- Set default database
--
USE lessonsbykiprin;

--
-- Create table `lessons`
--
CREATE TABLE IF NOT EXISTS lessons (
  Lessons_Id int NOT NULL AUTO_INCREMENT,
  Title varchar(50) DEFAULT NULL,
  PRIMARY KEY (Lessons_Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 41,
AVG_ROW_LENGTH = 455,
CHARACTER SET utf8mb4,
ROW_FORMAT = DYNAMIC;

--
-- Create table `teacher`
--
CREATE TABLE IF NOT EXISTS teacher (
  Teacher_Id int NOT NULL AUTO_INCREMENT,
  Title varchar(50) DEFAULT NULL,
  Absent varchar(255) NOT NULL DEFAULT 'да',
  Lessons_ID int DEFAULT NULL,
  PRIMARY KEY (Teacher_Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 6,
AVG_ROW_LENGTH = 3276,
CHARACTER SET utf8mb4,
ROW_FORMAT = DYNAMIC;

--
-- Create foreign key
--
ALTER TABLE teacher
ADD CONSTRAINT FK_teacher_lessons_Lessons_Id FOREIGN KEY (Lessons_ID)
REFERENCES lessons (Lessons_Id);

--
-- Create table `course`
--
CREATE TABLE IF NOT EXISTS course (
  id int NOT NULL AUTO_INCREMENT,
  Title int DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 5,
CHARACTER SET utf8mb4,
ROW_FORMAT = DYNAMIC;

--
-- Create table `ggroups`
--
CREATE TABLE IF NOT EXISTS ggroups (
  Group_Id int NOT NULL AUTO_INCREMENT,
  Title varchar(50) DEFAULT NULL,
  ID_Course int DEFAULT NULL,
  PRIMARY KEY (Group_Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 24,
AVG_ROW_LENGTH = 1820,
CHARACTER SET utf8mb4,
ROW_FORMAT = DYNAMIC;

--
-- Create foreign key
--
ALTER TABLE ggroups
ADD CONSTRAINT FK_ggroups_course_id FOREIGN KEY (ID_Course)
REFERENCES course (id);

--
-- Create table `weekday`
--
CREATE TABLE IF NOT EXISTS weekday (
  id int NOT NULL AUTO_INCREMENT,
  Title varchar(255) DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 7,
CHARACTER SET utf8mb4,
ROW_FORMAT = DYNAMIC;

--
-- Create table `pairnumber`
--
CREATE TABLE IF NOT EXISTS pairnumber (
  id int NOT NULL AUTO_INCREMENT,
  Title int DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 7,
CHARACTER SET utf8mb4,
ROW_FORMAT = DYNAMIC;

--
-- Create table `cabinet`
--
CREATE TABLE IF NOT EXISTS cabinet (
  Cabinet_ID int NOT NULL AUTO_INCREMENT,
  Title varchar(255) DEFAULT NULL,
  Available varchar(255) NOT NULL DEFAULT 'Да',
  Appointment varchar(255) NOT NULL DEFAULT '-',
  PRIMARY KEY (Cabinet_ID)
)
ENGINE = INNODB,
AUTO_INCREMENT = 6,
AVG_ROW_LENGTH = 3276,
CHARACTER SET utf8mb4,
ROW_FORMAT = DYNAMIC;

--
-- Create table `timetable`
--
CREATE TABLE IF NOT EXISTS timetable (
  ID_GROUP int DEFAULT NULL,
  ID_CABINET int DEFAULT NULL,
  ID_LESSONS int DEFAULT NULL,
  ID_Pair_Number int DEFAULT NULL,
  ID_Week_Day int DEFAULT NULL,
  id int NOT NULL AUTO_INCREMENT,
  ID_TEACHER int DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
ROW_FORMAT = DYNAMIC;

--
-- Create foreign key
--
ALTER TABLE timetable
ADD CONSTRAINT FK_timetable_cabinet_Cabinet_ID FOREIGN KEY (ID_CABINET)
REFERENCES cabinet (Cabinet_ID);

--
-- Create foreign key
--
ALTER TABLE timetable
ADD CONSTRAINT FK_timetable_ggroups_Group_Id FOREIGN KEY (ID_GROUP)
REFERENCES ggroups (Group_Id);

--
-- Create foreign key
--
ALTER TABLE timetable
ADD CONSTRAINT FK_timetable_lessons_Lessons_Id FOREIGN KEY (ID_LESSONS)
REFERENCES lessons (Lessons_Id);

--
-- Create foreign key
--
ALTER TABLE timetable
ADD CONSTRAINT FK_timetable_pairnumber_id FOREIGN KEY (ID_Pair_Number)
REFERENCES pairnumber (id);

--
-- Create foreign key
--
ALTER TABLE timetable
ADD CONSTRAINT FK_timetable_teacher_Teacher_Id FOREIGN KEY (ID_TEACHER)
REFERENCES teacher (Teacher_Id);

--
-- Create foreign key
--
ALTER TABLE timetable
ADD CONSTRAINT FK_timetable_weekday_id FOREIGN KEY (ID_Week_Day)
REFERENCES weekday (id);

-- 
-- Dumping data for table lessons
--
INSERT INTO lessons VALUES
(1, 'Физ.культура'),
(2, 'Русский язык'),
(3, 'История'),
(4, 'Информатика'),
(5, 'ОБЖ'),
(6, 'Математика'),
(7, 'Англ.язык'),
(8, 'Физика'),
(9, 'Введение в сп'),
(10, 'Экономика'),
(11, 'Литература'),
(12, 'Родн.литерат'),
(13, 'Основы фин.грам'),
(14, 'Менеджмент'),
(15, 'Информ.технал'),
(16, 'Осн.маркет'),
(17, 'История'),
(18, 'ПМ01 МДК01.01'),
(19, 'Госуд.мун.сл'),
(20, 'Дискрет.математ-ка'),
(21, 'Психология общ.'),
(22, 'АФХД'),
(23, 'Правовое обес'),
(24, 'ПМ02 МДК02.01'),
(25, 'Требов. к зданиям'),
(26, 'Проф.этика'),
(27, 'ПМ02 МДК02.03'),
(28, 'ПМ05 МДК05.02'),
(29, 'Веб.прогр'),
(30, 'Экологич.основы'),
(31, 'Основы философии'),
(32, 'ПМ01 англ.яз'),
(33, 'Китайский язык'),
(34, 'Стилистика'),
(35, 'Экономика орг'),
(36, 'ОАИП');

-- 
-- Dumping data for table course
--
INSERT INTO course VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4);

-- 
-- Dumping data for table weekday
--
INSERT INTO weekday VALUES
(1, 'Понедельник'),
(2, 'Вторник'),
(3, 'Среда'),
(4, 'Четверг'),
(5, 'Пятница'),
(6, 'Суббота');

-- 
-- Dumping data for table teacher
--
INSERT INTO teacher VALUES
(1, 'Глушенко Л.А.', 'да', NULL),
(2, 'Пушкин А.А.', 'да', NULL),
(3, 'Борсова О.И.', 'да', NULL),
(4, 'Довгань С.В.', 'да', NULL),
(5, 'Ахрименко М.В.', 'да', NULL);

-- 
-- Dumping data for table pairnumber
--
INSERT INTO pairnumber VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6);

-- 
-- Dumping data for table ggroups
--
INSERT INTO ggroups VALUES
(1, '321', 1),
(2, '925', 2),
(3, '926', 3),
(4, '1021', 4),
(5, '1125', 1),
(6, '331-332', 2),
(7, '935', 3),
(8, '1031', 4),
(9, '1135', 1),
(23, '1223', 4);

-- 
-- Dumping data for table cabinet
--
INSERT INTO cabinet VALUES
(1, '4', 'Да', '-'),
(2, '1', 'Да', '-'),
(3, '22', 'Да', '-'),
(4, '313', 'Да', '-'),
(5, '32', 'Да', '-');

-- Table lessonsbykiprin.timetable does not contain any data (it is empty)

--
-- Restore previous SQL mode
--
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

--
-- Enable foreign keys
--
﻿--
-- Script was generated by Devart dbForge Studio for MySQL, Version 10.0.150.0
-- Product home page: http://www.devart.com/dbforge/mysql/studio
-- Script date 29.05.2024 4:00:23
-- Server version: 8.0.36
--

--
-- Disable foreign keys
--
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

--
-- Set SQL mode
--
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

--
-- Set character set the client will use to send SQL statements to the server
--
SET NAMES 'utf8';

DROP DATABASE IF EXISTS lessonsbykiprin;

CREATE DATABASE IF NOT EXISTS lessonsbykiprin
CHARACTER SET utf8mb4
COLLATE utf8mb4_0900_ai_ci;

--
-- Set default database
--
USE lessonsbykiprin;

--
-- Create table `lessons`
--
CREATE TABLE IF NOT EXISTS lessons (
  Lessons_Id int NOT NULL AUTO_INCREMENT,
  Title varchar(50) DEFAULT NULL,
  PRIMARY KEY (Lessons_Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 41,
AVG_ROW_LENGTH = 455,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
ROW_FORMAT = DYNAMIC;

--
-- Create table `teacher`
--
CREATE TABLE IF NOT EXISTS teacher (
  Teacher_Id int NOT NULL AUTO_INCREMENT,
  Title varchar(50) DEFAULT NULL,
  Absent varchar(255) NOT NULL DEFAULT 'да',
  Lessons_ID int DEFAULT NULL,
  PRIMARY KEY (Teacher_Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 6,
AVG_ROW_LENGTH = 3276,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
ROW_FORMAT = DYNAMIC;

--
-- Create foreign key
--
ALTER TABLE teacher
ADD CONSTRAINT FK_teacher_lessons_Lessons_Id FOREIGN KEY (Lessons_ID)
REFERENCES lessons (Lessons_Id);

--
-- Create table `course`
--
CREATE TABLE IF NOT EXISTS course (
  id int NOT NULL AUTO_INCREMENT,
  Title int DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 5,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci,
ROW_FORMAT = DYNAMIC;

--
-- Create table `ggroups`
--
CREATE TABLE IF NOT EXISTS ggroups (
  Group_Id int NOT NULL AUTO_INCREMENT,
  Title varchar(50) DEFAULT NULL,
  ID_Course int DEFAULT NULL,
  PRIMARY KEY (Group_Id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 24,
AVG_ROW_LENGTH = 1820,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
ROW_FORMAT = DYNAMIC;

--
-- Create foreign key
--
ALTER TABLE ggroups
ADD CONSTRAINT FK_ggroups_course_id FOREIGN KEY (ID_Course)
REFERENCES course (id);

--
-- Create table `weekday`
--
CREATE TABLE IF NOT EXISTS weekday (
  id int NOT NULL AUTO_INCREMENT,
  Title varchar(255) DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 7,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci,
ROW_FORMAT = DYNAMIC;

--
-- Create table `pairnumber`
--
CREATE TABLE IF NOT EXISTS pairnumber (
  id int NOT NULL AUTO_INCREMENT,
  Title int DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 7,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci,
ROW_FORMAT = DYNAMIC;

--
-- Create table `cabinet`
--
CREATE TABLE IF NOT EXISTS cabinet (
  Cabinet_ID int NOT NULL AUTO_INCREMENT,
  Title varchar(255) DEFAULT NULL,
  Available varchar(255) NOT NULL DEFAULT 'Да',
  Appointment varchar(255) NOT NULL DEFAULT '-',
  PRIMARY KEY (Cabinet_ID)
)
ENGINE = INNODB,
AUTO_INCREMENT = 6,
AVG_ROW_LENGTH = 3276,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
ROW_FORMAT = DYNAMIC;

--
-- Create table `timetable`
--
CREATE TABLE IF NOT EXISTS timetable (
  ID_GROUP int DEFAULT NULL,
  ID_CABINET int DEFAULT NULL,
  ID_LESSONS int DEFAULT NULL,
  ID_Pair_Number int DEFAULT NULL,
  ID_Week_Day int DEFAULT NULL,
  id int NOT NULL AUTO_INCREMENT,
  ID_TEACHER int DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
ROW_FORMAT = DYNAMIC;

--
-- Create foreign key
--
ALTER TABLE timetable
ADD CONSTRAINT FK_timetable_cabinet_Cabinet_ID FOREIGN KEY (ID_CABINET)
REFERENCES cabinet (Cabinet_ID);

--
-- Create foreign key
--
ALTER TABLE timetable
ADD CONSTRAINT FK_timetable_ggroups_Group_Id FOREIGN KEY (ID_GROUP)
REFERENCES ggroups (Group_Id);

--
-- Create foreign key
--
ALTER TABLE timetable
ADD CONSTRAINT FK_timetable_lessons_Lessons_Id FOREIGN KEY (ID_LESSONS)
REFERENCES lessons (Lessons_Id);

--
-- Create foreign key
--
ALTER TABLE timetable
ADD CONSTRAINT FK_timetable_pairnumber_id FOREIGN KEY (ID_Pair_Number)
REFERENCES pairnumber (id);

--
-- Create foreign key
--
ALTER TABLE timetable
ADD CONSTRAINT FK_timetable_teacher_Teacher_Id FOREIGN KEY (ID_TEACHER)
REFERENCES teacher (Teacher_Id);

--
-- Create foreign key
--
ALTER TABLE timetable
ADD CONSTRAINT FK_timetable_weekday_id FOREIGN KEY (ID_Week_Day)
REFERENCES weekday (id);

-- 
-- Dumping data for table lessons
--
INSERT INTO lessons VALUES
(1, 'Физ.культура'),
(2, 'Русский язык'),
(3, 'История'),
(4, 'Информатика'),
(5, 'ОБЖ'),
(6, 'Математика'),
(7, 'Англ.язык'),
(8, 'Физика'),
(9, 'Введение в сп'),
(10, 'Экономика'),
(11, 'Литература'),
(12, 'Родн.литерат'),
(13, 'Основы фин.грам'),
(14, 'Менеджмент'),
(15, 'Информ.технал'),
(16, 'Осн.маркет'),
(17, 'История'),
(18, 'ПМ01 МДК01.01'),
(19, 'Госуд.мун.сл'),
(20, 'Дискрет.математ-ка'),
(21, 'Психология общ.'),
(22, 'АФХД'),
(23, 'Правовое обес'),
(24, 'ПМ02 МДК02.01'),
(25, 'Требов. к зданиям'),
(26, 'Проф.этика'),
(27, 'ПМ02 МДК02.03'),
(28, 'ПМ05 МДК05.02'),
(29, 'Веб.прогр'),
(30, 'Экологич.основы'),
(31, 'Основы философии'),
(32, 'ПМ01 англ.яз'),
(33, 'Китайский язык'),
(34, 'Стилистика'),
(35, 'Экономика орг'),
(36, 'ОАИП');

-- 
-- Dumping data for table course
--
INSERT INTO course VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4);

-- 
-- Dumping data for table weekday
--
INSERT INTO weekday VALUES
(1, 'Понедельник'),
(2, 'Вторник'),
(3, 'Среда'),
(4, 'Четверг'),
(5, 'Пятница'),
(6, 'Суббота');

-- 
-- Dumping data for table teacher
--
INSERT INTO teacher VALUES
(1, 'Глушенко Л.А.', 'да', NULL),
(2, 'Пушкин А.А.', 'да', NULL),
(3, 'Борсова О.И.', 'да', NULL),
(4, 'Довгань С.В.', 'да', NULL),
(5, 'Ахрименко М.В.', 'да', NULL);

-- 
-- Dumping data for table pairnumber
--
INSERT INTO pairnumber VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6);

-- 
-- Dumping data for table ggroups
--
INSERT INTO ggroups VALUES
(1, '321', 1),
(2, '925', 2),
(3, '926', 3),
(4, '1021', 4),
(5, '1125', 1),
(6, '331-332', 2),
(7, '935', 3),
(8, '1031', 4),
(9, '1135', 1),
(23, '1223', 4);

-- 
-- Dumping data for table cabinet
--
INSERT INTO cabinet VALUES
(1, '4', 'Да', '-'),
(2, '1', 'Да', '-'),
(3, '22', 'Да', '-'),
(4, '313', 'Да', '-'),
(5, '32', 'Да', '-');

-- Table lessonsbykiprin.timetable does not contain any data (it is empty)

--
-- Restore previous SQL mode
--
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

--
-- Enable foreign keys
--
/*!40014 SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS */;