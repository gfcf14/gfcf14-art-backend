
# gfcf14's Art BackEnd
This repository is the backend part of my gfcf14's Art project. You can find the frontend part [here](https://github.com/gfcf14/gfcf14-art).

## Description
The gfcf14's Art backend project is a web service which receives communications from a frontend application [here](https://gfcf14-art-cjhfgxdxe8c6eree.eastus2-01.azurewebsites.net/) to fetch records from a remote database to display my artworks sorted by date, or a specific record from a specific page as noted by the artwork date. The project also features basic authentication, where only an authorized user (me) would be able to access to create new records, i.e. every time I post a newly drawn artwork online.

## Purpose
I created these repositories to leverage the hosting of my drawings and artworks from different social media sites, where I regularly post these images but these are mixed with other works. As such, I wanted to have a space where I could only show the artworks.

## Basic Data Flow
![enter image description here](https://github.com/gfcf14/gfcf14-art/blob/main/wwwroot/assets/gfcf14art-simple-flow.jpg?raw=true)
This application consists of a linear, layered architecture with a simple frontend application developed in Blazor, which communicates with a backend server in ASP.NET, which in turn communicates with a remote PostgreSQL database hosted in Supabase.

## Complete Data Flow
![enter image description here](https://github.com/gfcf14/gfcf14-art/blob/main/wwwroot/assets/gfcf14art-complete-flow.jpg?raw=true)
The complete data flow in the backend involves a basic configuration which blocks all incoming communications except those that are allowed as per CORS configuration. Successful communication beyond this is handled by two controllers. The artwork controller handles communications with the artwork service to either get artworks (all or a single one) or create an artwork. This service in turn communicates with the db context which accesses the database via EFCore and handles basic db operations (like Add), as well as more complex ones through LINQ, e.g. if the user attempts to enter a date for a post which doesn't exist, instead of simply routing them to a 404 page they are given the closest post date available, or the first post date (except when the user enters a date for a post in the future, which implicitly would not exist).

Please note this is the explanation of data flow for the backend part. If you wish to read on the frontend part, please go [here](https://github.com/gfcf14/gfcf14-art).

## Considerations

Since this is a personal project, I decided to develop and host my apps for free. As such, the frontend application is hosted in Azure's free tier, the backend is hosted via Azure's free tier, and the database is hosted using Supabase's free tier. However, though each of these deployments is successful, sometimes I have experienced delays in operations due to cold start times. Considering how this diminishes user experience, I have decided on an alternative approach that is virtually cold-start free, but instead utilizes an extendable [shared backend architecture](https://github.com/gfcf14/gfcf14-sba) hosted in Vercel to leverage their minimal cold start time.