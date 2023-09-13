CREATE TABLE [author]
(
    author_id           int IDENTITY (1,1),
    author_key          uniqueidentifier DEFAULT NEWID()   NOT NULL,
    email               varchar(255)                       NOT NULL,
    [password]          varchar(255)                       NOT NULL,
    [name]              varchar(100)                       NOT NULL,
    is_active           bit              DEFAULT 0         NOT NULL,
    created_at          datetime         DEFAULT GETDATE() NOT NULL,
    updated_at          datetime         DEFAULT null      NULL,
    CONSTRAINT ["pk_author"] PRIMARY KEY NONCLUSTERED (author_id)
)
GO

CREATE TABLE author_profile
(
    author_profile_id int IDENTITY (1,1),
    author_id         int                        NOT NULL,
    profile_id      int                        NOT NULL,
    created_at      datetime DEFAULT GETDATE() NOT NULL,
    updated_at      datetime DEFAULT null      NULL,
    CONSTRAINT ["pk_author_profile"] PRIMARY KEY NONCLUSTERED (author_profile_id)
)
GO

CREATE TABLE [profile]
(
    profile_id  int IDENTITY (1,1),
    [description] varchar(100)               NOT NULL,
    created_at  datetime DEFAULT GETDATE() NOT NULL,
    updated_at  datetime                   NULL,
    CONSTRAINT ["pk_profile"] PRIMARY KEY NONCLUSTERED (profile_id)
)
GO

CREATE TABLE posts
(
    post_id  int IDENTITY (1,1),
    author_profile_id int NOT NULL,
    title varchar(100)               NOT NULL,
    [description] varchar(250) NOT NULL,
    [readonly_by_author] bit NOT NULL, 
    publish_date datetime NULL,
    created_at  datetime DEFAULT GETDATE() NOT NULL,
    updated_at  datetime                   NULL,
    CONSTRAINT ["pk_posts"] PRIMARY KEY NONCLUSTERED (post_id)
)
GO

CREATE TABLE comments
(
    comment_id int IDENTITY (1,1),
    post_id int NOT NULL,
    comment varchar(250) NOT NULL,
    readble_by_author_profile_id int NULL,
    created_at  datetime DEFAULT GETDATE() NOT NULL,
    updated_at  datetime                   NULL,
    CONSTRAINT ["pk_comments"] PRIMARY KEY NONCLUSTERED (comment_id)
)
GO

CREATE TABLE submits
(
    submit_id int IDENTITY (1,1),
    post_id int NOT NULL,
    publish_type char(1) NOT NULL,
    comment varchar(250) NOT NULL,
    created_at  datetime DEFAULT GETDATE() NOT NULL,
    updated_at  datetime                   NULL,
    CONSTRAINT ["pk_submits"] PRIMARY KEY NONCLUSTERED (submit_id)
)
GO

ALTER TABLE author_profile
    ADD CONSTRAINT ["fk_author_profile_profile"]
        FOREIGN KEY (profile_id) REFERENCES [profile] (profile_id)
GO

ALTER TABLE author_profile
    ADD CONSTRAINT ["fk_author_profile_author"]
        FOREIGN KEY (author_id) REFERENCES [author] (author_id)
GO

ALTER TABLE comments
    ADD CONSTRAINT ["fk_comments_posts"]
        FOREIGN KEY (post_id) REFERENCES [posts] (post_id)
GO

ALTER TABLE submits
    ADD CONSTRAINT ["fk_submits_posts"]
        FOREIGN KEY (post_id) REFERENCES [posts] (post_id)
GO


INSERT INTO [profile] ([description], created_at) VALUES ('Public', GETDATE()), ('Writer', GETDATE()), ('Editor', GETDATE())
GO

INSERT INTO [author] (email, [password], name, is_active, created_at)
VALUES('public@email.com', 'public123@', 'Public', 1, GETDATE())
, ('writer@email.com', 'writer123@', 'Writer', 2, GETDATE())
, ('editor@email.com', 'editor123@', 'Editor', 3, GETDATE())
GO

INSERT INTO author_profile (author_id, profile_id, created_at) VALUES (1, 1, GETDATE()), (2, 2, GETDATE()), (3, 3, GETDATE())
GO

