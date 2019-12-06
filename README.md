# BUFFiMG
An image sharing website for the CSCI 3308 (Software Development) Project

Members: Jake Swartwout, Sam Feig, Connor Radeloff, Zack McKevitt, Peter Lu, Justin Jiang

BUFFiMG is an image sharing website. It allows you to upload images to then share with your
friends and the general CU community. Each image has a personal link to send to others,
but the website also allows to search the entire website by tags, so that users can search images
by topic. All of this allows the users to share images without sending the entire file, instead
uploading it over wifi once and then sending just a link to multiple other people. Our
site is also exclusive to CU students (found with a @colorado.edu email), which will build a
sense of community among users and the school.

The website uses ASP.NET Core MVC as a front end and integration layer. It is hosted on Heroku
[here](https://buffimg.herokuapp.com). In order to upload to heroku, we needed to use a buildpack,
so I chose [this one](https://github.com/somashekarg/netcorebuildpack-postgres.git), which seemed compatible with all of our versions. To get the project onto heroku then, we used Visual Studio to publish a final version of the project (just to a folder), which is then uploaded to the heroku
repository. The buildpack automatically detects the app settings files and uses these to build the
required deployment files.

The backend is a MySQL database hosted on AWS

To build the code for testing in localhost, we used Visual Studio's builtin asp.net hosting.
So, once the repository is downloaded and the solution opened, just click the run button to run
it using IIS hosting, and the website will run in local host then open a browser with the home page.

On github, we ensured that we worked on sub-branches before merging back into the master when the project was finally completed. Most of our styling work occurred on the front-end branch, our initial development was on our feature-presentation branch, then we moved over to "working", before uploading the working project with identity handling to "identity-integration".
