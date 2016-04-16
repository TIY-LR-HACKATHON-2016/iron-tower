# Hackathon!

This is the starter repository for the hackathon. This contains everything you need to get started.

## Getting Started

1. Elect one of your frontend students to start your repo. They'll need to create one via SourceTree making sure **to select TIY-LR-HACKATHON-2016 as the owner** when creating the remote repo. They'll then need to go into this directory and clone the start files by running `startfrom https://github.com/TIY-LR-HACKATHON-2016/starter-files.git`.

2. Elect on of your backend students to setup your deployment in Azure. Something something Daniel.

## General Notes!

**Make sure to branch**. You'll want to use a unique name for your branch that no one else in your group is using.

Your code in `master` is going to deploy automatically. The other members of your team are going to be dependent on the deployed code actually working. Work in a branch so that when you check your code in, it doesn't disrupt anyone else's workflow until you're ready to deploy your code.

When you're ready to deploy, merge your code back into master, then **go back to working in a branch**.

## Frontend!

Your files are located in the `client` directory. Inside of there, you'll be able to run gulp and do all of the normal things we do on Angular projects. Since you'll be interacting with backend students, I haven't included the files necessary to use Firebase. You'll be using trusty `$http` to interact with the backend that they create for you.

Instead of the usual `images` folder, there's an `assets` folder. I have no idea what you're going to want to use this weekend, so I didn't want to limit you or feel like you were making poor decisions by putting audio, video or anything else into the images folder. I highly recommend making a folder for each type of asset inside of the `assets` folder--so you can keep your images separate from your other stuff.

## Backend!

Your files will go in the `server` directory.

You're going to do some awesome stuff like setup deployment via Azure. Something something Daniel.
