USE [EnglishQuizSystem]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/19/2023 6:12:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[answer]    Script Date: 6/19/2023 6:12:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[answer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[question_id] [int] NULL,
	[text] [ntext] NULL,
	[is_correct] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[question]    Script Date: 6/19/2023 6:12:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[question](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[quiz_id] [int] NULL,
	[text] [ntext] NULL,
	[active] [bit] NULL,
	[type] [bit] NULL,
	[difficulty] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[quiz]    Script Date: 6/19/2023 6:12:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[quiz](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NULL,
	[code_active] [varchar](100) NULL,
	[active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 6/19/2023 6:12:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 6/19/2023 6:12:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [varchar](100) NULL,
	[password] [varchar](100) NULL,
	[role_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_answer]    Script Date: 6/19/2023 6:12:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_answer](
	[user_id] [int] NOT NULL,
	[question_id] [int] NOT NULL,
	[answer_id] [int] NOT NULL,
	[quiz_id] [int] NOT NULL,
 CONSTRAINT [user_answer_pk] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC,
	[question_id] ASC,
	[answer_id] ASC,
	[quiz_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_quiz]    Script Date: 6/19/2023 6:12:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_quiz](
	[user_id] [int] NOT NULL,
	[quiz_id] [int] NOT NULL,
	[score] [float] NULL,
 CONSTRAINT [user_topic_pk] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC,
	[quiz_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[answer] ON 
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (1, 1, N'enjoy', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (2, 1, N'years', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (3, 1, N'motorcycles', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (4, 1, N'was', 1)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (5, 2, N'would', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (6, 2, N'come', 1)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (7, 2, N'had told', 1)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (8, 2, N'was', 1)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (9, 3, N'Runned', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (10, 3, N'Ranned', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (11, 3, N'Ran', 1)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (12, 3, N'Ren', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (13, 4, N'It uses short body paragraphs to support the argument.', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (14, 4, N'It uses personal stories to develop the argument.', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (15, 4, N'It uses outside sources to support the argument.', 1)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (16, 4, N'It uses a variety of paragraph types to develop the argument.', 1)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (17, 5, N'in Canada', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (18, 5, N'We', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (19, 5, N'are going', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (20, 5, N'buy', 1)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (21, 6, N'are going to see', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (22, 6, N'told', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (23, 6, N'watched', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (24, 6, N'are eating', 1)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (25, 7, N'Tariq and Jordan joined the soccer game, and helped the team win.', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (26, 7, N'Tariq and Jordan joined the soccer game and helped the team win.', 1)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (27, 7, N'Tariq and Jordan, joined the soccer game and helped the team win.', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (29, 8, N'Jane watched a movie, but Steve, read a book.', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (30, 8, N'Jane watched a movie but Steve, read a book.', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (31, 8, N'Jane watched a movie, but Steve read a book.', 1)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (32, 9, N'The test was difficult but we knew all of the answers.', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (33, 9, N'The test was difficult, but we knew all of the answers.', 1)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (34, 9, N'The test was difficult however we knew all of the answers.', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (35, 10, N'Michael and Sam gave Paul and me some tips on how to throw a basketball, but didn''t help with our form.', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (36, 10, N'Michael and Sam, gave Paul and me some tips on how to throw a basketball but didn''t help with our form.
', 0)
GO
INSERT [dbo].[answer] ([id], [question_id], [text], [is_correct]) VALUES (37, 10, N'Michael and Sam gave Paul and me some tips on how to throw a basketball, but they didn''t help with our form.', 1)
GO
SET IDENTITY_INSERT [dbo].[answer] OFF
GO
SET IDENTITY_INSERT [dbo].[question] ON 
GO
INSERT [dbo].[question] ([id], [quiz_id], [text], [active], [type], [difficulty]) VALUES (1, 1, N'Which underlined word has a mistake?
We enjoy riding motorcycles. Modern motorcycles are lighter and faster than they was 30 years ago.', 1, 1, 1)
GO
INSERT [dbo].[question] ([id], [quiz_id], [text], [active], [type], [difficulty]) VALUES (2, 1, N'Which underlined word has a mistake?
My mother was mad at me because I had told her I would be home at 6:00, but I come home at 9:00.', 1, 0, 1)
GO
INSERT [dbo].[question] ([id], [quiz_id], [text], [active], [type], [difficulty]) VALUES (3, 1, N'What is the past tense of the verb "run"?', 1, 1, 1)
GO
INSERT [dbo].[question] ([id], [quiz_id], [text], [active], [type], [difficulty]) VALUES (4, 1, N'Which of the following are features of an advanced argument essay? Check all that apply.', 1, 0, 1)
GO
INSERT [dbo].[question] ([id], [quiz_id], [text], [active], [type], [difficulty]) VALUES (5, 1, N'Which underlined word has a mistake?
Next year, my family buy a house in Canada. We are going to visit it every summer.', 1, 1, 1)
GO
INSERT [dbo].[question] ([id], [quiz_id], [text], [active], [type], [difficulty]) VALUES (6, 1, N'Which underlined word has a mistake?
We watched a good movie last night and told our friends about it. Now, they want to see it too. All of us are going to see it again tonight after we are eating dinner.', 1, 1, 1)
GO
INSERT [dbo].[question] ([id], [quiz_id], [text], [active], [type], [difficulty]) VALUES (7, 1, N'Which sentence is punctuated correctly?', 1, 1, 1)
GO
INSERT [dbo].[question] ([id], [quiz_id], [text], [active], [type], [difficulty]) VALUES (8, 1, N'Which sentence uses commas correctly?', 1, 1, 1)
GO
INSERT [dbo].[question] ([id], [quiz_id], [text], [active], [type], [difficulty]) VALUES (9, 1, N'Which sentence is punctuated correctly?', 1, 1, 1)
GO
INSERT [dbo].[question] ([id], [quiz_id], [text], [active], [type], [difficulty]) VALUES (10, 1, N'Which sentence is punctuated correctly?', 1, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[question] OFF
GO
SET IDENTITY_INSERT [dbo].[quiz] ON 
GO
INSERT [dbo].[quiz] ([id], [name], [code_active], [active]) VALUES (1, N'ENW492c QuizBet', N'123456', 1)
GO
INSERT [dbo].[quiz] ([id], [name], [code_active], [active]) VALUES (2, N'QuizBet 2', N'123456', 1)
GO
SET IDENTITY_INSERT [dbo].[quiz] OFF
GO
SET IDENTITY_INSERT [dbo].[role] ON 
GO
INSERT [dbo].[role] ([id], [name]) VALUES (1, N'USER')
GO
INSERT [dbo].[role] ([id], [name]) VALUES (2, N'ADMIN')
GO
INSERT [dbo].[role] ([id], [name]) VALUES (3, N'LECTURE')
GO
SET IDENTITY_INSERT [dbo].[role] OFF
GO
SET IDENTITY_INSERT [dbo].[user] ON 
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (1, N'admin', N'123', 2)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (2, N'thangnd', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (7, N'thang0508', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (8, N'thang05081', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (9, N'Mollie1975', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (18, N'Quintin5', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (27, N'Kathern49', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (36, N'Felisa221', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (45, N'Shenita2027', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (54, N'Arnita2004', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (63, N'Woodrow2009', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (72, N'Chantel318', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (81, N'Aisha2010', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (90, N'Christopher1976', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (99, N'Stephan1976', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (108, N'Brinson972', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (117, N'Carpenter1994', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (126, N'Doyle2', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (135, N'Johnston1952', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (144, N'Booker6', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (153, N'Carvalho72', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (162, N'Aguilera1993', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (171, N'Paris2022', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (180, N'Jerrod1982', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (189, N'Suarez82', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (198, N'Abreu232', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (207, N'Shank212', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (216, N'Elise28', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (225, N'Alcala1977', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (234, N'Abernathy6', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (243, N'Lindsey295', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (252, N'Hubert2025', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (261, N'Paris24', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (270, N'Muncy1', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (279, N'Jestine2015', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (288, N'Alexander3', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (297, N'Seymour1989', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (306, N'Lorretta2028', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (315, N'Andre58', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (324, N'Carlson9', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (333, N'Jefferies7', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (342, N'Verona3', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (351, N'Lanette688', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (360, N'Crystle2000', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (369, N'Weston1954', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (378, N'Abdul134', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (387, N'Brigida1989', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (396, N'Christian1985', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (405, N'Alberto1984', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (414, N'Ernesto599', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (423, N'Rivka533', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (432, N'Opal1955', N'123', 1)
GO
INSERT [dbo].[user] ([id], [user_name], [password], [role_id]) VALUES (441, N'Cornell6', N'123', 1)
GO
SET IDENTITY_INSERT [dbo].[user] OFF
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (1, 1, 4, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (1, 2, 6, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (1, 2, 7, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (1, 2, 8, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (1, 3, 11, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (1, 4, 15, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (1, 4, 16, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (1, 5, 20, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (1, 6, 24, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (1, 7, 26, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (1, 8, 30, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (1, 9, 33, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (1, 10, 35, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (2, 1, 4, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (2, 2, 6, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (2, 2, 7, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (2, 2, 8, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (2, 3, 11, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (2, 4, 15, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (2, 4, 16, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (2, 5, 20, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (2, 6, 24, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (2, 7, 26, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (2, 8, 31, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (2, 9, 34, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (2, 10, 37, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (7, 1, 4, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (7, 2, 6, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (7, 2, 7, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (7, 2, 8, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (7, 3, 11, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (7, 4, 15, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (7, 4, 16, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (7, 5, 20, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (7, 6, 24, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (7, 7, 26, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (7, 8, 30, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (7, 9, 34, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (7, 10, 37, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (8, 1, 1, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (8, 2, 5, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (8, 3, 9, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (8, 4, 14, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (8, 5, 17, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (8, 6, 21, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (8, 7, 25, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (8, 8, 29, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (8, 9, 32, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (8, 10, 35, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (9, 1, 4, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (9, 2, 6, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (9, 2, 7, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (9, 2, 8, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (9, 3, 9, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (9, 4, 15, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (9, 4, 16, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (9, 5, 20, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (9, 6, 23, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (9, 7, 27, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (9, 8, 31, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (9, 9, 34, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (9, 10, 37, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (18, 1, 4, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (18, 2, 5, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (18, 3, 9, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (18, 4, 13, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (18, 5, 17, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (18, 6, 21, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (18, 7, 25, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (18, 8, 29, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (18, 9, 32, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (18, 10, 35, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (27, 1, 4, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (27, 2, 6, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (27, 2, 7, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (27, 2, 8, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (27, 3, 12, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (27, 4, 16, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (27, 5, 17, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (27, 6, 21, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (27, 7, 25, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (27, 8, 29, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (27, 9, 32, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (27, 10, 35, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (36, 1, 4, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (36, 2, 6, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (36, 2, 7, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (36, 2, 8, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (36, 3, 11, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (36, 4, 15, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (36, 4, 16, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (36, 5, 17, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (36, 6, 21, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (36, 7, 25, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (36, 8, 29, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (36, 9, 32, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (36, 10, 35, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (45, 1, 4, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (45, 2, 6, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (45, 2, 7, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (45, 2, 8, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (45, 3, 11, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (45, 4, 14, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (45, 5, 18, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (45, 6, 21, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (45, 7, 25, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (45, 8, 29, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (45, 9, 32, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (45, 10, 35, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (54, 1, 4, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (54, 2, 6, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (54, 2, 7, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (54, 2, 8, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (54, 3, 11, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (54, 4, 15, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (54, 4, 16, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (54, 5, 20, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (54, 6, 21, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (54, 7, 25, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (54, 8, 29, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (54, 9, 32, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (54, 10, 35, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (63, 1, 4, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (63, 2, 6, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (63, 2, 7, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (63, 2, 8, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (63, 3, 11, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (63, 4, 15, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (63, 4, 16, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (63, 5, 20, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (63, 6, 24, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (63, 7, 25, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (63, 8, 29, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (63, 9, 33, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (63, 10, 35, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (72, 1, 4, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (72, 2, 6, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (72, 2, 7, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (72, 2, 8, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (72, 3, 11, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (72, 4, 15, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (72, 4, 16, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (72, 5, 20, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (72, 6, 24, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (72, 7, 26, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (72, 8, 31, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (72, 9, 33, 1)
GO
INSERT [dbo].[user_answer] ([user_id], [question_id], [answer_id], [quiz_id]) VALUES (72, 10, 37, 1)
GO
INSERT [dbo].[user_quiz] ([user_id], [quiz_id], [score]) VALUES (1, 1, 8)
GO
INSERT [dbo].[user_quiz] ([user_id], [quiz_id], [score]) VALUES (2, 1, 9)
GO
INSERT [dbo].[user_quiz] ([user_id], [quiz_id], [score]) VALUES (7, 1, 8)
GO
INSERT [dbo].[user_quiz] ([user_id], [quiz_id], [score]) VALUES (8, 1, 0)
GO
INSERT [dbo].[user_quiz] ([user_id], [quiz_id], [score]) VALUES (9, 1, 6)
GO
INSERT [dbo].[user_quiz] ([user_id], [quiz_id], [score]) VALUES (18, 1, 1)
GO
INSERT [dbo].[user_quiz] ([user_id], [quiz_id], [score]) VALUES (27, 1, 2)
GO
INSERT [dbo].[user_quiz] ([user_id], [quiz_id], [score]) VALUES (36, 1, 4)
GO
INSERT [dbo].[user_quiz] ([user_id], [quiz_id], [score]) VALUES (45, 1, 3)
GO
INSERT [dbo].[user_quiz] ([user_id], [quiz_id], [score]) VALUES (54, 1, 5)
GO
INSERT [dbo].[user_quiz] ([user_id], [quiz_id], [score]) VALUES (63, 1, 7)
GO
INSERT [dbo].[user_quiz] ([user_id], [quiz_id], [score]) VALUES (72, 1, 10)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__user__7C9273C469183D65]    Script Date: 6/19/2023 6:12:45 PM ******/
ALTER TABLE [dbo].[user] ADD UNIQUE NONCLUSTERED 
(
	[user_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[answer]  WITH CHECK ADD FOREIGN KEY([question_id])
REFERENCES [dbo].[question] ([id])
GO
ALTER TABLE [dbo].[question]  WITH CHECK ADD FOREIGN KEY([quiz_id])
REFERENCES [dbo].[quiz] ([id])
GO
ALTER TABLE [dbo].[user]  WITH NOCHECK ADD FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([id])
GO
ALTER TABLE [dbo].[user_answer]  WITH CHECK ADD FOREIGN KEY([answer_id])
REFERENCES [dbo].[answer] ([id])
GO
ALTER TABLE [dbo].[user_answer]  WITH CHECK ADD FOREIGN KEY([question_id])
REFERENCES [dbo].[question] ([id])
GO
ALTER TABLE [dbo].[user_answer]  WITH CHECK ADD FOREIGN KEY([quiz_id])
REFERENCES [dbo].[quiz] ([id])
GO
ALTER TABLE [dbo].[user_answer]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[user_quiz]  WITH CHECK ADD FOREIGN KEY([quiz_id])
REFERENCES [dbo].[quiz] ([id])
GO
ALTER TABLE [dbo].[user_quiz]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
