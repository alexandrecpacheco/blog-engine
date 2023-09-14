# Blog Engine
Backend API that allows to create, edit and publish textbased posts, with an approval flow where two different user types may interact

## Software Prerequisites
``This project uses .NET Core 6 and SQL Server``

## Credentials
``` Open the Swagger and try the Authorization API using the credentials:
{
  "email": "public@email.com",
  "password": "public123@"
}
OR
{
  "email": "writer@email.com",
  "password": "writer123@"
}
OR
{
  "email": "editor@email.com",
  "password": "editor123@"
}
``` 

## Business Rules 
The Project has three types of roles (Public, Writer and Editor)

• All roles can retrieve a list of all published posts

• All roles can add comments to a published post

• Only the Writers can get, create and edit their own posts

• The Writer are able to create new posts and retrieve the posts they have written.

• The Writer are able to edit existing posts that haven't been published or submitted.

• The Writer can Submit their posts

• When a Writer submits a post, the post is moved to a “pending approval” ('P') status where it’s locked and cannot be updated.

• Editor can Get, Approve or Reject pending posts

• Editors are able to query for “pending” posts and approve or reject their publishing. Once an Editor approves a post, it will be published and visible to all roles. 
If the post is rejected, it will be unlocked and editable again by Writers.

• Editor are able to include a comment when rejecting a post, and this comment is visible to the Writer only.

## Runnig the project
- Clone the repository copying the URL on GitHub
```
    • You must to be change the Connection String on appSettings.Development.json
    • The Database will be created automatically
    • The profile of authors will be created automatically (Public, Writer, Editor)
    • The APIs must need the JWT Token to run the API. Is recommended to check the role you want to run on Swagger.
```
``
Total Time:30 hours ``
