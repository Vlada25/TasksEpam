INSERT [dbo].[Books] ([Name], [Author], [GenreId], [Condition])
VALUES (N'Miss Peregrines Home for Peculiar Children', N'Ransom Riggs', 1, 5),
	   (N'Alices Adventures in Wonderland', N'Lewis Carroll', 1, 5)

INSERT [dbo].[Users] ([Name], [Surname], [Patronymic], [Sex], [Birthday])
VALUES (N'Fedorovich', N'Anastasiya', N'Aleksandrovna', 0, CAST(N'2000-11-17' AS Date))