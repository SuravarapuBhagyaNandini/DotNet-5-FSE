-- MySQL Exercises: schema, sample data and solutions (Exercises 1-25)
-- Creates database, tables, inserts sample data, then queries for each exercise.

CREATE DATABASE IF NOT EXISTS community_portal;
USE community_portal;

-- DROP existing tables if present (safe for rerun)
SET FOREIGN_KEY_CHECKS=0;
DROP TABLE IF EXISTS Resources;
DROP TABLE IF EXISTS Feedback;
DROP TABLE IF EXISTS Registrations;
DROP TABLE IF EXISTS Sessions;
DROP TABLE IF EXISTS Events;
DROP TABLE IF EXISTS Users;
SET FOREIGN_KEY_CHECKS=1;

-- 1. Users
CREATE TABLE Users (
  user_id INT PRIMARY KEY AUTO_INCREMENT,
  full_name VARCHAR(100) NOT NULL,
  email VARCHAR(100) NOT NULL UNIQUE,
  city VARCHAR(100) NOT NULL,
  registration_date DATE NOT NULL
) ENGINE=InnoDB;

-- 2. Events
CREATE TABLE Events (
  event_id INT PRIMARY KEY AUTO_INCREMENT,
  title VARCHAR(200) NOT NULL,
  description TEXT,
  city VARCHAR(100) NOT NULL,
  start_date DATETIME NOT NULL,
  end_date DATETIME NOT NULL,
  status ENUM('upcoming','completed','cancelled') DEFAULT 'upcoming',
  organizer_id INT,
  FOREIGN KEY (organizer_id) REFERENCES Users(user_id)
) ENGINE=InnoDB;

-- 3. Sessions
CREATE TABLE Sessions (
  session_id INT PRIMARY KEY AUTO_INCREMENT,
  event_id INT NOT NULL,
  title VARCHAR(200) NOT NULL,
  speaker_name VARCHAR(100) NOT NULL,
  start_time DATETIME NOT NULL,
  end_time DATETIME NOT NULL,
  FOREIGN KEY (event_id) REFERENCES Events(event_id)
) ENGINE=InnoDB;

-- 4. Registrations
CREATE TABLE Registrations (
  registration_id INT PRIMARY KEY AUTO_INCREMENT,
  user_id INT NOT NULL,
  event_id INT NOT NULL,
  registration_date DATE NOT NULL,
  FOREIGN KEY (user_id) REFERENCES Users(user_id),
  FOREIGN KEY (event_id) REFERENCES Events(event_id)
) ENGINE=InnoDB;

-- 5. Feedback
CREATE TABLE Feedback (
  feedback_id INT PRIMARY KEY AUTO_INCREMENT,
  user_id INT NOT NULL,
  event_id INT NOT NULL,
  rating INT CHECK (rating BETWEEN 1 AND 5),
  comments TEXT,
  feedback_date DATE NOT NULL,
  FOREIGN KEY (user_id) REFERENCES Users(user_id),
  FOREIGN KEY (event_id) REFERENCES Events(event_id)
) ENGINE=InnoDB;

-- 6. Resources
CREATE TABLE Resources (
  resource_id INT PRIMARY KEY AUTO_INCREMENT,
  event_id INT NOT NULL,
  resource_type ENUM('pdf','image','link') NOT NULL,
  resource_url VARCHAR(255) NOT NULL,
  uploaded_at DATETIME NOT NULL,
  FOREIGN KEY (event_id) REFERENCES Events(event_id)
) ENGINE=InnoDB;

-- Insert sample Users
INSERT INTO Users (user_id, full_name, email, city, registration_date) VALUES
(1, 'Alice Johnson', 'alice@example.com', 'New York', '2024-12-01'),
(2, 'Bob Smith', 'bob@example.com', 'Los Angeles', '2024-12-05'),
(3, 'Charlie Lee', 'charlie@example.com', 'Chicago', '2024-12-10'),
(4, 'Diana King', 'diana@example.com', 'New York', '2025-01-15'),
(5, 'Ethan Hunt', 'ethan@example.com', 'Los Angeles', '2025-02-01');

-- Insert sample Events
INSERT INTO Events (event_id, title, description, city, start_date, end_date, status, organizer_id) VALUES
(1, 'Tech Innovators Meetup', 'A meetup for tech enthusiasts.', 'New York', '2025-06-10 10:00:00', '2025-06-10 16:00:00', 'upcoming', 1),
(2, 'AI & ML Conference', 'Conference on AI and ML advancements.', 'Chicago', '2025-05-15 09:00:00', '2025-05-15 17:00:00', 'completed', 3),
(3, 'Frontend Development Bootcamp', 'Hands-on training on frontend tech.', 'Los Angeles', '2025-07-01 10:00:00', '2025-07-03 16:00:00', 'upcoming', 2);

-- Insert sample Sessions
INSERT INTO Sessions (session_id, event_id, title, speaker_name, start_time, end_time) VALUES
(1, 1, 'Opening Keynote', 'Dr. Tech', '2025-06-10 10:00:00', '2025-06-10 11:00:00'),
(2, 1, 'Future of Web Dev', 'Alice Johnson', '2025-06-10 11:15:00', '2025-06-10 12:30:00'),
(3, 2, 'AI in Healthcare', 'Charlie Lee', '2025-05-15 09:30:00', '2025-05-15 11:00:00'),
(4, 3, 'Intro to HTML5', 'Bob Smith', '2025-07-01 10:00:00', '2025-07-01 12:00:00');

-- Insert sample Registrations
INSERT INTO Registrations (registration_id, user_id, event_id, registration_date) VALUES
(1,1,1,'2025-05-01'),
(2,2,1,'2025-05-02'),
(3,3,2,'2025-04-30'),
(4,4,2,'2025-04-28'),
(5,5,3,'2025-06-15');

-- Insert sample Feedback
INSERT INTO Feedback (feedback_id, user_id, event_id, rating, comments, feedback_date) VALUES
(1,3,2,4,'Great insights!', '2025-05-16'),
(2,4,2,5,'Very informative.', '2025-05-16'),
(3,2,1,3,'Could be better.', '2025-06-11');

-- Insert sample Resources
INSERT INTO Resources (resource_id, event_id, resource_type, resource_url, uploaded_at) VALUES
(1,2,'pdf','https://portal.com/resources/tech_meetup_agenda.pdf','2025-05-01 10:00:00'),
(2,1,'image','https://portal.com/resources/ai_poster.jpg','2025-04-20 09:00:00'),
(3,3,'link','https://portal.com/resources/html5_docs','2025-06-25 15:00:00');

-- =========================
-- Exercise Queries (1-25)
-- =========================

-- 1. User Upcoming Events
-- Show upcoming events a user is registered for in their city, sorted by date.
-- Replace :user_id with desired user id.
-- Example for user_id = 1
SELECT e.event_id, e.title, e.city, e.start_date, e.end_date
FROM Events e
JOIN Registrations r ON r.event_id = e.event_id
JOIN Users u ON u.user_id = r.user_id
WHERE u.user_id = 1
  AND e.city = u.city
  AND e.start_date > NOW()
ORDER BY e.start_date;

-- 2. Top Rated Events
-- Events with highest average rating considering only those with at least 10 feedbacks
SELECT e.event_id, e.title,
       AVG(f.rating) AS avg_rating,
       COUNT(f.feedback_id) AS feedback_count
FROM Events e
JOIN Feedback f ON f.event_id = e.event_id
GROUP BY e.event_id, e.title
HAVING COUNT(f.feedback_id) >= 10
ORDER BY avg_rating DESC;

-- 3. Inactive Users
-- Users who have not registered for any events in the last 90 days
SELECT u.user_id, u.full_name, u.email, u.registration_date
FROM Users u
WHERE NOT EXISTS (
  SELECT 1 FROM Registrations r
  WHERE r.user_id = u.user_id
    AND r.registration_date >= DATE_SUB(CURDATE(), INTERVAL 90 DAY)
);

-- 4. Peak Session Hours
-- Count sessions scheduled between 10:00 and 12:00 for each event
SELECT e.event_id, e.title, COUNT(s.session_id) AS sessions_10_12
FROM Events e
LEFT JOIN Sessions s ON s.event_id = e.event_id
  AND TIME(s.start_time) >= '10:00:00' AND TIME(s.start_time) < '12:00:00'
GROUP BY e.event_id, e.title;

-- 5. Most Active Cities
-- Top 5 cities with highest number of distinct user registrations
SELECT u.city, COUNT(DISTINCT r.user_id) AS distinct_registrations
FROM Users u
JOIN Registrations r ON r.user_id = u.user_id
GROUP BY u.city
ORDER BY distinct_registrations DESC
LIMIT 5;

-- 6. Event Resource Summary
-- Number of PDFs, images, and links uploaded per event
SELECT e.event_id, e.title,
       SUM(resource_type = 'pdf') AS pdf_count,
       SUM(resource_type = 'image') AS image_count,
       SUM(resource_type = 'link') AS link_count,
       COUNT(r.resource_id) AS total_resources
FROM Events e
LEFT JOIN Resources r ON r.event_id = e.event_id
GROUP BY e.event_id, e.title;

-- 7. Low Feedback Alerts
-- Users who gave rating < 3 with comments and event name
SELECT f.feedback_id, u.user_id, u.full_name, e.event_id, e.title, f.rating, f.comments, f.feedback_date
FROM Feedback f
JOIN Users u ON u.user_id = f.user_id
JOIN Events e ON e.event_id = f.event_id
WHERE f.rating < 3;

-- 8. Sessions per Upcoming Event
SELECT e.event_id, e.title, COUNT(s.session_id) AS session_count
FROM Events e
LEFT JOIN Sessions s ON s.event_id = e.event_id
WHERE e.start_date > NOW()
GROUP BY e.event_id, e.title;

-- 9. Organizer Event Summary
-- For each organizer show number of events and status counts
SELECT u.user_id AS organizer_id, u.full_name,
       COUNT(e.event_id) AS total_events,
       SUM(e.status = 'upcoming') AS upcoming_count,
       SUM(e.status = 'completed') AS completed_count,
       SUM(e.status = 'cancelled') AS cancelled_count
FROM Users u
LEFT JOIN Events e ON e.organizer_id = u.user_id
GROUP BY u.user_id, u.full_name;

-- 10. Feedback Gap
-- Events that had registrations but received no feedback
SELECT DISTINCT e.event_id, e.title
FROM Events e
WHERE EXISTS (SELECT 1 FROM Registrations r WHERE r.event_id = e.event_id)
  AND NOT EXISTS (SELECT 1 FROM Feedback f WHERE f.event_id = e.event_id);

-- 11. Daily New User Count (last 7 days)
SELECT registration_date, COUNT(*) AS new_users
FROM Users
WHERE registration_date >= DATE_SUB(CURDATE(), INTERVAL 6 DAY)
GROUP BY registration_date
ORDER BY registration_date DESC;

-- 12. Event with Maximum Sessions
SELECT e.event_id, e.title, COUNT(s.session_id) AS session_count
FROM Events e
LEFT JOIN Sessions s ON s.event_id = e.event_id
GROUP BY e.event_id, e.title
HAVING session_count = (
  SELECT MAX(t.cnt) FROM (
    SELECT COUNT(s2.session_id) AS cnt FROM Events e2 LEFT JOIN Sessions s2 ON s2.event_id = e2.event_id GROUP BY e2.event_id
  ) t
);

-- 13. Average Rating per City
SELECT e.city, AVG(f.rating) AS avg_rating
FROM Events e
JOIN Feedback f ON f.event_id = e.event_id
GROUP BY e.city;

-- 14. Most Registered Events (top 3)
SELECT e.event_id, e.title, COUNT(r.registration_id) AS registrations
FROM Events e
LEFT JOIN Registrations r ON r.event_id = e.event_id
GROUP BY e.event_id, e.title
ORDER BY registrations DESC
LIMIT 3;

-- 15. Event Session Time Conflict
-- Overlapping sessions within the same event
SELECT s1.event_id, s1.session_id AS session1, s2.session_id AS session2, s1.title AS title1, s2.title AS title2,
       s1.start_time AS s1_start, s1.end_time AS s1_end, s2.start_time AS s2_start, s2.end_time AS s2_end
FROM Sessions s1
JOIN Sessions s2 ON s1.event_id = s2.event_id AND s1.session_id < s2.session_id
WHERE s1.start_time < s2.end_time AND s2.start_time < s1.end_time;

-- 16. Unregistered Active Users
-- Users who created account in last 30 days but haven’t registered for any events
SELECT u.user_id, u.full_name, u.registration_date
FROM Users u
WHERE u.registration_date >= DATE_SUB(CURDATE(), INTERVAL 30 DAY)
  AND NOT EXISTS (SELECT 1 FROM Registrations r WHERE r.user_id = u.user_id);

-- 17. Multi-Session Speakers
SELECT speaker_name, COUNT(*) AS session_count
FROM Sessions
GROUP BY speaker_name
HAVING session_count > 1;

-- 18. Resource Availability Check
SELECT e.event_id, e.title
FROM Events e
LEFT JOIN Resources r ON r.event_id = e.event_id
WHERE r.resource_id IS NULL;

-- 19. Completed Events with Feedback Summary
SELECT e.event_id, e.title,
       COUNT(DISTINCT r.registration_id) AS total_registrations,
       AVG(f.rating) AS avg_rating
FROM Events e
LEFT JOIN Registrations r ON r.event_id = e.event_id
LEFT JOIN Feedback f ON f.event_id = e.event_id
WHERE e.status = 'completed'
GROUP BY e.event_id, e.title;

-- 20. User Engagement Index
-- For each user, count events attended (registrations) and feedback submitted
SELECT u.user_id, u.full_name,
       COUNT(DISTINCT r.event_id) AS events_attended,
       COUNT(f.feedback_id) AS feedback_submitted
FROM Users u
LEFT JOIN Registrations r ON r.user_id = u.user_id
LEFT JOIN Feedback f ON f.user_id = u.user_id
GROUP BY u.user_id, u.full_name;

-- 21. Top Feedback Providers (top 5)
SELECT u.user_id, u.full_name, COUNT(f.feedback_id) AS feedback_count
FROM Users u
JOIN Feedback f ON f.user_id = u.user_id
GROUP BY u.user_id, u.full_name
ORDER BY feedback_count DESC
LIMIT 5;

-- 22. Duplicate Registrations Check
SELECT user_id, event_id, COUNT(*) AS dup_count
FROM Registrations
GROUP BY user_id, event_id
HAVING dup_count > 1;

-- 23. Registration Trends (month-wise past 12 months)
SELECT DATE_FORMAT(registration_date, '%Y-%m') AS month, COUNT(*) AS registrations
FROM Registrations
WHERE registration_date >= DATE_SUB(CURDATE(), INTERVAL 12 MONTH)
GROUP BY month
ORDER BY month;

-- 24. Average Session Duration per Event (minutes)
SELECT e.event_id, e.title, AVG(TIMESTAMPDIFF(MINUTE, s.start_time, s.end_time)) AS avg_session_minutes
FROM Events e
LEFT JOIN Sessions s ON s.event_id = e.event_id
GROUP BY e.event_id, e.title;

-- 25. Events Without Sessions
SELECT e.event_id, e.title
FROM Events e
LEFT JOIN Sessions s ON s.event_id = e.event_id
WHERE s.session_id IS NULL;

-- End of file
