# Fitness Logger

A web app that helps you track sets and reps/time increments of exercises you have completed. The app also pulls exercise information and demonstations from an external database, allowing for easy searching AND ensuring you have perfect exercise form.
## Tech Stack

**Runtime & Framework:** .NET 9, ASP.NET Core

**Database ORM:** Entity Framework

**Database:** SQLite

## Lessons Learned

- There were two big challenges I ran into due to server-side verses client-side execution. I wanted to challenge myself, and allow the user to designate a whole number of reps for an exercise OR an amount of time (allowing decimal numbers), but only ensuring data is collected for the respective field and is displayed correctly in the table. I also wanted the user to be able to search for exercises by name at creation or when editing a record. Originally I tried to take care of this in the controller, but was having issues getting it functioning correctly. Javascript on the page to handle that on the client side wound up being my answer. The script does rely on the controller to fetch search results, but Javascript handles enabling and disabling the time/rep fields as well as populating search results.
- I really wanted the flow of the application to feel good, so I felt it was important to have navigating back and forth not completely wipe the user's search. I personally would use this by searching then clicking back and forth on exercises until I found whichever one I was looking for, so the app passes the exercise Ids and search queries between pages to create a seamless experience for the user. Especially since this relied on an external database and it's schema to function, I felt like that was a really good lesson (and an overall good call) in application UX design that a lot of modern apps implement already, though I'm sure there are much better ways of achieving it.
## Acknowledgements

 - [The C# Academy](https://www.thecsharpacademy.com/)
 - [README Editor](https://readme.so/editor)
 - [ExerciseDB.dev](https://www.exercisedb.dev)
